using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using VixenModules.OutputFilter.LipSync;

namespace VixenModules.OutputFilter.LipSync
{
    public partial class LipSyncBreakdownSetup : Form
    {
        private readonly LipSyncBreakdownData _data;
        private static NLog.Logger Logging = NLog.LogManager.GetCurrentClassLogger();
        public LipSyncBreakdownSetup(LipSyncBreakdownData breakdownData)
        {
            InitializeComponent();
            _data = breakdownData;
        }

        public List<LipSyncBreakdownItem> BreakdownItems
        {
            get
            {
                return
                    tableLayoutPanelControls.Controls.OfType<LipSyncBreakdownItemControl>().Select(
                        itemControl => itemControl.LipSyncBreakdownItem).ToList();
            }
        }

        private void LipSyncBreakdownSetup_Load(object sender, EventArgs e)
        {
            tableLayoutPanelControls.Controls.Clear();

            foreach (LipSyncBreakdownItem breakdownItem in _data.BreakdownItems)
            {
                addControl(new LipSyncBreakdownItemControl(breakdownItem));
            }

            // let's just make up some hardcoded templates. Can expand on this later; probably don't need to,
            // people can request new ones and stuff if they want.
            comboBoxTemplates.Items.Clear();
            comboBoxTemplates.Items.Add("HolidayCoro");
            comboBoxTemplates.SelectedIndex = 0;
        }

        private void buttonAddString_Click(object sender, EventArgs e)
        {
            addControl(new LipSyncBreakdownItemControl());
        }

        private void control_DeleteRequested(object sender, EventArgs e)
        {
            LipSyncBreakdownItemControl control = sender as LipSyncBreakdownItemControl;
            if (control == null)
                return;

            removeControl(control);
        }

        private void removeControl(LipSyncBreakdownItemControl control)
        {
            if (!tableLayoutPanelControls.Controls.Contains(control))
                return;

            tableLayoutPanelControls.Controls.Remove(control);
            control.DeleteRequested -= control_DeleteRequested;
        }

        private void addControl(LipSyncBreakdownItemControl control)
        {
            control.DeleteRequested += control_DeleteRequested;
            tableLayoutPanelControls.Controls.Add(control);
        }

        private void buttonApplyTemplate_Click(object sender, EventArgs e)
        {
            foreach (LipSyncBreakdownItemControl control in tableLayoutPanelControls.Controls.OfType<LipSyncBreakdownItemControl>())
            {
				removeControl(control);
			}

			tableLayoutPanelControls.Controls.Clear();

			string template = comboBoxTemplates.SelectedItem.ToString();
			switch (template) 
            {
				case "HolidayCoro":
                    addControl(new LipSyncBreakdownItemControl(Color.Red, "Red"));

					break;

				default:
					Logging.Error("Color Breakdown Setup: got an unknown template to apply: " + template);
					MessageBox.Show("Error applying template: Unknown template.");
					break;
			}
		}
    }
}