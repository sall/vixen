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

namespace VixenApplication.Setup.ElementTemplates
{
	public partial class LipSync : Form, IElementTemplate
	{
		private static Logger Logging = LogManager.GetCurrentClassLogger();

		private string lipsync_name;
		private int lipsync_stringcount;
		private bool lipsync_hasPixels;
		private int lipsync_pixelsperstring; 

        public LipSync()
        {
            InitializeComponent();
            lipsync_name = "LipSync";
            lipsync_stringcount = 8;
            lipsync_hasPixels = false;
            lipsync_pixelsperstring = 50;
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

            if (lipsync_name.Length == 0)
            {
                Logging.Error("lipsync_treename is null");
                return result;
            }

            if (lipsync_stringcount < 0)
            {
                Logging.Error("lipsync_stringcount negative count");
                return result;
            }

            if (lipsync_hasPixels && lipsync_pixelsperstring < 0)
            {
                Logging.Error("negative pixelsperstring");
                return result;
            }

            ElementNode head = ElementNodeService.Instance.CreateSingle(null, lipsync_name);
            result.Add(head);

            for (int i = 0; i < lipsync_stringcount; i++)
            {
                string stringname = head.Name + " S" + (i + 1);
                ElementNode stringnode = ElementNodeService.Instance.CreateSingle(head, stringname);
                result.Add(stringnode);

                if (lipsync_hasPixels)
                {
                    for (int j = 0; j < lipsync_pixelsperstring; j++)
                    {
                        string pixelname = stringnode.Name + " Px" + (j + 1);

                        ElementNode pixelnode = ElementNodeService.Instance.CreateSingle(stringnode, pixelname);
                        result.Add(pixelnode);
                    }
                }
            }

            return result;
        }

        private void lipsyncpixels_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            lipsyncpixelstring_updown.Enabled =  lipsyncpixels_checkbox.Checked;
        }

        private void LipSync_Load(object sender, EventArgs e)
        {
            lipsyncname_textbox.Text = lipsync_name;
            lipsyncstringcount_updown.Value = lipsync_stringcount;
            lipsyncpixels_checkbox.Checked = lipsync_hasPixels;
            lipsyncpixelstring_updown.Value = lipsync_pixelsperstring;
        }

        private void LipSync_FormClosed(object sender, FormClosedEventArgs e)
        {
            lipsync_name = lipsyncname_textbox.Text;
            lipsync_stringcount = Decimal.ToInt32(lipsyncstringcount_updown.Value);
            lipsync_hasPixels = lipsyncpixels_checkbox.Checked;
            lipsync_pixelsperstring = Decimal.ToInt32(lipsyncpixelstring_updown.Value);
        }
    }
}
