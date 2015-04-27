using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vixen.Module.EffectEditor;

namespace VideoEditor
{
    public class Descriptor : EffectEditorModuleDescriptorBase
    {
        private static Guid _typeId = new Guid("{AE590F0B-4336-4E86-918F-B94271FA870D}");
        private static Guid _effectId = new Guid("{F907E4DF-281A-43EC-B6F9-2BCFBB75A439}");

        public override string TypeName
        {
            get { return "Launcher Editor"; }
        }

        public override Guid TypeId
        {
            get { return _typeId; }
        }

        public override Type ModuleClass
        {
            get { return typeof(Module); }
        }

        public override string Author
        {
            get { return "Darren McDaniel"; }
        }

        public override string Description
        {
            get { return "Video Editor"; }
        }

        public override string Version
        {
            get { return "1.0"; }
        }

        public override Guid EffectTypeId
        {
            get { return _effectId; }
        }

        public override Type[] ParameterSignature
        {
            get { return new[] { typeof(string), typeof(string), typeof(bool), typeof(double), typeof(double), typeof(double) }; }
        }
       
    }

}
