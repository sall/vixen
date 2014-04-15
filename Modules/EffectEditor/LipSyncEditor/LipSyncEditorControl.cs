using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
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

        public LipSyncEditorControl()
        {
            InitializeComponent();
            pgoFileNameLabel.Text = "None";
        }

        public IEffect TargetEffect { get; set; }

        public object[] EffectParameterValues
        {
            get 
            { 
                return new object[] 
                {
                    StaticPhoneme,
                    PGOFilename 
                }; 
            }

            set
            {
                if (value.Length != 2)
                {
                    Logging.Warn("Papagaya effect parameters set with " + value.Length + " parameters");
                    return;
                }
                StaticPhoneme = (string)value[0];
                PGOFilename = (string)value[1];
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
                addHelperString(value);
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

        private void addHelperString(string helperString)
        {
            if ((helperStrings.Contains(helperString) == false) &&
                (helperString != ""))
            {
                helperStrings.Add(helperString);
            }
        }

        private void LipSyncEditorControl_Leave(object sender, EventArgs e)
        {
            string tempVal;
            if (staticRadioButton.Checked)
            {
                tempVal = staticPhoneMeCombo.Text.Trim();
                addHelperString(tempVal);
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
    }
}
