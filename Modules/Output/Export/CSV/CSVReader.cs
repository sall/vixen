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

namespace VixenModules.Output.Export.CSV
{
    class CSVReader : IExportReader
    {

        private Int32 _seqNumChannels = 0;
        private Int32 _seqNumPeriods = 0;

        private FileStream _outfs = null;
        private StreamReader _dataOut = null;

        public CSVReader()
        {

        }

        public UInt16 SeqPeriodTime { get; set; }

        private string[] ChannelData(string periodData)
        {
            if (periodData != null)
            {
                return periodData.Split(new char[] { ',' });
            }
            return new string[] { };
            
        }

        private int ChannelCount
        {
            get
            {
                if (_seqNumChannels == -1)
                {
                    _seqNumChannels = 0;
                    _outfs.Seek(0, SeekOrigin.Begin);

                    string line = _dataOut.ReadLine();
                    string[] channels = ChannelData(line);
                    if (channels != null)
                    {
                        _seqNumChannels = channels.Count();
                    }
                }

                return _seqNumChannels;
            }

        }

        private int PeriodCount
        {
            get
            {
                if (_seqNumPeriods == -1)
                {
                    _seqNumPeriods = 0;
                    _outfs.Seek(0, SeekOrigin.Begin);

                    using (StreamReader r = new StreamReader(_outfs))
                    {
                        while (r.ReadLine() != null)
                        {
                            _seqNumPeriods++;
                        }
                    }
                }

                return _seqNumPeriods;
            }
        }

        public void ResetStreamPtr()
        {
            _outfs.Seek(0, SeekOrigin.Begin);
        }

        public void OpenSession(string fileName)
        {
            try
            {
                _outfs = File.OpenRead(fileName);
                _dataOut = new StreamReader(_outfs);

                _seqNumChannels = ChannelCount;
                _seqNumPeriods = PeriodCount;
                _outfs.Seek(0, SeekOrigin.Begin);
            }
            catch (Exception e)
            {
                _dataOut = null;
                throw e;
            }

        }

        public List<Byte> ReadNextPeriodData()
        {
            string lineData = null;
            List<Byte> retVal = null; 

            if (PeriodCount > 0)
            {
                retVal = new List<byte>();
                ResetStreamPtr();
                while ((lineData = _dataOut.ReadLine()) != null)
                {
                    List<string> channelData = ChannelData(lineData).ToList();
                    channelData.ForEach(x => retVal.Add(Convert.ToByte(x)));
                }

            }
            return retVal;
        }

        public void CloseSession()
        {
            if (_dataOut != null)
            {
                try
                {
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
