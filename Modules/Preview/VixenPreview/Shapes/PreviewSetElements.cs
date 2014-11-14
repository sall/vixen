﻿using Common.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Vixen.Data.Flow;
using Vixen.Module;
using Vixen.Module.OutputFilter;
using Vixen.Services;
using Vixen.Sys;
using Vixen.Sys.Output;

namespace VixenModules.Preview.VixenPreview.Shapes
{
    public partial class PreviewSetElements : Form
    {
        private List<PreviewSetElementString> _strings = new List<PreviewSetElementString>();
        private List<PreviewBaseShape> _shapes;
        private bool connectStandardStrings;

        public PreviewSetElements(List<PreviewBaseShape> shapes)
        {
            InitializeComponent();
            _shapes = shapes;
            connectStandardStrings = shapes[0].connectStandardStrings;
            int i = 1;
            foreach (PreviewBaseShape shape in _shapes)
            {
                if (shape.Pixels.Count == 0)
                    continue;
                var newString = new PreviewSetElementString();
                // If this is a Standard string, only set the first pixel of the string
                if (shape.StringType == PreviewBaseShape.StringTypes.Standard)
                {
                    //Console.WriteLine("Standard String");
                    PreviewPixel pixel = shape.Pixels[0];
                    //Console.WriteLine(shape.Pixels[0].Node.Name.ToString());
                    newString.Pixels.Add(pixel.Clone());
                }
                // If this is a pixel string, let them set every pixel
                else if (shape.StringType == PreviewBaseShape.StringTypes.Pixel)
                {
                    foreach (PreviewPixel pixel in shape.Pixels)
                    {
                        newString.Pixels.Add(pixel.Clone());
                    }
                }

                newString.StringName = "String " + i.ToString();
                _strings.Add(newString);
                i++;
            }

            if (_shapes[0].Parent != null)
            {
                string shapeType = "";
                shapeType = _shapes[0].Parent.GetType().ToString();
                if ((shapeType.Contains("Icicle") && _shapes[0].StringType != PreviewBaseShape.StringTypes.Standard) || shapeType.Contains("MultiString") )
                {
                    panelSetLightCount.Visible = true;
                }
            }
        }

        private void PreviewSetElements_Load(object sender, EventArgs e)
        {
            PreviewTools.PopulateElementTree(treeElements);
            PopulateStringList();
            UpdateListLinkedElements();
        }

        private void PopulateStringList()
        {
            if (connectStandardStrings && _shapes[0].StringType == PreviewBaseShape.StringTypes.Standard)
            {
                comboStrings.Items.Add(new Common.Controls.ComboBoxItem(_strings[0].StringName, _strings[0]));
            }
            else
            {
                foreach (PreviewSetElementString lightString in _strings)
                {
                    comboStrings.Items.Add(new Common.Controls.ComboBoxItem(lightString.StringName, lightString));
                }
            }
            if (_strings.Count > 0)
                comboStrings.SelectedIndex = 0;
        }

        public void AddString(string stringName, List<PreviewPixel> pixels)
        {
            PreviewSetElementString lightString = new PreviewSetElementString();
            lightString.StringName = stringName;
            for (int i = 0; i < pixels.Count; i++)
            {
                lightString.Pixels.Add(pixels[i].Clone());
            }
            _strings.Add(lightString);
        }

        private void treeElements_ItemDrag(object sender, ItemDragEventArgs e)
        {
            treeElements.DoDragDrop(treeElements.SelectedNodes, DragDropEffects.Copy);
        }

        private void listLinkedElements_DragDrop(object sender, DragEventArgs e)
        {
            List<TreeNode> nodes = (List<TreeNode>)e.Data.GetData(typeof(List<TreeNode>));

            // Retrieve the client coordinates of the mouse position.
            Point targetPoint = listLinkedElements.PointToClient(new Point(e.X, e.Y));
            // Select the node at the mouse position.
            ListViewItem item = listLinkedElements.GetItemAt(targetPoint.X, targetPoint.Y);
            if (item == null)
            {
                MessageBox.Show("Elements must be dropped on a target.  Please try again.");
                return;
            }

            foreach (TreeNode treeNode in nodes)
            {
                if (treeNode != null)
                {
                    ElementNode channelNode = treeNode.Tag as ElementNode;

                    SetLinkedElementItems(item, channelNode);

                    if (item.Index == listLinkedElements.Items.Count - 1)
                    {
                        return;
                    }
                    else
                    {
                        item = listLinkedElements.Items[item.Index + 1];
                    }
                }
                else
                {
                    MessageBox.Show("treeNode==null!");
                }
            }
        }

        private void listLinkedElements_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }

        private void listLinkedElements_DragOver(object sender, DragEventArgs e)
        {
            // Retrieve the client coordinates of the mouse position.
            Point targetPoint = listLinkedElements.PointToClient(new Point(e.X, e.Y));
            // Select the node at the mouse position.
            ListViewItem itemToSelect = listLinkedElements.GetItemAt(targetPoint.X, targetPoint.Y);
            foreach (ListViewItem item in listLinkedElements.Items)
            {
                if (itemToSelect == item)
                    item.Selected = true;
                else
                    item.Selected = false;
            }
        }

        private void UpdateListLinkedElements()
        {
            listLinkedElements.Items.Clear();
            Common.Controls.ComboBoxItem comboBoxItem = comboStrings.SelectedItem as Common.Controls.ComboBoxItem;
            if (comboBoxItem != null)
            {
                PreviewSetElementString elementString = comboBoxItem.Value as PreviewSetElementString;
                if (elementString != null)
                {
                    foreach (PreviewPixel pixel in elementString.Pixels)
                    {
                        ListViewItem item = new ListViewItem((listLinkedElements.Items.Count + 1).ToString());
                        item.Tag = pixel;
                        listLinkedElements.Items.Add(item);
                        SetLinkedElementItems(item, pixel.Node);
                    }
                }
                else
                {
                    Console.WriteLine("elementString==null");
                }
                numericUpDownLightCount.Value = elementString.Pixels.Count();
            }
        }

        private void comboStrings_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Console.Write(comboStrings.SelectedIndex);
            UpdateListLinkedElements();
        }

        private void SetLinkedElementItems(ListViewItem item, ElementNode channelNode)
        {
            //ListViewItem item = listLinkedElements.Items[0];
            PreviewPixel pixel = item.Tag as PreviewPixel;

            if (channelNode != null)
            {
                // Is this node an element?
                if (channelNode.Element != null)
                {
                    pixel.Node = channelNode;
                    if (item != null)
                    {
                        if (item.SubItems.Count > 1)
                        {
                            item.SubItems[1].Text = channelNode.Name;
                        }
                        else
                        {
                            if (channelNode != null)
                            {
                                item.SubItems.Add(channelNode.Name);
                            }
                        }
                    }
                }
                else // This node is a group, iterate
                {
                    ListViewItem nextItem = item;
                    foreach (ElementNode child in channelNode.Children)
                    {
                        if (item.Index < listLinkedElements.Items.Count && child.Element != null)
                        {
                            SetLinkedElementItems(nextItem, child);
                            //}
                            //else
                            //{
                            //    SetLinkedElementItems(nextItem, child);
                            if (nextItem.Index == listLinkedElements.Items.Count - 1)
                            {
                                return;
                            }
                            else
                            {
                                nextItem = listLinkedElements.Items[nextItem.Index + 1];
                            }
                        }
                    }
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if ((connectStandardStrings || _shapes.Count() == 1) && _shapes[0].StringType == PreviewBaseShape.StringTypes.Standard)
            {
                //var shape = _shapes[0];
                for (int i = 0; i < _shapes.Count; i++)
                {
                    foreach (var pixel in _shapes[i]._pixels)
                    {
                        pixel.Node = _strings[0].Pixels[0].Node;
                        pixel.NodeId = _strings[0].Pixels[0].NodeId;
                    }
                }
            }
            else
            {
                // shapes with count==0 don't show up in combo box so keep separate index
                var comboidx = -1;
                for (var i = 0; i < _shapes.Count; i++)
                {                    
                    if (_shapes[i].Pixels.Count == 0)
                        continue;
                    comboidx++;
                    var item = comboStrings.Items[comboidx] as Common.Controls.ComboBoxItem;
                    var lightString = item.Value as PreviewSetElementString;
                    var shape = _shapes[i];

                    if (shape.StringType == PreviewBaseShape.StringTypes.Pixel) { 
                        while (shape.Pixels.Count > lightString.Pixels.Count)
                        {
                            shape.Pixels.RemoveAt(shape.Pixels.Count - 1);
                        }
                        while (shape.Pixels.Count < lightString.Pixels.Count)
                        {
                            var pixel = new PreviewPixel();
                            shape.Pixels.Add(pixel);
                        }
                    }

                    for (int pixelNum = 0; pixelNum < lightString.Pixels.Count; pixelNum++)
                    {
                        //Console.WriteLine("   pixelNum=" + pixelNum.ToString());
                        // If this is a standard light string, assing ALL pixels to the first node
                        if (shape.StringType == PreviewBaseShape.StringTypes.Standard)
                        {
                            foreach (var pixel in shape._pixels)
                            {
                                //Console.WriteLine("       pixel:" + lightString.Pixels[0].Node.Id.ToString());
                                pixel.Node = _strings[i].Pixels[0].Node;
                                pixel.NodeId = _strings[i].Pixels[0].NodeId;
                            }
                        }
                        else
                        {
                            shape.Pixels[pixelNum] = lightString.Pixels[pixelNum];
                        }
                    }
                }
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void treeElements_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (treeElements.SelectedNode != null && listLinkedElements.FocusedItem != null)
            {
                foreach (ListViewItem item in listLinkedElements.SelectedItems)
                {
                    SetLinkedElementItems(item, treeElements.SelectedNode.Tag as ElementNode);
                }
            }
        }

        private void copyToAllElementsAllStringsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem item = listLinkedElements.FocusedItem;
            if (item != null)
            {
                ElementNode node = (item.Tag as PreviewPixel).Node;
                for (int i = 0; i < _strings.Count; i++)
                {
                    foreach (PreviewPixel pixel in _strings[i].Pixels)
                    {
                        pixel.Node = node;
                    }
                }
                UpdateListLinkedElements();
            }
        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            Common.VixenHelp.VixenHelp.ShowHelp(Common.VixenHelp.VixenHelp.HelpStrings.Preview_LinkElements);
        }

        private void copyToAllElementsInThisStringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem selectedItem = listLinkedElements.FocusedItem;
            if (selectedItem != null)
            {
                ElementNode node = (selectedItem.Tag as PreviewPixel).Node;
                for (int i = 0; i < _strings.Count; i++)
                {
                    foreach (ListViewItem item in listLinkedElements.Items)
                    {
                        (item.Tag as PreviewPixel).Node = (selectedItem.Tag as PreviewPixel).Node;
                    }
                }

                UpdateListLinkedElements();
            }
        }

        private void buttonSetLightCount_Click(object sender, EventArgs e)
        {
            Common.Controls.ComboBoxItem comboBoxItem = comboStrings.SelectedItem as Common.Controls.ComboBoxItem;
            if (comboBoxItem != null)
            {
                PreviewSetElementString elementString = comboBoxItem.Value as PreviewSetElementString;
                if (elementString != null)
                {
                    while (elementString.Pixels.Count > numericUpDownLightCount.Value)
                    {
                        elementString.Pixels.RemoveAt(elementString.Pixels.Count - 1);
                    }
                    while (elementString.Pixels.Count < numericUpDownLightCount.Value)
                    {
                        PreviewPixel pixel = new PreviewPixel();
                        elementString.Pixels.Add(pixel);
                    }
                }
                UpdateListLinkedElements();
            }
        }

        private void reverseElementLinkingInThisStringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ComboBoxItem comboBoxItem = comboStrings.SelectedItem as Common.Controls.ComboBoxItem;
            if (comboBoxItem != null)
            {
                PreviewSetElementString elementString = comboBoxItem.Value as PreviewSetElementString;
                if (elementString != null)
                {
                    elementString.Pixels.Reverse();
                }
                else
                {
                    Console.WriteLine("elementString==null");
                }
                numericUpDownLightCount.Value = elementString.Pixels.Count();
                UpdateListLinkedElements();
            }
        }
    }
}