namespace VixenModules.App.LipSyncApp
{
    partial class LipSyncMapMatrixEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LipSyncMapMatrixEditor));
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.startingElementCombo = new System.Windows.Forms.ComboBox();
            this.horizontalRadio = new System.Windows.Forms.RadioButton();
            this.verticalRadio = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.zoomTrackbar = new System.Windows.Forms.TrackBar();
            this.stringsUpDown = new System.Windows.Forms.NumericUpDown();
            this.pixelsUpDown = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.blackCheckBox = new System.Windows.Forms.CheckBox();
            this.assignButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.phonemePicture = new System.Windows.Forms.PictureBox();
            this.prevPhonemeButton = new System.Windows.Forms.Button();
            this.nextPhonemeButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.phonemeLabel = new System.Windows.Forms.Label();
            this.importButton = new System.Windows.Forms.Button();
            this.exportButton = new System.Windows.Forms.Button();
            this.lipSyncMapColorCtrl1 = new VixenModules.App.LipSyncApp.LipSyncMapColorCtrl();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zoomTrackbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stringsUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pixelsUpDown)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.phonemePicture)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(596, 111);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(80, 25);
            this.buttonOK.TabIndex = 16;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(597, 142);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(80, 25);
            this.buttonCancel.TabIndex = 15;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(27, 111);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(554, 323);
            this.dataGridView1.TabIndex = 18;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView1_CellPainting);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 55);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Start At";
            // 
            // startingElementCombo
            // 
            this.startingElementCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.startingElementCombo.FormattingEnabled = true;
            this.startingElementCombo.Location = new System.Drawing.Point(97, 52);
            this.startingElementCombo.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.startingElementCombo.Name = "startingElementCombo";
            this.startingElementCombo.Size = new System.Drawing.Size(112, 21);
            this.startingElementCombo.TabIndex = 20;
            this.startingElementCombo.SelectedIndexChanged += new System.EventHandler(this.startingElementCombo_SelectedIndexChanged);
            // 
            // horizontalRadio
            // 
            this.horizontalRadio.AutoSize = true;
            this.horizontalRadio.Checked = true;
            this.horizontalRadio.Location = new System.Drawing.Point(10, 19);
            this.horizontalRadio.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.horizontalRadio.Name = "horizontalRadio";
            this.horizontalRadio.Size = new System.Drawing.Size(72, 17);
            this.horizontalRadio.TabIndex = 21;
            this.horizontalRadio.TabStop = true;
            this.horizontalRadio.Text = "Horizontal";
            this.horizontalRadio.UseVisualStyleBackColor = true;
            this.horizontalRadio.CheckedChanged += new System.EventHandler(this.Horizontal_CheckedChanged);
            // 
            // verticalRadio
            // 
            this.verticalRadio.AutoSize = true;
            this.verticalRadio.Location = new System.Drawing.Point(10, 43);
            this.verticalRadio.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.verticalRadio.Name = "verticalRadio";
            this.verticalRadio.Size = new System.Drawing.Size(60, 17);
            this.verticalRadio.TabIndex = 22;
            this.verticalRadio.Text = "Vertical";
            this.verticalRadio.UseVisualStyleBackColor = true;
            this.verticalRadio.CheckedChanged += new System.EventHandler(this.Vertical_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.verticalRadio);
            this.groupBox1.Controls.Add(this.horizontalRadio);
            this.groupBox1.Location = new System.Drawing.Point(247, 14);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox1.Size = new System.Drawing.Size(91, 91);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Strings are";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 19);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 26;
            this.label2.Text = "Strings";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 43);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 27;
            this.label3.Text = "Pixels";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.zoomTrackbar);
            this.groupBox2.Controls.Add(this.stringsUpDown);
            this.groupBox2.Controls.Add(this.pixelsUpDown);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(344, 14);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox2.Size = new System.Drawing.Size(118, 91);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Size";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 31;
            this.label5.Text = "Zoom";
            // 
            // zoomTrackbar
            // 
            this.zoomTrackbar.AutoSize = false;
            this.zoomTrackbar.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.zoomTrackbar.Location = new System.Drawing.Point(37, 64);
            this.zoomTrackbar.Margin = new System.Windows.Forms.Padding(1);
            this.zoomTrackbar.Maximum = 100;
            this.zoomTrackbar.Minimum = -100;
            this.zoomTrackbar.Name = "zoomTrackbar";
            this.zoomTrackbar.Size = new System.Drawing.Size(75, 23);
            this.zoomTrackbar.TabIndex = 30;
            this.zoomTrackbar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.zoomTrackbar.ValueChanged += new System.EventHandler(this.zoomTrackbar_ValueChanged);
            // 
            // stringsUpDown
            // 
            this.stringsUpDown.Location = new System.Drawing.Point(59, 12);
            this.stringsUpDown.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.stringsUpDown.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.stringsUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.stringsUpDown.Name = "stringsUpDown";
            this.stringsUpDown.Size = new System.Drawing.Size(51, 20);
            this.stringsUpDown.TabIndex = 29;
            this.stringsUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.stringsUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.stringsUpDown.ValueChanged += new System.EventHandler(this.rowsUpDown_ValueChanged);
            // 
            // pixelsUpDown
            // 
            this.pixelsUpDown.Location = new System.Drawing.Point(59, 41);
            this.pixelsUpDown.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pixelsUpDown.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.pixelsUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.pixelsUpDown.Name = "pixelsUpDown";
            this.pixelsUpDown.Size = new System.Drawing.Size(51, 20);
            this.pixelsUpDown.TabIndex = 28;
            this.pixelsUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.pixelsUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.pixelsUpDown.ValueChanged += new System.EventHandler(this.colsUpDown_ValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.blackCheckBox);
            this.groupBox3.Controls.Add(this.lipSyncMapColorCtrl1);
            this.groupBox3.Location = new System.Drawing.Point(468, 14);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox3.Size = new System.Drawing.Size(198, 91);
            this.groupBox3.TabIndex = 29;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Pen";
            // 
            // blackCheckBox
            // 
            this.blackCheckBox.AutoSize = true;
            this.blackCheckBox.Checked = true;
            this.blackCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.blackCheckBox.Location = new System.Drawing.Point(14, 70);
            this.blackCheckBox.Name = "blackCheckBox";
            this.blackCheckBox.Size = new System.Drawing.Size(123, 17);
            this.blackCheckBox.TabIndex = 1;
            this.blackCheckBox.Text = "Black is Transparent";
            this.blackCheckBox.UseVisualStyleBackColor = true;
            // 
            // assignButton
            // 
            this.assignButton.Location = new System.Drawing.Point(97, 23);
            this.assignButton.Name = "assignButton";
            this.assignButton.Size = new System.Drawing.Size(75, 23);
            this.assignButton.TabIndex = 32;
            this.assignButton.Text = "Assign";
            this.assignButton.UseVisualStyleBackColor = true;
            this.assignButton.Click += new System.EventHandler(this.assignButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 33;
            this.label6.Text = "Elements";
            // 
            // phonemePicture
            // 
            this.phonemePicture.Location = new System.Drawing.Point(281, 455);
            this.phonemePicture.Name = "phonemePicture";
            this.phonemePicture.Size = new System.Drawing.Size(48, 48);
            this.phonemePicture.TabIndex = 34;
            this.phonemePicture.TabStop = false;
            // 
            // prevPhonemeButton
            // 
            this.prevPhonemeButton.Location = new System.Drawing.Point(239, 469);
            this.prevPhonemeButton.Name = "prevPhonemeButton";
            this.prevPhonemeButton.Size = new System.Drawing.Size(36, 23);
            this.prevPhonemeButton.TabIndex = 35;
            this.prevPhonemeButton.Text = "<";
            this.prevPhonemeButton.UseVisualStyleBackColor = true;
            this.prevPhonemeButton.Click += new System.EventHandler(this.prevPhonemeButton_Click);
            // 
            // nextPhonemeButton
            // 
            this.nextPhonemeButton.Location = new System.Drawing.Point(335, 469);
            this.nextPhonemeButton.Name = "nextPhonemeButton";
            this.nextPhonemeButton.Size = new System.Drawing.Size(36, 23);
            this.nextPhonemeButton.TabIndex = 36;
            this.nextPhonemeButton.Text = ">";
            this.nextPhonemeButton.UseVisualStyleBackColor = true;
            this.nextPhonemeButton.Click += new System.EventHandler(this.nextPhonemeButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(602, 318);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(75, 23);
            this.clearButton.TabIndex = 37;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            // 
            // phonemeLabel
            // 
            this.phonemeLabel.Location = new System.Drawing.Point(282, 436);
            this.phonemeLabel.Name = "phonemeLabel";
            this.phonemeLabel.Size = new System.Drawing.Size(47, 18);
            this.phonemeLabel.TabIndex = 38;
            this.phonemeLabel.Text = "Phoneme";
            this.phonemeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // importButton
            // 
            this.importButton.Location = new System.Drawing.Point(602, 224);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(75, 23);
            this.importButton.TabIndex = 39;
            this.importButton.Text = "Import";
            this.importButton.UseVisualStyleBackColor = true;
            this.importButton.Click += new System.EventHandler(this.importButton_Click);
            // 
            // exportButton
            // 
            this.exportButton.Location = new System.Drawing.Point(602, 253);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(75, 23);
            this.exportButton.TabIndex = 40;
            this.exportButton.Text = "Export";
            this.exportButton.UseVisualStyleBackColor = true;
            // 
            // lipSyncMapColorCtrl1
            // 
            this.lipSyncMapColorCtrl1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lipSyncMapColorCtrl1.HSVColor = ((Common.Controls.ColorManagement.ColorModels.HSV)(resources.GetObject("lipSyncMapColorCtrl1.HSVColor")));
            this.lipSyncMapColorCtrl1.Intensity = 0D;
            this.lipSyncMapColorCtrl1.Location = new System.Drawing.Point(4, 14);
            this.lipSyncMapColorCtrl1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lipSyncMapColorCtrl1.Name = "lipSyncMapColorCtrl1";
            this.lipSyncMapColorCtrl1.Size = new System.Drawing.Size(188, 56);
            this.lipSyncMapColorCtrl1.TabIndex = 0;
            // 
            // LipSyncMapMatrixEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(687, 509);
            this.Controls.Add(this.exportButton);
            this.Controls.Add(this.importButton);
            this.Controls.Add(this.phonemeLabel);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.nextPhonemeButton);
            this.Controls.Add(this.prevPhonemeButton);
            this.Controls.Add(this.phonemePicture);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.assignButton);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.startingElementCombo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.groupBox2);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MinimumSize = new System.Drawing.Size(200, 239);
            this.Name = "LipSyncMapMatrixEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "LipSync Matrix Editor";
            this.Load += new System.EventHandler(this.LipSyncMapSetup_Load);
            this.Resize += new System.EventHandler(this.LipSyncBreakdownSetup_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zoomTrackbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stringsUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pixelsUpDown)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.phonemePicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox startingElementCombo;
        private System.Windows.Forms.RadioButton horizontalRadio;
        private System.Windows.Forms.RadioButton verticalRadio;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private LipSyncMapColorCtrl lipSyncMapColorCtrl1;
        private System.Windows.Forms.NumericUpDown stringsUpDown;
        private System.Windows.Forms.NumericUpDown pixelsUpDown;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar zoomTrackbar;
        private System.Windows.Forms.Button assignButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox blackCheckBox;
        private System.Windows.Forms.PictureBox phonemePicture;
        private System.Windows.Forms.Button prevPhonemeButton;
        private System.Windows.Forms.Button nextPhonemeButton;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Label phonemeLabel;
        private System.Windows.Forms.Button importButton;
        private System.Windows.Forms.Button exportButton;

    }
}