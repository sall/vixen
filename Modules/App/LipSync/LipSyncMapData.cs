using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;
using Vixen.Module;
using Vixen.Module.App;

namespace VixenModules.App.LipSyncMap
{
    public class LipSyncMapData : ModuleDataModelBase
    {
        public LipSyncMapData()
        {
            MapItems = new List<LipSyncMapItem>();
        }

        public LipSyncMapData(LipSyncMapData data)
        {
            MapItems = data.MapItems;
            IsCurrentLibraryMapping = data.IsCurrentLibraryMapping;
            LibraryReferenceName = data.LibraryReferenceName;
        }

        public override IModuleDataModel Clone()
        {
            LipSyncMapData newInstance = new LipSyncMapData();
            newInstance.MapItems = new List<LipSyncMapItem>(MapItems);
            return newInstance;
        }

        [DataMember]
        public int StringCount { get; set; }

        [DataMember]
        public int PixelCount { get; set; }

        [DataMember]
        public bool HasPixels { get; set; }

        [DataMember]
        public List<LipSyncMapItem> MapItems { get; set; }

        [DataMember]
        public bool IsCurrentLibraryMapping { get; set; }

        [DataMember]
        protected string _libraryReferenceName;

        public string LibraryReferenceName
        {
            get
            {
                if (_libraryReferenceName == null)
                    return string.Empty;
                else
                    return _libraryReferenceName;
            }
            set { _libraryReferenceName = value; }
        }
    }
}
