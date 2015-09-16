﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Controls.Theme;

namespace VixenModules.EffectEditor.NutcrackerEffectEditor
{
	public partial class NutcrackerProcessingMovie : Form
	{
		public NutcrackerProcessingMovie()
		{
			InitializeComponent();
			ForeColor = ThemeColorTable.ForeColor;
			BackColor = ThemeColorTable.BackgroundColor;
			ThemeUpdateControls.UpdateControls(this);
		}
	}
}