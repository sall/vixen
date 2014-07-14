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
        private UInt32 _stepSize = 0;
        private UInt16 _stepTime = 50;       //Default to 50ms Timing
        private UInt16 _numUniverses = 0;    //Ignored by Pi Player
        private UInt16 _universeSize = 0;    //Ignored by Pi Player
        private Byte _gamma = 1;             //0=encoded, 1=linear, 2=RGB
        private Byte _colorEncoding = 2;
        private String _fileName = null;

        //step size is number of channels in output
        //num steps is number of 25,50,100ms intervals

        public FPPSeqOutput()
        {

        }

        public FPPSeqOutput(FPPData data)
        {
            if (data != null)
            {
                _stepTime = data.StepTiming;
                _stepSize = data.StepSize;
                _fileName = data.FileName;
            }

        }

        public byte[] SeqData { get; set; }

        public void WriteFalconPiFile()
        {

/*
            //Add exception handling

            Byte outByte;

            if (_fileName != null)
            {
                FileStream fs = File.Create("C:\\binary.dat", 2048, FileOptions.None);
                BinaryWriter dataOut = new BinaryWriter(fs);

                // Header Information
                // Format Identifier
                dataOut.Write("FSEQ");

                // Data offset
                dataOut.Write((Byte)(_dataOffset % 256));
                dataOut.Write((Byte)(_dataOffset / 256));

                // Data header
                dataOut.Write(_vMinor);
                dataOut.Write(_vMajor);

                // Fixed header length
                dataOut.Write((Byte)(_fixedHeaderLength % 256));
                dataOut.Write((Byte)(_fixedHeaderLength / 256));

                // Step Size
                dataOut.Write((Byte)(_stepSize & 0xFF));
                dataOut.Write((Byte)((_stepSize >> 8) & 0xFF));
                dataOut.Write((Byte)((_stepSize >> 16) & 0xFF));
                dataOut.Write((Byte)((_stepSize >> 24) & 0xFF));

                // Number of Steps
                dataOut.Write((Byte)(SeqNumPeriods & 0xFF));
                dataOut.Write((Byte)((SeqNumPeriods >> 8) & 0xFF));
                dataOut.Write((Byte)((SeqNumPeriods >> 16) & 0xFF));
                dataOut.Write((Byte)((SeqNumPeriods >> 24) & 0xFF));

                // Step time in ms
                dataOut.Write((Byte)(_stepTime & 0xFF));
                dataOut.Write((Byte)((_stepTime >> 8) & 0xFF));

                // universe count
                dataOut.Write((Byte)(_numUniverses & 0xFF));
                dataOut.Write((Byte)((_numUniverses >> 8) & 0xFF));

                // universe Size
                dataOut.Write((Byte)(_universeSize & 0xFF));
                dataOut.Write((Byte)((_universeSize >> 8) & 0xFF));

                // universe Size
                dataOut.Write(_gamma);

                // universe Size
                dataOut.Write(_colorEncoding);
                dataOut.Write(0);
                dataOut.Write(0);

                for (long period = 0; period < SeqNumPeriods; period++)
                {
                    for (int ch = 0; ch < _stepSize; ch++)
                    {
                        outByte = ch < SeqNumChannels ? SeqData[(ch * SeqNumPeriods) + period] : 0;
                        dataOut.Write(outByte);
                    }
                }

                dataOut.Close();
                fs.Close();
            }
 */
        }
    }
}
