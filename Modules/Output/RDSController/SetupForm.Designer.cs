﻿namespace VixenModules.Output.CommandController
{
	partial class SetupForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetupForm));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkSlow = new System.Windows.Forms.CheckBox();
            this.chkBiDirectional = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboPortName = new System.Windows.Forms.ComboBox();
            this.chkRequiresAuthentication = new System.Windows.Forms.CheckBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.txtHttpPassword = new System.Windows.Forms.TextBox();
            this.txtHttpUsername = new System.Windows.Forms.TextBox();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblUrl = new System.Windows.Forms.Label();
            this.radioHttp = new System.Windows.Forms.RadioButton();
            this.radioVFMT212R = new System.Windows.Forms.RadioButton();
            this.radioMRDS1322 = new System.Windows.Forms.RadioButton();
            this.radioMRDS192 = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnTX = new System.Windows.Forms.Button();
            this.txtPSInterface = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLbl1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkHideLaunchedWindows = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.chkVideoPlaybackEnabled = new System.Windows.Forms.CheckBox();
            this.chkPlaybackFullScreen = new System.Windows.Forms.CheckBox();
            this.chkRepeatPlayback = new System.Windows.Forms.CheckBox();
            this.txtStopMedia = new System.Windows.Forms.Button();
            this.txtPlayMedia = new System.Windows.Forms.Button();
            this.btnGetMediaFileName = new System.Windows.Forms.Button();
            this.txtMediaFileNAme = new System.Windows.Forms.TextBox();
            this.btnChangeMonitor = new System.Windows.Forms.Button();
            this.txtPlaybackMonitor = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboMonitorList = new System.Windows.Forms.ComboBox();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkSlow);
            this.groupBox3.Controls.Add(this.chkBiDirectional);
            this.groupBox3.Location = new System.Drawing.Point(399, 259);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(110, 76);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Options";
            // 
            // chkSlow
            // 
            this.chkSlow.AutoSize = true;
            this.chkSlow.Location = new System.Drawing.Point(9, 42);
            this.chkSlow.Name = "chkSlow";
            this.chkSlow.Size = new System.Drawing.Size(49, 17);
            this.chkSlow.TabIndex = 1;
            this.chkSlow.Text = "Slow";
            this.chkSlow.UseVisualStyleBackColor = true;
            this.chkSlow.CheckedChanged += new System.EventHandler(this.chkSlow_CheckedChanged);
            // 
            // chkBiDirectional
            // 
            this.chkBiDirectional.AutoSize = true;
            this.chkBiDirectional.Location = new System.Drawing.Point(9, 19);
            this.chkBiDirectional.Name = "chkBiDirectional";
            this.chkBiDirectional.Size = new System.Drawing.Size(85, 17);
            this.chkBiDirectional.TabIndex = 0;
            this.chkBiDirectional.Text = "BiDirectional";
            this.chkBiDirectional.UseVisualStyleBackColor = true;
            this.chkBiDirectional.CheckedChanged += new System.EventHandler(this.chkBiDirectional_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.cboPortName);
            this.groupBox4.Controls.Add(this.chkRequiresAuthentication);
            this.groupBox4.Controls.Add(this.lblPassword);
            this.groupBox4.Controls.Add(this.lblUserName);
            this.groupBox4.Controls.Add(this.txtHttpPassword);
            this.groupBox4.Controls.Add(this.txtHttpUsername);
            this.groupBox4.Controls.Add(this.txtUrl);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.lblUrl);
            this.groupBox4.Controls.Add(this.radioHttp);
            this.groupBox4.Controls.Add(this.radioVFMT212R);
            this.groupBox4.Controls.Add(this.radioMRDS1322);
            this.groupBox4.Controls.Add(this.radioMRDS192);
            this.groupBox4.Location = new System.Drawing.Point(12, 66);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(381, 213);
            this.groupBox4.TabIndex = 16;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "RDS Hardware";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(243, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Serial Port";
            // 
            // cboPortName
            // 
            this.cboPortName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPortName.FormattingEnabled = true;
            this.cboPortName.Location = new System.Drawing.Point(246, 38);
            this.cboPortName.Name = "cboPortName";
            this.cboPortName.Size = new System.Drawing.Size(77, 21);
            this.cboPortName.TabIndex = 0;
            this.cboPortName.SelectedIndexChanged += new System.EventHandler(this.cboPortName_SelectedIndexChanged);
            // 
            // chkRequiresAuthentication
            // 
            this.chkRequiresAuthentication.AutoSize = true;
            this.chkRequiresAuthentication.Enabled = false;
            this.chkRequiresAuthentication.Location = new System.Drawing.Point(41, 150);
            this.chkRequiresAuthentication.Name = "chkRequiresAuthentication";
            this.chkRequiresAuthentication.Size = new System.Drawing.Size(182, 17);
            this.chkRequiresAuthentication.TabIndex = 14;
            this.chkRequiresAuthentication.Text = "HTTP(s) Requires Authentication";
            this.chkRequiresAuthentication.UseVisualStyleBackColor = true;
            this.chkRequiresAuthentication.CheckedChanged += new System.EventHandler(this.chkRequiresAuthentication_CheckedChanged);
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Enabled = false;
            this.lblPassword.Location = new System.Drawing.Point(218, 176);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(53, 13);
            this.lblPassword.TabIndex = 13;
            this.lblPassword.Text = "Password";
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Enabled = false;
            this.lblUserName.Location = new System.Drawing.Point(62, 176);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(57, 13);
            this.lblUserName.TabIndex = 12;
            this.lblUserName.Text = "UserName";
            // 
            // txtHttpPassword
            // 
            this.txtHttpPassword.Enabled = false;
            this.txtHttpPassword.Location = new System.Drawing.Point(272, 173);
            this.txtHttpPassword.MaxLength = 8;
            this.txtHttpPassword.Name = "txtHttpPassword";
            this.txtHttpPassword.Size = new System.Drawing.Size(103, 20);
            this.txtHttpPassword.TabIndex = 11;
            this.txtHttpPassword.TextChanged += new System.EventHandler(this.txtHttpPassword_TextChanged);
            // 
            // txtHttpUsername
            // 
            this.txtHttpUsername.Enabled = false;
            this.txtHttpUsername.Location = new System.Drawing.Point(125, 173);
            this.txtHttpUsername.MaxLength = 8;
            this.txtHttpUsername.Name = "txtHttpUsername";
            this.txtHttpUsername.Size = new System.Drawing.Size(88, 20);
            this.txtHttpUsername.TabIndex = 10;
            this.txtHttpUsername.TextChanged += new System.EventHandler(this.txtHttpUsername_TextChanged);
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(41, 65);
            this.txtUrl.Multiline = true;
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(334, 53);
            this.txtUrl.TabIndex = 9;
            this.txtUrl.TextChanged += new System.EventHandler(this.txtUrl_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(131, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "{text}, {Time}";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Macro Fields:";
            // 
            // lblUrl
            // 
            this.lblUrl.AutoSize = true;
            this.lblUrl.Location = new System.Drawing.Point(6, 70);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(29, 13);
            this.lblUrl.TabIndex = 6;
            this.lblUrl.Text = "URL";
            // 
            // radioHttp
            // 
            this.radioHttp.AutoSize = true;
            this.radioHttp.Location = new System.Drawing.Point(9, 42);
            this.radioHttp.Name = "radioHttp";
            this.radioHttp.Size = new System.Drawing.Size(67, 17);
            this.radioHttp.TabIndex = 3;
            this.radioHttp.Text = "HTTP(S)";
            this.radioHttp.UseVisualStyleBackColor = true;
            this.radioHttp.CheckedChanged += new System.EventHandler(this.radioHttp_CheckedChanged);
            // 
            // radioVFMT212R
            // 
            this.radioVFMT212R.AutoSize = true;
            this.radioVFMT212R.Location = new System.Drawing.Point(120, 42);
            this.radioVFMT212R.Name = "radioVFMT212R";
            this.radioVFMT212R.Size = new System.Drawing.Size(83, 17);
            this.radioVFMT212R.TabIndex = 2;
            this.radioVFMT212R.Text = "V-FMT212R";
            this.radioVFMT212R.UseVisualStyleBackColor = true;
            this.radioVFMT212R.CheckedChanged += new System.EventHandler(this.radioVFMT212R_CheckedChanged);
            // 
            // radioMRDS1322
            // 
            this.radioMRDS1322.AutoSize = true;
            this.radioMRDS1322.Location = new System.Drawing.Point(120, 19);
            this.radioMRDS1322.Name = "radioMRDS1322";
            this.radioMRDS1322.Size = new System.Drawing.Size(81, 17);
            this.radioMRDS1322.TabIndex = 1;
            this.radioMRDS1322.Text = "MRDS1322";
            this.radioMRDS1322.UseVisualStyleBackColor = true;
            this.radioMRDS1322.CheckedChanged += new System.EventHandler(this.radioMRDS1322_CheckedChanged);
            // 
            // radioMRDS192
            // 
            this.radioMRDS192.AutoSize = true;
            this.radioMRDS192.Checked = true;
            this.radioMRDS192.Location = new System.Drawing.Point(9, 19);
            this.radioMRDS192.Name = "radioMRDS192";
            this.radioMRDS192.Size = new System.Drawing.Size(75, 17);
            this.radioMRDS192.TabIndex = 0;
            this.radioMRDS192.TabStop = true;
            this.radioMRDS192.Text = "MRDS192";
            this.radioMRDS192.UseVisualStyleBackColor = true;
            this.radioMRDS192.CheckedChanged += new System.EventHandler(this.radioMRDS192_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnTX);
            this.groupBox2.Controls.Add(this.txtPSInterface);
            this.groupBox2.Location = new System.Drawing.Point(12, 285);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(381, 47);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Test RDS Interface";
            // 
            // btnTX
            // 
            this.btnTX.Location = new System.Drawing.Point(300, 17);
            this.btnTX.Name = "btnTX";
            this.btnTX.Size = new System.Drawing.Size(75, 23);
            this.btnTX.TabIndex = 1;
            this.btnTX.Text = "Send";
            this.btnTX.Click += new System.EventHandler(this.btnTX_Click);
            // 
            // txtPSInterface
            // 
            this.txtPSInterface.Location = new System.Drawing.Point(12, 20);
            this.txtPSInterface.MaxLength = 64;
            this.txtPSInterface.Name = "txtPSInterface";
            this.txtPSInterface.Size = new System.Drawing.Size(282, 20);
            this.txtPSInterface.TabIndex = 4;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Enabled = false;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(399, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(222, 229);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 20;
            this.pictureBox1.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLbl1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 439);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(670, 22);
            this.statusStrip1.TabIndex = 24;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLbl1
            // 
            this.StatusLbl1.ForeColor = System.Drawing.Color.Crimson;
            this.StatusLbl1.Name = "StatusLbl1";
            this.StatusLbl1.Size = new System.Drawing.Size(33, 17);
            this.StatusLbl1.Text = "DIYC";
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(544, 338);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(126, 97);
            this.button1.TabIndex = 25;
            this.button1.Text = "Save Configuration Settings";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkHideLaunchedWindows);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(381, 48);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Launcher Options";
            // 
            // chkHideLaunchedWindows
            // 
            this.chkHideLaunchedWindows.AutoSize = true;
            this.chkHideLaunchedWindows.Location = new System.Drawing.Point(9, 19);
            this.chkHideLaunchedWindows.Name = "chkHideLaunchedWindows";
            this.chkHideLaunchedWindows.Size = new System.Drawing.Size(146, 17);
            this.chkHideLaunchedWindows.TabIndex = 0;
            this.chkHideLaunchedWindows.Text = "Hide Launched Windows";
            this.chkHideLaunchedWindows.UseVisualStyleBackColor = true;
            this.chkHideLaunchedWindows.CheckedChanged += new System.EventHandler(this.chkHideLaunchedWindows_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.chkVideoPlaybackEnabled);
            this.groupBox5.Controls.Add(this.chkPlaybackFullScreen);
            this.groupBox5.Controls.Add(this.chkRepeatPlayback);
            this.groupBox5.Controls.Add(this.txtStopMedia);
            this.groupBox5.Controls.Add(this.txtPlayMedia);
            this.groupBox5.Controls.Add(this.btnGetMediaFileName);
            this.groupBox5.Controls.Add(this.txtMediaFileNAme);
            this.groupBox5.Controls.Add(this.btnChangeMonitor);
            this.groupBox5.Controls.Add(this.txtPlaybackMonitor);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.cboMonitorList);
            this.groupBox5.Location = new System.Drawing.Point(12, 338);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(526, 97);
            this.groupBox5.TabIndex = 27;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Video Playback";
            this.groupBox5.Enter += new System.EventHandler(this.groupBox5_Enter);
            // 
            // chkVideoPlaybackEnabled
            // 
            this.chkVideoPlaybackEnabled.AutoSize = true;
            this.chkVideoPlaybackEnabled.Location = new System.Drawing.Point(12, 46);
            this.chkVideoPlaybackEnabled.Name = "chkVideoPlaybackEnabled";
            this.chkVideoPlaybackEnabled.Size = new System.Drawing.Size(65, 17);
            this.chkVideoPlaybackEnabled.TabIndex = 22;
            this.chkVideoPlaybackEnabled.Text = "Enabled";
            this.chkVideoPlaybackEnabled.UseVisualStyleBackColor = true;
            this.chkVideoPlaybackEnabled.CheckedChanged += new System.EventHandler(this.chkVideoPlaybackEnabled_CheckedChanged);
            // 
            // chkPlaybackFullScreen
            // 
            this.chkPlaybackFullScreen.AutoSize = true;
            this.chkPlaybackFullScreen.Location = new System.Drawing.Point(310, 46);
            this.chkPlaybackFullScreen.Name = "chkPlaybackFullScreen";
            this.chkPlaybackFullScreen.Size = new System.Drawing.Size(126, 17);
            this.chkPlaybackFullScreen.TabIndex = 21;
            this.chkPlaybackFullScreen.Text = "Playback Full Screen";
            this.chkPlaybackFullScreen.UseVisualStyleBackColor = true;
            this.chkPlaybackFullScreen.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // chkRepeatPlayback
            // 
            this.chkRepeatPlayback.AutoSize = true;
            this.chkRepeatPlayback.Location = new System.Drawing.Point(197, 46);
            this.chkRepeatPlayback.Name = "chkRepeatPlayback";
            this.chkRepeatPlayback.Size = new System.Drawing.Size(108, 17);
            this.chkRepeatPlayback.TabIndex = 20;
            this.chkRepeatPlayback.Text = "Repeat Playback";
            this.chkRepeatPlayback.UseVisualStyleBackColor = true;
            this.chkRepeatPlayback.CheckedChanged += new System.EventHandler(this.chkRepeatPlayback_CheckedChanged);
            // 
            // txtStopMedia
            // 
            this.txtStopMedia.Location = new System.Drawing.Point(469, 66);
            this.txtStopMedia.Name = "txtStopMedia";
            this.txtStopMedia.Size = new System.Drawing.Size(45, 23);
            this.txtStopMedia.TabIndex = 19;
            this.txtStopMedia.Text = "Stop";
            this.txtStopMedia.Click += new System.EventHandler(this.txtStopMedia_Click);
            // 
            // txtPlayMedia
            // 
            this.txtPlayMedia.Location = new System.Drawing.Point(414, 66);
            this.txtPlayMedia.Name = "txtPlayMedia";
            this.txtPlayMedia.Size = new System.Drawing.Size(46, 23);
            this.txtPlayMedia.TabIndex = 18;
            this.txtPlayMedia.Text = "Play";
            this.txtPlayMedia.Click += new System.EventHandler(this.txtPlayMedia_Click);
            // 
            // btnGetMediaFileName
            // 
            this.btnGetMediaFileName.Location = new System.Drawing.Point(372, 66);
            this.btnGetMediaFileName.Name = "btnGetMediaFileName";
            this.btnGetMediaFileName.Size = new System.Drawing.Size(35, 23);
            this.btnGetMediaFileName.TabIndex = 17;
            this.btnGetMediaFileName.Text = "...";
            this.btnGetMediaFileName.Click += new System.EventHandler(this.btnGetMediaFileName_Click);
            // 
            // txtMediaFileNAme
            // 
            this.txtMediaFileNAme.Location = new System.Drawing.Point(6, 69);
            this.txtMediaFileNAme.MaxLength = 64;
            this.txtMediaFileNAme.Name = "txtMediaFileNAme";
            this.txtMediaFileNAme.Size = new System.Drawing.Size(360, 20);
            this.txtMediaFileNAme.TabIndex = 16;
            // 
            // btnChangeMonitor
            // 
            this.btnChangeMonitor.Location = new System.Drawing.Point(420, 16);
            this.btnChangeMonitor.Name = "btnChangeMonitor";
            this.btnChangeMonitor.Size = new System.Drawing.Size(100, 23);
            this.btnChangeMonitor.TabIndex = 15;
            this.btnChangeMonitor.Text = "Change Monitor";
            this.btnChangeMonitor.UseVisualStyleBackColor = true;
            this.btnChangeMonitor.Click += new System.EventHandler(this.btnChangeMonitor_Click);
            // 
            // txtPlaybackMonitor
            // 
            this.txtPlaybackMonitor.Location = new System.Drawing.Point(110, 20);
            this.txtPlaybackMonitor.MaxLength = 8;
            this.txtPlaybackMonitor.Name = "txtPlaybackMonitor";
            this.txtPlaybackMonitor.ReadOnly = true;
            this.txtPlaybackMonitor.Size = new System.Drawing.Size(139, 20);
            this.txtPlaybackMonitor.TabIndex = 14;
            this.txtPlaybackMonitor.TextChanged += new System.EventHandler(this.txtPlaybackMonitor_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Playback Monitor";
            // 
            // cboMonitorList
            // 
            this.cboMonitorList.FormattingEnabled = true;
            this.cboMonitorList.Location = new System.Drawing.Point(255, 19);
            this.cboMonitorList.Name = "cboMonitorList";
            this.cboMonitorList.Size = new System.Drawing.Size(158, 21);
            this.cboMonitorList.TabIndex = 0;
            // 
            // SetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 461);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SetupForm";
            this.Text = "Launcher Commands and RDS Configuration";
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.CheckBox chkSlow;
		private System.Windows.Forms.CheckBox chkBiDirectional;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.RadioButton radioVFMT212R;
		private System.Windows.Forms.RadioButton radioMRDS1322;
		private System.Windows.Forms.RadioButton radioMRDS192;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button btnTX;
		private System.Windows.Forms.TextBox txtPSInterface;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel StatusLbl1;
		private System.Windows.Forms.Label lblUrl;
		private System.Windows.Forms.RadioButton radioHttp;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox txtUrl;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox chkHideLaunchedWindows;
		private System.Windows.Forms.TextBox txtHttpPassword;
		private System.Windows.Forms.TextBox txtHttpUsername;
		private System.Windows.Forms.Label lblPassword;
		private System.Windows.Forms.Label lblUserName;
		private System.Windows.Forms.CheckBox chkRequiresAuthentication;
		private System.Windows.Forms.ComboBox cboPortName;
		private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ComboBox cboMonitorList;
        private System.Windows.Forms.Button btnChangeMonitor;
        private System.Windows.Forms.TextBox txtPlaybackMonitor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnGetMediaFileName;
        private System.Windows.Forms.TextBox txtMediaFileNAme;
        private System.Windows.Forms.Button txtPlayMedia;
        private System.Windows.Forms.Button txtStopMedia;
        private System.Windows.Forms.CheckBox chkRepeatPlayback;
        private System.Windows.Forms.CheckBox chkPlaybackFullScreen;
        private System.Windows.Forms.CheckBox chkVideoPlaybackEnabled;
	}
}