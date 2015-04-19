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
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.statusStrip1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.chkSlow);
			this.groupBox3.Controls.Add(this.chkBiDirectional);
			this.groupBox3.Location = new System.Drawing.Point(598, 398);
			this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.groupBox3.Size = new System.Drawing.Size(165, 117);
			this.groupBox3.TabIndex = 3;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Options";
			// 
			// chkSlow
			// 
			this.chkSlow.AutoSize = true;
			this.chkSlow.Location = new System.Drawing.Point(14, 65);
			this.chkSlow.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.chkSlow.Name = "chkSlow";
			this.chkSlow.Size = new System.Drawing.Size(69, 24);
			this.chkSlow.TabIndex = 1;
			this.chkSlow.Text = "Slow";
			this.chkSlow.UseVisualStyleBackColor = true;
			this.chkSlow.CheckedChanged += new System.EventHandler(this.chkSlow_CheckedChanged);
			// 
			// chkBiDirectional
			// 
			this.chkBiDirectional.AutoSize = true;
			this.chkBiDirectional.Location = new System.Drawing.Point(14, 29);
			this.chkBiDirectional.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.chkBiDirectional.Name = "chkBiDirectional";
			this.chkBiDirectional.Size = new System.Drawing.Size(124, 24);
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
			this.groupBox4.Location = new System.Drawing.Point(18, 102);
			this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.groupBox4.Size = new System.Drawing.Size(572, 328);
			this.groupBox4.TabIndex = 1;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "RDS Hardware";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(364, 35);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(82, 20);
			this.label1.TabIndex = 15;
			this.label1.Text = "Serial Port";
			// 
			// cboPortName
			// 
			this.cboPortName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboPortName.FormattingEnabled = true;
			this.cboPortName.Location = new System.Drawing.Point(369, 58);
			this.cboPortName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.cboPortName.Name = "cboPortName";
			this.cboPortName.Size = new System.Drawing.Size(114, 28);
			this.cboPortName.TabIndex = 4;
			this.cboPortName.SelectedIndexChanged += new System.EventHandler(this.cboPortName_SelectedIndexChanged);
			// 
			// chkRequiresAuthentication
			// 
			this.chkRequiresAuthentication.AutoSize = true;
			this.chkRequiresAuthentication.Enabled = false;
			this.chkRequiresAuthentication.Location = new System.Drawing.Point(62, 231);
			this.chkRequiresAuthentication.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.chkRequiresAuthentication.Name = "chkRequiresAuthentication";
			this.chkRequiresAuthentication.Size = new System.Drawing.Size(268, 24);
			this.chkRequiresAuthentication.TabIndex = 6;
			this.chkRequiresAuthentication.Text = "HTTP(s) Requires Authentication";
			this.chkRequiresAuthentication.UseVisualStyleBackColor = true;
			this.chkRequiresAuthentication.CheckedChanged += new System.EventHandler(this.chkRequiresAuthentication_CheckedChanged);
			// 
			// lblPassword
			// 
			this.lblPassword.AutoSize = true;
			this.lblPassword.Enabled = false;
			this.lblPassword.Location = new System.Drawing.Point(327, 271);
			this.lblPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new System.Drawing.Size(78, 20);
			this.lblPassword.TabIndex = 13;
			this.lblPassword.Text = "Password";
			// 
			// lblUserName
			// 
			this.lblUserName.AutoSize = true;
			this.lblUserName.Enabled = false;
			this.lblUserName.Location = new System.Drawing.Point(93, 271);
			this.lblUserName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblUserName.Name = "lblUserName";
			this.lblUserName.Size = new System.Drawing.Size(85, 20);
			this.lblUserName.TabIndex = 12;
			this.lblUserName.Text = "UserName";
			// 
			// txtHttpPassword
			// 
			this.txtHttpPassword.Enabled = false;
			this.txtHttpPassword.Location = new System.Drawing.Point(408, 266);
			this.txtHttpPassword.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.txtHttpPassword.MaxLength = 8;
			this.txtHttpPassword.Name = "txtHttpPassword";
			this.txtHttpPassword.Size = new System.Drawing.Size(152, 26);
			this.txtHttpPassword.TabIndex = 8;
			this.txtHttpPassword.TextChanged += new System.EventHandler(this.txtHttpPassword_TextChanged);
			// 
			// txtHttpUsername
			// 
			this.txtHttpUsername.Enabled = false;
			this.txtHttpUsername.Location = new System.Drawing.Point(188, 266);
			this.txtHttpUsername.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.txtHttpUsername.MaxLength = 8;
			this.txtHttpUsername.Name = "txtHttpUsername";
			this.txtHttpUsername.Size = new System.Drawing.Size(130, 26);
			this.txtHttpUsername.TabIndex = 7;
			this.txtHttpUsername.TextChanged += new System.EventHandler(this.txtHttpUsername_TextChanged);
			// 
			// txtUrl
			// 
			this.txtUrl.Location = new System.Drawing.Point(62, 100);
			this.txtUrl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.txtUrl.Multiline = true;
			this.txtUrl.Name = "txtUrl";
			this.txtUrl.Size = new System.Drawing.Size(499, 79);
			this.txtUrl.TabIndex = 5;
			this.txtUrl.TextChanged += new System.EventHandler(this.txtUrl_TextChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(196, 191);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(97, 20);
			this.label3.TabIndex = 8;
			this.label3.Text = "{text}, {Time}";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(68, 191);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(103, 20);
			this.label2.TabIndex = 7;
			this.label2.Text = "Macro Fields:";
			// 
			// lblUrl
			// 
			this.lblUrl.AutoSize = true;
			this.lblUrl.Location = new System.Drawing.Point(9, 108);
			this.lblUrl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblUrl.Name = "lblUrl";
			this.lblUrl.Size = new System.Drawing.Size(42, 20);
			this.lblUrl.TabIndex = 6;
			this.lblUrl.Text = "URL";
			// 
			// radioHttp
			// 
			this.radioHttp.AutoSize = true;
			this.radioHttp.Location = new System.Drawing.Point(14, 65);
			this.radioHttp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.radioHttp.Name = "radioHttp";
			this.radioHttp.Size = new System.Drawing.Size(95, 24);
			this.radioHttp.TabIndex = 2;
			this.radioHttp.Text = "HTTP(S)";
			this.radioHttp.UseVisualStyleBackColor = true;
			this.radioHttp.CheckedChanged += new System.EventHandler(this.radioHttp_CheckedChanged);
			// 
			// radioVFMT212R
			// 
			this.radioVFMT212R.AutoSize = true;
			this.radioVFMT212R.Location = new System.Drawing.Point(180, 65);
			this.radioVFMT212R.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.radioVFMT212R.Name = "radioVFMT212R";
			this.radioVFMT212R.Size = new System.Drawing.Size(121, 24);
			this.radioVFMT212R.TabIndex = 3;
			this.radioVFMT212R.Text = "V-FMT212R";
			this.radioVFMT212R.UseVisualStyleBackColor = true;
			this.radioVFMT212R.CheckedChanged += new System.EventHandler(this.radioVFMT212R_CheckedChanged);
			// 
			// radioMRDS1322
			// 
			this.radioMRDS1322.AutoSize = true;
			this.radioMRDS1322.Location = new System.Drawing.Point(180, 29);
			this.radioMRDS1322.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.radioMRDS1322.Name = "radioMRDS1322";
			this.radioMRDS1322.Size = new System.Drawing.Size(118, 24);
			this.radioMRDS1322.TabIndex = 1;
			this.radioMRDS1322.Text = "MRDS1322";
			this.radioMRDS1322.UseVisualStyleBackColor = true;
			this.radioMRDS1322.CheckedChanged += new System.EventHandler(this.radioMRDS1322_CheckedChanged);
			// 
			// radioMRDS192
			// 
			this.radioMRDS192.AutoSize = true;
			this.radioMRDS192.Checked = true;
			this.radioMRDS192.Location = new System.Drawing.Point(14, 29);
			this.radioMRDS192.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.radioMRDS192.Name = "radioMRDS192";
			this.radioMRDS192.Size = new System.Drawing.Size(109, 24);
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
			this.groupBox2.Location = new System.Drawing.Point(18, 438);
			this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.groupBox2.Size = new System.Drawing.Size(572, 72);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Test RDS Interface";
			// 
			// btnTX
			// 
			this.btnTX.Location = new System.Drawing.Point(450, 26);
			this.btnTX.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnTX.Name = "btnTX";
			this.btnTX.Size = new System.Drawing.Size(112, 35);
			this.btnTX.TabIndex = 1;
			this.btnTX.Text = "Send";
			this.btnTX.Click += new System.EventHandler(this.btnTX_Click);
			// 
			// txtPSInterface
			// 
			this.txtPSInterface.Location = new System.Drawing.Point(18, 31);
			this.txtPSInterface.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.txtPSInterface.MaxLength = 64;
			this.txtPSInterface.Name = "txtPSInterface";
			this.txtPSInterface.Size = new System.Drawing.Size(421, 26);
			this.txtPSInterface.TabIndex = 0;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Enabled = false;
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(598, 18);
			this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(333, 352);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 20;
			this.pictureBox1.TabStop = false;
			// 
			// statusStrip1
			// 
			this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.StatusLbl1});
			this.statusStrip1.Location = new System.Drawing.Point(0, 528);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
			this.statusStrip1.Size = new System.Drawing.Size(945, 30);
			this.statusStrip1.TabIndex = 24;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// StatusLbl1
			// 
			this.StatusLbl1.ForeColor = System.Drawing.Color.Crimson;
			this.StatusLbl1.Name = "StatusLbl1";
			this.StatusLbl1.Size = new System.Drawing.Size(51, 25);
			this.StatusLbl1.Text = "DIYC";
			// 
			// button1
			// 
			this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button1.Location = new System.Drawing.Point(768, 415);
			this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(164, 100);
			this.button1.TabIndex = 4;
			this.button1.Text = "Save Configuration Settings";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.chkHideLaunchedWindows);
			this.groupBox1.Location = new System.Drawing.Point(18, 18);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.groupBox1.Size = new System.Drawing.Size(572, 74);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Launcher Options";
			// 
			// chkHideLaunchedWindows
			// 
			this.chkHideLaunchedWindows.AutoSize = true;
			this.chkHideLaunchedWindows.Location = new System.Drawing.Point(14, 29);
			this.chkHideLaunchedWindows.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.chkHideLaunchedWindows.Name = "chkHideLaunchedWindows";
			this.chkHideLaunchedWindows.Size = new System.Drawing.Size(211, 24);
			this.chkHideLaunchedWindows.TabIndex = 0;
			this.chkHideLaunchedWindows.Text = "Hide Launched Windows";
			this.chkHideLaunchedWindows.UseVisualStyleBackColor = true;
			this.chkHideLaunchedWindows.CheckedChanged += new System.EventHandler(this.chkHideLaunchedWindows_CheckedChanged);
			// 
			// SetupForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(945, 558);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox2);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
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
	}
}