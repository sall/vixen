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
using VixenModules.Effect.Papagayo;

namespace VixenModules.EffectEditor.PapagayoEditor
{
    public partial class PapagayoEditorControl : UserControl, IEffectEditorControl
    {
        private static NLog.Logger Logging = NLog.LogManager.GetCurrentClassLogger();
//TODO - get from Papagayo Data        private PapagayoDoc m_papagayoData = null;
        public PapagayoEditorControl()
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
                    PGOFilename 
                }; 
            }

            set
            {
                if (value.Length != 1)
                {
                    Logging.Warn("Papagaya effect parameters set with " + value.Length + " parameters");
                    return;
                }

                PGOFilename = (string)value[0];
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

        private void loadPapagayoFile()
        {
            try
            {
//TODO                m_papagayoData.Load(PGOFilename);
            }
            catch (Exception) { }
        }

        private void PGOFileButton_Click(object sender, EventArgs e)
        {
            PGOFilename = setSrcFile();

           // srcFileImport(tmpSrcFile);
        }
    }
}
