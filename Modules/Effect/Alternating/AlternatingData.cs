using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Common.Controls.ColorManagement.ColorModels;
using Vixen.Module;

namespace VixenModules.Effect.Alternating
{
    [DataContract]
    class AlternatingData : ModuleDataModelBase
    {

        [DataMember]
        public RGB Color1 { get; set; }
        [DataMember]
        public RGB Color2 { get; set; }

        [DataMember]
        public double Level1 { get; set; }
        [DataMember]
        public double Level2 { get; set; }

        [DataMember]
        public int Interval { get; set; }
        [DataMember]
        public bool Enable { get; set; }

        public AlternatingData()
        {
            Level1 = 1;
            Level2 = 1;
            Color1 = Color.White;
            Color2 = Color.Red;
            Enable = false;
            Interval = 1000;
        }

        public override IModuleDataModel Clone()
        {
            AlternatingData result = new AlternatingData();
            result.Level1 = Level1 ;
            result.Level2 = Level2;
            result.Color1 = Color1;
            result.Color2 = Color2;
            result.Enable = Enable;
            result.Interval = Interval;
            return result;
        }
    }
}
