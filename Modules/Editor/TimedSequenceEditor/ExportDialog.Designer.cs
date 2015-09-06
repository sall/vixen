﻿namespace VixenModules.Editor.TimedSequenceEditor
{
    partial class ExportDialog
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
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Test");
			this.buttonStart = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.outputFormatComboBox = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.resolutionComboBox = new System.Windows.Forms.ComboBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.progressLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.exportProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.currentTimeLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.buttonStop = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.networkListView = new Common.Controls.ListViewEx();
			this.controllerColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.channelsColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.mappingColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.groupBox1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonStart
			// 
			this.buttonStart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonStart.FlatAppearance.BorderColor = System.Drawing.Color.Black;
			this.buttonStart.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
			this.buttonStart.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.buttonStart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.buttonStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonStart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.buttonStart.Location = new System.Drawing.Point(118, 377);
			this.buttonStart.Name = "buttonStart";
			this.buttonStart.Size = new System.Drawing.Size(89, 23);
			this.buttonStart.TabIndex = 8;
			this.buttonStart.Text = "Start";
			this.buttonStart.UseVisualStyleBackColor = true;
			this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
			this.buttonStart.MouseLeave += new System.EventHandler(this.buttonBackground_MouseLeave);
			this.buttonStart.MouseHover += new System.EventHandler(this.buttonBackground_MouseHover);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.label3.Location = new System.Drawing.Point(16, 24);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(77, 13);
			this.label3.TabIndex = 9;
			this.label3.Text = "Output Format:";
			// 
			// outputFormatComboBox
			// 
			this.outputFormatComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
			this.outputFormatComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.outputFormatComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.outputFormatComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.outputFormatComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.outputFormatComboBox.FormattingEnabled = true;
			this.outputFormatComboBox.Location = new System.Drawing.Point(99, 21);
			this.outputFormatComboBox.Name = "outputFormatComboBox";
			this.outputFormatComboBox.Size = new System.Drawing.Size(121, 21);
			this.outputFormatComboBox.TabIndex = 10;
			this.outputFormatComboBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_DrawItem);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.label4.Location = new System.Drawing.Point(235, 24);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(82, 13);
			this.label4.TabIndex = 11;
			this.label4.Text = "Resolution (ms):";
			// 
			// resolutionComboBox
			// 
			this.resolutionComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
			this.resolutionComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.resolutionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.resolutionComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.resolutionComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.resolutionComboBox.FormattingEnabled = true;
			this.resolutionComboBox.Items.AddRange(new object[] {
            "25",
            "50",
            "100"});
			this.resolutionComboBox.Location = new System.Drawing.Point(323, 21);
			this.resolutionComboBox.Name = "resolutionComboBox";
			this.resolutionComboBox.Size = new System.Drawing.Size(53, 21);
			this.resolutionComboBox.TabIndex = 12;
			this.resolutionComboBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_DrawItem);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.resolutionComboBox);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.outputFormatComboBox);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.groupBox1.Location = new System.Drawing.Point(8, 51);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(404, 61);
			this.groupBox1.TabIndex = 13;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Settings";
			this.groupBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBoxes_Paint);
			// 
			// statusStrip1
			// 
			this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
			this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressLabel,
            this.exportProgressBar,
            this.currentTimeLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 415);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(442, 22);
			this.statusStrip1.TabIndex = 14;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// progressLabel
			// 
			this.progressLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.progressLabel.Name = "progressLabel";
			this.progressLabel.Size = new System.Drawing.Size(0, 17);
			// 
			// exportProgressBar
			// 
			this.exportProgressBar.BackColor = System.Drawing.Color.WhiteSmoke;
			this.exportProgressBar.Name = "exportProgressBar";
			this.exportProgressBar.Size = new System.Drawing.Size(100, 16);
			// 
			// currentTimeLabel
			// 
			this.currentTimeLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.currentTimeLabel.Name = "currentTimeLabel";
			this.currentTimeLabel.Size = new System.Drawing.Size(55, 17);
			this.currentTimeLabel.Text = "00:00.000";
			// 
			// buttonStop
			// 
			this.buttonStop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonStop.BackColor = System.Drawing.Color.Transparent;
			this.buttonStop.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonStop.FlatAppearance.BorderColor = System.Drawing.Color.Black;
			this.buttonStop.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
			this.buttonStop.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.buttonStop.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.buttonStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonStop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.buttonStop.Location = new System.Drawing.Point(208, 377);
			this.buttonStop.Name = "buttonStop";
			this.buttonStop.Size = new System.Drawing.Size(89, 23);
			this.buttonStop.TabIndex = 15;
			this.buttonStop.Text = "Stop";
			this.buttonStop.UseVisualStyleBackColor = false;
			this.buttonStop.Click += new System.EventHandler(this.buttonCancel_Click);
			this.buttonStop.MouseEnter += new System.EventHandler(this.stopButton_MouseEnter);
			this.buttonStop.MouseLeave += new System.EventHandler(this.buttonBackground_MouseLeave);
			this.buttonStop.MouseHover += new System.EventHandler(this.buttonBackground_MouseHover);
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.networkListView);
			this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.groupBox2.Location = new System.Drawing.Point(8, 129);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(418, 233);
			this.groupBox2.TabIndex = 16;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Network";
			this.groupBox2.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBoxes_Paint);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(266, 19);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(48, 13);
			this.label5.TabIndex = 4;
			this.label5.Text = "Mapping";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(187, 19);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(51, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Channels";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(51, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Controller";
			// 
			// networkListView
			// 
			this.networkListView.AllowDrop = true;
			this.networkListView.AllowRowReorder = true;
			this.networkListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.networkListView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
			this.networkListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.networkListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.controllerColumn,
            this.channelsColumn,
            this.mappingColumn});
			this.networkListView.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.networkListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.networkListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
			this.networkListView.Location = new System.Drawing.Point(6, 35);
			this.networkListView.MultiSelect = false;
			this.networkListView.Name = "networkListView";
			this.networkListView.Size = new System.Drawing.Size(406, 192);
			this.networkListView.TabIndex = 1;
			this.networkListView.UseCompatibleStateImageBehavior = false;
			this.networkListView.View = System.Windows.Forms.View.Details;
			// 
			// controllerColumn
			// 
			this.controllerColumn.Text = "Controller";
			this.controllerColumn.Width = 176;
			// 
			// channelsColumn
			// 
			this.channelsColumn.Text = "Channels";
			this.channelsColumn.Width = 76;
			// 
			// mappingColumn
			// 
			this.mappingColumn.Text = "Mapping";
			this.mappingColumn.Width = 128;
			// 
			// backgroundWorker1
			// 
			this.backgroundWorker1.WorkerReportsProgress = true;
			this.backgroundWorker1.WorkerSupportsCancellation = true;
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.textBox1.Location = new System.Drawing.Point(14, 13);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(398, 37);
			this.textBox1.TabIndex = 17;
			this.textBox1.Text = "You must have all of your required display elements to controller outputs fully p" +
    "atched in the Display Setup for the export to produce correct results..";
			// 
			// ExportDialog
			// 
			this.AcceptButton = this.buttonStart;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
			this.CancelButton = this.buttonStop;
			this.ClientSize = new System.Drawing.Size(442, 437);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.buttonStop);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.buttonStart);
			this.MaximumSize = new System.Drawing.Size(458, 747);
			this.MinimumSize = new System.Drawing.Size(458, 470);
			this.Name = "ExportDialog";
			this.Text = "Export Sequence";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ExportDialog_FormClosed);
			this.Load += new System.EventHandler(this.ExportForm_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox outputFormatComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox resolutionComboBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel progressLabel;
        private System.Windows.Forms.ToolStripProgressBar exportProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel currentTimeLabel;
		private System.Windows.Forms.Button buttonStop;
		private System.Windows.Forms.GroupBox groupBox2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TextBox textBox1;
		private Common.Controls.ListViewEx networkListView;
		private System.Windows.Forms.ColumnHeader controllerColumn;
		private System.Windows.Forms.ColumnHeader channelsColumn;
		private System.Windows.Forms.ColumnHeader mappingColumn;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
    }
}