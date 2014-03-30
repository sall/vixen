using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Vixen.Module;

namespace VixenModules.Effect.Papagayo
{
    [DataContract]
    internal class PapagayoData : ModuleDataModelBase
    {
        [DataMember]
        public String PGOFilename { get; set; }

        public PapagayoData()
        {
            PGOFilename = "Dude!";
        }

        public override IModuleDataModel Clone()
        {
            PapagayoData result = new PapagayoData();
            return result;
        }
    }
}
