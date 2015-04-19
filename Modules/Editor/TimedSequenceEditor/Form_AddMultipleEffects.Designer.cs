﻿namespace VixenModules.Editor.TimedSequenceEditor
{
	partial class Form_AddMultipleEffects
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
			this.components = new System.ComponentModel.Container();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.txtStartTime = new System.Windows.Forms.MaskedTextBox();
			this.txtDuration = new System.Windows.Forms.MaskedTextBox();
			this.txtDurationBetween = new System.Windows.Forms.MaskedTextBox();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.txtEffectCount = new System.Windows.Forms.NumericUpDown();
			this.lblPossibleEffects = new System.Windows.Forms.Label();
			this.listBoxMarkCollections = new System.Windows.Forms.ListView();
			this.checkBoxAlignToBeatMarks = new System.Windows.Forms.CheckBox();
			this.checkBoxFillDuration = new System.Windows.Forms.CheckBox();
			this.checkBoxEditEffects = new System.Windows.Forms.CheckBox();
			this.checkBoxSelectEffects = new System.Windows.Forms.CheckBox();
			this.btnShowBeatMarkOptions = new System.Windows.Forms.Button();
			this.lblShowBeatMarkOptions = new System.Windows.Forms.Label();
			this.btnHideBeatMarkOptions = new System.Windows.Forms.Button();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.panelMain = new System.Windows.Forms.Panel();
			this.lblEndingTime = new System.Windows.Forms.Label();
			this.txtEndTime = new System.Windows.Forms.MaskedTextBox();
			this.panelBeatAlignment = new System.Windows.Forms.Panel();
			this.checkBoxSkipEOBeat = new System.Windows.Forms.CheckBox();
			this.panelOKCancel = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.txtEffectCount)).BeginInit();
			this.flowLayoutPanel1.SuspendLayout();
			this.panelMain.SuspendLayout();
			this.panelBeatAlignment.SuspendLayout();
			this.panelOKCancel.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(22, 60);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(185, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "Number of effects to add";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(22, 100);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(99, 20);
			this.label2.TabIndex = 2;
			this.label2.Text = "Starting time";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(22, 180);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(70, 20);
			this.label3.TabIndex = 4;
			this.label3.Text = "Duration";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(22, 220);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(135, 20);
			this.label4.TabIndex = 6;
			this.label4.Text = "Duration between";
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(22, 40);
			this.btnOK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(112, 35);
			this.btnOK.TabIndex = 13;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(255, 40);
			this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(112, 35);
			this.btnCancel.TabIndex = 14;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// txtStartTime
			// 
			this.txtStartTime.Location = new System.Drawing.Point(218, 95);
			this.txtStartTime.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.txtStartTime.Name = "txtStartTime";
			this.txtStartTime.Size = new System.Drawing.Size(148, 26);
			this.txtStartTime.TabIndex = 2;
			this.txtStartTime.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.txtStartTime_MaskInputRejected);
			this.txtStartTime.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtStartTime_KeyDown);
			this.txtStartTime.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtStartTime_KeyUp);
			// 
			// txtDuration
			// 
			this.txtDuration.Location = new System.Drawing.Point(218, 175);
			this.txtDuration.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.txtDuration.Name = "txtDuration";
			this.txtDuration.Size = new System.Drawing.Size(148, 26);
			this.txtDuration.TabIndex = 4;
			this.txtDuration.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.txtDuration_MaskInputRejected);
			this.txtDuration.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDuration_KeyDown);
			this.txtDuration.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDuration_KeyUp);
			// 
			// txtDurationBetween
			// 
			this.txtDurationBetween.Location = new System.Drawing.Point(218, 215);
			this.txtDurationBetween.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.txtDurationBetween.Name = "txtDurationBetween";
			this.txtDurationBetween.Size = new System.Drawing.Size(148, 26);
			this.txtDurationBetween.TabIndex = 5;
			this.txtDurationBetween.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.txtDurationBetween_MaskInputRejected);
			this.txtDurationBetween.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDurationBetween_KeyDown);
			this.txtDurationBetween.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDurationBetween_KeyUp);
			// 
			// txtEffectCount
			// 
			this.txtEffectCount.Location = new System.Drawing.Point(218, 55);
			this.txtEffectCount.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.txtEffectCount.Name = "txtEffectCount";
			this.txtEffectCount.Size = new System.Drawing.Size(150, 26);
			this.txtEffectCount.TabIndex = 1;
			// 
			// lblPossibleEffects
			// 
			this.lblPossibleEffects.AutoSize = true;
			this.lblPossibleEffects.Location = new System.Drawing.Point(114, 14);
			this.lblPossibleEffects.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblPossibleEffects.Name = "lblPossibleEffects";
			this.lblPossibleEffects.Size = new System.Drawing.Size(137, 20);
			this.lblPossibleEffects.TabIndex = 15;
			this.lblPossibleEffects.Text = "n effects possible.";
			this.lblPossibleEffects.DoubleClick += new System.EventHandler(this.lblPossibleEffects_DoubleClick);
			// 
			// listBoxMarkCollections
			// 
			this.listBoxMarkCollections.CheckBoxes = true;
			this.listBoxMarkCollections.Enabled = false;
			this.listBoxMarkCollections.Location = new System.Drawing.Point(27, 111);
			this.listBoxMarkCollections.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.listBoxMarkCollections.Name = "listBoxMarkCollections";
			this.listBoxMarkCollections.Size = new System.Drawing.Size(320, 181);
			this.listBoxMarkCollections.TabIndex = 10;
			this.listBoxMarkCollections.UseCompatibleStateImageBehavior = false;
			this.listBoxMarkCollections.View = System.Windows.Forms.View.List;
			this.listBoxMarkCollections.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listBoxMarkCollections_ItemChecked);
			// 
			// checkBoxAlignToBeatMarks
			// 
			this.checkBoxAlignToBeatMarks.AutoSize = true;
			this.checkBoxAlignToBeatMarks.Location = new System.Drawing.Point(27, 5);
			this.checkBoxAlignToBeatMarks.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.checkBoxAlignToBeatMarks.Name = "checkBoxAlignToBeatMarks";
			this.checkBoxAlignToBeatMarks.Size = new System.Drawing.Size(171, 24);
			this.checkBoxAlignToBeatMarks.TabIndex = 7;
			this.checkBoxAlignToBeatMarks.Text = "Align to beat marks";
			this.checkBoxAlignToBeatMarks.UseVisualStyleBackColor = true;
			this.checkBoxAlignToBeatMarks.CheckedChanged += new System.EventHandler(this.checkBoxAlignToBeatMarks_CheckStateChanged);
			// 
			// checkBoxFillDuration
			// 
			this.checkBoxFillDuration.AutoSize = true;
			this.checkBoxFillDuration.Enabled = false;
			this.checkBoxFillDuration.Location = new System.Drawing.Point(27, 75);
			this.checkBoxFillDuration.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.checkBoxFillDuration.Name = "checkBoxFillDuration";
			this.checkBoxFillDuration.Size = new System.Drawing.Size(228, 24);
			this.checkBoxFillDuration.TabIndex = 9;
			this.checkBoxFillDuration.Text = "Fill duration between marks";
			this.checkBoxFillDuration.UseVisualStyleBackColor = true;
			this.checkBoxFillDuration.CheckedChanged += new System.EventHandler(this.checkBoxFillDuration_CheckStateChanged);
			// 
			// checkBoxEditEffects
			// 
			this.checkBoxEditEffects.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkBoxEditEffects.AutoSize = true;
			this.checkBoxEditEffects.Location = new System.Drawing.Point(249, 7);
			this.checkBoxEditEffects.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.checkBoxEditEffects.Name = "checkBoxEditEffects";
			this.checkBoxEditEffects.Size = new System.Drawing.Size(116, 24);
			this.checkBoxEditEffects.TabIndex = 12;
			this.checkBoxEditEffects.Text = "Edit effects";
			this.checkBoxEditEffects.UseVisualStyleBackColor = true;
			// 
			// checkBoxSelectEffects
			// 
			this.checkBoxSelectEffects.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkBoxSelectEffects.AutoSize = true;
			this.checkBoxSelectEffects.Location = new System.Drawing.Point(27, 7);
			this.checkBoxSelectEffects.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.checkBoxSelectEffects.Name = "checkBoxSelectEffects";
			this.checkBoxSelectEffects.Size = new System.Drawing.Size(133, 24);
			this.checkBoxSelectEffects.TabIndex = 11;
			this.checkBoxSelectEffects.Text = "Select effects";
			this.checkBoxSelectEffects.UseVisualStyleBackColor = true;
			// 
			// btnShowBeatMarkOptions
			// 
			this.btnShowBeatMarkOptions.FlatAppearance.BorderSize = 0;
			this.btnShowBeatMarkOptions.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.btnShowBeatMarkOptions.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.btnShowBeatMarkOptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnShowBeatMarkOptions.Location = new System.Drawing.Point(22, 255);
			this.btnShowBeatMarkOptions.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnShowBeatMarkOptions.Name = "btnShowBeatMarkOptions";
			this.btnShowBeatMarkOptions.Size = new System.Drawing.Size(36, 35);
			this.btnShowBeatMarkOptions.TabIndex = 21;
			this.btnShowBeatMarkOptions.Text = "+";
			this.btnShowBeatMarkOptions.UseVisualStyleBackColor = true;
			this.btnShowBeatMarkOptions.Click += new System.EventHandler(this.btnShowBeatMarkOptions_Click);
			// 
			// lblShowBeatMarkOptions
			// 
			this.lblShowBeatMarkOptions.AutoSize = true;
			this.lblShowBeatMarkOptions.Location = new System.Drawing.Point(68, 263);
			this.lblShowBeatMarkOptions.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblShowBeatMarkOptions.Name = "lblShowBeatMarkOptions";
			this.lblShowBeatMarkOptions.Size = new System.Drawing.Size(253, 20);
			this.lblShowBeatMarkOptions.TabIndex = 22;
			this.lblShowBeatMarkOptions.Text = "Show beat mark alignment options";
			// 
			// btnHideBeatMarkOptions
			// 
			this.btnHideBeatMarkOptions.FlatAppearance.BorderSize = 0;
			this.btnHideBeatMarkOptions.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.btnHideBeatMarkOptions.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.btnHideBeatMarkOptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnHideBeatMarkOptions.Location = new System.Drawing.Point(22, 255);
			this.btnHideBeatMarkOptions.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnHideBeatMarkOptions.Name = "btnHideBeatMarkOptions";
			this.btnHideBeatMarkOptions.Size = new System.Drawing.Size(36, 35);
			this.btnHideBeatMarkOptions.TabIndex = 6;
			this.btnHideBeatMarkOptions.Text = "+";
			this.btnHideBeatMarkOptions.UseVisualStyleBackColor = true;
			this.btnHideBeatMarkOptions.Visible = false;
			this.btnHideBeatMarkOptions.Click += new System.EventHandler(this.btnHideBeatMarkOptions_Click);
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.AutoSize = true;
			this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.flowLayoutPanel1.Controls.Add(this.panelMain);
			this.flowLayoutPanel1.Controls.Add(this.panelBeatAlignment);
			this.flowLayoutPanel1.Controls.Add(this.panelOKCancel);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(413, 723);
			this.flowLayoutPanel1.TabIndex = 25;
			// 
			// panelMain
			// 
			this.panelMain.Controls.Add(this.lblEndingTime);
			this.panelMain.Controls.Add(this.txtEndTime);
			this.panelMain.Controls.Add(this.label1);
			this.panelMain.Controls.Add(this.label2);
			this.panelMain.Controls.Add(this.btnHideBeatMarkOptions);
			this.panelMain.Controls.Add(this.label3);
			this.panelMain.Controls.Add(this.lblShowBeatMarkOptions);
			this.panelMain.Controls.Add(this.label4);
			this.panelMain.Controls.Add(this.btnShowBeatMarkOptions);
			this.panelMain.Controls.Add(this.txtStartTime);
			this.panelMain.Controls.Add(this.txtDuration);
			this.panelMain.Controls.Add(this.txtDurationBetween);
			this.panelMain.Controls.Add(this.lblPossibleEffects);
			this.panelMain.Controls.Add(this.txtEffectCount);
			this.panelMain.Location = new System.Drawing.Point(4, 5);
			this.panelMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.panelMain.Name = "panelMain";
			this.panelMain.Size = new System.Drawing.Size(390, 295);
			this.panelMain.TabIndex = 0;
			// 
			// lblEndingTime
			// 
			this.lblEndingTime.AutoSize = true;
			this.lblEndingTime.Location = new System.Drawing.Point(22, 140);
			this.lblEndingTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblEndingTime.Name = "lblEndingTime";
			this.lblEndingTime.Size = new System.Drawing.Size(93, 20);
			this.lblEndingTime.TabIndex = 24;
			this.lblEndingTime.Text = "Ending time";
			// 
			// txtEndTime
			// 
			this.txtEndTime.Location = new System.Drawing.Point(218, 135);
			this.txtEndTime.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.txtEndTime.Name = "txtEndTime";
			this.txtEndTime.Size = new System.Drawing.Size(148, 26);
			this.txtEndTime.TabIndex = 3;
			this.txtEndTime.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.txtEndTime_MaskInputRejected);
			this.txtEndTime.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEndTime_KeyDown);
			this.txtEndTime.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtEndTime_KeyUp);
			// 
			// panelBeatAlignment
			// 
			this.panelBeatAlignment.Controls.Add(this.checkBoxSkipEOBeat);
			this.panelBeatAlignment.Controls.Add(this.checkBoxAlignToBeatMarks);
			this.panelBeatAlignment.Controls.Add(this.checkBoxFillDuration);
			this.panelBeatAlignment.Controls.Add(this.listBoxMarkCollections);
			this.panelBeatAlignment.Location = new System.Drawing.Point(4, 310);
			this.panelBeatAlignment.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.panelBeatAlignment.Name = "panelBeatAlignment";
			this.panelBeatAlignment.Size = new System.Drawing.Size(390, 298);
			this.panelBeatAlignment.TabIndex = 1;
			// 
			// checkBoxSkipEOBeat
			// 
			this.checkBoxSkipEOBeat.AutoSize = true;
			this.checkBoxSkipEOBeat.Enabled = false;
			this.checkBoxSkipEOBeat.Location = new System.Drawing.Point(27, 40);
			this.checkBoxSkipEOBeat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.checkBoxSkipEOBeat.Name = "checkBoxSkipEOBeat";
			this.checkBoxSkipEOBeat.Size = new System.Drawing.Size(184, 24);
			this.checkBoxSkipEOBeat.TabIndex = 8;
			this.checkBoxSkipEOBeat.Text = "Skip every other beat";
			this.checkBoxSkipEOBeat.UseVisualStyleBackColor = true;
			this.checkBoxSkipEOBeat.CheckedChanged += new System.EventHandler(this.checkBoxSkipEOBeat_CheckedChanged);
			// 
			// panelOKCancel
			// 
			this.panelOKCancel.Controls.Add(this.checkBoxSelectEffects);
			this.panelOKCancel.Controls.Add(this.btnOK);
			this.panelOKCancel.Controls.Add(this.checkBoxEditEffects);
			this.panelOKCancel.Controls.Add(this.btnCancel);
			this.panelOKCancel.Location = new System.Drawing.Point(4, 618);
			this.panelOKCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.panelOKCancel.Name = "panelOKCancel";
			this.panelOKCancel.Size = new System.Drawing.Size(390, 88);
			this.panelOKCancel.TabIndex = 2;
			// 
			// Form_AddMultipleEffects
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(413, 723);
			this.ControlBox = false;
			this.Controls.Add(this.flowLayoutPanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "Form_AddMultipleEffects";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Add Multiple Effects";
			this.Load += new System.EventHandler(this.Form_AddMultipleEffects_Load);
			((System.ComponentModel.ISupportInitialize)(this.txtEffectCount)).EndInit();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.panelMain.ResumeLayout(false);
			this.panelMain.PerformLayout();
			this.panelBeatAlignment.ResumeLayout(false);
			this.panelBeatAlignment.PerformLayout();
			this.panelOKCancel.ResumeLayout(false);
			this.panelOKCancel.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.MaskedTextBox txtStartTime;
		private System.Windows.Forms.MaskedTextBox txtDuration;
		private System.Windows.Forms.MaskedTextBox txtDurationBetween;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.NumericUpDown txtEffectCount;
		private System.Windows.Forms.Label lblPossibleEffects;
		private System.Windows.Forms.ListView listBoxMarkCollections;
		private System.Windows.Forms.CheckBox checkBoxAlignToBeatMarks;
		private System.Windows.Forms.CheckBox checkBoxFillDuration;
		private System.Windows.Forms.CheckBox checkBoxEditEffects;
		private System.Windows.Forms.CheckBox checkBoxSelectEffects;
		private System.Windows.Forms.Button btnShowBeatMarkOptions;
		private System.Windows.Forms.Label lblShowBeatMarkOptions;
		private System.Windows.Forms.Button btnHideBeatMarkOptions;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Panel panelMain;
		private System.Windows.Forms.Panel panelBeatAlignment;
		private System.Windows.Forms.Panel panelOKCancel;
		private System.Windows.Forms.Label lblEndingTime;
		private System.Windows.Forms.MaskedTextBox txtEndTime;
		private System.Windows.Forms.CheckBox checkBoxSkipEOBeat;
	}
}