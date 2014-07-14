using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vixen.Module;
using System.Runtime.Serialization;

namespace VixenModules.Output.FPPVirtual
{
    [DataContract]
    public class FPPData : ModuleDataModelBase
    {

        private UInt16 _eventTiming;

        [DataMember]
        public UInt16 EventTiming
        {
            get
            {
                return (_eventTiming == 0) ? (UInt16)50 : _eventTiming;
            }

            set
            {
                if ((value == 25) || (value == 50) || (value == 100))
                {
                    _eventTiming = value;
                }
            }
        }

        [DataMember]
        public String FileName { get; set; }

        override public IModuleDataModel Clone()
        {
            return MemberwiseClone() as IModuleDataModel;
        }
    }
}
