﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using Common.Controls.Timeline;
using Vixen.Module.App;
using Vixen.Module.Effect;
using Vixen.Services;
using WeifenLuo.WinFormsUI.Docking;

namespace VixenModules.Editor.TimedSequenceEditor
{
	public partial class Form_Effects : DockContent
	{
		private TreeNode _mNode;
		private bool _beginDragDrop;
		public event EventHandler EscapeDrawMode;
		public TimelineControl TimelineControl { get; set; }

		public Form_Effects(TimelineControl timelineControl)
		{
			InitializeComponent();
			TimelineControl = timelineControl;
		}

		private void Form_Effects_Load(object sender, EventArgs e)
		{
			// Remove "Advanced Lighting" for now
			treeEffects.Nodes.RemoveAt(1);
			treeEffects.Sorted = true;
			LoadAvailableEffects();
		}


		#region Effect Loading

		private void LoadAvailableEffects()
		{
			foreach (
				IEffectModuleDescriptor effectDesriptor in
					ApplicationServices.GetModuleDescriptors<IEffectModuleInstance>().Cast<IEffectModuleDescriptor>())
			{
				// Add the effects to the tree
				// Set default to basic to get rid of annoying possible null reference warning.

				TreeNode parentNode = treeEffects.Nodes["treeBasic"];
				
				switch (effectDesriptor.EffectGroup)
				{
					case EffectGroups.Basic:
						parentNode = treeEffects.Nodes["treeBasic"];
						break;
					case EffectGroups.Advanced:
						parentNode = treeEffects.Nodes["treeAdvanced"];
						break;
					case EffectGroups.Device:
						parentNode = treeEffects.Nodes["treeDevice"];
						break;
				}
				TreeNode node = new TreeNode(effectDesriptor.EffectName) {Tag = effectDesriptor.TypeId};
				parentNode.Nodes.Add(node);
				// Set the image
				Image image = effectDesriptor.GetRepresentativeImage(48, 48);
				if (image != null)
				{
					effectTreeImages.Images.Add(effectDesriptor.EffectName, image);
					node.ImageIndex = effectTreeImages.Images.Count - 1;
					node.SelectedImageIndex = node.ImageIndex;
				}
				else
				{
					SetNodeImage(node, "blank.png");
				}

				if (treeEffects.Nodes.Count > 0)
				{
					treeEffects.CollapseAll();
					treeEffects.Nodes[0].Expand();
					treeEffects.Nodes[1].Expand();
				}
			}
		}

		#endregion Effect Loading

		#region TreeView Event Handlers

		private void treeEffects_MouseDown(object sender, MouseEventArgs e)
		{
			_mNode = treeEffects.GetNodeAt(e.X, e.Y);

			_beginDragDrop = 
				(_mNode != null && _mNode.Nodes.Count == 0) &&
				(e.Button == MouseButtons.Left && e.Clicks == 1);
		}

		private void treeEffects_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && _beginDragDrop)
			{
				_beginDragDrop = false;
				DataObject data = new DataObject(DataFormats.Serializable, _mNode.Tag);
				DoDragDrop(data, DragDropEffects.Copy);
			}
		}

		private void treeEffects_MouseClick(object sender, MouseEventArgs e)
		{
			if (_mNode == null)
				return;

			//If this is a group, expand or collapse it
			if (_mNode.Nodes.Count > 0)
			{
				if (_mNode.IsExpanded)
				{
					_mNode.Collapse();
				}
				else
				{
					_mNode.Expand();
				}
			}

			else //This isn't a group or the group has no children
			{
				//We are not going to allow the Device group effects in draw mode at this time
				if (_mNode.Parent != null && _mNode.Parent.Name == "treeDevice")
				{
					MessageBox.Show(@"Currently, you must drag this item to the grid to place it.", @"Effect Selection",
						MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				
				//Make sure this isn't just an empty group
				if (_mNode.Level != 0)
				{
					SelectNodeForDrawing();
				}
			}
		}

		private void treeEffects_AfterExpand(object sender, TreeViewEventArgs e)
		{
			SetNodeImage(e.Node, "bullet_arrow_down.png");
		}

		private void treeEffects_AfterCollapse(object sender, TreeViewEventArgs e)
		{
			SetNodeImage(e.Node, "bullet_arrow_Right.png");
		}

		private void treeEffects_AfterSelect(object sender, TreeViewEventArgs e)
		{
			TreeView treeView = (TreeView)sender;
			treeView.SelectedNode = null;
		}

		#endregion TreeView Event Handlers

		public void DeselectAllNodes()
		{
			TimelineControl.grid.SelectedEffect = Guid.Empty;
			TimelineControl.grid.Cursor = Cursors.Default;

			int i = 0;
			while (i < treeEffects.Nodes.Count)
			{
				foreach (TreeNode node in treeEffects.Nodes[i].Nodes)
				{
					node.BackColor = Color.Empty;
				}
				i++;
			}
		}

		private void SelectNodeForDrawing()
		{
			if (_mNode.BackColor == Color.Blue)
			{
				TimelineControl.grid.SelectedEffect = Guid.Empty;
				_mNode.BackColor = Color.Empty;
			}
			else
			{
				TimelineControl.grid.SelectedEffect = (Guid)_mNode.Tag;

				int i = 0;
				while (i < treeEffects.Nodes.Count)
				{
					foreach (TreeNode node in treeEffects.Nodes[i].Nodes)
					{
						node.BackColor = Color.Empty;
					}
					i++;
				}

				_mNode.BackColor = Color.Blue;
			}
		}

		private void SetNodeImage(TreeNode node, string image)
		{
			node.ImageKey = image;
			node.SelectedImageKey = node.ImageKey;
		}
		
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			//This prevents the treeview from handling keystrokes while it still has focus
			//Was causing an annoying ding when the user had not drawn anything
			if (keyData == Keys.Escape)
				EscapeDrawMode(this,EventArgs.Empty);
			base.ProcessCmdKey(ref msg, keyData);
			return true;
		}
	}
}
