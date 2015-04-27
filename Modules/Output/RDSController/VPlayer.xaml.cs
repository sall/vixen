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
            SetRepeat(true);
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

            Dispatcher.Invoke((Action)(() =>
            {
                mePlayer.MediaEnded -= mePlayer_MediaEnded;
                mePlayer.MediaEnded += mePlayer_MediaEnded;

                this.mePlayer.Play();
            }));

        }


        public void Stop()
        {
            this.Dispatcher.Invoke((Action)(() =>
                {
                    this.mePlayer.Stop();
                    if (playingStatusTimer != null)
                        playingStatusTimer.Stop();
                    playingStatusTimer = null;
                }));

        }
        bool _repeat;
        public void SetRepeat(bool value)
        {
            this.Dispatcher.Invoke((Action)(() =>
            {

                _repeat = value;
            }));
        }

        public bool Repeat
        {
            get { return _repeat; }

        }
        public void SetVolume(double value)
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                this.mePlayer.Volume = value;
            }));
        }

        public double Volume
        {
            get { return this.mePlayer.Volume; }

        }
        public void SetSourceUri(Uri value)
        {
            this.Dispatcher.Invoke((Action)(() =>
                         {

                             this.mePlayer.Source = value;
                         }));
        }
        public Uri SourceUri
        {
            get { return this.mePlayer.Source; }

        }
        public TimeSpan StartPosition { get; set; }
        public TimeSpan StopPosition { get; set; }
        public void SetPosition(TimeSpan value)
        {
            this.Dispatcher.Invoke((Action)(() =>
            {

                this.mePlayer.Position = value;
            }));
        }
        public TimeSpan Position
        {
            get { return this.mePlayer.Position; }

        }
        public double DownloadProgress
        {
            get
            {
                return this.mePlayer.DownloadProgress;
            }
        }

        public bool HasAudio { get { return this.mePlayer.HasAudio; } }
        public bool HasVideo { get { return this.mePlayer.HasVideo; } }
        public Duration NaturalDuration { get { return this.mePlayer.NaturalDuration; } }

        public void SetSpeedRatio(double value)
        {
            this.Dispatcher.Invoke((Action)(() =>
            {

                this.mePlayer.SpeedRatio = value;
            }));
        }
        public double SpeedRatio
        {
            get { return this.mePlayer.SpeedRatio; }

        }

        private Timer playingStatusTimer;
    }
}
