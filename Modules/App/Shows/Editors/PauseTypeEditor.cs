﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using Common.Controls.Theme;
using Vixen.Module.Editor;
using Vixen.Module.SequenceType;
using Vixen.Services;
using Vixen.Sys;

namespace VixenModules.App.Shows
{

	public partial class PauseTypeEditor : TypeEditorBase
	{
		public static ShowItem _showItem;
		public static Label ContolLabel1;
		public static NumericUpDown ContolNumericUpDownPauseSeconds;

		public PauseTypeEditor(ShowItem showItem)
		{
			InitializeComponent();
			ForeColor = ThemeColorTable.ForeColor;
			BackColor = ThemeColorTable.BackgroundColor;
			ThemeUpdateControls.UpdateControls(this);
			ContolLabel1 = label1;
			ContolNumericUpDownPauseSeconds = numericUpDownPauseSeconds;
			_showItem = showItem;
		}

		private void SequenceTypeEditor_Load(object sender, EventArgs e)
		{
			numericUpDownPauseSeconds.Value = _showItem.Pause_Seconds;
		}

		private void numericUpDownPauseSeconds_ValueChanged(object sender, EventArgs e)
		{
			_showItem.Pause_Seconds = Convert.ToInt32(numericUpDownPauseSeconds.Value);
			_showItem.Name = "Pause for " + _showItem.Pause_Seconds.ToString() + " seconds";
			FireChanged(_showItem.Name);
		}

	}
}
