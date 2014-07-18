using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Vixen.Module.Controller;
using Vixen.Sys;
using Vixen.Execution;
using Vixen.Commands;
using System.Text;


namespace VixenModules.Output.FPPVirtual
{
    public class FPPSeqOutput
    {
        private const Byte _vMinor = 0;
        private const Byte _vMajor = 0;
        private const UInt32 _dataOffset = 28;
        private const UInt16 _fixedHeaderLength = 28;
        private Int32 _seqNumChannels = 0;
        private UInt32 _seqNumPeriods = 0;
        private UInt16 _numUniverses = 0;    //Ignored by Pi Player
        private UInt16 _universeSize = 0;    //Ignored by Pi Player
        private Byte _gamma = 1;             //0=encoded, 1=linear, 2=RGB
        private Byte _colorEncoding = 2;

        private FileStream _outfs = null;
        private BinaryWriter _dataOut = null;

        //step size is number of channels in output
        //num steps is number of 25,50,100ms intervals

        public FPPSeqOutput()
        {
            SeqPeriodTime = 50;  //Default to 50ms
        }


        public UInt16 SeqPeriodTime { get; set; }

        private void WriteHeader()
        {
            if (_dataOut != null)
            {

                // Header Information
                // Format Identifier
                _dataOut.Write("FSEQ");

                // Data offset
                _dataOut.Write((Byte)(_dataOffset % 256));
                _dataOut.Write((Byte)(_dataOffset / 256));

                // Data header
                _dataOut.Write(_vMinor);
                _dataOut.Write(_vMajor);

                // Fixed header length
                _dataOut.Write((Byte)(_fixedHeaderLength % 256));
                _dataOut.Write((Byte)(_fixedHeaderLength / 256));

                // Step Size
                _dataOut.Write((Byte)(_seqNumChannels & 0xFF));
                _dataOut.Write((Byte)((_seqNumChannels >> 8) & 0xFF));
                _dataOut.Write((Byte)((_seqNumChannels >> 16) & 0xFF));
                _dataOut.Write((Byte)((_seqNumChannels >> 24) & 0xFF));

                // Number of Steps
                _dataOut.Write((Byte)(_seqNumPeriods & 0xFF));
                _dataOut.Write((Byte)((_seqNumPeriods >> 8) & 0xFF));
                _dataOut.Write((Byte)((_seqNumPeriods >> 16) & 0xFF));
                _dataOut.Write((Byte)((_seqNumPeriods >> 24) & 0xFF));

                // Step time in ms
                _dataOut.Write((Byte)(SeqPeriodTime & 0xFF));
                _dataOut.Write((Byte)((SeqPeriodTime >> 8) & 0xFF));

                // universe count
                _dataOut.Write((Byte)(_numUniverses & 0xFF));
                _dataOut.Write((Byte)((_numUniverses >> 8) & 0xFF));

                // universe Size
                _dataOut.Write((Byte)(_universeSize & 0xFF));
                _dataOut.Write((Byte)((_universeSize >> 8) & 0xFF));

                // universe Size
                _dataOut.Write(_gamma);

                // universe Size
                _dataOut.Write(_colorEncoding);
                _dataOut.Write(0);
                _dataOut.Write(0);
            }
        }
        public void OpenSession(string fileName, Int32 numChannels)
        {
            try
            {
                _outfs = File.Create(fileName, numChannels * 16, FileOptions.None);
                _dataOut = new BinaryWriter(_outfs);
                _dataOut.Write(new Byte[_fixedHeaderLength]);
                _seqNumChannels = numChannels;
                _seqNumPeriods = 0;
            }
            catch(Exception e)
            {
                _outfs = null;
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
                    _dataOut.Write(periodData.ToArray());
                    _seqNumPeriods++;

                }
                catch (Exception e)
                {
                    _dataOut = null;
                    _outfs = null;
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
                    _dataOut.Seek(0, SeekOrigin.Begin);
                    WriteHeader();
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
    }
}
