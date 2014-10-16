﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Controls;
using Common.Resources;
using Common.Resources.Properties;

namespace VixenModules.Property.Color
{
	public partial class ColorSetsSetupForm : Form
	{
		private ColorStaticData _data;

		public ColorSetsSetupForm(ColorStaticData colorStaticData)
		{
			_data = colorStaticData;
			InitializeComponent();
			Icon = Resources.Icon_Vixen3;
			buttonAddColor.Image = Tools.GetIcon(Resources.add, 16);
			buttonAddColor.Text = "";
			buttonAddColorSet.Image = Tools.GetIcon(Resources.add, 16);
			buttonAddColorSet.Text = "";
			buttonRemoveColorSet.Image = Tools.GetIcon(Resources.delete, 16);
			buttonRemoveColorSet.Text = "";
		}

		private void ColorSetsSetupForm_Load(object sender, EventArgs e)
		{
			UpdateColorSetsList();
			UpdateGroupBoxWithColorSet(null, null);
		}

		private void UpdateColorSetsList()
		{
			listViewColorSets.BeginUpdate();
			listViewColorSets.Items.Clear();
			foreach (string colorSetName in _data.GetColorSetNames()) {
				listViewColorSets.Items.Add(colorSetName);
			}
			listViewColorSets.EndUpdate();
		}

		private void UpdateGroupBoxWithColorSet(string name, ColorSet cs)
		{
			if (cs == null) {
				groupBoxColorSet.Enabled = false;
				textBoxName.Text = string.Empty;
				tableLayoutPanelColors.Controls.Clear();
				return;
			}

			groupBoxColorSet.Enabled = true;
			textBoxName.Text = name;

			tableLayoutPanelColors.Controls.Clear();

			foreach (System.Drawing.Color color in cs) {
				ColorPanel colorPanel = new ColorPanel(color);
				tableLayoutPanelColors.Controls.Add(colorPanel);
			}
		}

		private void listViewColorSets_SelectedIndexChanged(object sender, EventArgs e)
		{
			bool selected = listViewColorSets.SelectedItems.Count > 0;

			if (selected) {
				string name = listViewColorSets.SelectedItems[0].Text;
				UpdateGroupBoxWithColorSet(name, _data.GetColorSet(name));
			}
			else {
				UpdateGroupBoxWithColorSet(null, null);
			}

			buttonRemoveColorSet.Enabled = selected;
		}

		private void buttonAddColorSet_Click(object sender, EventArgs e)
		{
			using (TextDialog textDialog = new TextDialog("New Color Set name?", "New Color Set")) {
				if (textDialog.ShowDialog() == DialogResult.OK) {
					string newName = textDialog.Response;

					if (_data.ContainsColorSet(newName)) {
						MessageBox.Show("Color Set already exists.");
						return;
					}

					ColorSet newcs = new ColorSet();
					_data.SetColorSet(newName, newcs);
					UpdateGroupBoxWithColorSet(newName, newcs);
					UpdateColorSetsList();
				}
			}
		}

		private void buttonRemoveColorSet_Click(object sender, EventArgs e)
		{
			if (listViewColorSets.SelectedItems.Count > 0) {
				string item = listViewColorSets.SelectedItems[0].Text;
				if (!_data.RemoveColorSet(item)) {
					MessageBox.Show("Error removing Color Set!");
				}
			}
			UpdateColorSetsList();
			UpdateGroupBoxWithColorSet(null, null);
		}

		private void buttonUpdate_Click(object sender, EventArgs e)
		{
			if (SaveDisplayedColorSet())
				UpdateColorSetsList();
		}

		private void buttonAddColor_Click(object sender, EventArgs e)
		{
			ColorPanel colorPanel = new ColorPanel(System.Drawing.Color.White);
			tableLayoutPanelColors.Controls.Add(colorPanel);
		}

		private void textBoxName_TextChanged(object sender, EventArgs e)
		{
			if (textBoxName.Text.Length <= 0)
				return;

			if (_data.ContainsColorSet(textBoxName.Text)) {
				buttonUpdate.Text = "Update Color Set";
			}
			else {
				buttonUpdate.Text = "Make New Color Set";
			}
		}

		private bool displayedColorSetHasDifferences()
		{
			if (!groupBoxColorSet.Enabled)
				return false;

			if (_data.ContainsColorSet(textBoxName.Text)) {
				ColorSet cs = _data.GetColorSet(textBoxName.Text);

				int i = 0;
				foreach (var control in tableLayoutPanelColors.Controls) {
					if (cs.Count <= i)
						return true;

					ColorPanel cp = (ColorPanel)control;
					if (cs[i].ToArgb() != cp.Color.ToArgb())
						return true;

					i++;
				}

				if (cs.Count != i)
					return true;

				return false;
			}

			return true;
		}

		private bool SaveDisplayedColorSet()
		{
			string name = textBoxName.Text;
			ColorSet newColorSet = new ColorSet();

			if (name.Length <= 0) {
				MessageBox.Show("You must enter a name for the Color Set.", "Name Requred", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}

			foreach (var control in tableLayoutPanelColors.Controls) {
				ColorPanel cp = (ColorPanel)control;
				newColorSet.Add(cp.Color);
			}

			_data.SetColorSet(textBoxName.Text, newColorSet);

			return true;
		}

		private void ColorSetsSetupForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (displayedColorSetHasDifferences()) {
				DialogResult dr = MessageBox.Show("Do you want to save changes to the displayed color set?", "Save Changes?",
				                                  MessageBoxButtons.YesNoCancel);

				switch (dr) {
					case DialogResult.Yes:
						if (!SaveDisplayedColorSet()) {
							e.Cancel = true;
						}
						break;

					case DialogResult.No:
						break;

					case DialogResult.Cancel:
						e.Cancel = true;
						break;
				}
			}
		}
	}
}