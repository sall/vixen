﻿using System;
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
	public partial class PixelGrid : Form, IElementTemplate
	{
		private static Logger Logging = LogManager.GetCurrentClassLogger();

		private string gridname;
		private int rows;
		private int columns;
		private bool rowsfirst;


		public PixelGrid()
		{
			InitializeComponent();

			gridname = "Grid";
			rows = 20;
			columns = 32;
			rowsfirst = true;
		}

		public string TemplateName
		{
			get { return "Pixel Grid"; }
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

			if (gridname.Length == 0) {
				Logging.Error("gridname is null");
				return result;
			}

			if (rows < 0) {
				Logging.Error("negative rows");
				return result;
			}

			if (columns < 0) {
				Logging.Error("negative columns");
				return result;
			}

			ElementNode head = ElementNodeService.Instance.CreateSingle(null, gridname);
			result.Add(head);

			int firstlimit, secondlimit;
			string firstprefix, secondprefix;

			if (rowsfirst) {
				firstlimit = rows;
				secondlimit = columns;
				firstprefix = " R";
				secondprefix = " C";
			} else {
				firstlimit = columns;
				secondlimit = rows;
				firstprefix = " C";
				secondprefix = " R";
			}

			for (int i = 0; i < firstlimit; i++) {
				string firstname = head.Name + firstprefix + (i + 1);
				ElementNode firstnode = ElementNodeService.Instance.CreateSingle(head, firstname);
				result.Add(firstnode);

				for (int j = 0; j < secondlimit; j++) {
					string secondname = firstnode.Name + secondprefix + (j + 1);
					ElementNode secondnode = ElementNodeService.Instance.CreateSingle(firstnode, secondname);
					result.Add(secondnode);
				}
			}

			return result;
		}

		private void PixelGrid_Load(object sender, EventArgs e)
		{
			textBoxName.Text = gridname;
			numericUpDownRows.Value = rows;
			numericUpDownColumns.Value = columns;
			if (rowsfirst)
				radioButtonRowsFirst.Checked = true;
			else
				radioButtonColumnsFirst.Checked = true;
		}

		private void PixelGrid_FormClosed(object sender, FormClosedEventArgs e)
		{
			gridname = textBoxName.Text;
			rows = Decimal.ToInt32(numericUpDownRows.Value);
			columns = Decimal.ToInt32(numericUpDownColumns.Value);
			rowsfirst = radioButtonRowsFirst.Checked;
		}
	}
}
