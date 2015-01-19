using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VixenModules.Output.CommandController
{
    /// <summary>
    /// Interaction logic for VPlayer.xaml
    /// </summary>
    public partial class VPlayer : UserControl
    {
        public VPlayer()
        {
            InitializeComponent();
            Repeat = true;
            mePlayer.UnloadedBehavior = MediaState.Stop;

        }

        void mePlayer_MediaEnded(object sender, RoutedEventArgs e)
        {


            var endTime = (StopPosition.TotalMilliseconds > 0) ? StopPosition : NaturalDuration.TimeSpan;

            if (Repeat)
            {

                mePlayer.Position = (StartPosition.TotalMilliseconds == 0) ? TimeSpan.FromMilliseconds(1) : StartPosition;
                mePlayer.Play();
            }


            if (MediaEnded != null)
                MediaEnded(sender, e);
        }

        public event EventHandler MediaEnded;

        public void Play()
        {
            mePlayer.MediaEnded -= mePlayer_MediaEnded;
            mePlayer.MediaEnded += mePlayer_MediaEnded;

            this.mePlayer.Play();

        }


        public void Stop()
        {
            this.mePlayer.Stop();
            if (playingStatusTimer != null)
                playingStatusTimer.Stop();
            playingStatusTimer = null;
        }
        public bool Repeat { get; set; }
        public double Volume { get { return this.mePlayer.Volume; } set { this.mePlayer.Volume = value; } }
        public Uri SourceUri { get { return this.mePlayer.Source; } set { this.mePlayer.Source = value; } }
        public TimeSpan StartPosition { get; set; }
        public TimeSpan StopPosition { get; set; }
        public TimeSpan Position { get { return this.mePlayer.Position; } set { this.mePlayer.Position = value; } }
        public double DownloadProgress { get { return this.mePlayer.DownloadProgress; } }
        public bool HasAudio { get { return this.mePlayer.HasAudio; } }
        public bool HasVideo { get { return this.mePlayer.HasVideo; } }
        public Duration NaturalDuration { get { return this.mePlayer.NaturalDuration; } }
        public double SpeedRatio { get { return this.mePlayer.SpeedRatio; } set { this.mePlayer.SpeedRatio = value; } }

        private Timer playingStatusTimer;
    }
}
