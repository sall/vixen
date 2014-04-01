using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;
using Vixen.Module;


namespace VixenModules.OutputFilter.LipSync
{
    public class LipSyncBreakdownData : ModuleDataModelBase
    {
        public LipSyncBreakdownData()
        {
            BreakdownItems = new List<LipSyncBreakdownItem>();
        }

        public override IModuleDataModel Clone()
        {
            LipSyncBreakdownData newInstance = new LipSyncBreakdownData();
            newInstance.BreakdownItems = new List<LipSyncBreakdownItem>(BreakdownItems);
            return newInstance;
        }

        [DataMember]
        public List<LipSyncBreakdownItem> BreakdownItems { get; set; }

    }
}
