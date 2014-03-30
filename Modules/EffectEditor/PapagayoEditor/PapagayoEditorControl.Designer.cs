namespace VixenModules.EffectEditor.PapagayoEditor
{
    partial class PapagayoEditorControl
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
            this.SuspendLayout();
            // 
            // pgoFileNameLabel
            // 
            this.pgoFileNameLabel.Location = new System.Drawing.Point(3, 13);
            this.pgoFileNameLabel.Name = "pgoFileNameLabel";
            this.pgoFileNameLabel.Size = new System.Drawing.Size(313, 39);
            this.pgoFileNameLabel.TabIndex = 2;
            this.pgoFileNameLabel.Text = "None";
            // 
            // PGOFileButton
            // 
            this.PGOFileButton.Location = new System.Drawing.Point(6, 55);
            this.PGOFileButton.Name = "PGOFileButton";
            this.PGOFileButton.Size = new System.Drawing.Size(75, 23);
            this.PGOFileButton.TabIndex = 3;
            this.PGOFileButton.Text = "PGO File";
            this.PGOFileButton.UseVisualStyleBackColor = true;
            this.PGOFileButton.Click += new System.EventHandler(this.PGOFileButton_Click);
            // 
            // PapagayoEditorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PGOFileButton);
            this.Controls.Add(this.pgoFileNameLabel);
            this.Name = "PapagayoEditorControl";
            this.Size = new System.Drawing.Size(328, 196);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label pgoFileNameLabel;
        private System.Windows.Forms.Button PGOFileButton;
    }
}
