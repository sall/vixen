using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;
using Vixen.Module;
using Vixen.Module.App;

namespace VixenModules.App.LipSyncApp
{
    public class LipSyncMapData : ModuleDataModelBase
    {
        public LipSyncMapData()
        {
            MapItems = new List<LipSyncMapItem>();
            MapItems.Add(new LipSyncMapItem());
            IsDefaultMapping = false;
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
            MapItems = new List<LipSyncMapItem>(data.MapItems);
            IsCurrentLibraryMapping = data.IsCurrentLibraryMapping;
            LibraryReferenceName = (string)data.LibraryReferenceName.Clone();
            IsDefaultMapping = data.IsDefaultMapping;
            StringCount = data.StringCount;
        }

        public override IModuleDataModel Clone()
        {
            LipSyncMapData newInstance = new LipSyncMapData();
            newInstance.MapItems = new List<LipSyncMapItem>(MapItems);
            newInstance.StringCount = StringCount;
            newInstance.LibraryReferenceName = LibraryReferenceName;
            newInstance.IsDefaultMapping = false;
            return newInstance;
        }

        [DataMember]
        public int StringCount { get; set; }

        [DataMember]
        public List<LipSyncMapItem> MapItems { get; set; }

        [DataMember]
        public bool IsCurrentLibraryMapping { get; set; }

        [DataMember]
        public bool IsDefaultMapping { get; set; }

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

        public LipSyncMapItem FindMapItem(string itemName)
        {
            return MapItems.Find(x => x.Name.Equals(itemName));
        }

        public Color ConfiguredColor(string itemName, string phonemeName)
        {
            Color retVal = Color.Black;
            LipSyncMapItem item = FindMapItem(itemName);

            if (item != null)
            {
                if (item.PhonemeList[phonemeName] == true)
                {
                    retVal = item.ElementColor;
                }
            }
            return retVal;
        }

        public float ConfiguredIntensity(string itemName, string phonemeName)
        {
            float retVal = 0.0f;
            LipSyncMapItem item = FindMapItem(itemName);

            if (item != null)
            {
                if (item.PhonemeList[phonemeName] == true)
                {
                    retVal = 1.0f;
                }
            }
            return retVal;
        }

        public bool PhonemeState(string itemName, string phonemeName)
        {
            bool retVal = false;
            LipSyncMapItem item = FindMapItem(itemName);
            if (item != null)
            {
                item.PhonemeList.TryGetValue(phonemeName, out retVal);
            }

            return retVal;
        }


        public override string ToString()
        {
            return LibraryReferenceName;
        }
       
    }
}
