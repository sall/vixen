using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Vixen.Module;
using Vixen.Module.App;

namespace VixenModules.App.LipSyncMap
{
    public class LipSyncMapLibrary : AppModuleInstanceBase, IEnumerable<KeyValuePair<string, LipSyncMapData>>
    {
        private LipSyncMapStaticData _staticData;
        private LipSyncMapData _data;
        static int uniqueKeyIndex = 0;

        public override void Loading()
        {
        }

        public override void Unloading()
        {
        }

        public override Vixen.Sys.IApplication Application
        {
            set { }
        }

        public override IModuleDataModel StaticModuleData
        {
            get { return _staticData; }
            set { _staticData = value as LipSyncMapStaticData; }
        }

        public Dictionary<string, LipSyncMapData> Library
        {
            get { return _staticData.Library; }
        }

        public IEnumerator<KeyValuePair<string, LipSyncMapData>> GetEnumerator()
        {
            return Library.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Library.GetEnumerator();
        }

        public bool Contains(string name)
        {
            return Library.ContainsKey(name);
        }

        public LipSyncMapData GetMapping(string name)
        {
            if (Library.ContainsKey(name))
                return Library[name];
            else
                return null;
        }

        public bool AddMapping(string name, LipSyncMapData mapping)
        {

            string mapName = name;

            if (string.IsNullOrWhiteSpace(mapName) == true)
            {
                mapName = "New Map";
                while (Library.Keys.Contains(mapName) == true)
                {
                    mapName = string.Format("New Map {0}", ++uniqueKeyIndex);
                }
            }


            bool inLibrary = Contains(mapName);
            if (inLibrary)
            {
                Library[mapName].IsCurrentLibraryMapping = false;
            }
            mapping.IsCurrentLibraryMapping = true;
            mapping.LibraryReferenceName = mapName;
            Library[mapName] = mapping;
            return inLibrary;
        }

        public bool RemoveMapping(string name)
        {
            if (!Contains(name))
                return false;

            Library[name].IsCurrentLibraryMapping = false;
            Library.Remove(name);

            return true;
        }

        public bool EditLibraryMapping(string name)
        {
            LipSyncMapData mapping = GetMapping(name);
            if (mapping == null)
            {
                return false;
            }

            LipSyncMapEditor editor = new LipSyncMapEditor(mapping);
            editor.LibraryMappingName = name;

            if (editor.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                RemoveMapping(name);
                AddMapping(editor.LibraryMappingName, editor.MapData);
                return true;
            }

            return false;
        }

    }
}
