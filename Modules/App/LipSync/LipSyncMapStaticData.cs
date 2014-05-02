using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Vixen.Module;

namespace VixenModules.App.LipSyncMap 
{
    [DataContract]
    class LipSyncMapStaticData : ModuleDataModelBase
    {
        [DataMember]
        public Rectangle SelectorWindowBounds { get; set; }

        [DataMember]
        private Dictionary<string, LipSyncMapData> _library;

        public Dictionary<string, LipSyncMapData> Library
        {
            get
            {
                if (_library == null)
                {
                    _library = new Dictionary<string, LipSyncMapData>();
                    LipSyncMapData mapData = new LipSyncMapData();
                }
                    

                return _library;
            }
            set { _library = value; }
        }

        public override IModuleDataModel Clone()
        {
            LipSyncMapStaticData result = new LipSyncMapStaticData();
            result.Library = new Dictionary<string, LipSyncMapData>(Library);
            return result;
        }
    }
}
