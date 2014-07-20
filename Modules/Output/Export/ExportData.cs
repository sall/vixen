using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vixen.Module;
using System.Runtime.Serialization;

namespace VixenModules.Output.Export
{
    [DataContract]
    public class ExportData : ModuleDataModelBase
    {

        override public IModuleDataModel Clone()
        {
            return MemberwiseClone() as IModuleDataModel;
        }
    }
}
