﻿namespace VixenModules.Editor.TimedSequenceEditor
{
	partial class EffectTimeEditor
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.btnOk = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.txtStartTime = new System.Windows.Forms.MaskedTextBox();
			this.txtDuration = new System.Windows.Forms.MaskedTextBox();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// btnOk
			// 
			this.btnOk.BackColor = System.Drawing.Color.Transparent;
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.btnOk.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
			this.btnOk.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.btnOk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnOk.Location = new System.Drawing.Point(32, 102);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 1;
			this.btnOk.Text = "Ok";
			this.btnOk.UseVisualStyleBackColor = false;
			this.btnOk.MouseLeave += new System.EventHandler(this.buttonBackground_MouseLeave);
			this.btnOk.MouseHover += new System.EventHandler(this.buttonBackground_MouseHover);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(35, 39);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(29, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Start";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(17, 68);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(47, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Duration";
			// 
			// btnCancel
			// 
			this.btnCancel.BackColor = System.Drawing.Color.Transparent;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.btnCancel.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
			this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnCancel.Location = new System.Drawing.Point(113, 102);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.MouseLeave += new System.EventHandler(this.buttonBackground_MouseLeave);
			this.btnCancel.MouseHover += new System.EventHandler(this.buttonBackground_MouseHover);
			// 
			// txtStartTime
			// 
			this.txtStartTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
			this.txtStartTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtStartTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.txtStartTime.Location = new System.Drawing.Point(70, 36);
			this.txtStartTime.Name = "txtStartTime";
			this.txtStartTime.Size = new System.Drawing.Size(100, 20);
			this.txtStartTime.TabIndex = 6;
			this.txtStartTime.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.txtStartTime_MaskInputRejected);
			this.txtStartTime.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtStartTime_KeyDown);
			this.txtStartTime.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtStartTime_KeyUp);
			// 
			// txtDuration
			// 
			this.txtDuration.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
			this.txtDuration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtDuration.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.txtDuration.Location = new System.Drawing.Point(70, 65);
			this.txtDuration.Name = "txtDuration";
			this.txtDuration.Size = new System.Drawing.Size(100, 20);
			this.txtDuration.TabIndex = 7;
			this.txtDuration.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.txtDuration_MaskInputRejected);
			this.txtDuration.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDuration_KeyDown);
			this.txtDuration.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDuration_KeyUp);
			// 
			// toolTip
			// 
			this.toolTip.AutoPopDelay = 3000;
			this.toolTip.InitialDelay = 500;
			this.toolTip.ReshowDelay = 100;
			// 
			// EffectTimeEditor
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(212, 167);
			this.Controls.Add(this.txtDuration);
			this.Controls.Add(this.txtStartTime);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnOk);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(228, 206);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(228, 206);
			this.Name = "EffectTimeEditor";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "EffectTimeEditor";
			this.Load += new System.EventHandler(this.EffectTimeEditor_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.MaskedTextBox txtStartTime;
		private System.Windows.Forms.MaskedTextBox txtDuration;
		private System.Windows.Forms.ToolTip toolTip;

	}
}