using System;
using System.Collections.Generic;
using System.Linq;
using Vixen.Module.Controller;
using Vixen.Sys;
using Vixen.Execution;
using Vixen.Commands;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;


namespace VixenModules.Output.FPPVirtual
{
    public class FPPModule : ControllerModuleInstanceBase
    {
        private bool _sequenceStarted = false;
        private FPPData _fppData;
        private FPPCommandHandler _fppCommandHandler;
        private FPPSeqOutput _output;
        private List<byte> _eventData;
        private Stopwatch _timer;
        private long _lastUpdateMs;

        private static NLog.Logger Logging = NLog.LogManager.GetCurrentClassLogger();


        public FPPModule()
        {
            _fppCommandHandler = new FPPCommandHandler();
            DataPolicyFactory = new FPPDataPolicyFactory();


            VixenSystem.Contexts.ContextCreated += Contexts_ContextCreated;
            VixenSystem.Contexts.ContextReleased += Contexts_ContextReleased;

        }

        void Contexts_ContextCreated(object sender, ContextEventArgs e)
        {
            IContext sequenceContext = e.Context as IContext;

            if (sequenceContext != null)
            {
                sequenceContext.ContextStarted += sequenceContext_ContextStarted;
                sequenceContext.ContextEnded += sequenceContext_ContextEnded;

            }
        }


        void Contexts_ContextReleased(object sender, ContextEventArgs e)
        {

            IContext sequenceContext = e.Context as IContext;
            if (sequenceContext != null)
            {
                sequenceContext.ContextStarted -= sequenceContext_ContextStarted;
                sequenceContext.ContextEnded -= sequenceContext_ContextEnded;

            }

        }

        void sequenceContext_ContextEnded(object sender, EventArgs e)
        {
            _sequenceStarted = false;
            _output.SeqData = _eventData.ToArray<byte>();
            
        }

        void sequenceContext_ContextStarted(object sender, EventArgs e)
        {

            Vixen.Execution.Context.ISequenceContext sequenceContext = (Vixen.Execution.Context.ISequenceContext)sender;

            _timer = new Stopwatch();
            _eventData = new List<byte>();
/*            _output = new VixenXmlOutput() { Audio = new Audio(), Channels = new List<string>() };

            _output.Audio.filename = sequenceContext.Sequence.SequenceData.SelectedTimingProvider.SourceName;
            string audioname = _output.Audio.filename.Substring(_output.Audio.filename.LastIndexOf("\\") + 1);

            _output.Audio.Value = audioname.Substring(0, audioname.LastIndexOf("."));

            _output.Time = sequenceContext.Sequence.Length.TotalMilliseconds.ToString();

            _output.EventPeriodInMilliseconds = _helixData.EventPeriod.ToString();
*/
            _timer.Start();
            _sequenceStarted = true;

        }

        public override bool Setup()
        {
            using (FPPSetup setup = new FPPSetup())
            {
                if (setup.ShowDialog() == DialogResult.OK)
                {
                    _fppData.StepTiming = setup.EventTiming;
                    return true;
                }
            }
            return false;
        }

        public override void UpdateState(int chainIndex, ICommand[] outputStates)
        {
            if (_sequenceStarted)
            {
                if (_timer.ElapsedMilliseconds < _lastUpdateMs + MsPerUpdate)
                {
                    return;
                }

                _lastUpdateMs = _timer.ElapsedMilliseconds;

                for (int i = 0; i < outputStates.Length; i++)
                {
                    _fppCommandHandler.Reset();
                    ICommand command = outputStates[i];
                    if (command != null)
                    {
                        command.Dispatch(_fppCommandHandler);
                    }
                    _eventData.Add(_fppCommandHandler.Value);
                }

            }
        }

        public override void Start()
        {
            base.Start();
        }
        public override void Stop()
        {
            base.Stop();
        }

        public override Vixen.Module.IModuleDataModel ModuleData
        {
            get
            {
                return _fppData;
            }
            set
            {
                _fppData = (FPPData)value;
                initModule();
            }
        }

        private void initModule()
        {
            MsPerUpdate = _fppData.StepTiming;
            _lastUpdateMs = int.MinValue;
        }

        public int MsPerUpdate { get; set; }

    }

}

