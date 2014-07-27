using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using Vixen.Module;
using Vixen.Module.Controller;
using Vixen.Module.Timing;
using Vixen.Module.App;
using Vixen.Services;
using Vixen.Execution;
using Vixen.Execution.Context;
using Vixen.Factory;
using Vixen.Sys;
using Vixen.Sys.Output;
using NLog;

namespace VixenApplication
{
    public class Export
    {
        private ISequenceContext _context = null;
        private OutputController _outputController = null;
        Guid _controllerTypeId = new Guid("{F79764D7-5153-41C6-913C-2321BC2E1819}");
		List<OutputController> _nonExportControllers = null;
		private const string EXPORT_CONTROLLER_NAME = "ExportGateway";
		private IExportController _controllerModuleInstance = null;


        private static NLog.Logger Logging = NLog.LogManager.GetCurrentClassLogger();

        public string[] FormatTypes
        {
            get
            {
                string[] retVal = new string[0];

                OutputController outputController = this.ExportController;

                if (outputController != null)
                {
                    IExportController _controllerModule = (IExportController)outputController.ControllerModuleInstance;
                    retVal = _controllerModule.ExportFileTypes.Keys.ToArray();
                }
                return retVal;
            }
        }

		public string OutFileName
		{
			get
			{
				return _controllerModuleInstance.OutFileName;
			}

			set
			{
				_controllerModuleInstance.OutFileName = value;
			}
		}

		public int UpdateInterval 
		{
			get
			{
				return _controllerModuleInstance.UpdateInterval;
			}

			set
			{
				_controllerModuleInstance.UpdateInterval = value;
			}
		}

		public Dictionary<string, string> ExportFileTypes
		{
			get
			{
				return _controllerModuleInstance.ExportFileTypes;
			}
		}

        private OutputController FindExportControler()
        {
            return
                VixenSystem.OutputControllers.ToList().Find(x => x.ModuleId.Equals(_controllerTypeId));
        }

        private List<OutputController> NonExportControllers
        {
			get
			{
				if (_nonExportControllers == null)
				{
					_nonExportControllers = VixenSystem.OutputControllers.ToList().FindAll(x => x.ModuleId != _controllerTypeId);
				}
				return _nonExportControllers;
			}
        }

        private void AutoConfigExportController()
        {
            _outputController = FindExportControler();
            
            if (_outputController == null) 
            {
                ControllerFactory controllerFactory = new ControllerFactory();
                _outputController = (OutputController)controllerFactory.CreateDevice(_controllerTypeId, EXPORT_CONTROLLER_NAME);
                VixenSystem.OutputControllers.Add(_outputController);
            }
            PopulateControllerCommands();
        }

        private void PopulateControllerCommands()
        {
            int totalOutputCount = 0;
            int currentOutput = 0;
            CommandOutput workCommand;
			
            if (ExportController != null)
            {
				List<OutputController> ocList = NonExportControllers;
                ocList.ForEach(x => totalOutputCount  += x.OutputCount);

				ExportController.OutputCount = totalOutputCount;
				foreach (OutputController controller in ocList)
				{
					foreach (CommandOutput output in controller.Outputs)
					{
						workCommand = ExportController.Outputs[currentOutput++];
						workCommand.Source = output.Source;
					}
				}

            }

        }     
 
        public OutputController ExportController
        {
            get
            {
                if (_outputController == null)
                {
                    AutoConfigExportController();
					_controllerModuleInstance = (IExportController)_outputController.ControllerModuleInstance;
                }

                return _outputController;
            }
        }

        public void CancelExport()
        {
            if (_context != null)
            {
                _context.Stop();
            }
        }
	    
	    public void StopRunningControllers()
		{
			VixenSystem.OutputDeviceManagement.StopAll();
			VixenSystem.OutputDeviceManagement.StartOnly(ExportController);
		}

	    public void RestartStoppedControllers()
	    {
			VixenSystem.OutputDeviceManagement.StartAll();
			ExportController.Stop();
	    }

		private void WriteControllerInfo(ISequence sequence)
		{
			int chanStart = 1;

			string xmlOutName =
				Path.GetDirectoryName(OutFileName) +
				Path.DirectorySeparatorChar +
				Path.GetFileNameWithoutExtension(OutFileName) +
				"_Network.xml";
			
			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;
			settings.IndentChars = "\t";

			using (XmlWriter writer = XmlWriter.Create(xmlOutName,settings))
			{
				writer.WriteStartDocument();
				writer.WriteStartElement("Vixen3_Export");
				writer.WriteElementString("Resolution", UpdateInterval.ToString());
				writer.WriteElementString("OutFile", Path.GetFileName(OutFileName));
				writer.WriteElementString("Duration", sequence.Length.ToString());

				writer.WriteStartElement("Network");
				foreach (ControllerExportInfo exportInfo in ControllerExportData)
				{
					writer.WriteStartElement("Controller");
					writer.WriteElementString("Index", exportInfo.Index.ToString()); 
					writer.WriteElementString("Name", exportInfo.Name);
					writer.WriteElementString("StartChan", chanStart.ToString());
					writer.WriteElementString("Channels", exportInfo.Channels.ToString());
					writer.WriteEndElement();

					chanStart += exportInfo.Channels;
				}
				writer.WriteEndElement();

				writer.WriteEndElement();
				writer.WriteEndDocument();
			}

		}

        public void DoExport(ISequence sequence)
        {

            if (sequence != null)
            {
				PopulateControllerCommands();

                string[] timingSources;
                TimingProviders timingProviders = new TimingProviders(sequence);
                timingSources = timingProviders.GetAvailableTimingSources("Export");

                if (timingSources.Length > 0)
                {
                    SelectedTimingProvider exportTimingProvider = new SelectedTimingProvider("Export", timingSources.First());
                    sequence.SelectedTimingProvider = exportTimingProvider;
                }

                _context = VixenSystem.Contexts.CreateSequenceContext(new ContextFeatures(ContextCaching.NoCaching), sequence);
                if (_context == null)
                {
                   // Logging.Error(@"Null context when attempting to play sequence.");
                    MessageBox.Show(@"Unable to play this sequence.  See error log for details.");
                    return;
                }

				WriteControllerInfo(_context.Sequence);

				_context.Sequence.ClearMedia();
				
                _context.Play(TimeSpan.Zero, TimeSpan.MaxValue);

                _context.ContextEnded += context_ContextEnded;
            }
        }

        void context_ContextEnded(object sender, EventArgs e)
        {
			if (_context != null)
			{
				_context.ContextEnded -= context_ContextEnded;
				VixenSystem.Contexts.ReleaseContext(_context);
				_context = null;
			}
        }

        public double SequenceLenghth
        {
            get
            {
                double retVal = 0;
                if (_context != null)
                {
                    retVal = _context.Sequence.Length.TotalMilliseconds;
                }
                return retVal;
            }
        }

        public ITiming SequenceTiming
        {
            get
            {
                ITiming retVal = null;
                if (_context != null)
                {
                    retVal = _context.Sequence.GetTiming();
                }
                return retVal;
            }
        }

        public void SetContextEndHandler(System.EventHandler eventHandler)
        {
            if (_context != null)
            {
                _context.ContextEnded += eventHandler;
            }
        }

        public void ClearContextEndHandler(System.EventHandler eventHandler)
        {
            if (_context != null)
            {
                _context.ContextEnded -= eventHandler;
            }
        }

        public List<ControllerExportInfo> ControllerExportData
        {
            get
            {
                int index = 0;
                List<ControllerExportInfo> retVal = new List<ControllerExportInfo>();
                NonExportControllers.ForEach(x => retVal.Add(new ControllerExportInfo(x, index++)));
                return retVal;
            }
        }
    }

    public class ControllerExportInfo
    {
        public ControllerExportInfo(OutputController controller, int index)
        {
            Name = controller.Name;
            Index = index;
            Channels = controller.OutputCount;
        }

        public int Index { get; set; }
        public int Channels { get; set; }
        public string Name { get; set; }
    }
}