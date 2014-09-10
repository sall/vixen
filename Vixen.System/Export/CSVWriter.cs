﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Vixen.Commands;
using Vixen.Execution;
using Vixen.Execution.Context;
using Vixen.Module.Controller;
using Vixen.Module.Timing;
using Vixen.Sys;

namespace Vixen.Export
{
    public class CSVWriter : IExportWriter
    {
        private Int32 _seqNumChannels = 0;
        private Int32 _seqNumPeriods = 0;

        private FileStream _outfs = null;
        private BinaryWriter _dataOut = null;

        public CSVWriter()
        {
            SeqPeriodTime = 50;  //Default to 50ms
        }


        public int SeqPeriodTime { get; set; }

        public void WriteFileHeader()
        {

        }

        public void WriteFileFooter()
        {

        }

        public void OpenSession(SequenceSessionData data)
        {
            OpenSession(data.OutFileName, data.NumPeriods, data.ChannelNames.Count());
        }

        private void OpenSession(string fileName, Int32 numPeriods, Int32 numChannels)
        {
            _seqNumChannels = numChannels;
            _seqNumPeriods = numPeriods;

            try
            {
                _outfs = File.Create(fileName, numChannels * 2, FileOptions.None);
                _dataOut = new BinaryWriter(_outfs);
            }
            catch (Exception e)
            {
                _dataOut = null;
                throw e;
            }
        }

        public void ResetStreamPtr()
        {
            _dataOut.Seek(0, SeekOrigin.Begin);
        }

        public void WriteNextPeriodData(List<Byte> periodData)
        {
            if (_dataOut != null)
            {
                try
                {
                    _dataOut.Write(periodData[0].ToString("000").ToCharArray()); 
                    for (int j = 1; j < _seqNumChannels; j++)
                    {
                        _dataOut.Write(',');
                        _dataOut.Write(periodData[j].ToString("000").ToCharArray());
                    }
                    _dataOut.Write(System.Environment.NewLine.ToCharArray());
                }
                catch (Exception e)
                {
                    _dataOut = null;
                    throw e;
                }
            }
        }


        public void CloseSession()
        {
            if (_dataOut != null)
            {
                try
                {
                    _dataOut.Flush();
                    _dataOut.Close();
                    _dataOut = null;
                    _outfs.Close();
                    _outfs.Close();
                    _outfs = null;

                }
                catch (Exception e)
                {
                    _dataOut = null;
                    _outfs = null;
                    throw e;
                }
            }
        }

        public string FileType
        {
            get
            {
                return "csv";
            }
        }

        public string FileTypeDescr
        {
            get
            {
                return "CSV File";
            }
        }
    }
}
