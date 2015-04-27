using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Vixen.Module;

namespace Video
{
    [DataContract]
    public class Data : ModuleDataModelBase
    {

        [DataMember]
        public string Description { get; set; }


        [DataMember]
        public string VideoFileName { get; set; }

        [DataMember]
        public bool Repeat { get; set; }

        [DataMember]
        public double StartTime { get; set; }

        [DataMember]
        public double EndTime { get; set; }
       
        [DataMember]
        public double Volume { get; set; }

        
        public override IModuleDataModel Clone()
        {
            return (IModuleDataModel)this.MemberwiseClone();
        }
    }
}
