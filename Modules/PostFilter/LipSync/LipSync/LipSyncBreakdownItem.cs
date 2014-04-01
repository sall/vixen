using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace VixenModules.OutputFilter.LipSync
{
    [DataContract]
    public class LipSyncBreakdownItem
    {
        public LipSyncBreakdownItem()
        {
           Name = "Unnamed";
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public List<LipSyncBreakdownItemPhoneme> PhonemeList { get; set; }
    }

    public class LipSyncBreakdownItemPhoneme
    {
        public LipSyncBreakdownItemPhoneme()
        {
            phonemeName = "";
            isChecked = false;
        }

        public LipSyncBreakdownItemPhoneme(LipSyncBreakdownItemPhoneme item)
        {
            phonemeName = item.phonemeName; ;
            isChecked = item.isChecked;
        }

        public string phonemeName { get; set; }

        public bool isChecked { get; set; }
    }

}
