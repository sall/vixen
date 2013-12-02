using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VixenModules.Output.ConductorOutput
{
   
    public partial class ConductorSetupForm : Form
    {
        ConductorModuleData _savedata;

        public ConductorSetupForm(ConductorModuleData savedata)
        {
            InitializeComponent();
            _savedata = savedata;
        }

        private void ConductorSetupForm_Load(object sender, EventArgs e)
        {
            cmcb.Checked = _savedata.savedata;
			cmcb2.Checked = _savedata.OutputDebug;
        }

        public Boolean SaveData
        {
            get { return this.cmcb.Checked; }
        }

		public Boolean OutputDebug
		{
			get { return this.cmcb2.Checked; }
		}

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

    }
}
