using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vixen.Module.EffectEditor;

namespace VixenModules.EffectEditor.PapagayoEditor
{
    internal class PapagayoEditorDescriptor : EffectEditorModuleDescriptorBase
    {
        private static Guid _typeId = new Guid("{5F078B66-2961-4187-8C08-8EBEEA669671}");

        public override string Author
        {
            get { return "Ed Brady"; }
        }

        public override string Description
        {
            get { return "An editor for Papagayo Animations."; }
        }

        public override Type ModuleClass
        {
            get { return typeof(PapagayoEditorModule); }
        }

        public override Guid TypeId
        {
            get { return _typeId; }
        }

        public override string TypeName
        {
            get { return "Papagayo Editor"; }
        }

        public override string Version
        {
            get { return "1.0"; }
        }

        public override Type[] ParameterSignature
        {
            get { return null; }
        }

        public override Guid EffectTypeId
        {
            get { return Guid.Empty; }
        }
    }
}