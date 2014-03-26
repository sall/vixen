using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Vixen.Module;

namespace VixenModules.Effect.Papagayo
{
    [DataContract]
    public class PapagayoData : ModuleDataModelBase
    {

        public PapagayoData()
        {
        }

        public override IModuleDataModel Clone()
        {
            PapagayoData result = new PapagayoData();
            return result;
        }
    }
}
