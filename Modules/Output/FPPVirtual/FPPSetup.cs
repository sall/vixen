using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VixenModules.Output.FPPVirtual
{
    public partial class FPPSetup : Form
    {
        public FPPSetup()
        {
            InitializeComponent();
        }

        private void FPPSetup_Load(object sender, EventArgs e)
        {
            eventTimingComboBox.SelectedIndex = 0;
        }

        public Byte EventTiming
        {
            get
            {
                return Convert.ToByte(eventTimingComboBox.SelectedItem.ToString());
            }
            set
            {
                int index = eventTimingComboBox.Items.IndexOf(value);
                if (index != -1)
                {
                    eventTimingComboBox.SelectedIndex = index;
                }
            }
        }
    }
}
