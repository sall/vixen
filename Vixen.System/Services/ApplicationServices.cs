﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using Vixen.IO.Result;
using Vixen.IO;
using Vixen.Module;
using Vixen.Module.Editor;
using Vixen.Module.Effect;
using Vixen.Module.EffectEditor;
using Vixen.Module.Sequence;
using Vixen.Module.Script;
using Vixen.Rule;
using Vixen.Sys;
using Vixen.Sys.Output;
using ISequence = Vixen.Sys.ISequence;

namespace Vixen.Services {
	/// <summary>
	/// Provides controlled access to otherwise inaccessible members and some convenience methods.
	/// </summary>
    public class ApplicationServices {
        static internal IApplication ClientApplication = null;

		static public IModuleDescriptor[] GetModuleDescriptors(string typeOfModule) {
			return Modules.GetDescriptors(typeOfModule);
		}
		
		static public IModuleDescriptor[] GetModuleDescriptors<T>()
			where T : class, IModuleInstance {
			return Modules.GetDescriptors<T>();
		}

		static public IModuleDescriptor GetModuleDescriptor(Guid moduleTypeId) {
			return Modules.GetDescriptorById(moduleTypeId);
		}

		static public T GetModuleDescriptor<T>(Guid moduleTypeId)
			where T : class, IModuleDescriptor {
			return Modules.GetDescriptorById(moduleTypeId) as T;
		}

		/// <summary>
		/// Gets a dictionary of the available modules based on the descriptors of installed modules.
		/// </summary>
		/// <param name="moduleType"></param>
		/// <returns></returns>
		static public Dictionary<Guid, string> GetAvailableModules<T>()
			where T : class, IModuleInstance {
			return Modules.GetDescriptors<T>().ToDictionary(x => x.TypeId, x => x.TypeName);
		}

		static public string[] GetTypesOfModules() {
			return Modules.GetImplementations().Select(x => x.TypeOfModule).ToArray();
		}

		static public void UnloadModule(Guid moduleTypeId) {
			IModuleDescriptor descriptor = Modules.GetDescriptorById(moduleTypeId);
			if(descriptor != null) {
				Modules.UnloadModule(descriptor);
			}
		}

		static public void ReloadModule(Guid moduleTypeId) {
			IModuleDescriptor descriptor = Modules.GetDescriptorById(moduleTypeId);
			string moduleFilePath = descriptor.Assembly.Location;
			Modules.UnloadModule(descriptor);
			Modules.LoadModule(moduleFilePath, new[] { moduleTypeId });
		}

		/// <summary>
		/// Gets an instance of a module.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		static public T Get<T>(Guid id)
			where T : class, IModuleInstance {
			// Must go through the module type manager, instead of using
			// Modules.GetById, so that the type manager can affect the instance.
			// Modules.ModuleManagement can be called when the name of the module
			// type is known, which it is not here.
			IModuleManagement moduleManager = Modules.GetManager<T>();
			if(moduleManager != null) {
				return moduleManager.Get(id) as T;
			}
			return null;
		}

		/// <summary>
		/// Gets an instance of each module of the type.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		static public T[] GetAll<T>()
			where T : class, IModuleInstance {
			// Must go through the module type manager so that it can affect the instance.
			IModuleManagement moduleManager = Modules.GetManager<T>();
			if(moduleManager != null) {
				return moduleManager.GetAll().Cast<T>().ToArray();
			}
			return null;
		}

		static public IEnumerable<IEffectEditorControl> GetEffectEditorControls(Guid effectId) {
			// Need the module-specific manager.
			EffectEditorModuleManagement manager = Modules.GetManager<IEffectEditorModuleInstance, EffectEditorModuleManagement>();
			return manager.GetEffectEditors(effectId);
		}

		//Maybe have an overload that takes a file type filter.
		static public string[] GetSequenceFileNames() {
			return Vixen.Sys.Sequence.GetAllFileNames();
		}

		static public ISequence CreateSequence(string sequenceFileType) {
			SequenceModuleManagement manager = Modules.GetManager<ISequenceModuleInstance, SequenceModuleManagement>();
			return manager.Get(sequenceFileType) as ISequence;
		}

		static public OutputController[] GetControllers() {
			return VixenSystem.Controllers.Cast<OutputController>().ToArray();
		}

		static public string[] GetScriptLanguages() {
			ScriptModuleManagement manager = Modules.GetManager<IScriptModuleInstance, ScriptModuleManagement>();
			return manager.GetLanguages();
		}

		static public void PackageSystemContext(string targetFilePath) {
			SystemContext context = SystemContext.PackageSystemContext(targetFilePath);
			context.Save(targetFilePath);
		}

		static public string UnpackageSystemContext(string contextFilePath) {
			SystemContext context = SystemContext.UnpackageSystemContext(contextFilePath);
			return context.Explode(contextFilePath);
		}

		static public IEditorUserInterface CreateEditor(string sequenceFilePath) {
			// Get the sequence type's serializer and load the sequence.
			// Note: THIS IS CRAP.
			Sequence sequence = null;
			if(File.Exists(sequenceFilePath)) {
				sequence = _LoadSequence(sequenceFilePath);
			} else {
				sequence = _CreateSequence(sequenceFilePath);
			}

			//// Get the sequence.
			//Sequence sequence = null;
			//if(File.Exists(sequenceFilePath)) {
			//    sequence = Sequence.Load(sequenceFilePath);
			//} else {
			//    sequence = Sequence.Create(sequenceFilePath);
			//}

			//*** isn't this done as part of the sequence loading?
			//if(sequence != null) {
			//    // Get any sequence module data.
			//    sequence.ModuleDataSet.AssignModuleTypeData(sequence as ISequenceModuleInstance);
			//}

			// Get the editor.
			IEditorUserInterface editor = null;
			EditorModuleManagement manager = Modules.GetManager<IEditorModuleInstance, EditorModuleManagement>();
			if(manager != null) {
				editor = manager.Get(sequenceFilePath);
			}

			if(editor != null && sequence != null) {
				// Get any editor module data from the sequence.
				sequence.ModuleDataSet.AssignModuleTypeData(editor.OwnerModule);

				// Assign the sequence to the editor.
				editor.Sequence = sequence;
			}

			return editor;
		}

		static public INamingRule[] GetAllNamingRules() {
			return typeof(INamingRule).FindConcreteImplementationsWithin(Assembly.GetExecutingAssembly()).Select(Activator.CreateInstance).Cast<INamingRule>().ToArray();
		}

		static public IPatchingRule[] GetAllPatchingRules() {
			return typeof(IPatchingRule).FindConcreteImplementationsWithin(Assembly.GetExecutingAssembly()).Select(Activator.CreateInstance).Cast<IPatchingRule>().ToArray();
		}

		static public bool AreAllEffectRequiredPropertiesPresent(IEffectModuleInstance effectModule) {
			// This only happens if the effect is created outside of us.
			// In that case, it's not really a module instance and we can't check for required properties.
			// They're on their own as far as the outcome.
			if(effectModule.Descriptor != null) {
				EffectModuleDescriptorBase effectDescriptor = Modules.GetDescriptorById<EffectModuleDescriptorBase>(effectModule.Descriptor.TypeId);
				return effectModule.TargetNodes.All(x => x.Properties.Select(y => y.Descriptor.TypeId).Intersect(effectDescriptor.PropertyDependencies).Count() == effectDescriptor.PropertyDependencies.Length);
			}
			return true;
		}

		private static Sequence _LoadSequence(string sequenceFilePath) {
			// Get the sequence type.
			SequenceModuleManagement sequenceManager = (SequenceModuleManagement)Modules.GetManager<ISequenceModuleInstance>();
			SequenceType sequenceType = sequenceManager.GetSequenceType(sequenceFilePath);

			switch(sequenceType) {
				case SequenceType.Standard:
					FileSerializer<Sequence> sequenceSerializer = SerializerFactory.Instance.CreateStandardSequenceSerializer();
					SerializationResult<Sequence> sequenceResult = sequenceSerializer.Read(sequenceFilePath);
					return sequenceResult.Object;
				case SequenceType.Script:
					FileSerializer<ScriptSequence> scriptSequenceSerializer = SerializerFactory.Instance.CreateScriptSequenceSerializer();
					SerializationResult<ScriptSequence> scriptSequenceResult = scriptSequenceSerializer.Read(sequenceFilePath);
					return scriptSequenceResult.Object;
			}

			return null;
		}

		private static Sequence _CreateSequence(string sequenceFilePath) {
			return Sequence.Create(sequenceFilePath);
		}
	}
}
