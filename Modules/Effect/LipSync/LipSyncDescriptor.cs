using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vixen.Sys;
using Vixen.Module.Effect;
using VixenModules.App.Curves;
using VixenModules.App.ColorGradients;

namespace VixenModules.Effect.LipSync
{
    public class LipSyncDescriptor : EffectModuleDescriptorBase
    {
        private static Guid _typeId = new Guid("{52F17F4B-2159-4820-8660-05CD9D1F47C1}");
        private static Guid _CurvesId = new Guid("{4e258de2-7a75-4f0f-aa43-c8182e7f3400}");
        private static Guid _ColorGradientId = new Guid("{64f4ab26-3ed4-49a3-a004-23656ed0424a}");

        public override string EffectName
        {
            get { return "LipSync"; }
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
            get { return typeof(LipSync); }
        }

        public override Type ModuleDataClass
        {
            get { return typeof(LipSyncData); }
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
            get { return "Incorporate Lipsync Files and Data"; }
        }

        public override string Version
        {
            get { return "1.0"; }
        }

        public override Guid[] Dependencies
        {
            get { return new Guid[] { _CurvesId, _ColorGradientId }; }
        }

        public override ParameterSignature Parameters
        {
            get
            {
                return new ParameterSignature(
                    new ParameterSpecification("StaticPhoneme", typeof(string),false),
                    new ParameterSpecification("PGOFilename", typeof(string),false),
                    new ParameterSpecification("PhonemeMapping", typeof(string),false),
                    new ParameterSpecification("Intensity Curve", typeof(Curve)),
                    new ParameterSpecification("Gradient Override", typeof(ColorGradient))
                    );
            }
        }
    }
}