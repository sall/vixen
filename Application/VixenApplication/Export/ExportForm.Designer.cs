namespace VixenApplication
{
    partial class ExportForm
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
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.sequenceNameField = new System.Windows.Forms.TextBox();
            this.sequenceChangeButton = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.startButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.outputFormatComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.resolutionComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.exportProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.currentTimeLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.stopButton = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.networkListView = new System.Windows.Forms.ListView();
            this.controllerColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.channelsColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mappingColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.Title = "Sequence to Export";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sequence:";
            // 
            // sequenceNameField
            // 
            this.sequenceNameField.Location = new System.Drawing.Point(87, 63);
            this.sequenceNameField.Multiline = true;
            this.sequenceNameField.Name = "sequenceNameField";
            this.sequenceNameField.ReadOnly = true;
            this.sequenceNameField.Size = new System.Drawing.Size(289, 37);
            this.sequenceNameField.TabIndex = 3;
            // 
            // sequenceChangeButton
            // 
            this.sequenceChangeButton.Location = new System.Drawing.Point(398, 63);
            this.sequenceChangeButton.Name = "sequenceChangeButton";
            this.sequenceChangeButton.Size = new System.Drawing.Size(53, 23);
            this.sequenceChangeButton.TabIndex = 6;
            this.sequenceChangeButton.Text = "Change";
            this.sequenceChangeButton.UseVisualStyleBackColor = true;
            this.sequenceChangeButton.Click += new System.EventHandler(this.sequenceSetButton_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Title = "Save as File";
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(111, 463);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 8;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Output Format:";
            // 
            // outputFormatComboBox
            // 
            this.outputFormatComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.outputFormatComboBox.FormattingEnabled = true;
            this.outputFormatComboBox.Location = new System.Drawing.Point(99, 21);
            this.outputFormatComboBox.Name = "outputFormatComboBox";
            this.outputFormatComboBox.Size = new System.Drawing.Size(121, 21);
            this.outputFormatComboBox.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(235, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Resolution (ms):";
            // 
            // resolutionComboBox
            // 
            this.resolutionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.resolutionComboBox.FormattingEnabled = true;
            this.resolutionComboBox.Items.AddRange(new object[] {
            "25",
            "50",
            "100"});
            this.resolutionComboBox.Location = new System.Drawing.Point(323, 24);
            this.resolutionComboBox.Name = "resolutionComboBox";
            this.resolutionComboBox.Size = new System.Drawing.Size(53, 21);
            this.resolutionComboBox.TabIndex = 12;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.sequenceNameField);
            this.groupBox1.Controls.Add(this.resolutionComboBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.sequenceChangeButton);
            this.groupBox1.Controls.Add(this.outputFormatComboBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(472, 106);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.exportProgressBar,
            this.currentTimeLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 489);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(500, 22);
            this.statusStrip1.TabIndex = 14;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(53, 17);
            this.toolStripStatusLabel1.Text = "Progress:";
            // 
            // exportProgressBar
            // 
            this.exportProgressBar.Name = "exportProgressBar";
            this.exportProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // currentTimeLabel
            // 
            this.currentTimeLabel.Name = "currentTimeLabel";
            this.currentTimeLabel.Size = new System.Drawing.Size(57, 17);
            this.currentTimeLabel.Text = "00:00.000";
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(218, 463);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 15;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.networkListView);
            this.groupBox2.Location = new System.Drawing.Point(12, 135);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(472, 309);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Network";
            // 
            // networkListView
            // 
            this.networkListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.controllerColumn,
            this.channelsColumn,
            this.mappingColumn});
            listViewItem1.StateImageIndex = 0;
            this.networkListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.networkListView.Location = new System.Drawing.Point(6, 19);
            this.networkListView.Name = "networkListView";
            this.networkListView.Size = new System.Drawing.Size(370, 284);
            this.networkListView.TabIndex = 0;
            this.networkListView.UseCompatibleStateImageBehavior = false;
            this.networkListView.View = System.Windows.Forms.View.Details;
            // 
            // controllerColumn
            // 
            this.controllerColumn.Text = "Controller";
            this.controllerColumn.Width = 164;
            // 
            // channelsColumn
            // 
            this.channelsColumn.Text = "Channels";
            this.channelsColumn.Width = 63;
            // 
            // mappingColumn
            // 
            this.mappingColumn.Text = "Mapping";
            this.mappingColumn.Width = 134;
            // 
            // ExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 511);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.startButton);
            this.Name = "ExportForm";
            this.Text = "Export Sequence";
            this.Load += new System.EventHandler(this.ExportForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox sequenceNameField;
        private System.Windows.Forms.Button sequenceChangeButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox outputFormatComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox resolutionComboBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar exportProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel currentTimeLabel;
        private System.Windows.Forms.Button stopButton;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView networkListView;
        private System.Windows.Forms.ColumnHeader controllerColumn;
        private System.Windows.Forms.ColumnHeader channelsColumn;
        private System.Windows.Forms.ColumnHeader mappingColumn;
    }
}