﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Common.Resources;
using Common.Resources.Properties;

namespace VixenModules.App.Shows
{
	public partial class ShowEditorForm : Form
	{
		ShowItemType currentShowItemType;

		public ShowEditorForm(Show show)
		{
			InitializeComponent();

			buttonAddItem.Image = Tools.GetIcon(Resources.add, 16);
			buttonAddItem.Text = "";
			buttonDeleteItem.Image = Tools.GetIcon(Resources.delete, 16);
			buttonDeleteItem.Text = "";
			buttonHelp.Image = Tools.GetIcon(Resources.help, 16);

			ShowData = show;
		}

		private void ShowEditorForm_Load(object sender, EventArgs e)
		{
			currentShowItemType = ShowItemType.Startup;
			PopulateActions();
			PopulateItemList(null);
			textBoxShowName.Text = ShowData.Name;
			LoadCurrentTab();
			CheckButtons();

			//tabControlShowItems.TabPages.Remove(tabPageBackground);
			tabControlShowItems.TabPages.Remove(tabPageInput);
		}

		public Show ShowData { get; set; }

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Are you sure you want to cancel? Any changes made to the show setup will be lost.", "Cancel Show setup changes", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
			{
				DialogResult = System.Windows.Forms.DialogResult.No;
			}
		}

		private void buttonHelp_Click(object sender, EventArgs e)
		{
			Common.VixenHelp.VixenHelp.ShowHelp(Common.VixenHelp.VixenHelp.HelpStrings.Show_Editor);
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			ShowData.Name = textBoxShowName.Text;
			UpdateListViewItems();
			DialogResult = System.Windows.Forms.DialogResult.OK;
			Close();
		}

		public void PopulateItemList(ShowItem selectedItem)
		{
			listViewShowItems.Items.Clear();

			ShowData.Items.Sort((item1, item2) => item1.ItemOrder.CompareTo(item2.ItemOrder));

			foreach (ShowItem item in ShowData.Items)
			{
				if (item.ItemType == currentShowItemType)
				{
					AddItemToList(item);
				}
			}
		}

		public ListViewItem AddItemToList(ShowItem item)
		{
			ListViewItem lvItem = new ListViewItem(item.Name);
			lvItem.Tag = item;
			return listViewShowItems.Items.Add(lvItem);
		}

		private void listViewStartup_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listViewShowItems.SelectedItems.Count > 0)
			{
				// There can be only one!
				ListViewItem lvItem = listViewShowItems.SelectedItems[0];
				ShowItem item = lvItem.Tag as ShowItem;
			}
		}

		private ActionType ActionStringToAction(string actionString)
		{
			foreach (ActionType action in Enum.GetValues(typeof(ActionType)))
			{
				if (actionString == action.ToString())
					return action;
			}
			return ActionType.Sequence;
		}

		TypeEditorBase currentEditor = null;
		private void SetCurrentEditor(string action)
		{
			if (action == "")
			{
				if (currentEditor != null)
				{
					groupBoxItemEdit.Controls.Remove(currentEditor);
				}
			}
			else
			{
				if (currentEditor != null)
				{
					groupBoxItemEdit.Controls.Remove(currentEditor);
				}
				if (SelectedShowItem != null)
				{
					SelectedShowItem.Action = ActionStringToAction(action);
					currentEditor = SelectedShowItem.Editor;
					currentEditor.Location = new Point(4, 16);
					currentEditor.Width = groupBoxItemEdit.Width - (currentEditor.Left * 2);
					currentEditor.OnTextChanged += OnTextChanged;
					groupBoxItemEdit.Controls.Add(currentEditor);
				}
				else
				{
					MessageBox.Show("SetCurrentEditor: SelectedShowItem == null");
				}
			}
		}

		public void OnTextChanged(object sender, EventArgs e)
		{
			TypeEditorBase editor = sender as TypeEditorBase;
			listViewShowItems.SelectedItems[0].SubItems[0].Text = editor.Text;
		}

		private void PopulateActions()
		{
			foreach (ActionType action in Enum.GetValues(typeof(ActionType)))
			{
				comboBoxActions.Items.Add(action.ToString());
			}
		}

		private ShowItem SelectedShowItem
		{
			get
			{
				ShowItem item = null;
				if (listViewShowItems != null)
				{
					if (listViewShowItems.SelectedItems.Count > 0)
					{
						item = listViewShowItems.SelectedItems[0].Tag as ShowItem;
					}
				}
				else
				{
					//MessageBox.Show("SelectedShowItem: currentListView == null");
				}
				return item;
			}
		}

		private void comboBoxActions_SelectedIndexChanged(object sender, EventArgs e)
		{
			SetCurrentEditor(comboBoxActions.SelectedItem.ToString());
		}

		private void tabControlShowItems_SelectedIndexChanged(object sender, EventArgs e)
		{
			LoadCurrentTab();
		}

		private void LoadCurrentTab()
		{
			if (tabControlShowItems.SelectedTab == tabPageStartup)
			{
				currentShowItemType = ShowItemType.Startup;
			}
			else if (tabControlShowItems.SelectedTab == tabPageBackground)
			{
				currentShowItemType = ShowItemType.Background;
			}
			else if (tabControlShowItems.SelectedTab == tabPageSequential)
			{
				currentShowItemType = ShowItemType.Sequential;
			}
			else if (tabControlShowItems.SelectedTab == tabPageInput)
			{
				currentShowItemType = ShowItemType.Input;
			}
			else if (tabControlShowItems.SelectedTab == tabPageShutdown)
			{
				currentShowItemType = ShowItemType.Shutdown;
			}

			SetHelpLabel();
			LoadSelectedItem();
			SetCurrentEditor("");
			PopulateItemList(null);
			CheckButtons();
		}

		private void CheckButtons()
		{
			buttonDeleteItem.Enabled = (listViewShowItems.SelectedItems.Count > 0);
			groupBoxItemEdit.Enabled = (listViewShowItems.SelectedItems.Count > 0);
			groupBoxAction.Enabled = (listViewShowItems.SelectedItems.Count > 0);
		}

		private void SetHelpLabel()
		{
			if (tabControlShowItems.SelectedTab != null)
				labelHelp.Text = tabControlShowItems.SelectedTab.Tag as String;
		}

		private void UpdateListViewItems()
		{
			foreach (ListViewItem lvItem in listViewShowItems.Items)
			{
				ShowItem item = lvItem.Tag as ShowItem;
				item.ItemOrder = lvItem.Index;
			}
		}

		private void buttonAddItem_Click(object sender, EventArgs e)
		{
			ShowItem item = ShowData.AddItem(currentShowItemType, "New Item");
			item.ItemType = currentShowItemType;
			item.Action = ActionType.Sequence;
			ListViewItem lvItem = AddItemToList(item);
			lvItem.Selected = true;
			UpdateListViewItems();
		}

		private void buttonDeleteItem_Click(object sender, EventArgs e)
		{
			if (SelectedShowItem != null)
			{
				int index = SelectedShowItem.ItemOrder;
				ShowData.DeleteItem(SelectedShowItem);
				listViewShowItems.Items.RemoveAt(listViewShowItems.SelectedIndices[0]);

				CheckButtons();
				UpdateListViewItems();
				if (index > 0)
				{
					this.listViewShowItems.Items[index - 1].Selected = true;
				}
				else
				{
					if (listViewShowItems.Items.Count != index)
					{
						this.listViewShowItems.Items[index].Selected = true;
					}
				}
			}
		}

		private void listViewShowItems_AfterLabelEdit(object sender, LabelEditEventArgs e)
		{
			if (e.Label != null && e.Label.Length > 0)
			{
				(listViewShowItems.Items[e.Item].Tag as ShowItem).Name = e.Label;
			}
		}

		private void listViewShowItems_SelectedIndexChanged(object sender, EventArgs e)
		{
			LoadSelectedItem();
		}

		private void LoadSelectedItem()
		{
			if (SelectedShowItem != null)
			{
				comboBoxActions.SelectedItem = SelectedShowItem.Action.ToString();
				SetCurrentEditor(SelectedShowItem.Action.ToString());
			}
			CheckButtons();
		}

		private void listViewShowItems_Highlight(object sender, DrawListViewItemEventArgs e)
		{
			// If this item is the selected item
			if (e.Item != null)
			{
				if (e.Item.Selected)
				{
					// If the selected item has focus Set the colors to the normal colors for a selected item
					e.Item.ForeColor = SystemColors.HighlightText;
					e.Item.BackColor = SystemColors.Highlight;
				}
				else
				{
					// Set the normal colors for items that are not selected
					e.Item.ForeColor = listViewShowItems.ForeColor;
					e.Item.BackColor = listViewShowItems.BackColor;
				}
				e.DrawBackground();
				e.DrawText();
			}
		}

		#region Drag/Drop

		private void listViewShowItems_ItemDrag(object sender, ItemDragEventArgs e)
		{
			listViewShowItems.DoDragDrop(listViewShowItems.SelectedItems, DragDropEffects.Move);
		}

		private void listViewShowItems_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(ListView.SelectedListViewItemCollection)))
			{
				e.Effect = DragDropEffects.Move;
			}
		}

		private void listViewShowItems_DragDrop(object sender, DragEventArgs e)
		{
			listViewShowItems.Alignment = ListViewAlignment.Default;
			if (listViewShowItems.SelectedItems.Count == 0)
				return;
			Point p = listViewShowItems.PointToClient(new Point(e.X, e.Y));
			ListViewItem MovetoNewPosition = listViewShowItems.GetItemAt(p.X, p.Y);
			if (MovetoNewPosition == null) return;
			ListViewItem DropToNewPosition = (e.Data.GetData(typeof(ListView.SelectedListViewItemCollection)) as ListView.SelectedListViewItemCollection)[0];
			ListViewItem CloneToNew = (ListViewItem)DropToNewPosition.Clone();
			int index = MovetoNewPosition.Index;
			listViewShowItems.Items.Remove(DropToNewPosition);
			listViewShowItems.Items.Insert(index, CloneToNew);
			listViewShowItems.Alignment = ListViewAlignment.SnapToGrid;
			this.listViewShowItems.Items[index].Selected = true;
			UpdateListViewItems();
		}

		#endregion

		private void listViewShowItems_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			if (!e.Item.Selected)
			{
				ShowItem item = e.Item.Tag as ShowItem;
				e.Item.SubItems[0].Text = item.Name;
			}
		}
	}
}
