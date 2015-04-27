using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vixen.Module.Effect;
using Vixen.Sys;

namespace Video
{
    public class Descriptor : EffectModuleDescriptorBase
    {
        private static Guid _typeId = new Guid("{F907E4DF-281A-43EC-B6F9-2BCFBB75A439}");

        public override string EffectName
        {
            get { return "Video"; }
        }

        public override EffectGroups EffectGroup
        {
            get { return EffectGroups.Device; }
        }

        public override Guid TypeId
        {
            get { return _typeId; }
        }

        public override Type ModuleClass
        {
            get { return typeof(Module); }
        }

        public override Type ModuleDataClass
        {
            get { return typeof(Data); }
        }

        public override string Author
        {
            get { return "Darren McDaniel"; }
        }

        public override string TypeName
        {
            get { return EffectName; }
        }

        public override string Description
        {
            get { return "Launch External Commands"; }
        }

        public override string Version
        {
            get { return "1.0"; }
        }

        public override Guid[] Dependencies
        {
            get { return new Guid[] { }; }
        }



        public override Vixen.Sys.ParameterSignature Parameters
        {
            get
            {
                return new ParameterSignature(
                    new ParameterSpecification("Description", typeof(string)),
                    new ParameterSpecification("VideoFileName", typeof(string)),
                    new ParameterSpecification("Repeat", typeof(bool)),
                    new ParameterSpecification("StartTime", typeof(double)),
                    new ParameterSpecification("EndTime", typeof(double)),
                    new ParameterSpecification("Volume", typeof(double))
                        );
            }
        }
    }

}
