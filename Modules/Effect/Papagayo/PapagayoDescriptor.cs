using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Vixen.Sys;
using Vixen.Module.Effect;

namespace VixenModules.Effect.Papagayo
{
    public class PapagayoDescriptor : EffectModuleDescriptorBase
    {
        private static Guid _typeId = new Guid("{52F17F4B-2159-4820-8660-05CD9D1F47C1}");

        public override string EffectName
        {
            get { return "Papagayo"; }
        }

        public override EffectGroups EffectGroup
        {
            get { return EffectGroups.Basic; }
        }

        public override Guid TypeId
        {
            get { return _typeId; }
        }

        public override Type ModuleClass
        {
            get { return typeof(Papagayo); }
        }

        public override Type ModuleDataClass
        {
            get { return typeof(PapagayoData); }
        }

        public override string Author
        {
            get { return "Ed Brady"; }
        }

        public override string TypeName
        {
            get { return EffectName; }
        }

        public override string Description
        {
            get { return "Incorporate Papagyo Lipsync Files"; }
        }

        public override string Version
        {
            get { return "1.0"; }
        }

        public override ParameterSignature Parameters
        {
            get
            {
                return new ParameterSignature(
                    new ParameterSpecification("PGOFilename", typeof(String))
                    );
            }
        }
    }
}