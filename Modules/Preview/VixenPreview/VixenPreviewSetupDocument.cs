﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace VixenModules.Preview.VixenPreview
{
	public partial class VixenPreviewSetupDocument : DockContent
	{
		public VixenPreviewSetupDocument()
		{
			InitializeComponent();
		}

		public VixenPreviewControl Preview
		{
			get { return previewControl; }
		}

		private void timerRender_Tick(object sender, EventArgs e)
		{
			timerRender.Stop();
			Preview.RenderInForeground();
			timerRender.Start();
		}

		private void VixenPreviewSetupDocument_Load(object sender, EventArgs e)
		{
			//vScroll.Left = ClientSize.Width - vScroll.Width;
			//hScroll.Top = ClientSize.Height - hScroll.Height;
			//previewControl.Width = vScroll.Left - 1;
			//previewControl.Height = hScroll.Top - 1;
			previewControl.EditMode = true;
		}

		private void VixenPreviewSetupDocument_FormClosing(object sender, FormClosingEventArgs e)
		{
			timerRender.Stop();
		}
	}
}