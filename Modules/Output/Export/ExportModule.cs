using System;
using System.Collections.Generic;
using System.Linq;
using Vixen.Module.Controller;
using Vixen.Sys;
using Vixen.Execution;
using Vixen.Execution.Context;
using Vixen.Commands;
using Vixen.Module.Timing;
using Vixen.Module.Media;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;


namespace VixenModules.Output.Export
{
    public class ExportModule : ControllerModuleInstanceBase, IExportController
    {
        private bool _exportStarted = false;
        private ExportData _exporterData;
        private ExportCommandHandler _exporterCommandHandler;
        private IExportWriter _output;
        private string _outFileName;
        private List<byte> _eventData;
        private double _nextUpdateMS;
        private ITiming _timer;
        private TimeSpan ts1MS;
        private static EventWaitHandle runWH;
        private static EventWaitHandle saveWH;
        private int _updateInterval;
        private Dictionary<string, IExportWriter> _writers;
        private Dictionary<string,string> _exportFileTypes;
        private bool _doStartupDelay;
        private ISequenceContext _sequenceContext = null;
		private int _sequenceLength;
		private int _origUpdateInterval;

        private static Byte globalCount = 0;
        private Byte myCount = 0;

        private static NLog.Logger Logging = NLog.LogManager.GetCurrentClassLogger();


        public ExportModule()
        {
            _updateInterval = -1;
            _exporterCommandHandler = new ExportCommandHandler();
            _eventData = new List<byte>();
            DataPolicyFactory = new ExportDataPolicyFactory();
            _writers = new Dictionary<string, IExportWriter>();
            _exportFileTypes = new Dictionary<string, string>();
            _doStartupDelay = true;
			_sequenceLength = 0;

            ts1MS = new TimeSpan(0, 0, 0, 0, 1);

            runWH = new EventWaitHandle(false, EventResetMode.ManualReset);
            runWH.Set();

            saveWH = new EventWaitHandle(false, EventResetMode.AutoReset);
            saveWH.Set();

            VixenSystem.Contexts.ContextCreated += Contexts_ContextCreated;
            VixenSystem.Contexts.ContextReleased += Contexts_ContextReleased;

            var type = typeof(IExportWriter);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.Equals(type));

            IExportWriter exportWriter;
            foreach (Type theType in types.ToArray())
            {
                exportWriter = (IExportWriter)Activator.CreateInstance(theType);
                _writers[exportWriter.FileType] = exportWriter;
                _exportFileTypes[exportWriter.FileTypeDescr] = exportWriter.FileType;
            }

            _output = _writers.FirstOrDefault().Value;

			_origUpdateInterval = Vixen.Sys.VixenSystem.DefaultUpdateInterval;

            globalCount++;
            myCount = globalCount;

        }

        public Dictionary<string,string> ExportFileTypes 
        { 
            get
            {
                return _exportFileTypes;
            }
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
			VixenSystem.DefaultUpdateInterval = _origUpdateInterval;
            IContext sequenceContext = e.Context as IContext;
            if (sequenceContext != null)
            {
                sequenceContext.ContextStarted -= sequenceContext_ContextStarted;
                sequenceContext.ContextEnded -= sequenceContext_ContextEnded;

            }

			_sequenceContext = null;
        }

        void sequenceContext_ContextEnded(object sender, EventArgs e)
        {
			VixenSystem.DefaultUpdateInterval = _origUpdateInterval;
			_exportStarted = false;
            _doStartupDelay = true;
            if (_output != null)
            {
                _output.CloseSession();

                _sequenceContext = null;

                saveWH.Reset();
            }    
        }

        void sequenceContext_ContextStarted(object sender, EventArgs e)
        {
            runWH.Reset();
            _sequenceContext = sender as ISequenceContext;

            _timer = _sequenceContext.Sequence.GetTiming();

			if (_sequenceContext.Sequence.SelectedTimingProvider.ProviderType.Equals("Export"))
            {
				_sequenceLength = Convert.ToInt32(_sequenceContext.Sequence.Length.TotalMilliseconds);
				if ((_sequenceLength % UpdateInterval) != 0)
				{
					_sequenceLength = (_sequenceLength - (_sequenceLength % UpdateInterval)) + UpdateInterval;
				}
				try
				{
					_output.SeqPeriodTime = (ushort)UpdateInterval;
					_output.OpenSession(_outFileName, this.OutputCount);

					_nextUpdateMS = 0;

					_exportStarted = true;

				}
				catch (Exception ex)
				{
					_sequenceContext.Stop();
				}

            }

            
            runWH.Set();
        }

        public override void UpdateState(int chainIndex, ICommand[] outputStates)
        {
            runWH.WaitOne();
            if ((_exportStarted) && (_output != null))
            {
                saveWH.Reset();
                double currentMS = _timer.Position.TotalMilliseconds;

                if ((currentMS < _nextUpdateMS ) || (currentMS >= _sequenceLength))
                {
                    return;
                }

				_eventData.Clear();

				for (int i = 0; i < outputStates.Length; i++)
				{
					_exporterCommandHandler.Reset();
					ICommand command = outputStates[i];
					if (command != null)
					{
						command.Dispatch(_exporterCommandHandler);
					}
					_eventData.Add(_exporterCommandHandler.Value);
				}
				_output.WriteNextPeriodData(_eventData);
                
				if (_doStartupDelay == true)
                {
					Vixen.Sys.VixenSystem.DefaultUpdateInterval = 10;
                    _doStartupDelay = false;
                }

                _nextUpdateMS += UpdateInterval;


                _timer.Position = TimeSpan.FromMilliseconds(_nextUpdateMS);
                

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
                _exporterData = (ExportData)value;
            }
        }


        public new int UpdateInterval
        {
            get
            {
                if (_updateInterval == -1)
                {
                    _updateInterval = Vixen.Sys.VixenSystem.DefaultUpdateInterval;
                }
                return _updateInterval;
            }

            set { _updateInterval = value; }
        }

        public string OutputType
        {
            get
            {
                return _output.FileType;
            }

            set
            {
                IExportWriter tempOutput;
                if (_writers.TryGetValue((string)value, out tempOutput))
                {
                    _output = tempOutput;
                }
            }
        }

        public string OutFileName
        {
            get
            {
                return _outFileName;
            }

            set
            {
                _outFileName = (string)value;
                string ext = Path.GetExtension(_outFileName).ToUpper();
                OutputType = ext.TrimStart(new char[] {'.'});
            }
        }

    }

}

