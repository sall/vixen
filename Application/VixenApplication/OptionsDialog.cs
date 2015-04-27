using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VixenApplication
{
	public partial class OptionsDialog : Form
	{
		public OptionsDialog()
		{
			InitializeComponent();
		}

		private void OptionsDialog_Load(object sender, EventArgs e)
		{
			ctlUpdateInteral.Value = Vixen.Sys.VixenSystem.DefaultUpdateInterval;
			ctlChkAutoUpdateFalcon.Checked = Vixen.Sys.VixenSystem.FalconPiPlayerAutomaticUpdate;
			ctlIpFalcon.Text = Vixen.Sys.VixenSystem.FalconPiPlayerIpAddress;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			Vixen.Sys.VixenSystem.DefaultUpdateInterval = (int)ctlUpdateInteral.Value;
			Vixen.Sys.VixenSystem.FalconPiPlayerIpAddress = ctlIpFalcon.Text;
			Vixen.Sys.VixenSystem.FalconPiPlayerAutomaticUpdate = ctlChkAutoUpdateFalcon.Checked;
		}

	}
}
