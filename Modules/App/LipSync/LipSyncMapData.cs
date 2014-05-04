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
            MapItems.Add(new LipSyncMapItem());
        }

        public LipSyncMapData(List<string> stringNames)
        {
            int stringNum = 0;
            MapItems = new List<LipSyncMapItem>();
            foreach(string stringName in stringNames)
            {
                MapItems.Add(new LipSyncMapItem(stringName,stringNum++));
            }

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
            newInstance.StringCount = StringCount;
            newInstance.LibraryReferenceName = LibraryReferenceName;
            return newInstance;
        }

        [DataMember]
        public int StringCount { get; set; }

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

        public override string ToString()
        {
            return LibraryReferenceName;
        }
       
    }
}
