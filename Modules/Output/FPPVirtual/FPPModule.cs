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

        private Dictionary<long,ICommand[]> _commandCache;

        private static NLog.Logger Logging = NLog.LogManager.GetCurrentClassLogger();


        public FPPModule()
        {
            _fppCommandHandler = new FPPCommandHandler();
            _eventData = new List<byte>();
            DataPolicyFactory = new FPPDataPolicyFactory();
            _commandCache = new Dictionary<long, ICommand[]>();


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
            _output.CloseSession();
            _sequenceStarted = false;         
        }

        void sequenceContext_ContextStarted(object sender, EventArgs e)
        {

            Vixen.Execution.Context.ISequenceContext sequenceContext = (Vixen.Execution.Context.ISequenceContext)sender;

            _timer = new Stopwatch();
            _output = new FPPSeqOutput();
            _output.SeqPeriodTime = _fppData.EventTiming;
            _output.OpenSession("C:\\Users\\ebrad\\Documents\\Vixen 3\\test.fseq",this.OutputCount);

            _lastUpdateMs = 0;
            _commandCache.Clear();
            _sequenceStarted = true;

        }

        public override bool Setup()
        {
            using (FPPSetup setup = new FPPSetup())
            {
                if (setup.ShowDialog() == DialogResult.OK)
                {
                    _fppData.EventTiming = setup.EventTiming;
                    return true;
                }
            }
            return false;
        }

        public override void UpdateState(int chainIndex, ICommand[] outputStates)
        {
            if (_sequenceStarted)
            {

                _commandCache[_timer.ElapsedMilliseconds] = outputStates;

                if (_timer.ElapsedMilliseconds == 0);
                {
                    _timer.Start();
                }


/*
                else if (_timer.ElapsedMilliseconds < _lastUpdateMs + _fppData.EventTiming)
                {
                    return;
                }
                _lastUpdateMs = _timer.ElapsedMilliseconds;
                _eventData.Clear();                
                

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
                _output.WriteNextPeriodData(_eventData);
*/
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
            _lastUpdateMs = int.MinValue;
        }

    }

}

