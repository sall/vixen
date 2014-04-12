using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VixenModules.Preview.VixenPreview.Shapes
{
    public partial class PreviewLipSyncControl : DisplayItemBaseControl
    {
		public PreviewLipSyncControl(PreviewBitmapShape shape) : base(shape)
		{
			InitializeComponent();
			propertyGrid1.SelectedObject = Shape;
			Shape.OnPropertiesChanged += OnPropertiesChanged;
		}

        ~PreviewLipSyncControl()
		{
			Shape.OnPropertiesChanged -= OnPropertiesChanged;
		}

        private void OnPropertiesChanged(object sender, PreviewBaseShape shape)
		{
			propertyGrid1.Refresh();
		}

		private void buttonHelp_Click(object sender, EventArgs e)
		{
			Common.VixenHelp.VixenHelp.ShowHelp(Common.VixenHelp.VixenHelp.HelpStrings.Preview_BasicShapes);
		}
    }
}
