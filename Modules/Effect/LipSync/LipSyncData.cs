﻿using System;
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
        public String PhonemeMapping { get; set; }

        [DataMember]
        public String LyricData { get; set; }

        public LipSyncData()
        {
            StaticPhoneme = "REST";
            PhonemeMapping = "";
        }

        public override IModuleDataModel Clone()
        {
            LipSyncData result = new LipSyncData();
            result.StaticPhoneme = StaticPhoneme;
            result.PhonemeMapping = PhonemeMapping;
            return result;
        }
    }
}
