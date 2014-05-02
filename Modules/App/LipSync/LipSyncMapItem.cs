using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;
using Common.Controls.ColorManagement.ColorModels;
using Vixen.Module;
using Vixen.Module.App;

namespace VixenModules.App.LipSyncMap
{
    [DataContract]
    public class LipSyncMapItem
    {
        public LipSyncMapItem()
        {
            Name = "String";
            PhonemeList = new Dictionary<string, Boolean>();
            ActiveColorIndex = 0;
            StringNum = -1;
            PixelNum = -1;
        }

        public LipSyncMapItem Clone()
        {
            LipSyncMapItem retVal = new LipSyncMapItem();
            retVal.Name = Name;
            retVal.PhonemeList = new Dictionary<string, bool>(PhonemeList);
            retVal.ActiveColorIndex = ActiveColorIndex;
            retVal.StringNum = StringNum;
            retVal.PixelNum = PixelNum;

            return retVal;
        }

        [DataMember]
        public int StringNum { get; set; }

        [DataMember]
        public int PixelNum { get; set; }


        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int ActiveColorIndex { get; set; }

        [DataMember]
        public Dictionary<string, Boolean> PhonemeList { get; set; }

        [DataMember]
        public Color ElementColor { get; set; }


    }

   
}