using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace VixenModules.Output.CommandController
{
    public partial class VideoPlayer : Form
    {
         
        public VideoPlayer(Uri mediaFile)
        {

            InitializeComponent();
            this.vPlayer1.SourceUri = mediaFile;
            this.vPlayer1.MediaEnded += vPlayer1_MediaEnded;
            this.WindowState = FormWindowState.Normal;
            this.vPlayer1.Loaded += vPlayer1_Loaded;


        }

        void vPlayer1_MediaEnded(object sender, EventArgs e)
        {
            this.Close();
            if (MediaEnded != null)
                MediaEnded(sender, e);

        }
        public Duration VideoDuration { get { return this.vPlayer1.NaturalDuration; } }
        public event EventHandler MediaEnded;
        public event EventHandler MediaLoaded;
        public void SetSourceUri(Uri value)
        {
            if (this.InvokeRequired)
            {

                this.BeginInvoke(new Action<Uri>(SetSourceUri), new object[] { value });

                return;
            }
            Loaded = false;
            this.vPlayer1.SourceUri = value;
        }
        public bool Loaded { get; private set; }
        void vPlayer1_Loaded(object sender, RoutedEventArgs e)
        {
            if (MediaLoaded != null)
            {
                MediaLoaded(sender, e);
            }
            Loaded = true;
            this.Show();
        }

        public void SetFullScreen(bool value)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<bool>(SetFullScreen), new object[] { value });
                return;
            }
            this.WindowState = value ? FormWindowState.Maximized : FormWindowState.Normal;
            this.FormBorderStyle = value ? FormBorderStyle.None : System.Windows.Forms.FormBorderStyle.SizableToolWindow;
        }

        public void SetVolume(double value)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<double>(SetVolume), new object[] { value });
                return;
            }
            this.vPlayer1.Volume = value;
        }
        public void SetSpeed(double value)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<double>(SetSpeed), new object[] { value });
                return;
            }
            this.vPlayer1.SpeedRatio = value;
        }
        public void SetRepeat(bool value)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<bool>(SetRepeat), new object[] { value });
                return;
            }
            this.vPlayer1.Repeat = value;
        }
        public void SetPosition(TimeSpan value)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<TimeSpan>(SetPosition), new object[] { value });
                return;
            }
            this.vPlayer1.Position = value;
        }
        public void SetStartPosition(TimeSpan value)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<TimeSpan>(SetStartPosition), new object[] { value });
                return;
            }
            this.vPlayer1.StartPosition = value;
        }
        public void SetStopPosition(TimeSpan value)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<TimeSpan>(SetStopPosition), new object[] { value });
                return;
            }
            this.vPlayer1.StopPosition = value;
        }
      
        public void Play()
        {
            if (this.InvokeRequired)
            {

                this.BeginInvoke(new Action(Play));
                return;
            }
            Stopwatch w = Stopwatch.StartNew();
            
            this.vPlayer1.Play();

        }

        public void Stop()
        {
            if (this.InvokeRequired)
            {

                this.BeginInvoke(new Action(Stop));
                return;
            }
            this.vPlayer1.Stop();
        }
        public void SetScreen(Screen screen)
        {
            this.Location = screen.WorkingArea.Location;
        }
    }
}
