using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Vixen.Module.Timing;
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
        private ITiming _timing;
        private bool _doProgressUpdate;
        private const int RENDER_TIME_DELTA = 250;

        public ExportForm()
        {
            InitializeComponent();
            _exportDir = Path.Combine(Paths.DataRootPath, "Export");
            _exportOps = new Export();
            exportProgressBar.Visible = false;
            currentTimeLabel.Visible = false;

            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
        }

        public ISequence Sequence { get; set; }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            double percentComplete = 0;
            TimeSpan renderCheck = new TimeSpan(0, 0, 0, 0, 250);
            if (_timing != null)
            {
                while (_doProgressUpdate)
                {
                    Thread.Sleep(25);
                    if (_timing.Position.TotalMilliseconds < RENDER_TIME_DELTA)
                    {
                        currentTimeLabel.Text = "Rendering Elements";
                    }
                    else
                    {
                        currentTimeLabel.Text = string.Format("{0:D2}:{1:D2}.{2:D3}",
                                                                _timing.Position.Minutes,
                                                                _timing.Position.Seconds,
                                                                _timing.Position.Milliseconds);

                        percentComplete =
                            (_timing.Position.TotalMilliseconds /
                            _exportOps.SequenceLenghth) * 100;

                        backgroundWorker1.ReportProgress((int)percentComplete);                    
                    }
                }
                this.UseWaitCursor = false;
                MessageBox.Show("File saved to " + _outFileName);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            exportProgressBar.Value = e.ProgressPercentage;
        }

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

            if (outputController != null)
            {
                _controllerModule = (IExportController)outputController.ControllerModuleInstance;


                outputFormatComboBox.Items.Clear();
                outputFormatComboBox.Items.AddRange(_controllerModule.ExportFileTypes.Keys.ToArray());

                outputFormatComboBox.SelectedIndex = 0;
                resolutionComboBox.SelectedIndex = 1;

            }
            else
            {
                MessageBox.Show("Unable to find Virtual Export Controller, have you added it to your display?", "Error");
                Close();
            }
            
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

            this.UseWaitCursor = true;

            //Make sure a sequence is loaded
            if (string.IsNullOrWhiteSpace(sequenceNameField.Text))
            {
                if (!loadSequence())
                {
                    return;
                }
            }

            checkExportdir();
            _outFileName = _exportDir +
                Path.DirectorySeparatorChar +
                Path.GetFileNameWithoutExtension(openFileDialog.FileName) + "." +
                _controllerModule.ExportFileTypes[outputFormatComboBox.SelectedItem.ToString()];

            _controllerModule.OutFileName = _outFileName;
            _controllerModule.UpdateInterval = Convert.ToInt32(resolutionComboBox.Text);
            exportProgressBar.Visible = true;
            currentTimeLabel.Visible = true;

            startButton.Enabled = false;
            cancelButton.Enabled = true;

 
            _exportOps.DoExport(sequenceNameField.Text);
            _exportOps.SetContextEndHandler(context_SequenceEnded);
            _timing = _exportOps.SequenceTiming;

            _doProgressUpdate = true;
            backgroundWorker1.RunWorkerAsync();



        }

        private void context_SequenceEnded(object sender, EventArgs e)
        {
            startButton.Enabled = true;
            cancelButton.Enabled = false;
            _doProgressUpdate = false;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            _doProgressUpdate = false;
            cancelButton.Enabled = false;
            startButton.Enabled = true;
            _exportOps.CancelExport();
            _exportOps.ClearContextEndHandler(context_SequenceEnded);
        }

    }
}
