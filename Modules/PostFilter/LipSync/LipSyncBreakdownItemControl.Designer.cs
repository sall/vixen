namespace VixenModules.OutputFilter.LipSyncBreakdown
{
    partial class LipSyncBreakdownItemControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonDelete = new System.Windows.Forms.Button();
            this.elementNameTB = new System.Windows.Forms.TextBox();
            this.phonemeCheckListBox = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(477, 5);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(75, 25);
            this.buttonDelete.TabIndex = 6;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // elementNameTB
            // 
            this.elementNameTB.Location = new System.Drawing.Point(3, 5);
            this.elementNameTB.Name = "elementNameTB";
            this.elementNameTB.Size = new System.Drawing.Size(181, 20);
            this.elementNameTB.TabIndex = 4;
            this.elementNameTB.Text = "Element Name";
            // 
            // phonemeCheckListBox
            // 
            this.phonemeCheckListBox.ColumnWidth = 50;
            this.phonemeCheckListBox.FormattingEnabled = true;
            this.phonemeCheckListBox.Location = new System.Drawing.Point(190, 5);
            this.phonemeCheckListBox.MultiColumn = true;
            this.phonemeCheckListBox.Name = "phonemeCheckListBox";
            this.phonemeCheckListBox.Size = new System.Drawing.Size(281, 34);
            this.phonemeCheckListBox.TabIndex = 7;
            // 
            // LipSyncBreakdownItemControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.phonemeCheckListBox);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.elementNameTB);
            this.DoubleBuffered = true;
            this.Name = "LipSyncBreakdownItemControl";
            this.Size = new System.Drawing.Size(560, 45);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.TextBox elementNameTB;
        private System.Windows.Forms.CheckedListBox phonemeCheckListBox;
    }
}
