namespace VideoEditor
{
	partial class VideoEditorControl
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
			if (disposing && (components != null)) {
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkRepeatVideo = new System.Windows.Forms.CheckBox();
            this.trkVolume = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tsStartTime = new Common.Controls.TimeSpanEditor();
            this.tsEndTime = new Common.Controls.TimeSpanEditor();
            this.btnPlayMedia = new System.Windows.Forms.Button();
            this.btnStopMedia = new System.Windows.Forms.Button();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.vPlayer1 = new VixenModules.Output.CommandController.VPlayer();
            ((System.ComponentModel.ISupportInitialize)(this.trkVolume)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Video File";
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(6, 60);
            this.txtFileName.Multiline = true;
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(411, 38);
            this.txtFileName.TabIndex = 2;
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(423, 60);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(32, 38);
            this.btnOpenFile.TabIndex = 4;
            this.btnOpenFile.Text = "...";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(6, 17);
            this.txtDescription.MaxLength = 30;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(449, 20);
            this.txtDescription.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Description";
            // 
            // chkRepeatVideo
            // 
            this.chkRepeatVideo.AutoSize = true;
            this.chkRepeatVideo.Location = new System.Drawing.Point(6, 115);
            this.chkRepeatVideo.Name = "chkRepeatVideo";
            this.chkRepeatVideo.Size = new System.Drawing.Size(61, 17);
            this.chkRepeatVideo.TabIndex = 8;
            this.chkRepeatVideo.Text = "Repeat";
            this.chkRepeatVideo.UseVisualStyleBackColor = true;
            // 
            // trkVolume
            // 
            this.trkVolume.LargeChange = 30;
            this.trkVolume.Location = new System.Drawing.Point(6, 160);
            this.trkVolume.Maximum = 100;
            this.trkVolume.Name = "trkVolume";
            this.trkVolume.Size = new System.Drawing.Size(186, 45);
            this.trkVolume.SmallChange = 210;
            this.trkVolume.TabIndex = 9;
            this.trkVolume.Scroll += new System.EventHandler(this.trkVolume_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Media Volume";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(242, 154);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "End";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(242, 129);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Start";
            // 
            // tsStartTime
            // 
            this.tsStartTime.Location = new System.Drawing.Point(277, 122);
            this.tsStartTime.Mask = "00:00.000";
            this.tsStartTime.Name = "tsStartTime";
            this.tsStartTime.Size = new System.Drawing.Size(100, 20);
            this.tsStartTime.TabIndex = 14;
            this.tsStartTime.Text = "0000000";
            this.tsStartTime.TimeSpan = System.TimeSpan.Parse("00:00:00");
            // 
            // tsEndTime
            // 
            this.tsEndTime.Location = new System.Drawing.Point(277, 151);
            this.tsEndTime.Mask = "00:00.000";
            this.tsEndTime.Name = "tsEndTime";
            this.tsEndTime.Size = new System.Drawing.Size(100, 20);
            this.tsEndTime.TabIndex = 15;
            this.tsEndTime.Text = "0000000";
            this.tsEndTime.TimeSpan = System.TimeSpan.Parse("00:00:00");
            // 
            // btnPlayMedia
            // 
            this.btnPlayMedia.Location = new System.Drawing.Point(407, 145);
            this.btnPlayMedia.Name = "btnPlayMedia";
            this.btnPlayMedia.Size = new System.Drawing.Size(48, 38);
            this.btnPlayMedia.TabIndex = 16;
            this.btnPlayMedia.Text = "Play";
            this.btnPlayMedia.UseVisualStyleBackColor = true;
            this.btnPlayMedia.Click += new System.EventHandler(this.btnPlayMedia_Click);
            // 
            // btnStopMedia
            // 
            this.btnStopMedia.Enabled = false;
            this.btnStopMedia.Location = new System.Drawing.Point(407, 189);
            this.btnStopMedia.Name = "btnStopMedia";
            this.btnStopMedia.Size = new System.Drawing.Size(48, 38);
            this.btnStopMedia.TabIndex = 17;
            this.btnStopMedia.Text = "Stop";
            this.btnStopMedia.UseVisualStyleBackColor = true;
            this.btnStopMedia.Click += new System.EventHandler(this.btnStopMedia_Click);
            // 
            // elementHost1
            // 
            this.elementHost1.Location = new System.Drawing.Point(462, 17);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(300, 210);
            this.elementHost1.TabIndex = 7;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = this.vPlayer1;
            // 
            // VideoEditorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnStopMedia);
            this.Controls.Add(this.btnPlayMedia);
            this.Controls.Add(this.tsEndTime);
            this.Controls.Add(this.tsStartTime);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.trkVolume);
            this.Controls.Add(this.chkRepeatVideo);
            this.Controls.Add(this.elementHost1);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.label1);
            this.Name = "VideoEditorControl";
            this.Size = new System.Drawing.Size(780, 288);
            ((System.ComponentModel.ISupportInitialize)(this.trkVolume)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFileName;
		private System.Windows.Forms.Button btnOpenFile;
		private System.Windows.Forms.TextBox txtDescription;
		private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private VixenModules.Output.CommandController.VPlayer vPlayer1;
        private System.Windows.Forms.CheckBox chkRepeatVideo;
        private System.Windows.Forms.TrackBar trkVolume;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private Common.Controls.TimeSpanEditor tsStartTime;
        private Common.Controls.TimeSpanEditor tsEndTime;
        private System.Windows.Forms.Button btnPlayMedia;
        private System.Windows.Forms.Button btnStopMedia;
	}
}
