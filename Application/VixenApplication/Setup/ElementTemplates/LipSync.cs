using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NLog;
using Vixen.Rule;
using Vixen.Services;
using Vixen.Sys;
using Vixen.Module.App;

namespace VixenApplication.Setup.ElementTemplates
{
    public partial class LipSync : Form, IElementTemplate
    {
        private static Logger Logging = LogManager.GetCurrentClassLogger();
//        private LipSyncMapLibrary _library = null;

        private string treename;
        private int stringcount;

        public LipSync()
        {
            InitializeComponent();
//            _library = ApplicationServices.Get<IAppModuleInstance>(LipSyncMapDescriptor.ModuleID) as LipSyncMapLibrary;
            treename = "LipSync";
            stringcount = 8;
        }

        public string TemplateName
        {
            get { return "LipSync"; }
        }

        public bool SetupTemplate(IEnumerable<ElementNode> selectedNodes = null)
        {
            DialogResult result = ShowDialog();

            if (result == DialogResult.OK)
                return true;

            return false;
        }

        public IEnumerable<ElementNode> GenerateElements(IEnumerable<ElementNode> selectedNodes = null)
        {
            List<ElementNode> result = new List<ElementNode>();

            if (treename.Length == 0)
            {
                Logging.Error("starburst is null");
                return result;
            }

            if (stringcount < 0)
            {
                Logging.Error("negative count");
                return result;
            }

            ElementNode head = ElementNodeService.Instance.CreateSingle(null, treename);
            result.Add(head);

            List<string> stringNames = new List<string>();

            for (int i = 0; i < stringcount; i++)
            {
                string stringname = head.Name + " S" + (i + 1);
                ElementNode stringnode = ElementNodeService.Instance.CreateSingle(head, stringname);
                result.Add(stringnode);
                stringNames.Add(stringname);
            }

            if (generateMapCheckBox.Checked)
            {
//                _library.AddMapping(true, treename, new LipSyncMapData(stringNames));
            }

            return result;
        }

        private void LipSync_Load(object sender, EventArgs e)
        {
            textBoxTreeName.Text = treename;
            numericUpDownStrings.Value = stringcount;
        }

        private void LipSync_FormClosed(object sender, FormClosedEventArgs e)
        {
            treename = textBoxTreeName.Text;
            stringcount = Decimal.ToInt32(numericUpDownStrings.Value);
        }
    }
}
