using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VixenModules.OutputFilter.LipSyncBreakdown
{
    public partial class LipSyncBreakdownAddPhoneme : Form
    {
        public LipSyncBreakdownAddPhoneme()
        {
            InitializeComponent();
        }

        public string PhonemeName
        {
            get
            {
                return phoneMeName.Text;
            }
            
            set
            {
                phoneMeName.Text = value;
            }
        }
    }
}
