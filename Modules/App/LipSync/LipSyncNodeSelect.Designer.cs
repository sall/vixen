namespace VixenModules.App.LipSyncApp
{
    partial class LipSyncNodeSelect
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LipSyncNodeSelect));
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.chosenTargets = new System.Windows.Forms.ListBox();
            this.addButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.resetButton = new System.Windows.Forms.Button();
            this.nodeTreeView = new Common.Controls.MultiSelectTreeview();
            this.recurseCB = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(327, 290);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(408, 290);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // chosenTargets
            // 
            this.chosenTargets.FormattingEnabled = true;
            this.chosenTargets.Location = new System.Drawing.Point(327, 12);
            this.chosenTargets.Name = "chosenTargets";
            this.chosenTargets.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.chosenTargets.Size = new System.Drawing.Size(187, 251);
            this.chosenTargets.Sorted = true;
            this.chosenTargets.TabIndex = 3;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(227, 83);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 4;
            this.addButton.Text = "->";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(227, 124);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(75, 23);
            this.removeButton.TabIndex = 5;
            this.removeButton.Text = "<-";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(227, 166);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(75, 23);
            this.resetButton.TabIndex = 6;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // nodeTreeView
            // 
            this.nodeTreeView.AllowDrop = true;
            this.nodeTreeView.Cursor = System.Windows.Forms.Cursors.Default;
            this.nodeTreeView.CustomDragCursor = null;
            this.nodeTreeView.DragDefaultMode = System.Windows.Forms.DragDropEffects.Move;
            this.nodeTreeView.DragDestinationNodeBackColor = System.Drawing.SystemColors.Highlight;
            this.nodeTreeView.DragDestinationNodeForeColor = System.Drawing.SystemColors.HighlightText;
            this.nodeTreeView.DragSourceNodeBackColor = System.Drawing.SystemColors.ControlLight;
            this.nodeTreeView.DragSourceNodeForeColor = System.Drawing.SystemColors.ControlText;
            this.nodeTreeView.Location = new System.Drawing.Point(12, 12);
            this.nodeTreeView.Name = "nodeTreeView";
            this.nodeTreeView.SelectedNodes = ((System.Collections.Generic.List<System.Windows.Forms.TreeNode>)(resources.GetObject("nodeTreeView.SelectedNodes")));
            this.nodeTreeView.Size = new System.Drawing.Size(187, 256);
            this.nodeTreeView.TabIndex = 2;
            this.nodeTreeView.UsingCustomDragCursor = false;
            // 
            // recurseCB
            // 
            this.recurseCB.AutoSize = true;
            this.recurseCB.Checked = true;
            this.recurseCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.recurseCB.Location = new System.Drawing.Point(218, 216);
            this.recurseCB.Name = "recurseCB";
            this.recurseCB.Size = new System.Drawing.Size(103, 17);
            this.recurseCB.TabIndex = 7;
            this.recurseCB.Text = "Add Recursively";
            this.recurseCB.UseVisualStyleBackColor = true;
            // 
            // LipSyncNodeSelect
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(532, 327);
            this.Controls.Add(this.recurseCB);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.chosenTargets);
            this.Controls.Add(this.nodeTreeView);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Name = "LipSyncNodeSelect";
            this.Text = "LipSyncNodeSelect";
            this.Load += new System.EventHandler(this.LipSyncNodeSelect_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private Common.Controls.MultiSelectTreeview nodeTreeView;
        private System.Windows.Forms.ListBox chosenTargets;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.CheckBox recurseCB;
    }
}