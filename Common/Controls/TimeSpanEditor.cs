using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;


namespace Common.Controls
{
    public class TimeSpanEditor : MaskedTextBox
    {
        private const string timeFormat = @"mm\:ss\.fff";
        private System.Windows.Forms.ToolTip toolTip;

        public TimeSpanEditor()
            : base()
        {
            TimeSpan = new TimeSpan();
            this.Mask = @"00:00.000";
            this.MaskInputRejected += TimeSpanEditor_MaskInputRejected;
            this.KeyDown += TimeSpanEditor_KeyDown;
            this.KeyUp += TimeSpanEditor_KeyUp;
            toolTip = new ToolTip();

        }

        void TimeSpanEditor_KeyUp(object sender, KeyEventArgs e)
        {
            TimeSpan start;
            if (TimeSpan.TryParseExact(this.Text, timeFormat, CultureInfo.InvariantCulture, out start))
            {
                this.Valid = true;
            }
            else
            {
                this.Valid = false;
            }
        }

        void TimeSpanEditor_KeyDown(object sender, KeyEventArgs e)
        {
            toolTip.Hide(this);
        }

        void TimeSpanEditor_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            if (this.MaskFull)
            {
                toolTip.ToolTipTitle = "Invalid Time";
                toolTip.Show("You cannot enter any more data into the time field.", this, this.Top, this.Right, 5000);
            }
            else if (e.Position == this.Mask.Length)
            {
                toolTip.ToolTipTitle = "Invalid Time";
                toolTip.Show("You cannot add extra number to the end of this time.", this, this.Top, this.Right, 5000);
            }
            else
            {
                toolTip.ToolTipTitle = "Invalid Time";
                toolTip.Show("You can only add numeric characters (0-9) into this time field.", this, this.Top, this.Right, 5000);
            }
        }


        public TimeSpan TimeSpan
        {
            get
            {
                TimeSpan duration;
                TimeSpan.TryParseExact(this.Text, timeFormat, CultureInfo.InvariantCulture, out duration);
                return duration;
            }
            set { this.Text = value.ToString(timeFormat); }
        }
        public bool Valid { get; private set; }
    }
}
