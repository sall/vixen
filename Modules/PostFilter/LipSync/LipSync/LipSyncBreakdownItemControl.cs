using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Vixen.Data.Value;

namespace VixenModules.OutputFilter.LipSync
{
    public partial class LipSyncBreakdownItemControl : UserControl
    {
        public LipSyncBreakdownItemControl()
        {
            InitializeComponent();
            elementNameTB.Text = "New String";
            phonemeCheckListBox.Items.Clear();
            phonemeCheckListBox.Items.Add("AI");
            phonemeCheckListBox.Items.Add("E");
            phonemeCheckListBox.Items.Add("O");
            phonemeCheckListBox.Items.Add("U");
            phonemeCheckListBox.Items.Add("FV");
            phonemeCheckListBox.Items.Add("L");
            phonemeCheckListBox.Items.Add("MBP");
            phonemeCheckListBox.Items.Add("WQ");
            phonemeCheckListBox.Items.Add("etc");
            phonemeCheckListBox.Items.Add("Rest");
        }

        public LipSyncBreakdownItemControl(LipSyncBreakdownItem breakdownItem)
		{
			InitializeComponent();
            elementNameTB.Text = breakdownItem.Name;

            phonemeCheckListBox.Items.Clear();
            List<LipSyncBreakdownItemPhoneme> phonemeList = breakdownItem.PhonemeList;
            foreach(LipSyncBreakdownItemPhoneme ip in phonemeList)
            {
                phonemeCheckListBox.Items.Add(ip.phonemeName,ip.isChecked);
            }
		}

        public LipSyncBreakdownItem LipSyncBreakdownItem
        {
            get
            {
                LipSyncBreakdownItem result = new LipSyncBreakdownItem();
                return result;
            }
        }

        public event EventHandler DeleteRequested;

        private void buttonDelete_Click(object sender, EventArgs e)
		{
			if (DeleteRequested != null)
				DeleteRequested(this, EventArgs.Empty);
        }
    }
}
