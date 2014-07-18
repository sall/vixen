using System;
using System.Collections.Generic;
using System.Linq;
using Vixen.Module;
using Vixen.Module.Timing;
using Vixen.Sys;

namespace Vixen.Execution
{
    internal class ExportTimingProvider : ITimingProvider
    {
        static Dictionary<string, TimingModuleInstanceBase> timers = null;

        public string TimingProviderTypeName
        {
            get { return "Export"; }
        }

        public string[] GetAvailableTimingSources(ISequence sequence)
        {
            if (timers == null)
            {
                timers = new Dictionary<string, TimingModuleInstanceBase>();
            }

            if (sequence.FilePath != null) 
            {
                if (!timers.ContainsKey(sequence.FilePath))
                {
                    IModuleDescriptor moduleDescriptor =
                        Modules.GetDescriptors<ITimingModuleInstance>().FirstOrDefault(x => x.TypeName == "Export timing");

                    if (moduleDescriptor != null)
                    {
                        timers[sequence.FilePath] = Modules.ModuleManagement.GetTiming(moduleDescriptor.TypeId);
                    }
                }
                return new string[] { sequence.FilePath };
            }

            return new string[0];
        }

        public ITiming GetTimingSource(ISequence sequence, string sourceName)
        {
            TimingModuleInstanceBase retVal = null;
            
            timers.TryGetValue(sourceName,out retVal);
            
            return retVal;
        }
    }
}
