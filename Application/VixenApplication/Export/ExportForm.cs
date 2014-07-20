using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Vixen.Services;
using Vixen.Sys;
using Vixen.Sys.Output;
using Vixen.Module.Controller;

namespace VixenApplication
{
    public partial class ExportForm : Form
    {
        private string _exportDir;
        private string _outFileName;
        private Export _exportOps;
        private IExportController _controllerModule = null;

        public ExportForm()
        {
            InitializeComponent();
            _exportDir = Path.Combine(Paths.DataRootPath, "Export");
            _exportOps = new Export();
            exportProgressBar.Visible = false;
            currentTimeLabel.Visible = false;
        }

        public ISequence Sequence { get; set; }

        private bool loadSequence()
        {
            bool retVal = false;
            openFileDialog.InitialDirectory = SequenceService.SequenceDirectory;
            DialogResult dr = openFileDialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                if (File.Exists(openFileDialog.FileName))
                {
                    Sequence = SequenceService.Instance.Load(openFileDialog.FileName);
                    if (Sequence != null)
                    {
                        retVal = true;
                        sequenceNameField.Text = openFileDialog.FileName;
                        _outFileName = _exportDir +
                            Path.DirectorySeparatorChar +
                            Path.GetFileNameWithoutExtension(openFileDialog.FileName) + "." +
                            _controllerModule.ExportFileTypes[outputFormatComboBox.SelectedItem.ToString()];
                    }
                }
            }
            return retVal;
        }

        private void ExportForm_Load(object sender, EventArgs e)
        {
            Sequence = null;
            OutputController outputController = null;
            
            IEnumerable<OutputController> controllers = VixenSystem.OutputControllers.GetAll();

            var type = typeof(IExportController);

            outputController = 
                controllers.Where(x => x.Name.Equals("Export Virtual Controller")).FirstOrDefault();

            _controllerModule = (IExportController)outputController.ControllerModuleInstance;

            
            outputFormatComboBox.Items.Clear();
            outputFormatComboBox.Items.AddRange(_controllerModule.ExportFileTypes.Keys.ToArray());

            outputFormatComboBox.SelectedIndex = 0;
            resolutionComboBox.SelectedIndex = 1;
            
        }

        private void sequenceSetButton_Click(object sender, EventArgs e)
        {
            loadSequence();
        }

        private bool checkExportdir()
        {
            if (!Directory.Exists(_exportDir))
            {
                try
                {
                    Directory.CreateDirectory(_exportDir);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            //Make sure a sequence is loaded
            if (string.IsNullOrWhiteSpace(sequenceNameField.Text))
            {
                if (!loadSequence())
                {
                    return;
                }
            }

            _controllerModule.OutFileName = _outFileName;

            exportProgressBar.Visible = false;
            currentTimeLabel.Visible = false;

 
            _exportOps.DoExport(sequenceNameField.Text);

        }

    }
}
