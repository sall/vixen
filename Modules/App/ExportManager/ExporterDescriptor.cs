using System;
using Vixen.Module.App;

namespace VixenModules.App.Exporter
{
    public class InstrumentationDescriptor : AppModuleDescriptorBase
    {
        private Guid _typeId = new Guid("{5F4CDFBE-8449-42CE-A759-9B6B1002E958}");

        public override string TypeName
        {
            get { return "Export Manager"; }
        }

        public override Guid TypeId
        {
            get { return _typeId; }
        }

        public override string Author
        {
            get { return "Ed Brady"; }
        }

        public override string Description
        {
            get { return "Export Vixen3 data to other formats"; }
        }

        public override string Version
        {
            get { return "1.0"; }
        }

        public override Type ModuleClass
        {
            get { return typeof(ExporterModule); }
        }
    }
}