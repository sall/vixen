using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vixen.Module.OutputFilter;

namespace VixenModules.OutputFilter.LipSync
{
    public class LipSyncBreakdownDescriptor : OutputFilterModuleDescriptorBase
    {
        private static readonly Guid _typeId = new Guid("{2CE5BAE0-73C8-461D-9295-EDB3F5B9E41B}");

        public override string TypeName
        {
            get { return "LipSync"; }
        }

        public override Guid TypeId
        {
            get { return _typeId; }
        }

        public static Guid ModuleId
        {
            get { return _typeId; }
        }

        public override Type ModuleClass
        {
            get { return typeof(LipSyncBreakdownModule); }
        }

        public override Type ModuleDataClass
        {
            get { return typeof(LipSyncBreakdownData); }
        }

        public override string Author
        {
            get { return "Ed Brady"; }
        }

        public override string Description
        {
            get { return "An output filter that breaks down Phoneme intents into discrete animation components."; }
        }

        public override string Version
        {
            get { return "1.0"; }
        }
    }
}
