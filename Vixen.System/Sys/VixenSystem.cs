﻿using System;
using System.Linq;
using System.Reflection;
using System.IO;
using Vixen.Module;
using Vixen.Instrumentation;
using Vixen.Services;
using Vixen.Sys.Managers;
using Vixen.Sys.Output;

namespace Vixen.Sys
{
	public class VixenSystem
	{
		private static NLog.Logger Logging = NLog.LogManager.GetCurrentClassLogger();

		public enum RunState
		{
			Stopped,
			Starting,
			Started,
			Stopping
		};

		private static RunState _state = RunState.Stopped;

		public static void Start(IApplication clientApplication, bool openExecution = true, bool disableDevices = false,
		                         string dataRootDirectory = null)
		{
			if (_state == RunState.Stopped) {
				try {
					_state = RunState.Starting;
					ApplicationServices.ClientApplication = clientApplication;

					// A user data file in the binary branch will give any alternate
					// data branch to use.
					Paths.DataRootPath = dataRootDirectory ?? _GetUserDataPath();

					 
					Logging.Info("Vixen System starting up...");

					Instrumentation = new Instrumentation.Instrumentation();

					ModuleImplementation[] moduleImplementations = Modules.GetImplementations();

					// Build branches for each module type.
					foreach (ModuleImplementation moduleImplementation in moduleImplementations) {
						Helper.EnsureDirectory(Path.Combine(Modules.Directory, moduleImplementation.TypeOfModule));
					}
					// There is going to be a "Common" directory for non-module DLLs.
					// This will give them a place to be other than the module directories.
					// If they're in a module directory, the system will try to load them and
					// it will result in an unnecessary log notification for the user.
					// All other binary directories (module directories) have something driving
					// their presence, but this doesn't.  So it's going to be a blatantly
					// ugly statement for now.
					Helper.EnsureDirectory(Path.Combine(Paths.BinaryRootPath, "Common"));

					// Load all module descriptors.
					Modules.LoadAllModules();

					LoadSystemConfig();

					// Add modules to repositories.
					Modules.PopulateRepositories();

					if (disableDevices) {
						SystemConfig.DisabledDevices = OutputDeviceManagement.Devices;
					}
					if (openExecution) {
						Execution.OpenExecution();
					}

					_state = RunState.Started;
					Logging.Info("Vixen System successfully started.");
				}
				catch (Exception ex) {
					// The client is expected to have subscribed to the logging event
					// so that it knows that an exception occurred during loading.
					Logging.ErrorException("Error during system startup; the system has been stopped.", ex);
					Stop();
				}
			}
		}

		public static void Stop()
		{
			if (_state == RunState.Starting || _state == RunState.Started) {
				_state = RunState.Stopping;
				Logging.Info("Vixen System stopping...");
				ApplicationServices.ClientApplication = null;
				// Need to get the disabled devices before stopping them all.
				SaveDisabledDevices();
				Execution.CloseExecution();
				Modules.ClearRepositories();
				SaveSystemConfig();
				_state = RunState.Stopped;
				Logging.Info("Vixen System successfully stopped.");
			}
		}

		public static void SaveDisabledDevices()
		{
			if (SystemConfig != null) {
				SystemConfig.DisabledDevices = OutputDeviceManagement.Devices.Where(x => x != null).Where(x => !x.IsRunning);
			}
		}

		public static void SaveSystemConfig()
		{
			if (SystemConfig != null) {
				// 'copy' the current details (nodes/elements/controllers) from the executing state
				// to the SystemConfig, so they're there for writing when we save

				// we may not want to always save the disabled devices to the config (ie. if the system is stopped at the
				// moment) since the disabled devices are inferred from the running status of active devices
				if (_state == RunState.Started) {
					SaveDisabledDevices();
				}

				SystemConfig.OutputControllers = OutputControllers;
				SystemConfig.SmartOutputControllers = SmartOutputControllers;
				SystemConfig.Previews = Previews;
				SystemConfig.Elements = Elements;
				SystemConfig.Nodes = Nodes.GetRootNodes();
				SystemConfig.ControllerLinking = ControllerLinking;
				SystemConfig.Filters = Filters;
				SystemConfig.DataFlow = DataFlow;

				SystemConfig.Save();
			}

			if (ModuleStore != null) {
				ModuleStore.Save();
			}
		}

		public static void LoadSystemConfig()
		{
			Execution.initInstrumentation();
			DataFlow = new DataFlowManager();
			Elements = new ElementManager();
			Nodes = new NodeManager();
			OutputControllers = new OutputControllerManager(
				new ControllerLinkingManagement<OutputController>(),
				new OutputDeviceCollection<OutputController>(),
				new OutputDeviceExecution<OutputController>());
			SmartOutputControllers = new SmartOutputControllerManager(
				new ControllerLinkingManagement<SmartOutputController>(),
				new OutputDeviceCollection<SmartOutputController>(),
				new OutputDeviceExecution<SmartOutputController>());
			Previews = new PreviewManager(
				new OutputDeviceCollection<OutputPreview>(),
				new OutputDeviceExecution<OutputPreview>());
			Contexts = new ContextManager();
			Filters = new FilterManager(DataFlow);
			ControllerLinking = new ControllerLinker();
			ControllerManagement = new ControllerFacade();
			ControllerManagement.AddParticipant(OutputControllers);
			ControllerManagement.AddParticipant(SmartOutputControllers);
			OutputDeviceManagement = new OutputDeviceFacade();
			OutputDeviceManagement.AddParticipant(OutputControllers);
			OutputDeviceManagement.AddParticipant(SmartOutputControllers);
			OutputDeviceManagement.AddParticipant(Previews);

			// Load system data in order of dependency.
			// The system data generally resides in the data branch, but it
			// may not be in the case of an alternate context.
			string systemDataPath = _GetSystemDataPath();
			// Load module data before system config.
			// System config creates objects that use modules that have data in the store.
			ModuleStore = _LoadModuleStore(systemDataPath) ?? new ModuleStore();
			SystemConfig = _LoadSystemConfig(systemDataPath) ?? new SystemConfig();

			Elements.AddElements(SystemConfig.Elements);
			Nodes.AddNodes(SystemConfig.Nodes);
			OutputControllers.AddRange(SystemConfig.OutputControllers.Cast<OutputController>());
			SmartOutputControllers.AddRange(SystemConfig.SmartOutputControllers.Cast<SmartOutputController>());
			Previews.AddRange(SystemConfig.Previews.Cast<OutputPreview>());
			ControllerLinking.AddRange(SystemConfig.ControllerLinking);
			Filters.AddRange(SystemConfig.Filters);

			DataFlow.Initialize(SystemConfig.DataFlow);
		}

		public static void ReloadSystemConfig()
		{
			bool wasRunning = Execution.IsOpen;
			Execution.CloseExecution();

			// purge all existing elements, nodes, and controllers (to try and clean up a bit).
			// might not actually matter, since we're going to make new Managers for them all
			// in a tick, but better safe than sorry.
			foreach (ElementNode cn in Nodes.ToArray())
				Nodes.RemoveNode(cn, null, true);
			foreach (OutputController oc in OutputControllers.ToArray())
				OutputControllers.Remove(oc);
			foreach (SmartOutputController smartOutputController in SmartOutputControllers.ToArray()) {
				SmartOutputControllers.Remove(smartOutputController);
			}
			foreach (OutputPreview outputPreview in Previews.ToArray())
				Previews.Remove(outputPreview);

			LoadSystemConfig();

			if (wasRunning)
				Execution.OpenExecution();
		}


		public static RunState State
		{
			get { return _state; }
		}

		public static string AssemblyFileName
		{
			get { return Assembly.GetExecutingAssembly().GetFilePath(); }
		}

	

		public static bool AllowFilterEvaluation
		{
			get { return SystemConfig.AllowFilterEvaluation; }
			set { SystemConfig.AllowFilterEvaluation = value; }
		}

		public static int DefaultUpdateInterval
		{
			get { 
				// turns out we might need the default before we have SystemConfig set up...
				return SystemConfig == null
					? SystemConfig.DEFAULT_UPDATE_INTERVAL
					: SystemConfig.DefaultUpdateInterval; 
			}
			set { SystemConfig.DefaultUpdateInterval = value; }
		}

		public static ElementManager Elements { get; private set; }
		public static NodeManager Nodes { get; private set; }
		public static OutputControllerManager OutputControllers { get; private set; }
		public static SmartOutputControllerManager SmartOutputControllers { get; private set; }
		public static PreviewManager Previews { get; private set; }
		public static ContextManager Contexts { get; private set; }
		public static FilterManager Filters { get; private set; }
		public static IInstrumentation Instrumentation { get; private set; }
		public static ControllerLinker ControllerLinking { get; private set; }
		public static DataFlowManager DataFlow { get; private set; }
		public static ControllerFacade ControllerManagement { get; private set; }
		public static OutputDeviceFacade OutputDeviceManagement { get; private set; }
		public static bool VersionBeyondWindowsXP {
			get {
				System.OperatingSystem osInfo = System.Environment.OSVersion;
				if (osInfo.Platform == PlatformID.Win32NT) {
					//If OsVersion is > XP then allow D2D
					if (osInfo.Version.Major > 5) return true;
				}

				//Otherwise force GDI Preview rendering
				return false;
			}
		}
		public static Guid Identity
		{
			get { return SystemConfig.Identity; }
		}

		internal static ModuleStore ModuleStore { get; private set; }
		internal static SystemConfig SystemConfig { get; private set; }

		private static ModuleStore _LoadModuleStore(string systemDataPath)
		{
			string moduleStoreFilePath = Path.Combine(systemDataPath, ModuleStore.FileName);
			return FileService.Instance.LoadModuleStoreFile(moduleStoreFilePath);
		}

		private static SystemConfig _LoadSystemConfig(string systemDataPath)
		{
			string systemConfigFilePath = Path.Combine(systemDataPath, SystemConfig.FileName);
			return FileService.Instance.LoadSystemConfigFile(systemConfigFilePath);
		}

	
		private static string _GetSystemDataPath()
		{
			// Look for a user data file in the binary directory.
			string filePath = Path.Combine(Paths.BinaryRootPath, SystemConfig.FileName);
			if (_OperatingWithinContext(filePath)) {
				// We're going to use the context's user data file and not the
				// one in the data branch.
				return Path.GetDirectoryName(filePath);
			}

			// Use the default path in the data branch.
			return SystemConfig.Directory;
		}

		private static bool _OperatingWithinContext(string systemConfigFilePath)
		{
			SystemConfig systemConfig = FileService.Instance.LoadSystemConfigFile(systemConfigFilePath);
			return systemConfig != null && systemConfig.IsContext;
		}

		private static string _GetUserDataPath()
		{
			string dataPath = System.Configuration.ConfigurationManager.AppSettings["DataPath"];
			return dataPath ?? Paths.DefaultDataRootPath;
		}
	}
}