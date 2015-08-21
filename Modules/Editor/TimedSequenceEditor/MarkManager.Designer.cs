﻿namespace VixenModules.Editor.TimedSequenceEditor
{
	partial class MarkManager
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
			if (_audio != null)
			{
				_audio.DetectFrequeniesEnabled = false;
				_audio.FrequencyDetected -= _audio_FrequencyDetected;
				_audio = null;
			}
			if (audioDetectionSettings != null && disposing)
				audioDetectionSettings.Dispose();

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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MarkManager));
			this.groupBoxMarkCollections = new System.Windows.Forms.GroupBox();
			this.panelMarkCollectionsButtons = new System.Windows.Forms.Panel();
			this.buttonExportBeatMarks = new System.Windows.Forms.Button();
			this.buttonRemoveCollection = new System.Windows.Forms.Button();
			this.buttonImportAudacity = new System.Windows.Forms.Button();
			this.buttonAddCollection = new System.Windows.Forms.Button();
			this.label12 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.listViewMarkCollections = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.buttonOK = new System.Windows.Forms.Button();
			this.groupBoxSelectedMarkCollection = new System.Windows.Forms.GroupBox();
			this.groupBoxSelectedMarks = new System.Windows.Forms.GroupBox();
			this.panel3 = new System.Windows.Forms.Panel();
			this.buttonIncreaseSelectedMarks = new System.Windows.Forms.Button();
			this.buttonDecreaseSelectedMarks = new System.Windows.Forms.Button();
			this.label7 = new System.Windows.Forms.Label();
			this.textBoxTimeIncrement = new System.Windows.Forms.TextBox();
			this.groupBoxDetails = new System.Windows.Forms.GroupBox();
			this.checkBoxEnabled = new System.Windows.Forms.CheckBox();
			this.label3 = new System.Windows.Forms.Label();
			this.panelColor = new System.Windows.Forms.Panel();
			this.label2 = new System.Windows.Forms.Label();
			this.numericUpDownWeight = new System.Windows.Forms.NumericUpDown();
			this.textBoxCollectionName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBoxOperations = new System.Windows.Forms.GroupBox();
			this.buttonGenerateGrid = new System.Windows.Forms.Button();
			this.buttonGenerateBeatMarks = new System.Windows.Forms.Button();
			this.buttonCopyAndOffsetMarks = new System.Windows.Forms.Button();
			this.buttonPasteEffectsToMarks = new System.Windows.Forms.Button();
			this.buttonOffsetMarks = new System.Windows.Forms.Button();
			this.buttonGenerateSubmarks = new System.Windows.Forms.Button();
			this.buttonEvenlySpaceMarks = new System.Windows.Forms.Button();
			this.groupBoxMarks = new System.Windows.Forms.GroupBox();
			this.listViewMarks = new System.Windows.Forms.ListView();
			this.Times = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.buttonAddOrUpdateMark = new System.Windows.Forms.Button();
			this.textBoxTime = new System.Windows.Forms.TextBox();
			this.buttonSelectAllMarks = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.groupBoxPlayback = new System.Windows.Forms.GroupBox();
			this.panel4 = new System.Windows.Forms.Panel();
			this.labelTapperInstructions = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.buttonIncreasePlaybackSpeed = new System.Windows.Forms.Button();
			this.buttonDecreasePlaySpeed = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.buttonPlay = new System.Windows.Forms.Button();
			this.buttonStop = new System.Windows.Forms.Button();
			this.groupBoxFreqDetection = new System.Windows.Forms.GroupBox();
			this.btnCreateCollections = new System.Windows.Forms.Button();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.radioSelected = new System.Windows.Forms.RadioButton();
			this.radioAll = new System.Windows.Forms.RadioButton();
			this.btnAutoDetectionSettings = new System.Windows.Forms.Button();
			this.ChkAutoTapper = new System.Windows.Forms.CheckBox();
			this.groupBoxAudioFilter = new System.Windows.Forms.GroupBox();
			this.numHighPass = new System.Windows.Forms.NumericUpDown();
			this.numLowPass = new System.Windows.Forms.NumericUpDown();
			this.chkHighPass = new System.Windows.Forms.CheckBox();
			this.chkLowPass = new System.Windows.Forms.CheckBox();
			this.label6 = new System.Windows.Forms.Label();
			this.trackBarPlayBack = new System.Windows.Forms.TrackBar();
			this.textBoxTimingSpeed = new System.Windows.Forms.TextBox();
			this.textBoxPosition = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.panelMarkView = new System.Windows.Forms.Panel();
			this.lblLastMarkHit = new System.Windows.Forms.Label();
			this.textBoxCurrentMark = new System.Windows.Forms.TextBox();
			this.groupBoxMode = new System.Windows.Forms.GroupBox();
			this.radioButtonPlayback = new System.Windows.Forms.RadioButton();
			this.radioButtonTapper = new System.Windows.Forms.RadioButton();
			this.timerPlayback = new System.Windows.Forms.Timer(this.components);
			this.timerMarkHit = new System.Windows.Forms.Timer(this.components);
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.groupBoxMarkCollections.SuspendLayout();
			this.panelMarkCollectionsButtons.SuspendLayout();
			this.groupBoxSelectedMarkCollection.SuspendLayout();
			this.groupBoxSelectedMarks.SuspendLayout();
			this.panel3.SuspendLayout();
			this.groupBoxDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownWeight)).BeginInit();
			this.groupBoxOperations.SuspendLayout();
			this.groupBoxMarks.SuspendLayout();
			this.groupBoxPlayback.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.groupBoxFreqDetection.SuspendLayout();
			this.groupBoxAudioFilter.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numHighPass)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numLowPass)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarPlayBack)).BeginInit();
			this.groupBoxMode.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBoxMarkCollections
			// 
			this.groupBoxMarkCollections.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
			this.groupBoxMarkCollections.Controls.Add(this.panelMarkCollectionsButtons);
			this.groupBoxMarkCollections.Controls.Add(this.label12);
			this.groupBoxMarkCollections.Controls.Add(this.label11);
			this.groupBoxMarkCollections.Controls.Add(this.label10);
			this.groupBoxMarkCollections.Controls.Add(this.listViewMarkCollections);
			this.groupBoxMarkCollections.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.groupBoxMarkCollections.Location = new System.Drawing.Point(12, 6);
			this.groupBoxMarkCollections.Name = "groupBoxMarkCollections";
			this.groupBoxMarkCollections.Size = new System.Drawing.Size(230, 328);
			this.groupBoxMarkCollections.TabIndex = 0;
			this.groupBoxMarkCollections.TabStop = false;
			this.groupBoxMarkCollections.Text = "Mark Collections";
			this.groupBoxMarkCollections.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBoxes_Paint);
			// 
			// panelMarkCollectionsButtons
			// 
			this.panelMarkCollectionsButtons.Controls.Add(this.buttonExportBeatMarks);
			this.panelMarkCollectionsButtons.Controls.Add(this.buttonRemoveCollection);
			this.panelMarkCollectionsButtons.Controls.Add(this.buttonImportAudacity);
			this.panelMarkCollectionsButtons.Controls.Add(this.buttonAddCollection);
			this.panelMarkCollectionsButtons.Location = new System.Drawing.Point(5, 231);
			this.panelMarkCollectionsButtons.Margin = new System.Windows.Forms.Padding(2);
			this.panelMarkCollectionsButtons.Name = "panelMarkCollectionsButtons";
			this.panelMarkCollectionsButtons.Size = new System.Drawing.Size(221, 92);
			this.panelMarkCollectionsButtons.TabIndex = 3;
			this.panelMarkCollectionsButtons.EnabledChanged += new System.EventHandler(this.panelMarkCollectionsButtons_EnabledChanged);
			// 
			// buttonExportBeatMarks
			// 
			this.buttonExportBeatMarks.BackColor = System.Drawing.Color.Transparent;
			this.buttonExportBeatMarks.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.buttonExportBeatMarks.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonExportBeatMarks.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.buttonExportBeatMarks.Location = new System.Drawing.Point(6, 63);
			this.buttonExportBeatMarks.Name = "buttonExportBeatMarks";
			this.buttonExportBeatMarks.Size = new System.Drawing.Size(214, 23);
			this.buttonExportBeatMarks.TabIndex = 4;
			this.buttonExportBeatMarks.Text = "Export Beat Marks";
			this.buttonExportBeatMarks.UseVisualStyleBackColor = false;
			this.buttonExportBeatMarks.Click += new System.EventHandler(this.buttonExportBeatMarks_Click);
			this.buttonExportBeatMarks.MouseLeave += new System.EventHandler(this.buttonBackground_MouseLeave);
			this.buttonExportBeatMarks.MouseHover += new System.EventHandler(this.buttonBackground_MouseHover);
			// 
			// buttonRemoveCollection
			// 
			this.buttonRemoveCollection.BackColor = System.Drawing.Color.Transparent;
			this.buttonRemoveCollection.Enabled = false;
			this.buttonRemoveCollection.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.buttonRemoveCollection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonRemoveCollection.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.buttonRemoveCollection.Location = new System.Drawing.Point(114, 3);
			this.buttonRemoveCollection.Name = "buttonRemoveCollection";
			this.buttonRemoveCollection.Size = new System.Drawing.Size(104, 25);
			this.buttonRemoveCollection.TabIndex = 2;
			this.buttonRemoveCollection.Text = "Remove Collection";
			this.buttonRemoveCollection.UseVisualStyleBackColor = false;
			this.buttonRemoveCollection.EnabledChanged += new System.EventHandler(this.buttonTextColorChange);
			this.buttonRemoveCollection.Click += new System.EventHandler(this.buttonRemoveCollection_Click);
			this.buttonRemoveCollection.MouseLeave += new System.EventHandler(this.buttonBackground_MouseLeave);
			this.buttonRemoveCollection.MouseHover += new System.EventHandler(this.buttonBackground_MouseHover);
			// 
			// buttonImportAudacity
			// 
			this.buttonImportAudacity.BackColor = System.Drawing.Color.Transparent;
			this.buttonImportAudacity.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.buttonImportAudacity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonImportAudacity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.buttonImportAudacity.Location = new System.Drawing.Point(6, 34);
			this.buttonImportAudacity.Name = "buttonImportAudacity";
			this.buttonImportAudacity.Size = new System.Drawing.Size(212, 23);
			this.buttonImportAudacity.TabIndex = 3;
			this.buttonImportAudacity.Text = "Import Beat Marks";
			this.buttonImportAudacity.UseVisualStyleBackColor = false;
			this.buttonImportAudacity.Click += new System.EventHandler(this.buttonImportAudacity_Click);
			this.buttonImportAudacity.MouseLeave += new System.EventHandler(this.buttonBackground_MouseLeave);
			this.buttonImportAudacity.MouseHover += new System.EventHandler(this.buttonBackground_MouseHover);
			// 
			// buttonAddCollection
			// 
			this.buttonAddCollection.BackColor = System.Drawing.Color.Transparent;
			this.buttonAddCollection.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.buttonAddCollection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonAddCollection.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.buttonAddCollection.Location = new System.Drawing.Point(6, 3);
			this.buttonAddCollection.Name = "buttonAddCollection";
			this.buttonAddCollection.Size = new System.Drawing.Size(103, 25);
			this.buttonAddCollection.TabIndex = 1;
			this.buttonAddCollection.Text = "Add Collection";
			this.buttonAddCollection.UseVisualStyleBackColor = false;
			this.buttonAddCollection.Click += new System.EventHandler(this.buttonAddCollection_Click);
			this.buttonAddCollection.MouseLeave += new System.EventHandler(this.buttonBackground_MouseLeave);
			this.buttonAddCollection.MouseHover += new System.EventHandler(this.buttonBackground_MouseHover);
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(179, 18);
			this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(36, 13);
			this.label12.TabIndex = 7;
			this.label12.Text = "Marks";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(117, 18);
			this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(41, 13);
			this.label11.TabIndex = 6;
			this.label11.Text = "Weight";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(6, 18);
			this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(35, 13);
			this.label10.TabIndex = 5;
			this.label10.Text = "Name";
			// 
			// listViewMarkCollections
			// 
			this.listViewMarkCollections.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
			this.listViewMarkCollections.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.listViewMarkCollections.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader3,
            this.columnHeader2});
			this.listViewMarkCollections.FullRowSelect = true;
			this.listViewMarkCollections.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.listViewMarkCollections.HideSelection = false;
			this.listViewMarkCollections.Location = new System.Drawing.Point(6, 34);
			this.listViewMarkCollections.Name = "listViewMarkCollections";
			this.listViewMarkCollections.Size = new System.Drawing.Size(221, 193);
			this.listViewMarkCollections.TabIndex = 0;
			this.listViewMarkCollections.UseCompatibleStateImageBehavior = false;
			this.listViewMarkCollections.View = System.Windows.Forms.View.Details;
			this.listViewMarkCollections.SelectedIndexChanged += new System.EventHandler(this.listViewMarkCollections_SelectedIndexChanged);
			this.listViewMarkCollections.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listViewMarkCollections_KeyDown);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Name";
			this.columnHeader1.Width = 125;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Weight";
			this.columnHeader3.Width = 48;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Marks";
			this.columnHeader2.Width = 43;
			// 
			// buttonOK
			// 
			this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOK.BackColor = System.Drawing.Color.Transparent;
			this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOK.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.buttonOK.Location = new System.Drawing.Point(564, 577);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(80, 25);
			this.buttonOK.TabIndex = 3;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = false;
			this.buttonOK.MouseLeave += new System.EventHandler(this.buttonBackground_MouseLeave);
			this.buttonOK.MouseHover += new System.EventHandler(this.buttonBackground_MouseHover);
			// 
			// groupBoxSelectedMarkCollection
			// 
			this.groupBoxSelectedMarkCollection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
			this.groupBoxSelectedMarkCollection.Controls.Add(this.groupBoxSelectedMarks);
			this.groupBoxSelectedMarkCollection.Controls.Add(this.groupBoxDetails);
			this.groupBoxSelectedMarkCollection.Controls.Add(this.groupBoxOperations);
			this.groupBoxSelectedMarkCollection.Controls.Add(this.groupBoxMarks);
			this.groupBoxSelectedMarkCollection.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.groupBoxSelectedMarkCollection.Location = new System.Drawing.Point(247, 6);
			this.groupBoxSelectedMarkCollection.Name = "groupBoxSelectedMarkCollection";
			this.groupBoxSelectedMarkCollection.Size = new System.Drawing.Size(482, 329);
			this.groupBoxSelectedMarkCollection.TabIndex = 1;
			this.groupBoxSelectedMarkCollection.TabStop = false;
			this.groupBoxSelectedMarkCollection.Text = "Selected Collection";
			this.groupBoxSelectedMarkCollection.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBoxes_Paint);
			// 
			// groupBoxSelectedMarks
			// 
			this.groupBoxSelectedMarks.Controls.Add(this.panel3);
			this.groupBoxSelectedMarks.Controls.Add(this.label7);
			this.groupBoxSelectedMarks.Controls.Add(this.textBoxTimeIncrement);
			this.groupBoxSelectedMarks.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.groupBoxSelectedMarks.Location = new System.Drawing.Point(307, 262);
			this.groupBoxSelectedMarks.Name = "groupBoxSelectedMarks";
			this.groupBoxSelectedMarks.Size = new System.Drawing.Size(164, 53);
			this.groupBoxSelectedMarks.TabIndex = 7;
			this.groupBoxSelectedMarks.TabStop = false;
			this.groupBoxSelectedMarks.Text = "SelectedMarks";
			this.groupBoxSelectedMarks.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBoxes_Paint);
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.buttonIncreaseSelectedMarks);
			this.panel3.Controls.Add(this.buttonDecreaseSelectedMarks);
			this.panel3.Location = new System.Drawing.Point(75, 16);
			this.panel3.Margin = new System.Windows.Forms.Padding(2);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(74, 34);
			this.panel3.TabIndex = 14;
			// 
			// buttonIncreaseSelectedMarks
			// 
			this.buttonIncreaseSelectedMarks.FlatAppearance.BorderSize = 0;
			this.buttonIncreaseSelectedMarks.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
			this.buttonIncreaseSelectedMarks.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.buttonIncreaseSelectedMarks.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.buttonIncreaseSelectedMarks.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonIncreaseSelectedMarks.Location = new System.Drawing.Point(1, -1);
			this.buttonIncreaseSelectedMarks.Name = "buttonIncreaseSelectedMarks";
			this.buttonIncreaseSelectedMarks.Size = new System.Drawing.Size(32, 32);
			this.buttonIncreaseSelectedMarks.TabIndex = 1;
			this.buttonIncreaseSelectedMarks.Text = "+";
			this.buttonIncreaseSelectedMarks.UseVisualStyleBackColor = true;
			this.buttonIncreaseSelectedMarks.Click += new System.EventHandler(this.buttonIncreaseSelectedMarks_Click);
			// 
			// buttonDecreaseSelectedMarks
			// 
			this.buttonDecreaseSelectedMarks.FlatAppearance.BorderSize = 0;
			this.buttonDecreaseSelectedMarks.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
			this.buttonDecreaseSelectedMarks.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.buttonDecreaseSelectedMarks.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.buttonDecreaseSelectedMarks.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonDecreaseSelectedMarks.Location = new System.Drawing.Point(36, -1);
			this.buttonDecreaseSelectedMarks.Name = "buttonDecreaseSelectedMarks";
			this.buttonDecreaseSelectedMarks.Size = new System.Drawing.Size(32, 32);
			this.buttonDecreaseSelectedMarks.TabIndex = 2;
			this.buttonDecreaseSelectedMarks.Text = "-";
			this.buttonDecreaseSelectedMarks.UseVisualStyleBackColor = true;
			this.buttonDecreaseSelectedMarks.Click += new System.EventHandler(this.buttonDecreaseSelectedMarks_Click);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(54, 32);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(20, 13);
			this.label7.TabIndex = 13;
			this.label7.Text = "ms";
			// 
			// textBoxTimeIncrement
			// 
			this.textBoxTimeIncrement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
			this.textBoxTimeIncrement.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxTimeIncrement.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.textBoxTimeIncrement.Location = new System.Drawing.Point(14, 25);
			this.textBoxTimeIncrement.Name = "textBoxTimeIncrement";
			this.textBoxTimeIncrement.Size = new System.Drawing.Size(38, 20);
			this.textBoxTimeIncrement.TabIndex = 0;
			this.textBoxTimeIncrement.Text = "10";
			// 
			// groupBoxDetails
			// 
			this.groupBoxDetails.Controls.Add(this.checkBoxEnabled);
			this.groupBoxDetails.Controls.Add(this.label3);
			this.groupBoxDetails.Controls.Add(this.panelColor);
			this.groupBoxDetails.Controls.Add(this.label2);
			this.groupBoxDetails.Controls.Add(this.numericUpDownWeight);
			this.groupBoxDetails.Controls.Add(this.textBoxCollectionName);
			this.groupBoxDetails.Controls.Add(this.label1);
			this.groupBoxDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.groupBoxDetails.Location = new System.Drawing.Point(6, 19);
			this.groupBoxDetails.Name = "groupBoxDetails";
			this.groupBoxDetails.Size = new System.Drawing.Size(154, 153);
			this.groupBoxDetails.TabIndex = 0;
			this.groupBoxDetails.TabStop = false;
			this.groupBoxDetails.Text = "Details";
			this.groupBoxDetails.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBoxes_Paint);
			// 
			// checkBoxEnabled
			// 
			this.checkBoxEnabled.AutoSize = true;
			this.checkBoxEnabled.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.checkBoxEnabled.Location = new System.Drawing.Point(17, 27);
			this.checkBoxEnabled.Name = "checkBoxEnabled";
			this.checkBoxEnabled.Size = new System.Drawing.Size(65, 17);
			this.checkBoxEnabled.TabIndex = 0;
			this.checkBoxEnabled.Text = "Enabled";
			this.checkBoxEnabled.UseVisualStyleBackColor = true;
			this.checkBoxEnabled.CheckedChanged += new System.EventHandler(this.checkBoxEnabled_CheckedChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.label3.Location = new System.Drawing.Point(18, 114);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(34, 13);
			this.label3.TabIndex = 13;
			this.label3.Text = "Color:";
			// 
			// panelColor
			// 
			this.panelColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelColor.Location = new System.Drawing.Point(58, 109);
			this.panelColor.Name = "panelColor";
			this.panelColor.Size = new System.Drawing.Size(60, 25);
			this.panelColor.TabIndex = 3;
			this.panelColor.Click += new System.EventHandler(this.panelColor_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.label2.Location = new System.Drawing.Point(8, 85);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(44, 13);
			this.label2.TabIndex = 11;
			this.label2.Text = "Weight:";
			// 
			// numericUpDownWeight
			// 
			this.numericUpDownWeight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
			this.numericUpDownWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.numericUpDownWeight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.numericUpDownWeight.Location = new System.Drawing.Point(58, 83);
			this.numericUpDownWeight.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
			this.numericUpDownWeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDownWeight.Name = "numericUpDownWeight";
			this.numericUpDownWeight.Size = new System.Drawing.Size(46, 20);
			this.numericUpDownWeight.TabIndex = 2;
			this.numericUpDownWeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDownWeight.ValueChanged += new System.EventHandler(this.numericUpDownWeight_ValueChanged);
			// 
			// textBoxCollectionName
			// 
			this.textBoxCollectionName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
			this.textBoxCollectionName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxCollectionName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.textBoxCollectionName.Location = new System.Drawing.Point(58, 57);
			this.textBoxCollectionName.Name = "textBoxCollectionName";
			this.textBoxCollectionName.Size = new System.Drawing.Size(86, 20);
			this.textBoxCollectionName.TabIndex = 1;
			this.textBoxCollectionName.TextChanged += new System.EventHandler(this.textBoxCollectionName_TextChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.label1.Location = new System.Drawing.Point(14, 60);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(38, 13);
			this.label1.TabIndex = 8;
			this.label1.Text = "Name:";
			// 
			// groupBoxOperations
			// 
			this.groupBoxOperations.Controls.Add(this.buttonGenerateGrid);
			this.groupBoxOperations.Controls.Add(this.buttonGenerateBeatMarks);
			this.groupBoxOperations.Controls.Add(this.buttonCopyAndOffsetMarks);
			this.groupBoxOperations.Controls.Add(this.buttonPasteEffectsToMarks);
			this.groupBoxOperations.Controls.Add(this.buttonOffsetMarks);
			this.groupBoxOperations.Controls.Add(this.buttonGenerateSubmarks);
			this.groupBoxOperations.Controls.Add(this.buttonEvenlySpaceMarks);
			this.groupBoxOperations.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.groupBoxOperations.Location = new System.Drawing.Point(307, 19);
			this.groupBoxOperations.Name = "groupBoxOperations";
			this.groupBoxOperations.Size = new System.Drawing.Size(164, 240);
			this.groupBoxOperations.TabIndex = 2;
			this.groupBoxOperations.TabStop = false;
			this.groupBoxOperations.Text = "Operations";
			this.groupBoxOperations.EnabledChanged += new System.EventHandler(this.groupBoxOperations_EnabledChanged);
			this.groupBoxOperations.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBoxes_Paint);
			// 
			// buttonGenerateGrid
			// 
			this.buttonGenerateGrid.BackColor = System.Drawing.Color.Transparent;
			this.buttonGenerateGrid.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.buttonGenerateGrid.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonGenerateGrid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.buttonGenerateGrid.Location = new System.Drawing.Point(8, 203);
			this.buttonGenerateGrid.Name = "buttonGenerateGrid";
			this.buttonGenerateGrid.Size = new System.Drawing.Size(151, 25);
			this.buttonGenerateGrid.TabIndex = 6;
			this.buttonGenerateGrid.Text = "Generate Grid";
			this.toolTip1.SetToolTip(this.buttonGenerateGrid, "Generate a \'grid\' of equally space marks across the sequence.");
			this.buttonGenerateGrid.UseVisualStyleBackColor = false;
			this.buttonGenerateGrid.EnabledChanged += new System.EventHandler(this.buttonTextColorChange);
			this.buttonGenerateGrid.Click += new System.EventHandler(this.buttonGenerateGrid_Click);
			this.buttonGenerateGrid.MouseLeave += new System.EventHandler(this.buttonBackground_MouseLeave);
			this.buttonGenerateGrid.MouseHover += new System.EventHandler(this.buttonBackground_MouseHover);
			// 
			// buttonGenerateBeatMarks
			// 
			this.buttonGenerateBeatMarks.BackColor = System.Drawing.Color.Transparent;
			this.buttonGenerateBeatMarks.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.buttonGenerateBeatMarks.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonGenerateBeatMarks.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.buttonGenerateBeatMarks.Location = new System.Drawing.Point(7, 172);
			this.buttonGenerateBeatMarks.Name = "buttonGenerateBeatMarks";
			this.buttonGenerateBeatMarks.Size = new System.Drawing.Size(151, 25);
			this.buttonGenerateBeatMarks.TabIndex = 5;
			this.buttonGenerateBeatMarks.Text = "Generate beat marks";
			this.toolTip1.SetToolTip(this.buttonGenerateBeatMarks, "Generate more marks based on the frequency of the selected marks.");
			this.buttonGenerateBeatMarks.UseVisualStyleBackColor = false;
			this.buttonGenerateBeatMarks.EnabledChanged += new System.EventHandler(this.buttonTextColorChange);
			this.buttonGenerateBeatMarks.Click += new System.EventHandler(this.buttonGenerateBeatMarks_Click);
			this.buttonGenerateBeatMarks.MouseLeave += new System.EventHandler(this.buttonBackground_MouseLeave);
			this.buttonGenerateBeatMarks.MouseHover += new System.EventHandler(this.buttonBackground_MouseHover);
			// 
			// buttonCopyAndOffsetMarks
			// 
			this.buttonCopyAndOffsetMarks.BackColor = System.Drawing.Color.Transparent;
			this.buttonCopyAndOffsetMarks.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.buttonCopyAndOffsetMarks.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonCopyAndOffsetMarks.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.buttonCopyAndOffsetMarks.Location = new System.Drawing.Point(7, 141);
			this.buttonCopyAndOffsetMarks.Name = "buttonCopyAndOffsetMarks";
			this.buttonCopyAndOffsetMarks.Size = new System.Drawing.Size(151, 25);
			this.buttonCopyAndOffsetMarks.TabIndex = 4;
			this.buttonCopyAndOffsetMarks.Text = "Copy && offset marks";
			this.toolTip1.SetToolTip(this.buttonCopyAndOffsetMarks, "Duplicate the selected marks, offsetting the new ones by a fixed amount of time.");
			this.buttonCopyAndOffsetMarks.UseVisualStyleBackColor = false;
			this.buttonCopyAndOffsetMarks.EnabledChanged += new System.EventHandler(this.buttonTextColorChange);
			this.buttonCopyAndOffsetMarks.Click += new System.EventHandler(this.buttonCopyAndOffsetMarks_Click);
			this.buttonCopyAndOffsetMarks.MouseLeave += new System.EventHandler(this.buttonBackground_MouseLeave);
			this.buttonCopyAndOffsetMarks.MouseHover += new System.EventHandler(this.buttonBackground_MouseHover);
			// 
			// buttonPasteEffectsToMarks
			// 
			this.buttonPasteEffectsToMarks.BackColor = System.Drawing.Color.Transparent;
			this.buttonPasteEffectsToMarks.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.buttonPasteEffectsToMarks.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonPasteEffectsToMarks.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.buttonPasteEffectsToMarks.Location = new System.Drawing.Point(7, 112);
			this.buttonPasteEffectsToMarks.Name = "buttonPasteEffectsToMarks";
			this.buttonPasteEffectsToMarks.Size = new System.Drawing.Size(151, 25);
			this.buttonPasteEffectsToMarks.TabIndex = 3;
			this.buttonPasteEffectsToMarks.Text = "Paste effect to marks";
			this.toolTip1.SetToolTip(this.buttonPasteEffectsToMarks, "Place a copy of the effect currently in the paste buffer to begin at each selecte" +
        "d mark.");
			this.buttonPasteEffectsToMarks.UseVisualStyleBackColor = false;
			this.buttonPasteEffectsToMarks.EnabledChanged += new System.EventHandler(this.buttonTextColorChange);
			this.buttonPasteEffectsToMarks.Click += new System.EventHandler(this.buttonPasteEffectsToMarks_Click);
			this.buttonPasteEffectsToMarks.MouseLeave += new System.EventHandler(this.buttonBackground_MouseLeave);
			this.buttonPasteEffectsToMarks.MouseHover += new System.EventHandler(this.buttonBackground_MouseHover);
			// 
			// buttonOffsetMarks
			// 
			this.buttonOffsetMarks.BackColor = System.Drawing.Color.Transparent;
			this.buttonOffsetMarks.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.buttonOffsetMarks.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonOffsetMarks.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.buttonOffsetMarks.Location = new System.Drawing.Point(7, 19);
			this.buttonOffsetMarks.Name = "buttonOffsetMarks";
			this.buttonOffsetMarks.Size = new System.Drawing.Size(151, 25);
			this.buttonOffsetMarks.TabIndex = 0;
			this.buttonOffsetMarks.Text = "Offset marks";
			this.toolTip1.SetToolTip(this.buttonOffsetMarks, "Adjust selected marks left or right a fixed amount of time.");
			this.buttonOffsetMarks.UseVisualStyleBackColor = false;
			this.buttonOffsetMarks.EnabledChanged += new System.EventHandler(this.buttonTextColorChange);
			this.buttonOffsetMarks.Click += new System.EventHandler(this.buttonOffsetMarks_Click);
			this.buttonOffsetMarks.MouseLeave += new System.EventHandler(this.buttonBackground_MouseLeave);
			this.buttonOffsetMarks.MouseHover += new System.EventHandler(this.buttonBackground_MouseHover);
			// 
			// buttonGenerateSubmarks
			// 
			this.buttonGenerateSubmarks.BackColor = System.Drawing.Color.Transparent;
			this.buttonGenerateSubmarks.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.buttonGenerateSubmarks.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonGenerateSubmarks.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.buttonGenerateSubmarks.Location = new System.Drawing.Point(7, 81);
			this.buttonGenerateSubmarks.Name = "buttonGenerateSubmarks";
			this.buttonGenerateSubmarks.Size = new System.Drawing.Size(151, 25);
			this.buttonGenerateSubmarks.TabIndex = 2;
			this.buttonGenerateSubmarks.Text = "Generate submarks";
			this.toolTip1.SetToolTip(this.buttonGenerateSubmarks, "Create new marks by subdividing regions of other marks (select at least 2).");
			this.buttonGenerateSubmarks.UseVisualStyleBackColor = false;
			this.buttonGenerateSubmarks.EnabledChanged += new System.EventHandler(this.buttonTextColorChange);
			this.buttonGenerateSubmarks.Click += new System.EventHandler(this.buttonGenerateSubmarks_Click);
			this.buttonGenerateSubmarks.MouseLeave += new System.EventHandler(this.buttonBackground_MouseLeave);
			this.buttonGenerateSubmarks.MouseHover += new System.EventHandler(this.buttonBackground_MouseHover);
			// 
			// buttonEvenlySpaceMarks
			// 
			this.buttonEvenlySpaceMarks.BackColor = System.Drawing.Color.Transparent;
			this.buttonEvenlySpaceMarks.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.buttonEvenlySpaceMarks.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonEvenlySpaceMarks.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.buttonEvenlySpaceMarks.Location = new System.Drawing.Point(7, 50);
			this.buttonEvenlySpaceMarks.Name = "buttonEvenlySpaceMarks";
			this.buttonEvenlySpaceMarks.Size = new System.Drawing.Size(151, 25);
			this.buttonEvenlySpaceMarks.TabIndex = 1;
			this.buttonEvenlySpaceMarks.Text = "Evenly space marks";
			this.toolTip1.SetToolTip(this.buttonEvenlySpaceMarks, "Evenly space out the selected marks (choose at least 3 marks).");
			this.buttonEvenlySpaceMarks.UseVisualStyleBackColor = false;
			this.buttonEvenlySpaceMarks.EnabledChanged += new System.EventHandler(this.buttonTextColorChange);
			this.buttonEvenlySpaceMarks.Click += new System.EventHandler(this.buttonEvenlySpaceMarks_Click);
			this.buttonEvenlySpaceMarks.MouseLeave += new System.EventHandler(this.buttonBackground_MouseLeave);
			this.buttonEvenlySpaceMarks.MouseHover += new System.EventHandler(this.buttonBackground_MouseHover);
			// 
			// groupBoxMarks
			// 
			this.groupBoxMarks.Controls.Add(this.listViewMarks);
			this.groupBoxMarks.Controls.Add(this.buttonAddOrUpdateMark);
			this.groupBoxMarks.Controls.Add(this.textBoxTime);
			this.groupBoxMarks.Controls.Add(this.buttonSelectAllMarks);
			this.groupBoxMarks.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.groupBoxMarks.Location = new System.Drawing.Point(166, 19);
			this.groupBoxMarks.Name = "groupBoxMarks";
			this.groupBoxMarks.Size = new System.Drawing.Size(136, 296);
			this.groupBoxMarks.TabIndex = 1;
			this.groupBoxMarks.TabStop = false;
			this.groupBoxMarks.Text = "Marks";
			this.groupBoxMarks.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBoxes_Paint);
			// 
			// listViewMarks
			// 
			this.listViewMarks.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
			this.listViewMarks.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.listViewMarks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Times});
			this.listViewMarks.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.listViewMarks.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.listViewMarks.HideSelection = false;
			this.listViewMarks.Location = new System.Drawing.Point(6, 19);
			this.listViewMarks.Name = "listViewMarks";
			this.listViewMarks.Size = new System.Drawing.Size(126, 203);
			this.listViewMarks.TabIndex = 0;
			this.listViewMarks.UseCompatibleStateImageBehavior = false;
			this.listViewMarks.View = System.Windows.Forms.View.Details;
			this.listViewMarks.SelectedIndexChanged += new System.EventHandler(this.listViewMarks_SelectedIndexChanged);
			this.listViewMarks.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listViewMarks_KeyDown);
			// 
			// Times
			// 
			this.Times.Width = 90;
			// 
			// buttonAddOrUpdateMark
			// 
			this.buttonAddOrUpdateMark.BackColor = System.Drawing.Color.Transparent;
			this.buttonAddOrUpdateMark.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.buttonAddOrUpdateMark.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonAddOrUpdateMark.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.buttonAddOrUpdateMark.Location = new System.Drawing.Point(72, 228);
			this.buttonAddOrUpdateMark.Name = "buttonAddOrUpdateMark";
			this.buttonAddOrUpdateMark.Size = new System.Drawing.Size(59, 25);
			this.buttonAddOrUpdateMark.TabIndex = 2;
			this.buttonAddOrUpdateMark.Text = "Add";
			this.buttonAddOrUpdateMark.UseVisualStyleBackColor = false;
			this.buttonAddOrUpdateMark.EnabledChanged += new System.EventHandler(this.buttonTextColorChange);
			this.buttonAddOrUpdateMark.Click += new System.EventHandler(this.buttonAddOrUpdateMark_Click);
			this.buttonAddOrUpdateMark.MouseLeave += new System.EventHandler(this.buttonBackground_MouseLeave);
			this.buttonAddOrUpdateMark.MouseHover += new System.EventHandler(this.buttonBackground_MouseHover);
			// 
			// textBoxTime
			// 
			this.textBoxTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
			this.textBoxTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.textBoxTime.Location = new System.Drawing.Point(6, 231);
			this.textBoxTime.Name = "textBoxTime";
			this.textBoxTime.Size = new System.Drawing.Size(59, 20);
			this.textBoxTime.TabIndex = 1;
			// 
			// buttonSelectAllMarks
			// 
			this.buttonSelectAllMarks.BackColor = System.Drawing.Color.Transparent;
			this.buttonSelectAllMarks.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.buttonSelectAllMarks.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonSelectAllMarks.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.buttonSelectAllMarks.Location = new System.Drawing.Point(6, 259);
			this.buttonSelectAllMarks.Name = "buttonSelectAllMarks";
			this.buttonSelectAllMarks.Size = new System.Drawing.Size(125, 25);
			this.buttonSelectAllMarks.TabIndex = 3;
			this.buttonSelectAllMarks.Text = "Select All";
			this.buttonSelectAllMarks.UseVisualStyleBackColor = false;
			this.buttonSelectAllMarks.EnabledChanged += new System.EventHandler(this.buttonTextColorChange);
			this.buttonSelectAllMarks.Click += new System.EventHandler(this.buttonSelectAllMarks_Click);
			this.buttonSelectAllMarks.MouseLeave += new System.EventHandler(this.buttonBackground_MouseLeave);
			this.buttonSelectAllMarks.MouseHover += new System.EventHandler(this.buttonBackground_MouseHover);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.BackColor = System.Drawing.Color.Transparent;
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.buttonCancel.Location = new System.Drawing.Point(649, 577);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(80, 25);
			this.buttonCancel.TabIndex = 4;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = false;
			this.buttonCancel.MouseLeave += new System.EventHandler(this.buttonBackground_MouseLeave);
			this.buttonCancel.MouseHover += new System.EventHandler(this.buttonBackground_MouseHover);
			// 
			// groupBoxPlayback
			// 
			this.groupBoxPlayback.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
			this.groupBoxPlayback.Controls.Add(this.panel4);
			this.groupBoxPlayback.Controls.Add(this.panel2);
			this.groupBoxPlayback.Controls.Add(this.panel1);
			this.groupBoxPlayback.Controls.Add(this.groupBoxFreqDetection);
			this.groupBoxPlayback.Controls.Add(this.groupBoxAudioFilter);
			this.groupBoxPlayback.Controls.Add(this.label6);
			this.groupBoxPlayback.Controls.Add(this.trackBarPlayBack);
			this.groupBoxPlayback.Controls.Add(this.textBoxTimingSpeed);
			this.groupBoxPlayback.Controls.Add(this.textBoxPosition);
			this.groupBoxPlayback.Controls.Add(this.label5);
			this.groupBoxPlayback.Controls.Add(this.panelMarkView);
			this.groupBoxPlayback.Controls.Add(this.lblLastMarkHit);
			this.groupBoxPlayback.Controls.Add(this.textBoxCurrentMark);
			this.groupBoxPlayback.Controls.Add(this.groupBoxMode);
			this.groupBoxPlayback.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.groupBoxPlayback.Location = new System.Drawing.Point(12, 339);
			this.groupBoxPlayback.Name = "groupBoxPlayback";
			this.groupBoxPlayback.Size = new System.Drawing.Size(717, 229);
			this.groupBoxPlayback.TabIndex = 2;
			this.groupBoxPlayback.TabStop = false;
			this.groupBoxPlayback.Text = "Playback";
			this.groupBoxPlayback.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBoxes_Paint);
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.labelTapperInstructions);
			this.panel4.Location = new System.Drawing.Point(17, 198);
			this.panel4.Margin = new System.Windows.Forms.Padding(2);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(270, 19);
			this.panel4.TabIndex = 17;
			// 
			// labelTapperInstructions
			// 
			this.labelTapperInstructions.AutoSize = true;
			this.labelTapperInstructions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.labelTapperInstructions.Location = new System.Drawing.Point(3, 3);
			this.labelTapperInstructions.Name = "labelTapperInstructions";
			this.labelTapperInstructions.Size = new System.Drawing.Size(248, 13);
			this.labelTapperInstructions.TabIndex = 14;
			this.labelTapperInstructions.Text = "Click the box or use the spacebar to create a mark.";
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.buttonIncreasePlaybackSpeed);
			this.panel2.Controls.Add(this.buttonDecreasePlaySpeed);
			this.panel2.Location = new System.Drawing.Point(371, 18);
			this.panel2.Margin = new System.Windows.Forms.Padding(2);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(85, 43);
			this.panel2.TabIndex = 16;
			// 
			// buttonIncreasePlaybackSpeed
			// 
			this.buttonIncreasePlaybackSpeed.BackColor = System.Drawing.Color.Transparent;
			this.buttonIncreasePlaybackSpeed.FlatAppearance.BorderSize = 0;
			this.buttonIncreasePlaybackSpeed.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
			this.buttonIncreasePlaybackSpeed.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.buttonIncreasePlaybackSpeed.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.buttonIncreasePlaybackSpeed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonIncreasePlaybackSpeed.Location = new System.Drawing.Point(4, 5);
			this.buttonIncreasePlaybackSpeed.Name = "buttonIncreasePlaybackSpeed";
			this.buttonIncreasePlaybackSpeed.Size = new System.Drawing.Size(32, 32);
			this.buttonIncreasePlaybackSpeed.TabIndex = 4;
			this.buttonIncreasePlaybackSpeed.Text = "+";
			this.buttonIncreasePlaybackSpeed.UseVisualStyleBackColor = false;
			this.buttonIncreasePlaybackSpeed.Click += new System.EventHandler(this.buttonIncreasePlaySpeed_Click);
			// 
			// buttonDecreasePlaySpeed
			// 
			this.buttonDecreasePlaySpeed.BackColor = System.Drawing.Color.Transparent;
			this.buttonDecreasePlaySpeed.FlatAppearance.BorderSize = 0;
			this.buttonDecreasePlaySpeed.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
			this.buttonDecreasePlaySpeed.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.buttonDecreasePlaySpeed.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.buttonDecreasePlaySpeed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonDecreasePlaySpeed.Location = new System.Drawing.Point(39, 5);
			this.buttonDecreasePlaySpeed.Name = "buttonDecreasePlaySpeed";
			this.buttonDecreasePlaySpeed.Size = new System.Drawing.Size(32, 32);
			this.buttonDecreasePlaySpeed.TabIndex = 5;
			this.buttonDecreasePlaySpeed.Text = "-";
			this.buttonDecreasePlaySpeed.UseVisualStyleBackColor = false;
			this.buttonDecreasePlaySpeed.Click += new System.EventHandler(this.buttonDecreasePlaySpeed_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.buttonPlay);
			this.panel1.Controls.Add(this.buttonStop);
			this.panel1.Location = new System.Drawing.Point(26, 18);
			this.panel1.Margin = new System.Windows.Forms.Padding(2);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(92, 42);
			this.panel1.TabIndex = 15;
			// 
			// buttonPlay
			// 
			this.buttonPlay.BackColor = System.Drawing.Color.Transparent;
			this.buttonPlay.FlatAppearance.BorderSize = 0;
			this.buttonPlay.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.buttonPlay.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.buttonPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonPlay.Location = new System.Drawing.Point(9, 5);
			this.buttonPlay.Name = "buttonPlay";
			this.buttonPlay.Size = new System.Drawing.Size(32, 32);
			this.buttonPlay.TabIndex = 0;
			this.buttonPlay.Text = "Play";
			this.buttonPlay.UseVisualStyleBackColor = false;
			this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
			// 
			// buttonStop
			// 
			this.buttonStop.BackColor = System.Drawing.Color.Transparent;
			this.buttonStop.FlatAppearance.BorderSize = 0;
			this.buttonStop.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
			this.buttonStop.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.buttonStop.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.buttonStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonStop.Location = new System.Drawing.Point(43, 5);
			this.buttonStop.Name = "buttonStop";
			this.buttonStop.Size = new System.Drawing.Size(32, 32);
			this.buttonStop.TabIndex = 1;
			this.buttonStop.Text = "Stop";
			this.buttonStop.UseVisualStyleBackColor = false;
			this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
			// 
			// groupBoxFreqDetection
			// 
			this.groupBoxFreqDetection.Controls.Add(this.btnCreateCollections);
			this.groupBoxFreqDetection.Controls.Add(this.label9);
			this.groupBoxFreqDetection.Controls.Add(this.label8);
			this.groupBoxFreqDetection.Controls.Add(this.radioSelected);
			this.groupBoxFreqDetection.Controls.Add(this.radioAll);
			this.groupBoxFreqDetection.Controls.Add(this.btnAutoDetectionSettings);
			this.groupBoxFreqDetection.Controls.Add(this.ChkAutoTapper);
			this.groupBoxFreqDetection.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.groupBoxFreqDetection.Location = new System.Drawing.Point(291, 101);
			this.groupBoxFreqDetection.Name = "groupBoxFreqDetection";
			this.groupBoxFreqDetection.Size = new System.Drawing.Size(177, 118);
			this.groupBoxFreqDetection.TabIndex = 8;
			this.groupBoxFreqDetection.TabStop = false;
			this.groupBoxFreqDetection.Text = "Automatic Frequency Detection";
			this.groupBoxFreqDetection.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBoxes_Paint);
			// 
			// btnCreateCollections
			// 
			this.btnCreateCollections.BackColor = System.Drawing.Color.Transparent;
			this.btnCreateCollections.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.btnCreateCollections.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnCreateCollections.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.btnCreateCollections.Location = new System.Drawing.Point(90, 89);
			this.btnCreateCollections.Name = "btnCreateCollections";
			this.btnCreateCollections.Size = new System.Drawing.Size(75, 23);
			this.btnCreateCollections.TabIndex = 4;
			this.btnCreateCollections.Text = "Create";
			this.btnCreateCollections.UseVisualStyleBackColor = false;
			this.btnCreateCollections.EnabledChanged += new System.EventHandler(this.buttonTextColorChange);
			this.btnCreateCollections.Click += new System.EventHandler(this.btnCreateCollections_Click);
			this.btnCreateCollections.MouseLeave += new System.EventHandler(this.buttonBackground_MouseLeave);
			this.btnCreateCollections.MouseHover += new System.EventHandler(this.buttonBackground_MouseHover);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.label9.Location = new System.Drawing.Point(7, 52);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(119, 13);
			this.label9.TabIndex = 22;
			this.label9.Text = "Create Collection(s) For:";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(9, 39);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(7, 13);
			this.label8.TabIndex = 18;
			this.label8.Text = "\t";
			// 
			// radioSelected
			// 
			this.radioSelected.AutoSize = true;
			this.radioSelected.Location = new System.Drawing.Point(52, 72);
			this.radioSelected.Name = "radioSelected";
			this.radioSelected.Size = new System.Drawing.Size(67, 17);
			this.radioSelected.TabIndex = 3;
			this.radioSelected.Text = "Selected";
			this.radioSelected.UseVisualStyleBackColor = true;
			// 
			// radioAll
			// 
			this.radioAll.AutoSize = true;
			this.radioAll.Checked = true;
			this.radioAll.Location = new System.Drawing.Point(10, 72);
			this.radioAll.Name = "radioAll";
			this.radioAll.Size = new System.Drawing.Size(36, 17);
			this.radioAll.TabIndex = 2;
			this.radioAll.TabStop = true;
			this.radioAll.Text = "All";
			this.radioAll.UseVisualStyleBackColor = true;
			// 
			// btnAutoDetectionSettings
			// 
			this.btnAutoDetectionSettings.BackColor = System.Drawing.Color.Transparent;
			this.btnAutoDetectionSettings.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.btnAutoDetectionSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnAutoDetectionSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.btnAutoDetectionSettings.Location = new System.Drawing.Point(90, 20);
			this.btnAutoDetectionSettings.Name = "btnAutoDetectionSettings";
			this.btnAutoDetectionSettings.Size = new System.Drawing.Size(75, 23);
			this.btnAutoDetectionSettings.TabIndex = 1;
			this.btnAutoDetectionSettings.Text = "Settings";
			this.btnAutoDetectionSettings.UseVisualStyleBackColor = false;
			this.btnAutoDetectionSettings.EnabledChanged += new System.EventHandler(this.buttonTextColorChange);
			this.btnAutoDetectionSettings.Click += new System.EventHandler(this.btnAutoDetectionSettings_Click);
			this.btnAutoDetectionSettings.MouseLeave += new System.EventHandler(this.buttonBackground_MouseLeave);
			this.btnAutoDetectionSettings.MouseHover += new System.EventHandler(this.buttonBackground_MouseHover);
			// 
			// ChkAutoTapper
			// 
			this.ChkAutoTapper.AutoSize = true;
			this.ChkAutoTapper.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.ChkAutoTapper.Location = new System.Drawing.Point(10, 26);
			this.ChkAutoTapper.Name = "ChkAutoTapper";
			this.ChkAutoTapper.Size = new System.Drawing.Size(65, 17);
			this.ChkAutoTapper.TabIndex = 0;
			this.ChkAutoTapper.Text = "Enabled";
			this.ChkAutoTapper.UseVisualStyleBackColor = true;
			this.ChkAutoTapper.CheckedChanged += new System.EventHandler(this.ChkAutoTapper_CheckedChanged);
			// 
			// groupBoxAudioFilter
			// 
			this.groupBoxAudioFilter.Controls.Add(this.numHighPass);
			this.groupBoxAudioFilter.Controls.Add(this.numLowPass);
			this.groupBoxAudioFilter.Controls.Add(this.chkHighPass);
			this.groupBoxAudioFilter.Controls.Add(this.chkLowPass);
			this.groupBoxAudioFilter.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.groupBoxAudioFilter.Location = new System.Drawing.Point(475, 101);
			this.groupBoxAudioFilter.Name = "groupBoxAudioFilter";
			this.groupBoxAudioFilter.Size = new System.Drawing.Size(229, 75);
			this.groupBoxAudioFilter.TabIndex = 9;
			this.groupBoxAudioFilter.TabStop = false;
			this.groupBoxAudioFilter.Text = "Audio Filter";
			this.groupBoxAudioFilter.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBoxes_Paint);
			// 
			// numHighPass
			// 
			this.numHighPass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
			this.numHighPass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.numHighPass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.numHighPass.Location = new System.Drawing.Point(95, 45);
			this.numHighPass.Maximum = new decimal(new int[] {
            22000,
            0,
            0,
            0});
			this.numHighPass.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.numHighPass.Name = "numHighPass";
			this.numHighPass.Size = new System.Drawing.Size(120, 20);
			this.numHighPass.TabIndex = 3;
			this.numHighPass.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
			this.numHighPass.ValueChanged += new System.EventHandler(this.numHighPass_ValueChanged);
			// 
			// numLowPass
			// 
			this.numLowPass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
			this.numLowPass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.numLowPass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.numLowPass.Location = new System.Drawing.Point(95, 19);
			this.numLowPass.Maximum = new decimal(new int[] {
            22000,
            0,
            0,
            0});
			this.numLowPass.Name = "numLowPass";
			this.numLowPass.Size = new System.Drawing.Size(120, 20);
			this.numLowPass.TabIndex = 1;
			this.numLowPass.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
			this.numLowPass.ValueChanged += new System.EventHandler(this.numLowPass_ValueChanged);
			// 
			// chkHighPass
			// 
			this.chkHighPass.AutoSize = true;
			this.chkHighPass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.chkHighPass.Location = new System.Drawing.Point(15, 46);
			this.chkHighPass.Name = "chkHighPass";
			this.chkHighPass.Size = new System.Drawing.Size(74, 17);
			this.chkHighPass.TabIndex = 2;
			this.chkHighPass.Text = "High Pass";
			this.chkHighPass.UseVisualStyleBackColor = true;
			this.chkHighPass.CheckedChanged += new System.EventHandler(this.chkHighPass_CheckedChanged);
			// 
			// chkLowPass
			// 
			this.chkLowPass.AutoSize = true;
			this.chkLowPass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.chkLowPass.Location = new System.Drawing.Point(15, 19);
			this.chkLowPass.Name = "chkLowPass";
			this.chkLowPass.Size = new System.Drawing.Size(72, 17);
			this.chkLowPass.TabIndex = 0;
			this.chkLowPass.Text = "Low Pass";
			this.chkLowPass.UseVisualStyleBackColor = true;
			this.chkLowPass.CheckedChanged += new System.EventHandler(this.chkLowPass_CheckedChanged);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.label6.Location = new System.Drawing.Point(221, 30);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(75, 13);
			this.label6.TabIndex = 12;
			this.label6.Text = "Timing Speed:";
			// 
			// trackBarPlayBack
			// 
			this.trackBarPlayBack.Location = new System.Drawing.Point(6, 65);
			this.trackBarPlayBack.Name = "trackBarPlayBack";
			this.trackBarPlayBack.Size = new System.Drawing.Size(703, 45);
			this.trackBarPlayBack.TabIndex = 2;
			this.trackBarPlayBack.Scroll += new System.EventHandler(this.trackBarPlayBack_Scroll);
			this.trackBarPlayBack.MouseDown += new System.Windows.Forms.MouseEventHandler(this.trackBarPlayBack_MouseDown);
			this.trackBarPlayBack.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackBarPlayBack_MouseUp);
			// 
			// textBoxTimingSpeed
			// 
			this.textBoxTimingSpeed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
			this.textBoxTimingSpeed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxTimingSpeed.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.textBoxTimingSpeed.Location = new System.Drawing.Point(310, 28);
			this.textBoxTimingSpeed.Name = "textBoxTimingSpeed";
			this.textBoxTimingSpeed.Size = new System.Drawing.Size(57, 20);
			this.textBoxTimingSpeed.TabIndex = 3;
			// 
			// textBoxPosition
			// 
			this.textBoxPosition.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
			this.textBoxPosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxPosition.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.textBoxPosition.Location = new System.Drawing.Point(597, 28);
			this.textBoxPosition.Name = "textBoxPosition";
			this.textBoxPosition.ReadOnly = true;
			this.textBoxPosition.Size = new System.Drawing.Size(89, 20);
			this.textBoxPosition.TabIndex = 6;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.label5.Location = new System.Drawing.Point(507, 31);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(84, 13);
			this.label5.TabIndex = 7;
			this.label5.Text = "Current Position:";
			// 
			// panelMarkView
			// 
			this.panelMarkView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelMarkView.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.panelMarkView.Location = new System.Drawing.Point(110, 116);
			this.panelMarkView.Name = "panelMarkView";
			this.panelMarkView.Size = new System.Drawing.Size(147, 73);
			this.panelMarkView.TabIndex = 8;
			this.panelMarkView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelMarkView_MouseDown);
			// 
			// lblLastMarkHit
			// 
			this.lblLastMarkHit.AutoSize = true;
			this.lblLastMarkHit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.lblLastMarkHit.Location = new System.Drawing.Point(507, 201);
			this.lblLastMarkHit.Name = "lblLastMarkHit";
			this.lblLastMarkHit.Size = new System.Drawing.Size(73, 13);
			this.lblLastMarkHit.TabIndex = 4;
			this.lblLastMarkHit.Text = "Last Mark Hit:";
			// 
			// textBoxCurrentMark
			// 
			this.textBoxCurrentMark.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
			this.textBoxCurrentMark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxCurrentMark.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.textBoxCurrentMark.Location = new System.Drawing.Point(586, 198);
			this.textBoxCurrentMark.Name = "textBoxCurrentMark";
			this.textBoxCurrentMark.ReadOnly = true;
			this.textBoxCurrentMark.Size = new System.Drawing.Size(89, 20);
			this.textBoxCurrentMark.TabIndex = 10;
			// 
			// groupBoxMode
			// 
			this.groupBoxMode.Controls.Add(this.radioButtonPlayback);
			this.groupBoxMode.Controls.Add(this.radioButtonTapper);
			this.groupBoxMode.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.groupBoxMode.Location = new System.Drawing.Point(17, 110);
			this.groupBoxMode.Name = "groupBoxMode";
			this.groupBoxMode.Size = new System.Drawing.Size(88, 78);
			this.groupBoxMode.TabIndex = 7;
			this.groupBoxMode.TabStop = false;
			this.groupBoxMode.Text = "Mode";
			this.groupBoxMode.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBoxes_Paint);
			// 
			// radioButtonPlayback
			// 
			this.radioButtonPlayback.AutoSize = true;
			this.radioButtonPlayback.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
			this.radioButtonPlayback.Checked = true;
			this.radioButtonPlayback.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.radioButtonPlayback.Location = new System.Drawing.Point(9, 24);
			this.radioButtonPlayback.Name = "radioButtonPlayback";
			this.radioButtonPlayback.Size = new System.Drawing.Size(69, 17);
			this.radioButtonPlayback.TabIndex = 1;
			this.radioButtonPlayback.Text = "Playback";
			this.radioButtonPlayback.UseVisualStyleBackColor = false;
			// 
			// radioButtonTapper
			// 
			this.radioButtonTapper.AutoSize = true;
			this.radioButtonTapper.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
			this.radioButtonTapper.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.radioButtonTapper.Location = new System.Drawing.Point(9, 47);
			this.radioButtonTapper.Name = "radioButtonTapper";
			this.radioButtonTapper.Size = new System.Drawing.Size(59, 17);
			this.radioButtonTapper.TabIndex = 0;
			this.radioButtonTapper.Text = "Tapper";
			this.radioButtonTapper.UseVisualStyleBackColor = false;
			this.radioButtonTapper.CheckedChanged += new System.EventHandler(this.radioButtonTapper_CheckedChanged);
			// 
			// timerPlayback
			// 
			this.timerPlayback.Interval = 1;
			this.timerPlayback.Tick += new System.EventHandler(this.timerPlayback_Tick);
			// 
			// timerMarkHit
			// 
			this.timerMarkHit.Interval = 40;
			this.timerMarkHit.Tick += new System.EventHandler(this.timerMarkHit_Tick);
			// 
			// openFileDialog
			// 
			this.openFileDialog.FileName = "openFileDialog1";
			// 
			// MarkManager
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(734, 605);
			this.Controls.Add(this.groupBoxPlayback);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.groupBoxSelectedMarkCollection);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.groupBoxMarkCollections);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(750, 644);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(742, 644);
			this.Name = "MarkManager";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Mark Collections Manager";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MarkManager_FormClosing);
			this.Load += new System.EventHandler(this.MarkManager_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MarkManager_KeyDown);
			this.groupBoxMarkCollections.ResumeLayout(false);
			this.groupBoxMarkCollections.PerformLayout();
			this.panelMarkCollectionsButtons.ResumeLayout(false);
			this.groupBoxSelectedMarkCollection.ResumeLayout(false);
			this.groupBoxSelectedMarks.ResumeLayout(false);
			this.groupBoxSelectedMarks.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.groupBoxDetails.ResumeLayout(false);
			this.groupBoxDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownWeight)).EndInit();
			this.groupBoxOperations.ResumeLayout(false);
			this.groupBoxMarks.ResumeLayout(false);
			this.groupBoxMarks.PerformLayout();
			this.groupBoxPlayback.ResumeLayout(false);
			this.groupBoxPlayback.PerformLayout();
			this.panel4.ResumeLayout(false);
			this.panel4.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.groupBoxFreqDetection.ResumeLayout(false);
			this.groupBoxFreqDetection.PerformLayout();
			this.groupBoxAudioFilter.ResumeLayout(false);
			this.groupBoxAudioFilter.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numHighPass)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numLowPass)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarPlayBack)).EndInit();
			this.groupBoxMode.ResumeLayout(false);
			this.groupBoxMode.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBoxMarkCollections;
		private System.Windows.Forms.Button buttonRemoveCollection;
		private System.Windows.Forms.Button buttonAddCollection;
		private System.Windows.Forms.ListView listViewMarkCollections;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.GroupBox groupBoxSelectedMarkCollection;
		private System.Windows.Forms.GroupBox groupBoxMarks;
		private System.Windows.Forms.Button buttonSelectAllMarks;
		private System.Windows.Forms.GroupBox groupBoxOperations;
		private System.Windows.Forms.Button buttonAddOrUpdateMark;
		private System.Windows.Forms.TextBox textBoxTime;
		private System.Windows.Forms.Button buttonOffsetMarks;
		private System.Windows.Forms.Button buttonGenerateSubmarks;
		private System.Windows.Forms.Button buttonEvenlySpaceMarks;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.ListView listViewMarks;
		private System.Windows.Forms.ColumnHeader Times;
		private System.Windows.Forms.GroupBox groupBoxDetails;
		private System.Windows.Forms.CheckBox checkBoxEnabled;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Panel panelColor;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown numericUpDownWeight;
		private System.Windows.Forms.TextBox textBoxCollectionName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonPasteEffectsToMarks;
		private System.Windows.Forms.Button buttonCopyAndOffsetMarks;
		private System.Windows.Forms.Button buttonGenerateBeatMarks;
		private System.Windows.Forms.Button buttonGenerateGrid;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.GroupBox groupBoxPlayback;
		private System.Windows.Forms.GroupBox groupBoxMode;
		private System.Windows.Forms.RadioButton radioButtonTapper;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBoxTimingSpeed;
		private System.Windows.Forms.Button buttonIncreasePlaybackSpeed;
		private System.Windows.Forms.Button buttonDecreasePlaySpeed;
		private System.Windows.Forms.Panel panelMarkView;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBoxPosition;
		private System.Windows.Forms.Label lblLastMarkHit;
		private System.Windows.Forms.TextBox textBoxCurrentMark;
		private System.Windows.Forms.Button buttonStop;
		private System.Windows.Forms.Button buttonPlay;
		private System.Windows.Forms.RadioButton radioButtonPlayback;
		private System.Windows.Forms.Timer timerPlayback;
		private System.Windows.Forms.Timer timerMarkHit;
		private System.Windows.Forms.GroupBox groupBoxSelectedMarks;
		private System.Windows.Forms.Button buttonIncreaseSelectedMarks;
		private System.Windows.Forms.Button buttonDecreaseSelectedMarks;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textBoxTimeIncrement;
		private System.Windows.Forms.Label labelTapperInstructions;
		private System.Windows.Forms.TrackBar trackBarPlayBack;
		private System.Windows.Forms.GroupBox groupBoxAudioFilter;
		private System.Windows.Forms.NumericUpDown numHighPass;
		private System.Windows.Forms.NumericUpDown numLowPass;
		private System.Windows.Forms.CheckBox chkHighPass;
		private System.Windows.Forms.CheckBox chkLowPass;
		private System.Windows.Forms.CheckBox ChkAutoTapper;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.GroupBox groupBoxFreqDetection;
		private System.Windows.Forms.Button btnAutoDetectionSettings;
		private System.Windows.Forms.Button btnCreateCollections;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.RadioButton radioSelected;
		private System.Windows.Forms.RadioButton radioAll;
		private System.Windows.Forms.Button buttonImportAudacity;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.Button buttonExportBeatMarks;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Panel panelMarkCollectionsButtons;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel4;

	}
}