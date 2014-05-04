﻿using System;
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

        private LipSyncMapData _defaultMap;

        public LipSyncMapData DefaultMapping
        {
            get
            {
                if (_defaultMap == null)
                {
                    _defaultMap = Library.FirstOrDefault().Value;
                }
                return _defaultMap;
            }

            set
            {
                _defaultMap = value;
            }
        }

        public string DefaultMappingName
        {
            get
            {
                return DefaultMapping.LibraryReferenceName;
            }

            set
            {
                string newDefaultName = (string)value;

                if (_staticData.Library.ContainsKey(newDefaultName))
                {
                    _defaultMap = _staticData.Library[newDefaultName];
                }
            }
        }

        public bool IsDefaultMapping(string compareName)
        {
            return DefaultMapping.LibraryReferenceName.Equals(compareName);
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

        public bool AddMapping(bool insertNew, string name, LipSyncMapData mapping)
        {

            string mapName = name;

            if (insertNew)
            {
                if (string.IsNullOrWhiteSpace(mapName) == true)
                {
                    mapName = "New Map";
                }
                else
                {
                    mapName = name;
                }
                while (Library.Keys.Contains(mapName) == true)
                {
                    mapName = string.Format(mapName + "({0})", ++uniqueKeyIndex);
                }
            }

            bool inLibrary = Contains(mapName);
            if (inLibrary)
            {
                Library[mapName].IsCurrentLibraryMapping = false;
            }
            mapping.IsCurrentLibraryMapping = true;
            mapping.LibraryReferenceName = mapName;
            Library[mapName] = (insertNew) ? (LipSyncMapData)mapping.Clone() : mapping;
            return inLibrary;
        }

        public bool RemoveMapping(string name)
        {
            if (!Contains(name))
                return false;

            Library[name].IsCurrentLibraryMapping = false;
            if (IsDefaultMapping(name) == true)
            {
                _defaultMap = null;
            }
            Library.Remove(name);

            return true;
        }

        public bool EditLibraryMapping(string name)
        {
            bool doRemove = true;
            bool retVal = false;
            LipSyncMapData mapping = GetMapping(name);

            if (mapping != null)
            {
                LipSyncMapEditor editor = new LipSyncMapEditor(mapping);
                editor.LibraryMappingName = name;

                if (editor.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if ((name.Equals(editor.LibraryMappingName) == false) &&
                        (this.Contains(editor.LibraryMappingName) == true)) 
                    {
                        DialogResult dr =
                            MessageBox.Show("Overwrite existing " + 
                                editor.LibraryMappingName + " mapping?", 
                                "Map exists", 
                                MessageBoxButtons.YesNo);

                        doRemove = (dr == DialogResult.Yes) ? true : false;
                    }

                    if (doRemove == true)
                    {
                        RemoveMapping(name);
                    }
                    
                    AddMapping(!doRemove,editor.LibraryMappingName, editor.MapData);
                    retVal = true;
                }
            }



            return retVal;
        }

    }
}
