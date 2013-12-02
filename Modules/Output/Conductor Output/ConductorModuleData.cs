using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vixen;
using Vixen.Module;
using System.Runtime.Serialization;


namespace VixenModules.Output.ConductorOutput
{
    [DataContract]
    public class ConductorModuleData : ModuleDataModelBase
    {
        [DataMember]
        public Boolean savedata;
		[DataMember]
		public Boolean OutputDebug;

        public override IModuleDataModel Clone()
        {
            return MemberwiseClone() as IModuleDataModel;
        }
    }
}
