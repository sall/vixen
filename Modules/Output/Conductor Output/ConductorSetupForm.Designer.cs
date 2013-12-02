namespace VixenModules.Output.ConductorOutput
{
    partial class ConductorSetupForm
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
			this.cmcb = new System.Windows.Forms.CheckBox();
			this.cmbtok = new System.Windows.Forms.Button();
			this.cmbtcancel = new System.Windows.Forms.Button();
			this.cmcb2 = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// cmcb
			// 
			this.cmcb.AutoSize = true;
			this.cmcb.Location = new System.Drawing.Point(81, 26);
			this.cmcb.Name = "cmcb";
			this.cmcb.Size = new System.Drawing.Size(112, 17);
			this.cmcb.TabIndex = 0;
			this.cmcb.Text = "Save Output Data";
			this.cmcb.UseVisualStyleBackColor = true;
			// 
			// cmbtok
			// 
			this.cmbtok.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.cmbtok.Location = new System.Drawing.Point(60, 91);
			this.cmbtok.Name = "cmbtok";
			this.cmbtok.Size = new System.Drawing.Size(75, 23);
			this.cmbtok.TabIndex = 1;
			this.cmbtok.Text = "OK";
			this.cmbtok.UseVisualStyleBackColor = true;
			// 
			// cmbtcancel
			// 
			this.cmbtcancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmbtcancel.Location = new System.Drawing.Point(141, 91);
			this.cmbtcancel.Name = "cmbtcancel";
			this.cmbtcancel.Size = new System.Drawing.Size(75, 23);
			this.cmbtcancel.TabIndex = 2;
			this.cmbtcancel.Text = "Cancel";
			this.cmbtcancel.UseVisualStyleBackColor = true;
			// 
			// cmcb2
			// 
			this.cmcb2.AutoSize = true;
			this.cmcb2.Location = new System.Drawing.Point(81, 49);
			this.cmcb2.Name = "cmcb2";
			this.cmcb2.Size = new System.Drawing.Size(113, 17);
			this.cmcb2.TabIndex = 3;
			this.cmcb2.Text = "Otput Debug Data";
			this.cmcb2.UseVisualStyleBackColor = true;
			this.cmcb2.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// ConductorSetupForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(281, 126);
			this.Controls.Add(this.cmcb2);
			this.Controls.Add(this.cmbtcancel);
			this.Controls.Add(this.cmbtok);
			this.Controls.Add(this.cmcb);
			this.Name = "ConductorSetupForm";
			this.Text = "ConductorSetupForm";
			this.Load += new System.EventHandler(this.ConductorSetupForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cmcb;
        private System.Windows.Forms.Button cmbtok;
        private System.Windows.Forms.Button cmbtcancel;
        private System.Windows.Forms.CheckBox cmcb2;
    }
}