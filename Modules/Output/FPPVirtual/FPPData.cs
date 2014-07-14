using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vixen.Module;
using System.Runtime.Serialization;

namespace VixenModules.Output.FPPVirtual
{
    [DataContract]
    public class FPPData : ModuleDataModelBase
    {
        [DataMember]
        public UInt32 StepSize { get; set; }

        [DataMember]
        public UInt16 StepTiming { get; set; }

        [DataMember]
        public String FileName { get; set; }

        override public IModuleDataModel Clone()
        {
            return MemberwiseClone() as IModuleDataModel;
        }
    }
}
