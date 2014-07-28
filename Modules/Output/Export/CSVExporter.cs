using System;
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

namespace VixenModules.Output.Export
{
    public class CSVExporter : IExportWriter
    {

        private Int32 _seqNumChannels = 0; 
        private StreamWriter _dataOut = null;

        public CSVExporter()
        {
            SeqPeriodTime = 50;  //Default to 50ms
        }


        public UInt16 SeqPeriodTime { get; set; }

        public void WriteFileHeader()
        {

        }

        public void WriteFileFooter()
        {

        }

        public void OpenSession(string fileName, Int32 numChannels)
        {
            try
            {
                _dataOut = new StreamWriter(fileName);
                _seqNumChannels = numChannels;
            }
            catch (Exception e)
            {
                _dataOut = null;
                throw e;
            }
        }

        public void WriteNextPeriodData(List<Byte> periodData)
        {
            if (_dataOut != null)
            {
                try
                {
                    _dataOut.Write(periodData[0].ToString()); 
                    for (int j = 0; j < _seqNumChannels; j++)
                    {
                        _dataOut.Write(',');
                        _dataOut.Write(periodData[j].ToString());
                    }
                    _dataOut.Write(System.Environment.NewLine);
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

                }
                catch (Exception e)
                {
                    _dataOut = null;
                    throw e;
                }
            }
        }

        public string FileType
        {
            get
            {
                return "CSV";
            }
        }

        public string FileTypeDescr
        {
            get
            {
                return "CSV File (CSV)";
            }
        }
    }
}
