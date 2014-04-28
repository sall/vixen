using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using Vixen.Module.EffectEditor;
using Vixen.Module.Effect;
using System.Diagnostics;
using System.Runtime.InteropServices;


namespace VixenModules.EffectEditor.LipSyncEditor
{
    public partial class LipSyncEditorControl : UserControl, IEffectEditorControl
    {
        private static NLog.Logger Logging = NLog.LogManager.GetCurrentClassLogger();
        private static List<string> helperStrings = new List<string>();
        private static Dictionary<string, Bitmap> _phonemeBitmaps = null;
        private ResourceManager lipSyncRM = null;

        //Note:  These two must match like named variables in LipSyncBreakdownSetup
        private static readonly int NUM_COLORSETS = 4;
        private const string COLORSET_NAME = "Color Set ";

        public LipSyncEditorControl()
        {
            InitializeComponent();
            pgoFileNameLabel.Text = "None";
            imageListView.View = View.LargeIcon;
            imageListView.LabelEdit = false;
            imageListView.AllowColumnReorder = false;
            imageListView.CheckBoxes = false;
            imageListView.FullRowSelect = false;
            imageListView.GridLines = false;
            imageListView.Sorting = SortOrder.Ascending;
            imageListView.MultiSelect = false;
            imageListView.HideSelection = false;

            imageList1.ImageSize = new Size(48,48);

            LoadResourceBitmaps();


        }

        private void LoadResourceBitmaps()
        {
            if (_phonemeBitmaps == null)
            {
                Assembly assembly = Assembly.Load("LipSync, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
                if (assembly != null)
                {
                    lipSyncRM = new ResourceManager("VixenModules.Effect.LipSync.LipSyncResources", assembly);
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
                    PGOFilename,
                    ColorGroupIndex,
                }; 
            }

            set
            {
                if (value.Length != 3)
                {
                    Logging.Warn("LipSync effect parameters set with " + value.Length + " parameters");
                    return;
                }

                imageList1.Images.Clear();
                imageListView.Items.Clear();
                staticPhoneMeCombo.Items.Clear();
                colorGroupComboBox.Items.Clear();

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

                for (int j = 0; j < NUM_COLORSETS; j++ )
                {
                    colorGroupComboBox.Items.Add(COLORSET_NAME + j);
                }

                StaticPhoneme = (string)value[0];
                PGOFilename = (string)value[1];
                ColorGroupIndex = (int)value[2];
            }
        }

        public int ColorGroupIndex
        {
            get
            {
                return colorGroupComboBox.SelectedIndex;
            }

            set
            {
                if ((value < 0) || (value >= NUM_COLORSETS))
                {
                    colorGroupComboBox.SelectedIndex = 0;
                }
                else
                {
                    colorGroupComboBox.SelectedIndex = value;
                }
                
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

        public String PGOFilename
        {
            get { return pgoFileNameLabel.Text; }
            set { pgoFileNameLabel.Text = value; }
        }

        private string setSrcFile()
        {
            string retVal = PGOFilename;
            FileDialog openDialog = new OpenFileDialog();

            openDialog.Filter = "Papagayo files (*.pgo)|*.pgo|All files (*.*)|*.*";
            openDialog.FilterIndex = 1;
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                retVal = openDialog.FileName;
            }
            return retVal;
        }

        private void PGOFileButton_Click(object sender, EventArgs e)
        {
            PGOFilename = setSrcFile();

        }

        private void setControlStates()
        {
            staticPhoneMeCombo.Enabled = staticRadioButton.Checked;
            PGOFileButton.Enabled = linkedRadioButton.Checked;
            pgoFileNameLabel.Enabled = linkedRadioButton.Checked;
        }

        private void linkedRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            setControlStates();
        }

        private void staticRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            setControlStates();
        }

        private void LipSyncEditorControl_Leave(object sender, EventArgs e)
        {
            string tempVal;
            if (staticRadioButton.Checked)
            {
                tempVal = staticPhoneMeCombo.Text.Trim();
                StaticPhoneme = tempVal;
                PGOFilename = "";
            }
            else if(linkedRadioButton.Checked)
            {
                StaticPhoneme = "";
                PGOFilename = pgoFileNameLabel.Text.Trim();
            }
        }

        private void LipSyncEditorControl_Load(object sender, EventArgs e)
        {
            staticPhoneMeCombo.Items.AddRange(helperStrings.ToArray());
            if (StaticPhoneme.Equals("") == false)
            {
                staticPhoneMeCombo.Text = StaticPhoneme;
                staticRadioButton.Checked = true;
                PGOFilename = "";
                setControlStates();
            }
            else if (PGOFilename.Equals("") == false)
            {
                pgoFileNameLabel.Text = PGOFilename;
                linkedRadioButton.Checked = true;
                StaticPhoneme = "";
                setControlStates();
            }
            else
            {
                staticRadioButton.Checked = true;
                setControlStates();
                if (staticPhoneMeCombo.Items.Count > 0)
                {
                    staticPhoneMeCombo.SelectedIndex = 0;
                }
            }
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
    }
}
