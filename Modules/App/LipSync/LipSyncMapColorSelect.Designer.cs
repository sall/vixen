namespace VixenModules.App.LipSyncApp
{
    partial class LipSyncMapColorSelect
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
            this.panelColor = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.intensityTrackBar = new System.Windows.Forms.TrackBar();
            this.intensityUpDown = new System.Windows.Forms.DomainUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.intensityTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // panelColor
            // 
            this.panelColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelColor.Location = new System.Drawing.Point(62, 27);
            this.panelColor.Name = "panelColor";
            this.panelColor.Size = new System.Drawing.Size(79, 45);
            this.panelColor.TabIndex = 0;
            this.panelColor.Click += new System.EventHandler(this.panelColor_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Color";
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(62, 132);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(143, 132);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Intensity";
            // 
            // intensityTrackBar
            // 
            this.intensityTrackBar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.intensityTrackBar.Location = new System.Drawing.Point(62, 81);
            this.intensityTrackBar.Maximum = 100;
            this.intensityTrackBar.Name = "intensityTrackBar";
            this.intensityTrackBar.Size = new System.Drawing.Size(79, 45);
            this.intensityTrackBar.TabIndex = 5;
            this.intensityTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.intensityTrackBar.ValueChanged += new System.EventHandler(this.intensityTrackBar_ValueChanged);
            // 
            // intensityUpDown
            // 
            this.intensityUpDown.Location = new System.Drawing.Point(147, 81);
            this.intensityUpDown.Name = "intensityUpDown";
            this.intensityUpDown.ReadOnly = true;
            this.intensityUpDown.Size = new System.Drawing.Size(37, 20);
            this.intensityUpDown.TabIndex = 6;
            this.intensityUpDown.Text = "0";
            this.intensityUpDown.SelectedItemChanged += new System.EventHandler(this.intensityUpDown_SelectedItemChanged);
            // 
            // LipSyncMapColorSelect
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(230, 180);
            this.Controls.Add(this.intensityUpDown);
            this.Controls.Add(this.intensityTrackBar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelColor);
            this.Name = "LipSyncMapColorSelect";
            this.Text = "LipSyncMapColorSelect";
            ((System.ComponentModel.ISupportInitialize)(this.intensityTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelColor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar intensityTrackBar;
        private System.Windows.Forms.DomainUpDown intensityUpDown;
    }
}