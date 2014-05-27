using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Controls;
using Common.Controls.ColorManagement.ColorModels;
using Common.Controls.ColorManagement.ColorPicker;
using Vixen.Module.Effect;
using VixenModules.Property.Color;
using Vixen.Sys;

namespace VixenModules.App.LipSyncMap
{
    public partial class LipSyncMapColorSelect : Form
    {
   		private bool _discreteColors;
		private IEnumerable<Color> _validDiscreteColors;

        public LipSyncMapColorSelect()
        {
            InitializeComponent();
            intensityUpDown.Items.AddRange(Enumerable.Range(0, 101).ToArray());
        }


        public List<ElementNode> ChosenNodes
		{
			//get { return _targetEffect; }
			set
			{
                List<ElementNode> nodeList = value;
				_discreteColors = false;
				if (value == null) return;

				HashSet<Color> validColors = new HashSet<Color>();

				// look for the color property of the target effect element, and restrict the gradient.
				// If it's a group, iterate through all children (and their children, etc.), finding as many color
				// properties as possible; then we can decide what to do based on that.
				validColors.AddRange(nodeList.SelectMany(x => ColorModule.getValidColorsForElementNode(x, true)));
				_discreteColors = validColors.Any();
				_validDiscreteColors = validColors;
			}
		}

		private HSV _color;

		public Color ColorValue
		{
			get { return _color.ToRGB().ToArgb(); }
			set
			{
                RGB tempVal = new RGB(value);
                _color = HSV.FromRGB(tempVal);
                intensityTrackBar.Value = (int)(Intensity * 100);
				panelColor.BackColor = value;
			}
		}

        public double Intensity
        {
            get
            {
                return _color.V;
            }

            set
            {
                _color.V = value;
                panelColor.BackColor = ColorValue;
                intensityTrackBar.Value = (int)(Intensity * 100);
            }
        }

        private void panelColor_Click(object sender, EventArgs e)
		{
            if (_discreteColors) 
            {
				using (DiscreteColorPicker dcp = new DiscreteColorPicker()) 
                {
					dcp.ValidColors = _validDiscreteColors;
					dcp.SingleColorOnly = true;
					dcp.SelectedColors = new List<Color> {ColorValue};
					DialogResult result = dcp.ShowDialog();
					if (result == DialogResult.OK) {
						if (dcp.SelectedColors.Count() == 0) {
							ColorValue = Color.White;
						}
						else {
                            _color = HSV.FromRGB(dcp.SelectedColors.First());
						}
					}
				}
			}
			else {
				using (ColorPicker cp = new ColorPicker()) {
					cp.LockValue_V = true;
					cp.Color = XYZ.FromRGB(ColorValue);
					DialogResult result = cp.ShowDialog();
					if (result == DialogResult.OK) 
                    {
					    _color = HSV.FromRGB(cp.Color.ToRGB());
					}
				}
			}
            panelColor.BackColor = ColorValue;
		}

        private void intensityTrackBar_ValueChanged(object sender, EventArgs e)
        {
            intensityUpDown.SelectedIndex = intensityTrackBar.Value;
            Intensity = intensityUpDown.SelectedIndex / 100.0;
        }

        private void intensityUpDown_SelectedItemChanged(object sender, EventArgs e)
        {
            intensityTrackBar.Value = this.intensityUpDown.SelectedIndex;
            Intensity = intensityUpDown.SelectedIndex / 100.0;
        }

	}
}
