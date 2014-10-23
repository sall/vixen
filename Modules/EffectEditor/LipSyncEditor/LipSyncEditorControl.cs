﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Vixen.Module.App;
using Vixen.Module.EffectEditor;
using Vixen.Module.Effect;
using Vixen.Services;
using VixenModules.App.LipSyncApp;
using VixenModules.App.Curves;
using VixenModules.App.ColorGradients;

namespace VixenModules.EffectEditor.LipSyncEditor
{
    public partial class LipSyncEditorControl : UserControl, IEffectEditorControl
    {
        private static NLog.Logger Logging = NLog.LogManager.GetCurrentClassLogger();
        private static List<string> helperStrings = new List<string>();
        private static Dictionary<string, Bitmap> _phonemeBitmaps = null;
        private ResourceManager lipSyncRM = null;
        private LipSyncMapLibrary _library = null;
        private Curve _defaultCurve = new Curve();

        public LipSyncEditorControl()
        {
            InitializeComponent();
            imageListView.View = View.LargeIcon;
            imageListView.LabelEdit = false;
            imageListView.AllowColumnReorder = false;
            imageListView.CheckBoxes = false;
            imageListView.FullRowSelect = false;
            imageListView.GridLines = false;
            imageListView.Sorting = SortOrder.Ascending;
            imageListView.MultiSelect = false;
            imageListView.HideSelection = false;

            imageList1.ImageSize = new Size(40,40);

            _library = ApplicationServices.Get<IAppModuleInstance>(LipSyncMapDescriptor.ModuleID) as LipSyncMapLibrary;
            foreach (LipSyncMapData data in _library.Library.Values)
            {
                mappingComboBox.Items.Add(data.ToString());
            }

            LoadResourceBitmaps();


        }

        private void LoadResourceBitmaps()
        {
            if (_phonemeBitmaps == null)
            {
                Assembly assembly = Assembly.Load("LipSyncApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
                if (assembly != null)
                {
                    lipSyncRM = new ResourceManager("VixenModules.App.LipSyncApp.LipSyncResources", assembly);
                    _phonemeBitmaps = new Dictionary<string, Bitmap>();
                    _phonemeBitmaps.Add("AI", (Bitmap)lipSyncRM.GetObject("AI"));
                    _phonemeBitmaps.Add("E", (Bitmap)lipSyncRM.GetObject("E"));
                    _phonemeBitmaps.Add("ETC", (Bitmap)lipSyncRM.GetObject("etc"));
                    _phonemeBitmaps.Add("FV", (Bitmap)lipSyncRM.GetObject("FV"));
                    _phonemeBitmaps.Add("L", (Bitmap)lipSyncRM.GetObject("L"));
                    _phonemeBitmaps.Add("MBP", (Bitmap)lipSyncRM.GetObject("MBP"));
                    _phonemeBitmaps.Add("O", (Bitmap)lipSyncRM.GetObject("O"));
                    _phonemeBitmaps.Add("PREVIEW", (Bitmap)lipSyncRM.GetObject("Preview"));
                    _phonemeBitmaps.Add("REST", (Bitmap)lipSyncRM.GetObject("rest"));
                    _phonemeBitmaps.Add("U", (Bitmap)lipSyncRM.GetObject("U"));
                    _phonemeBitmaps.Add("WQ", (Bitmap)lipSyncRM.GetObject("WQ"));
                }
            }
        }

        public IEffect TargetEffect { get; set; }

        public object[] EffectParameterValues
        {
            get 
            { 
                return new object[] 
                {
                    StaticPhoneme,
                    PhonemeMapping,
                    LyricData,
                    CurveData,
                    GradientOverride
                }; 
            }

            set
            {
                if (value.Length != 5)
                {
                    Logging.Warn("LipSync effect parameters set with " + value.Length + " parameters");
                    return;
                }

                imageList1.Images.Clear();
                imageListView.Items.Clear();
                staticPhoneMeCombo.Items.Clear();

                // Initialize the ImageList objects with bitmaps.
                foreach (string key in _phonemeBitmaps.Keys)
                {
                    if (!key.Equals("PREVIEW"))
                    {
                        imageList1.Images.Add(key, _phonemeBitmaps[key]);
                        imageListView.Items.Add(key, key);
                        staticPhoneMeCombo.Items.Add(key);
                    }
                }

                StaticPhoneme = (string)value[0];
                PhonemeMapping = (string)value[1];
                LyricData = (string)value[2];
                CurveData = (Curve)value[3];
                GradientOverride = (ColorGradient)value[4];
            }
        }

        public string PhonemeMapping
        {
            get
            {
                return (string)mappingComboBox.SelectedItem;
            }

            set
            {
                string inString = (string)value;
                int index = mappingComboBox.FindStringExact(inString);
                if (index == -1)
                {
                    index = mappingComboBox.FindStringExact(_library.DefaultMappingName);
                    if (index == -1)
                    {
                        index = 0;
                    }
                }
                mappingComboBox.SelectedIndex = index;
            }
        }
        public string StaticPhoneme
        {
            get 
            { 
                return staticPhoneMeCombo.Text; 
            }
            
            set 
            { 
                staticPhoneMeCombo.Text = value;
                selectImageListItem(value);
            }
        }

        public string LyricData
        {
            get
            {
                return lyricDataTextBox.Text;
            }

            set
            {
                lyricDataTextBox.Text = value;
            }
        }

        public Curve CurveData
        {
            get
            {
                return curveTypeEditorControl1.CurveValue;
            }

            set
            {
                curveTypeEditorControl1.CurveValue = value;
            }
        }

        public ColorGradient GradientOverride
        {
            get
            {
                if (gradientOverrideCheckBox.Checked)
                {
                    return colorGradientTypeEditorControl1.ColorGradientValue;
                }

                return null;
            }

            set
            {
                if (value != null)
                {
                    colorGradientTypeEditorControl1.ColorGradientValue = value;
                }
                gradientOverrideCheckBox.Checked = (value != null);
            }
        }

        private void selectImageListItem(string text)
        {
            foreach (ListViewItem viewItem in imageListView.Items)
            {
                if (viewItem.Text == text)
                {
                    viewItem.Selected = true;
                    break;
                }
            }
        }

        private void LipSyncEditorControl_Leave(object sender, EventArgs e)
        {
            StaticPhoneme = staticPhoneMeCombo.Text.Trim();
        }

        private void LipSyncEditorControl_Load(object sender, EventArgs e)
        {
            staticPhoneMeCombo.Items.AddRange(helperStrings.ToArray());
            if (StaticPhoneme.Equals("") == false)
            {
                staticPhoneMeCombo.Text = StaticPhoneme;
            }

            gradientOverrideCheckBox.Checked = (GradientOverride != null);
            colorGradientTypeEditorControl1.Visible = gradientOverrideCheckBox.Checked;

        }

        private void staticPhoneMeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectImageListItem(staticPhoneMeCombo.Text);
        }

        private void imageListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection items = imageListView.SelectedItems;
            if (items.Count > 0)
            {
                staticPhoneMeCombo.Text = imageListView.SelectedItems[0].Text;
            }        
        }

        private void gradientOverrideCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            colorGradientTypeEditorControl1.Visible = gradientOverrideCheckBox.Checked;
        }

    }
}
