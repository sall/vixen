namespace VixenModules.OutputFilter.LipSync
{
    partial class LipSyncBreakdownSetup
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
            this.tableLayoutPanelControls = new System.Windows.Forms.TableLayoutPanel();
            this.buttonApplyTemplate = new System.Windows.Forms.Button();
            this.comboBoxTemplates = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonAddString = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tableLayoutPanelControls
            // 
            this.tableLayoutPanelControls.ColumnCount = 1;
            this.tableLayoutPanelControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelControls.Location = new System.Drawing.Point(26, 91);
            this.tableLayoutPanelControls.Name = "tableLayoutPanelControls";
            this.tableLayoutPanelControls.RowCount = 1;
            this.tableLayoutPanelControls.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 360F));
            this.tableLayoutPanelControls.Size = new System.Drawing.Size(560, 360);
            this.tableLayoutPanelControls.TabIndex = 0;
            // 
            // buttonApplyTemplate
            // 
            this.buttonApplyTemplate.Location = new System.Drawing.Point(231, 45);
            this.buttonApplyTemplate.Name = "buttonApplyTemplate";
            this.buttonApplyTemplate.Size = new System.Drawing.Size(111, 25);
            this.buttonApplyTemplate.TabIndex = 14;
            this.buttonApplyTemplate.Text = "Apply Template";
            this.buttonApplyTemplate.UseVisualStyleBackColor = true;
            this.buttonApplyTemplate.Click += new System.EventHandler(this.buttonApplyTemplate_Click);
            // 
            // comboBoxTemplates
            // 
            this.comboBoxTemplates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTemplates.Location = new System.Drawing.Point(96, 48);
            this.comboBoxTemplates.Name = "comboBoxTemplates";
            this.comboBoxTemplates.Size = new System.Drawing.Size(121, 21);
            this.comboBoxTemplates.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Template:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(507, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Select a pre-configured template from the drop-down box, or build a custom lipsyn" +
    "c breakdown filter below.";
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(418, 475);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(80, 25);
            this.buttonOK.TabIndex = 16;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(504, 475);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(80, 25);
            this.buttonCancel.TabIndex = 15;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonAddString
            // 
            this.buttonAddString.Location = new System.Drawing.Point(26, 457);
            this.buttonAddString.Name = "buttonAddString";
            this.buttonAddString.Size = new System.Drawing.Size(75, 23);
            this.buttonAddString.TabIndex = 17;
            this.buttonAddString.Text = "Add String";
            this.buttonAddString.UseVisualStyleBackColor = true;
            this.buttonAddString.Click += new System.EventHandler(this.buttonAddString_Click);
            // 
            // LipSyncBreakdownSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 512);
            this.Controls.Add(this.buttonAddString);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonApplyTemplate);
            this.Controls.Add(this.comboBoxTemplates);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tableLayoutPanelControls);
            this.MinimumSize = new System.Drawing.Size(400, 480);
            this.Name = "LipSyncBreakdownSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "LipSyncSetup";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelControls;
        private System.Windows.Forms.Button buttonApplyTemplate;
        private System.Windows.Forms.ComboBox comboBoxTemplates;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonAddString;

    }
}