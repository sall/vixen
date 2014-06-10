using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Vixen.Sys;

namespace VixenModules.App.LipSyncApp
{
    public partial class LipSyncNodeSelect : Form
    {
        public LipSyncNodeSelect()
        {
            InitializeComponent();
            Changed = false;
        }

        public bool Changed { get; set; }
        
        private void BuildNode(TreeNode parentNode, ElementNode node)
        {
            foreach(ElementNode childNode in node.Children)
            {
                TreeNode newNode = new TreeNode(childNode.Name);
                BuildNode(newNode, childNode);
                parentNode.Nodes.Add(newNode);
            }
        }

        private void LipSyncNodeSelect_Load(object sender, EventArgs e)
        {
            foreach (ElementNode node in VixenSystem.Nodes.GetRootNodes())
            {
                TreeNode newNode = new TreeNode(node.Name);
                BuildNode(newNode, node);
                nodeTreeView.Nodes.Add(newNode);

            }

        }

        public List<string> NodeNames
        {
            get
            {
                List<string> retVal = new List<string>();
                foreach (ElementNode element in chosenTargets.Items)
                {
                    retVal.Add(element.Name);
                }
                return retVal;
            }

            set
            {
                List<string> names = value;
                if (names != null)
                {
                    names.ForEach(x => findAndAddElements(x, false));
                }
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {

        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            chosenTargets.Items.Clear();
            Changed = true;
        }

        private void addToChosenTargets(ElementNode node)
        {
            bool found = false;
            foreach (ElementNode chosenNode in chosenTargets.Items)
            {
                if (chosenNode.ToString().Equals(node.ToString()))
                {
                    found = true;
                    break;
                }
            }

            if (found == false)
            {
                chosenTargets.Items.Add(node);
            }
            Changed = true;
        }

        private void addElementNodes(ElementNode node, bool recurse)
        {
            addToChosenTargets(node);

            if (recurse == true)
            {
                foreach (ElementNode childNode in node.Children)
                {
                    addElementNodes(childNode, recurse);
                }
            }
        }

        private void findAndAddElements(string name, bool recurse)
        {
            foreach (ElementNode node in VixenSystem.Nodes)
            {
                if (node.Name.Equals(name))
                {
                    addElementNodes(node,recurse);
                }
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            foreach (TreeNode treeNode in nodeTreeView.SelectedNodes)
            {
                findAndAddElements(treeNode.Text, recurseCB.Checked);
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            for (int i = chosenTargets.SelectedIndices.Count - 1; i >= 0; i--)
            {
                chosenTargets.Items.RemoveAt(chosenTargets.SelectedIndices[i]);
                Changed = true;
            }
        }
    }
}
