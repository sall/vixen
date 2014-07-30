using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vixen.Module;
using Vixen.Module.App;

namespace VixenModules.App.Export
{
    public class ExportDescriptor : AppModuleDescriptorBase
    {
        private static readonly Guid _typeId = new Guid("9C55F3AB-914C-41D5-9CB3-A5720AB5CED7");

        public override string TypeName
        {
            get { return "Export Manager"; }
        }

        public override Guid TypeId
        {
            get { return _typeId; }
        }

        public override Type ModuleClass
        {
            get { return typeof(ExportModule); }
        }

        public override Type ModuleStaticDataClass
        {
            get { return typeof(ExportData); }
        }

        public override string Author
        {
            get { return "Ed Brady"; }
        }

        public override string Description
        {
            get { return "Interim Data Export Solution."; }
        }

        public override string Version
        {
            get { return "1.0"; }
        }
    }
}