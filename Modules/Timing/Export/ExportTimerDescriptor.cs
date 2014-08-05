using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vixen.Module;
using Vixen.Module.Timing;

namespace VixenModules.Timing.Export
{
    public class ExportTimerDescriptor : TimingModuleDescriptorBase
    {
        private static Guid _typeId = new Guid("{A3BD84DE-31EE-45E4-87F0-4FEE0FF2AB61}");

        public override string Author
        {
            get { return "Ed Brady"; }
        }

        public override string Description
        {
            get { return "Manual timing module for export of data"; }
        }

        public override Type ModuleClass
        {
            get { return typeof(ExportTimer); }
        }

        public override Type ModuleDataClass
        {
            get { return null; }
        }

        public override Guid TypeId
        {
            get { return _typeId; }
        }

        public override string TypeName
        {
            get { return "Export timing"; }
        }

        public override string Version
        {
            get { return "1.0"; }
        }
    }
}
