using System;
using System.Collections.Generic;
using System.Linq;
using Vixen.Module.Controller;
using Vixen.Sys;
using Vixen.Execution;
using Vixen.Commands;
using Vixen.Module.Timing;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;


namespace VixenModules.Output.Exporter
{
    public class ExporterModule : ControllerModuleInstanceBase
    {
        private bool _sequenceStarted = false;
        private ExporterData _exporterData;
        private ExporterCommandHandler _exporterCommandHandler;
        private FPPExporter _output;
        private List<byte> _eventData;
        private double _nextUpdateMS;
        private ITiming _timer;
        private TimeSpan ts1MS;
        private Dictionary<long,ICommand[]> _commandCache;
        private static EventWaitHandle runWH;
        private static EventWaitHandle saveWH;

        private static NLog.Logger Logging = NLog.LogManager.GetCurrentClassLogger();


        public ExporterModule()
        {
            _exporterCommandHandler = new ExporterCommandHandler();
            _eventData = new List<byte>();
            DataPolicyFactory = new ExporterDataPolicyFactory();
            _commandCache = new Dictionary<long, ICommand[]>();

            ts1MS = new TimeSpan(0, 0, 0, 0, 1);

            runWH = new EventWaitHandle(false, EventResetMode.ManualReset);
            runWH.Set();

            saveWH = new EventWaitHandle(false, EventResetMode.AutoReset);
            saveWH.Set();

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
            if (_output != null)
            {
                _sequenceStarted = false;
                saveWH.WaitOne();
                foreach (ICommand[] cmds in _commandCache.Values)
                {
                    _eventData.Clear();

                    for (int i = 1; i < cmds.Length; i++)
                    {
                        _exporterCommandHandler.Reset();
                        ICommand command = cmds[i];
                        if (command != null)
                        {
                            command.Dispatch(_exporterCommandHandler);
                        }
                        _eventData.Add(_exporterCommandHandler.Value);
                    }
                    _output.WriteNextPeriodData(_eventData);

                }

                _output.CloseSession();

                saveWH.Reset();
            }    
        }

        void sequenceContext_ContextStarted(object sender, EventArgs e)
        {
            runWH.Reset();
            Vixen.Execution.Context.ISequenceContext sequenceContext = (Vixen.Execution.Context.ISequenceContext)sender;
            _timer =  sequenceContext.Sequence.GetTiming();

            if (sequenceContext.Sequence.SelectedTimingProvider.ProviderType.Equals("Export"))
            {
                _output = new FPPExporter();
                _output.SeqPeriodTime = _exporterData.EventTiming;
                _output.OpenSession(System.IO.Path.GetTempPath() + "testSeq.fseq", this.OutputCount);

                _nextUpdateMS = 0;
                _commandCache.Clear();
                _sequenceStarted = true;
                runWH.Set();
            }
        }

        public override void UpdateState(int chainIndex, ICommand[] outputStates)
        {
            runWH.WaitOne();
            if (_sequenceStarted)
            {
                saveWH.Reset();
                double currentMS = _timer.Position.TotalMilliseconds;

                if (currentMS < _nextUpdateMS )
                {
                    return;
                }
                _commandCache[Convert.ToInt32(currentMS)] = (ICommand[])outputStates.Clone();
                _nextUpdateMS += _exporterData.EventTiming;
                _timer.Position = TimeSpan.FromMilliseconds(_nextUpdateMS);
                //_timer.Position = TimeSpan.FromMilliseconds(currentMS + 25);
            }
            saveWH.Set();

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
                return _exporterData;
            }
            set
            {
                _exporterData = (ExporterData)value;
                initModule();
            }
        }

        private void initModule()
        {

        }

    }

}

