namespace VixenModules.EffectEditor.LipSyncEditor
{
    partial class LipSyncEditorControl
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
            this.pgoFileNameLabel = new System.Windows.Forms.Label();
            this.PGOFileButton = new System.Windows.Forms.Button();
            this.staticRadioButton = new System.Windows.Forms.RadioButton();
            this.linkedRadioButton = new System.Windows.Forms.RadioButton();
            this.staticPhoneMeCombo = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // pgoFileNameLabel
            // 
            this.pgoFileNameLabel.Location = new System.Drawing.Point(3, 82);
            this.pgoFileNameLabel.Name = "pgoFileNameLabel";
            this.pgoFileNameLabel.Size = new System.Drawing.Size(307, 27);
            this.pgoFileNameLabel.TabIndex = 2;
            this.pgoFileNameLabel.Text = "None";
            // 
            // PGOFileButton
            // 
            this.PGOFileButton.Location = new System.Drawing.Point(69, 51);
            this.PGOFileButton.Name = "PGOFileButton";
            this.PGOFileButton.Size = new System.Drawing.Size(42, 28);
            this.PGOFileButton.TabIndex = 3;
            this.PGOFileButton.Text = "Load";
            this.PGOFileButton.UseVisualStyleBackColor = true;
            this.PGOFileButton.Click += new System.EventHandler(this.PGOFileButton_Click);
            // 
            // staticRadioButton
            // 
            this.staticRadioButton.AutoSize = true;
            this.staticRadioButton.Checked = true;
            this.staticRadioButton.Location = new System.Drawing.Point(6, 13);
            this.staticRadioButton.Name = "staticRadioButton";
            this.staticRadioButton.Size = new System.Drawing.Size(52, 17);
            this.staticRadioButton.TabIndex = 4;
            this.staticRadioButton.TabStop = true;
            this.staticRadioButton.Text = "Static";
            this.staticRadioButton.UseVisualStyleBackColor = true;
            this.staticRadioButton.CheckedChanged += new System.EventHandler(this.staticRadioButton_CheckedChanged);
            // 
            // linkedRadioButton
            // 
            this.linkedRadioButton.AutoSize = true;
            this.linkedRadioButton.Location = new System.Drawing.Point(6, 57);
            this.linkedRadioButton.Name = "linkedRadioButton";
            this.linkedRadioButton.Size = new System.Drawing.Size(57, 17);
            this.linkedRadioButton.TabIndex = 5;
            this.linkedRadioButton.Text = "Linked";
            this.linkedRadioButton.UseVisualStyleBackColor = true;
            this.linkedRadioButton.CheckedChanged += new System.EventHandler(this.linkedRadioButton_CheckedChanged);
            // 
            // staticPhoneMeCombo
            // 
            this.staticPhoneMeCombo.FormattingEnabled = true;
            this.staticPhoneMeCombo.Location = new System.Drawing.Point(69, 13);
            this.staticPhoneMeCombo.Name = "staticPhoneMeCombo";
            this.staticPhoneMeCombo.Size = new System.Drawing.Size(64, 21);
            this.staticPhoneMeCombo.TabIndex = 6;
            // 
            // LipSyncEditorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.staticPhoneMeCombo);
            this.Controls.Add(this.linkedRadioButton);
            this.Controls.Add(this.staticRadioButton);
            this.Controls.Add(this.PGOFileButton);
            this.Controls.Add(this.pgoFileNameLabel);
            this.Name = "LipSyncEditorControl";
            this.Size = new System.Drawing.Size(328, 149);
            this.Load += new System.EventHandler(this.LipSyncEditorControl_Load);
            this.Leave += new System.EventHandler(this.LipSyncEditorControl_Leave);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label pgoFileNameLabel;
        private System.Windows.Forms.Button PGOFileButton;
        private System.Windows.Forms.RadioButton staticRadioButton;
        private System.Windows.Forms.RadioButton linkedRadioButton;
        private System.Windows.Forms.ComboBox staticPhoneMeCombo;
    }
}
