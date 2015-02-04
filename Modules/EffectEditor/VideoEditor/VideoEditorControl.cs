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

namespace VideoEditor
{
    public partial class VideoEditorControl : UserControl, IEffectEditorControl
    {
        private const string timeFormat = @"mm\:ss\.fff";
        public VideoEditorControl()
        {
            InitializeComponent();

        }




        void vPlayer1_SourceUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        {
            vPlayer1.StartPosition = TimeSpan.FromMilliseconds(StartTime);
            vPlayer1.StopPosition = EndTime > 0 ? TimeSpan.FromMilliseconds(EndTime) : vPlayer1.NaturalDuration.TimeSpan;

        }

        #region IEffectEditorControl Members

        public object[] EffectParameterValues
        {
            get
            {
                return new object[] { 
                    Description, 
                    VideoFileName, 
                    Repeat, 
                    StartTime , 
                    EndTime, 
                    Volume };
            }
            set
            {
                Description = value[0] as string;
                VideoFileName = value[1] as string;
                Repeat = (bool)(value[2] ?? false);
                StartTime = (double)value[3];
                EndTime = (double)value[4];
                Volume = (double)(value[5] ?? 50);
            }
        }
        private IEffect _targetEffect;

        public IEffect TargetEffect
        {
            get { return _targetEffect; }
            set
            {
                _targetEffect = value;
                //Ensure target effect is passed through as these editors need it.
            }
        }

        #endregion

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Multiselect = false;
                var res = dlg.ShowDialog(this);
                if (res == DialogResult.OK)
                {
                    this.txtFileName.Text = dlg.FileName;
                    this.vPlayer1.SetSourceUri(new Uri(dlg.FileName));

                }
            }
        }

        public string VideoFileName
        {
            get { return this.txtFileName.Text; }
            set
            {
                this.txtFileName.Text = value;
                if (!string.IsNullOrWhiteSpace(value))
                    this.vPlayer1.SetSourceUri(new Uri(value));

            }
        }
        public string Description { get { return this.txtDescription.Text; } set { this.txtDescription.Text = value; } }
        public bool Repeat { get { return this.chkRepeatVideo.Checked; } set { this.chkRepeatVideo.Checked = value; } }
        public TimeSpan StartTimeTS { get { return tsStartTime.TimeSpan; } set { tsStartTime.TimeSpan = value; } }
        public TimeSpan EndTimeTS { get { return tsEndTime.TimeSpan; } set { tsEndTime.TimeSpan = value; } }
        public double StartTime { get { return tsStartTime.TimeSpan.TotalMilliseconds; } set { tsStartTime.TimeSpan = TimeSpan.FromMilliseconds(value); } }
        public double EndTime { get { return tsEndTime.TimeSpan.TotalMilliseconds; } set { tsEndTime.TimeSpan = TimeSpan.FromMilliseconds(value); } }

        public double Volume { get { return this.trkVolume.Value; } set { this.trkVolume.Value = (int)value; } }

        private void trkVolume_Scroll(object sender, EventArgs e)
        {
            vPlayer1.SetVolume((double)trkVolume.Value);
        }

        private void trkVideoLength_Scroll(object sender, EventArgs e)
        {
        }

        private void btnStopMedia_Click(object sender, EventArgs e)
        {
            vPlayer1.Stop();
            btnStopMedia.Enabled = false;
            btnPlayMedia.Enabled = true;
        }

        private void btnPlayMedia_Click(object sender, EventArgs e)
        {
            vPlayer1.Play();
            btnStopMedia.Enabled = true;
            btnPlayMedia.Enabled = false;
        }
    }
}
