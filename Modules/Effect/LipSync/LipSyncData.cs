using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Vixen.Module;

namespace VixenModules.Effect.LipSync
{
    [DataContract]
    internal class LipSyncData : ModuleDataModelBase
    {
        [DataMember]
        public String StaticPhoneme { get; set; }

        [DataMember]
        public String PGOFilename { get; set; }

        [DataMember]
        public String ColorGroup { get; set; }

        public LipSyncData()
        {
            StaticPhoneme = "";
            PGOFilename = "";
            ColorGroup = "";
        }

        public override IModuleDataModel Clone()
        {
            LipSyncData result = new LipSyncData();
            return result;
        }
    }
}
