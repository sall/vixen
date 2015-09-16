﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace VixenApplication
{
	partial class VixenApplication
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private IContainer components = null;

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
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("asdfadsa");
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("rewqrewq");
			System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("vbcbxvxc");
			System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("gfdsgfsd");
			System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("ytreyre");
			System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("xvcbxvcx");
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VixenApplication));
			this.contextMenuStripNewSequence = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.menuStripMain = new System.Windows.Forms.MenuStrip();
			this.vixenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.logsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewInstalledModulesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.profilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.systemConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.setupDisplayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.setupPreviewsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.oldConfigurationInterfacesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.setupElementsGroupsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.setupControllersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.setupFiltersPatchingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.executionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.label2 = new System.Windows.Forms.Label();
			this.buttonOpenSequence = new System.Windows.Forms.Button();
			this.buttonNewSequence = new System.Windows.Forms.Button();
			this.groupBoxSequences = new System.Windows.Forms.GroupBox();
			this.listViewRecentSequences = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.labelVersion = new System.Windows.Forms.Label();
			this.groupBoxSystemConfig = new System.Windows.Forms.GroupBox();
			this.buttonSetupDisplay = new System.Windows.Forms.Button();
			this.buttonSetupOutputPreviews = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.labelDebugVersion = new System.Windows.Forms.Label();
			this.toolStripStatusLabelExecutionLight = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabelExecutionState = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel_memory = new System.Windows.Forms.ToolStripStatusLabel();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.labelVixen = new System.Windows.Forms.Label();
			this.menuStripMain.SuspendLayout();
			this.groupBoxSequences.SuspendLayout();
			this.groupBoxSystemConfig.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.statusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// contextMenuStripNewSequence
			// 
			this.contextMenuStripNewSequence.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.contextMenuStripNewSequence.Name = "contextMenuStripNewSequence";
			this.contextMenuStripNewSequence.ShowImageMargin = false;
			this.contextMenuStripNewSequence.Size = new System.Drawing.Size(36, 4);
			// 
			// openFileDialog
			// 
			this.openFileDialog.Multiselect = true;
			// 
			// menuStripMain
			// 
			this.menuStripMain.BackColor = System.Drawing.Color.Transparent;
			this.menuStripMain.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vixenToolStripMenuItem});
			this.menuStripMain.Location = new System.Drawing.Point(0, 0);
			this.menuStripMain.Name = "menuStripMain";
			this.menuStripMain.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
			this.menuStripMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.menuStripMain.Size = new System.Drawing.Size(456, 25);
			this.menuStripMain.TabIndex = 2;
			this.menuStripMain.Text = "menuStrip1";
			// 
			// vixenToolStripMenuItem
			// 
			this.vixenToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logsToolStripMenuItem,
            this.viewInstalledModulesToolStripMenuItem,
            this.profilesToolStripMenuItem,
            this.systemConfigurationToolStripMenuItem,
            this.executionToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
			this.vixenToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.vixenToolStripMenuItem.Name = "vixenToolStripMenuItem";
			this.vixenToolStripMenuItem.Size = new System.Drawing.Size(57, 19);
			this.vixenToolStripMenuItem.Text = "System";
			// 
			// logsToolStripMenuItem
			// 
			this.logsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.logsToolStripMenuItem.Name = "logsToolStripMenuItem";
			this.logsToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
			this.logsToolStripMenuItem.Text = "Logs";
			// 
			// viewInstalledModulesToolStripMenuItem
			// 
			this.viewInstalledModulesToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.viewInstalledModulesToolStripMenuItem.Name = "viewInstalledModulesToolStripMenuItem";
			this.viewInstalledModulesToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
			this.viewInstalledModulesToolStripMenuItem.Text = "View Installed Modules";
			this.viewInstalledModulesToolStripMenuItem.Click += new System.EventHandler(this.viewInstalledModulesToolStripMenuItem_Click);
			// 
			// profilesToolStripMenuItem
			// 
			this.profilesToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.profilesToolStripMenuItem.Name = "profilesToolStripMenuItem";
			this.profilesToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
			this.profilesToolStripMenuItem.Text = "Profiles...";
			this.profilesToolStripMenuItem.Click += new System.EventHandler(this.profilesToolStripMenuItem_Click);
			// 
			// systemConfigurationToolStripMenuItem
			// 
			this.systemConfigurationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setupDisplayToolStripMenuItem,
            this.setupPreviewsToolStripMenuItem,
            this.toolStripSeparator2,
            this.oldConfigurationInterfacesToolStripMenuItem,
            this.toolStripSeparator3,
            this.setupElementsGroupsToolStripMenuItem,
            this.setupControllersToolStripMenuItem,
            this.setupFiltersPatchingToolStripMenuItem});
			this.systemConfigurationToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.systemConfigurationToolStripMenuItem.Name = "systemConfigurationToolStripMenuItem";
			this.systemConfigurationToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
			this.systemConfigurationToolStripMenuItem.Text = "System Configuration";
			// 
			// setupDisplayToolStripMenuItem
			// 
			this.setupDisplayToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.setupDisplayToolStripMenuItem.Name = "setupDisplayToolStripMenuItem";
			this.setupDisplayToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
			this.setupDisplayToolStripMenuItem.Text = "Setup Display";
			this.setupDisplayToolStripMenuItem.Click += new System.EventHandler(this.setupDisplayToolStripMenuItem_Click);
			// 
			// setupPreviewsToolStripMenuItem
			// 
			this.setupPreviewsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.setupPreviewsToolStripMenuItem.Name = "setupPreviewsToolStripMenuItem";
			this.setupPreviewsToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
			this.setupPreviewsToolStripMenuItem.Text = "Setup Previews";
			this.setupPreviewsToolStripMenuItem.Click += new System.EventHandler(this.setupPreviewsToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(218, 6);
			// 
			// oldConfigurationInterfacesToolStripMenuItem
			// 
			this.oldConfigurationInterfacesToolStripMenuItem.Enabled = false;
			this.oldConfigurationInterfacesToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.oldConfigurationInterfacesToolStripMenuItem.Name = "oldConfigurationInterfacesToolStripMenuItem";
			this.oldConfigurationInterfacesToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
			this.oldConfigurationInterfacesToolStripMenuItem.Text = "Old Configuration Interfaces";
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(218, 6);
			// 
			// setupElementsGroupsToolStripMenuItem
			// 
			this.setupElementsGroupsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.setupElementsGroupsToolStripMenuItem.Name = "setupElementsGroupsToolStripMenuItem";
			this.setupElementsGroupsToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
			this.setupElementsGroupsToolStripMenuItem.Text = "Setup Elements && Groups";
			this.setupElementsGroupsToolStripMenuItem.Click += new System.EventHandler(this.setupElementsGroupsToolStripMenuItem_Click);
			// 
			// setupControllersToolStripMenuItem
			// 
			this.setupControllersToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.setupControllersToolStripMenuItem.Name = "setupControllersToolStripMenuItem";
			this.setupControllersToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
			this.setupControllersToolStripMenuItem.Text = "Setup Controllers";
			this.setupControllersToolStripMenuItem.Click += new System.EventHandler(this.setupControllersToolStripMenuItem_Click);
			// 
			// setupFiltersPatchingToolStripMenuItem
			// 
			this.setupFiltersPatchingToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.setupFiltersPatchingToolStripMenuItem.Name = "setupFiltersPatchingToolStripMenuItem";
			this.setupFiltersPatchingToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
			this.setupFiltersPatchingToolStripMenuItem.Text = "Setup Filters && Patching";
			this.setupFiltersPatchingToolStripMenuItem.Click += new System.EventHandler(this.setupFiltersPatchingToolStripMenuItem_Click);
			// 
			// executionToolStripMenuItem
			// 
			this.executionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem});
			this.executionToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.executionToolStripMenuItem.Name = "executionToolStripMenuItem";
			this.executionToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
			this.executionToolStripMenuItem.Text = "Execution Engine";
			// 
			// startToolStripMenuItem
			// 
			this.startToolStripMenuItem.Name = "startToolStripMenuItem";
			this.startToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
			this.startToolStripMenuItem.Text = "Start";
			this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
			// 
			// stopToolStripMenuItem
			// 
			this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
			this.stopToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
			this.stopToolStripMenuItem.Text = "Stop";
			this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(192, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
			this.exitToolStripMenuItem.Text = "Shutdown and E&xit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.label2.Location = new System.Drawing.Point(8, 89);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(102, 13);
			this.label2.TabIndex = 8;
			this.label2.Text = "Recent Sequences:";
			// 
			// buttonOpenSequence
			// 
			this.buttonOpenSequence.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.buttonOpenSequence.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.buttonOpenSequence.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonOpenSequence.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.buttonOpenSequence.Location = new System.Drawing.Point(12, 54);
			this.buttonOpenSequence.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.buttonOpenSequence.Name = "buttonOpenSequence";
			this.buttonOpenSequence.Size = new System.Drawing.Size(180, 30);
			this.buttonOpenSequence.TabIndex = 2;
			this.buttonOpenSequence.Text = "Open Sequence...";
			this.buttonOpenSequence.UseVisualStyleBackColor = true;
			this.buttonOpenSequence.Click += new System.EventHandler(this.buttonOpenSequence_Click);
			this.buttonOpenSequence.MouseLeave += new System.EventHandler(this.buttonBackground_MouseLeave);
			this.buttonOpenSequence.MouseHover += new System.EventHandler(this.buttonBackground_MouseHover);
			// 
			// buttonNewSequence
			// 
			this.buttonNewSequence.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.buttonNewSequence.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.buttonNewSequence.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonNewSequence.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.buttonNewSequence.Location = new System.Drawing.Point(12, 17);
			this.buttonNewSequence.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.buttonNewSequence.Name = "buttonNewSequence";
			this.buttonNewSequence.Size = new System.Drawing.Size(180, 30);
			this.buttonNewSequence.TabIndex = 1;
			this.buttonNewSequence.Text = "New Sequence...";
			this.buttonNewSequence.UseVisualStyleBackColor = true;
			this.buttonNewSequence.Click += new System.EventHandler(this.buttonNewSequence_Click);
			this.buttonNewSequence.MouseLeave += new System.EventHandler(this.buttonBackground_MouseLeave);
			this.buttonNewSequence.MouseHover += new System.EventHandler(this.buttonBackground_MouseHover);
			// 
			// groupBoxSequences
			// 
			this.groupBoxSequences.Controls.Add(this.listViewRecentSequences);
			this.groupBoxSequences.Controls.Add(this.buttonNewSequence);
			this.groupBoxSequences.Controls.Add(this.buttonOpenSequence);
			this.groupBoxSequences.Controls.Add(this.label2);
			this.groupBoxSequences.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.groupBoxSequences.Location = new System.Drawing.Point(13, 205);
			this.groupBoxSequences.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.groupBoxSequences.Name = "groupBoxSequences";
			this.groupBoxSequences.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.groupBoxSequences.Size = new System.Drawing.Size(206, 231);
			this.groupBoxSequences.TabIndex = 0;
			this.groupBoxSequences.TabStop = false;
			this.groupBoxSequences.Text = "Sequences";
			this.groupBoxSequences.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBoxes_Paint);
			// 
			// listViewRecentSequences
			// 
			this.listViewRecentSequences.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
			this.listViewRecentSequences.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
			this.listViewRecentSequences.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.listViewRecentSequences.FullRowSelect = true;
			this.listViewRecentSequences.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.listViewRecentSequences.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6});
			this.listViewRecentSequences.Location = new System.Drawing.Point(11, 108);
			this.listViewRecentSequences.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.listViewRecentSequences.MultiSelect = false;
			this.listViewRecentSequences.Name = "listViewRecentSequences";
			this.listViewRecentSequences.Size = new System.Drawing.Size(180, 113);
			this.listViewRecentSequences.TabIndex = 0;
			this.listViewRecentSequences.UseCompatibleStateImageBehavior = false;
			this.listViewRecentSequences.View = System.Windows.Forms.View.Details;
			this.listViewRecentSequences.DoubleClick += new System.EventHandler(this.listViewRecentSequences_DoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Width = 150;
			// 
			// labelVersion
			// 
			this.labelVersion.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.labelVersion.Location = new System.Drawing.Point(321, 129);
			this.labelVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelVersion.Name = "labelVersion";
			this.labelVersion.Size = new System.Drawing.Size(106, 38);
			this.labelVersion.TabIndex = 1;
			this.labelVersion.Text = "[0.0.0]";
			// 
			// groupBoxSystemConfig
			// 
			this.groupBoxSystemConfig.Controls.Add(this.buttonSetupDisplay);
			this.groupBoxSystemConfig.Controls.Add(this.buttonSetupOutputPreviews);
			this.groupBoxSystemConfig.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.groupBoxSystemConfig.Location = new System.Drawing.Point(235, 205);
			this.groupBoxSystemConfig.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.groupBoxSystemConfig.Name = "groupBoxSystemConfig";
			this.groupBoxSystemConfig.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.groupBoxSystemConfig.Size = new System.Drawing.Size(208, 94);
			this.groupBoxSystemConfig.TabIndex = 1;
			this.groupBoxSystemConfig.TabStop = false;
			this.groupBoxSystemConfig.Text = "System Configuration";
			this.groupBoxSystemConfig.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBoxes_Paint);
			// 
			// buttonSetupDisplay
			// 
			this.buttonSetupDisplay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.buttonSetupDisplay.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.buttonSetupDisplay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonSetupDisplay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.buttonSetupDisplay.Location = new System.Drawing.Point(13, 17);
			this.buttonSetupDisplay.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.buttonSetupDisplay.Name = "buttonSetupDisplay";
			this.buttonSetupDisplay.Size = new System.Drawing.Size(180, 30);
			this.buttonSetupDisplay.TabIndex = 3;
			this.buttonSetupDisplay.Text = "Setup Display";
			this.buttonSetupDisplay.UseVisualStyleBackColor = true;
			this.buttonSetupDisplay.Click += new System.EventHandler(this.buttonSetupDisplay_Click);
			this.buttonSetupDisplay.MouseLeave += new System.EventHandler(this.buttonBackground_MouseLeave);
			this.buttonSetupDisplay.MouseHover += new System.EventHandler(this.buttonBackground_MouseHover);
			// 
			// buttonSetupOutputPreviews
			// 
			this.buttonSetupOutputPreviews.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.buttonSetupOutputPreviews.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.buttonSetupOutputPreviews.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonSetupOutputPreviews.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.buttonSetupOutputPreviews.Location = new System.Drawing.Point(13, 54);
			this.buttonSetupOutputPreviews.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.buttonSetupOutputPreviews.Name = "buttonSetupOutputPreviews";
			this.buttonSetupOutputPreviews.Size = new System.Drawing.Size(180, 30);
			this.buttonSetupOutputPreviews.TabIndex = 4;
			this.buttonSetupOutputPreviews.Text = "Setup Previews";
			this.buttonSetupOutputPreviews.UseVisualStyleBackColor = true;
			this.buttonSetupOutputPreviews.Click += new System.EventHandler(this.buttonSetupOutputPreviews_Click);
			this.buttonSetupOutputPreviews.MouseLeave += new System.EventHandler(this.buttonBackground_MouseLeave);
			this.buttonSetupOutputPreviews.MouseHover += new System.EventHandler(this.buttonBackground_MouseHover);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(13, 30);
			this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(430, 165);
			this.pictureBox1.TabIndex = 14;
			this.pictureBox1.TabStop = false;
			// 
			// labelDebugVersion
			// 
			this.labelDebugVersion.Font = new System.Drawing.Font("Arial", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelDebugVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.labelDebugVersion.Location = new System.Drawing.Point(323, 161);
			this.labelDebugVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelDebugVersion.Name = "labelDebugVersion";
			this.labelDebugVersion.Size = new System.Drawing.Size(117, 34);
			this.labelDebugVersion.TabIndex = 16;
			this.labelDebugVersion.Text = "[0.0.0]";
			// 
			// toolStripStatusLabelExecutionLight
			// 
			this.toolStripStatusLabelExecutionLight.AutoSize = false;
			this.toolStripStatusLabelExecutionLight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.toolStripStatusLabelExecutionLight.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
			this.toolStripStatusLabelExecutionLight.Name = "toolStripStatusLabelExecutionLight";
			this.toolStripStatusLabelExecutionLight.Size = new System.Drawing.Size(22, 22);
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(13, 22);
			this.toolStripStatusLabel1.Text = "  ";
			// 
			// toolStripStatusLabelExecutionState
			// 
			this.toolStripStatusLabelExecutionState.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
			this.toolStripStatusLabelExecutionState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.toolStripStatusLabelExecutionState.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
			this.toolStripStatusLabelExecutionState.Name = "toolStripStatusLabelExecutionState";
			this.toolStripStatusLabelExecutionState.Size = new System.Drawing.Size(119, 22);
			this.toolStripStatusLabelExecutionState.Text = "Execution: Unknown";
			// 
			// toolStripStatusLabel_memory
			// 
			this.toolStripStatusLabel_memory.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripStatusLabel_memory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
			this.toolStripStatusLabel_memory.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
			this.toolStripStatusLabel_memory.Name = "toolStripStatusLabel_memory";
			this.toolStripStatusLabel_memory.Size = new System.Drawing.Size(279, 22);
			this.toolStripStatusLabel_memory.Spring = true;
			this.toolStripStatusLabel_memory.Text = "Resource Usage";
			this.toolStripStatusLabel_memory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// statusStrip
			// 
			this.statusStrip.AutoSize = false;
			this.statusStrip.GripMargin = new System.Windows.Forms.Padding(0);
			this.statusStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelExecutionLight,
            this.toolStripStatusLabel1,
            this.toolStripStatusLabelExecutionState,
            this.toolStripStatusLabel_memory});
			this.statusStrip.Location = new System.Drawing.Point(0, 441);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
			this.statusStrip.Size = new System.Drawing.Size(456, 24);
			this.statusStrip.SizingGrip = false;
			this.statusStrip.TabIndex = 13;
			this.statusStrip.Text = "statusStrip";
			// 
			// labelVixen
			// 
			this.labelVixen.AutoSize = true;
			this.labelVixen.BackColor = System.Drawing.Color.Transparent;
			this.labelVixen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.labelVixen.Font = new System.Drawing.Font("Microsoft Sans Serif", 65F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelVixen.Location = new System.Drawing.Point(153, 31);
			this.labelVixen.Name = "labelVixen";
			this.labelVixen.Size = new System.Drawing.Size(265, 98);
			this.labelVixen.TabIndex = 17;
			this.labelVixen.Text = "Vixen";
			// 
			// VixenApplication
			// 
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
			this.ClientSize = new System.Drawing.Size(456, 465);
			this.Controls.Add(this.labelVixen);
			this.Controls.Add(this.labelDebugVersion);
			this.Controls.Add(this.statusStrip);
			this.Controls.Add(this.labelVersion);
			this.Controls.Add(this.groupBoxSystemConfig);
			this.Controls.Add(this.groupBoxSequences);
			this.Controls.Add(this.menuStripMain);
			this.Controls.Add(this.pictureBox1);
			this.DoubleBuffered = true;
			this.MainMenuStrip = this.menuStripMain;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(472, 504);
			this.MinimumSize = new System.Drawing.Size(472, 504);
			this.Name = "VixenApplication";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Vixen Administration";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VixenApp_FormClosing);
			this.Load += new System.EventHandler(this.VixenApplication_Load);
			this.Shown += new System.EventHandler(this.VixenApplication_Shown);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.VixenApplication_Paint);
			this.menuStripMain.ResumeLayout(false);
			this.menuStripMain.PerformLayout();
			this.groupBoxSequences.ResumeLayout(false);
			this.groupBoxSequences.PerformLayout();
			this.groupBoxSystemConfig.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private ContextMenuStrip contextMenuStripNewSequence;
		private OpenFileDialog openFileDialog;
		private MenuStrip menuStripMain;
		private ToolStripMenuItem vixenToolStripMenuItem;
		private ToolStripMenuItem executionToolStripMenuItem;
		private ToolStripMenuItem startToolStripMenuItem;
		private ToolStripMenuItem stopToolStripMenuItem;
		private ToolStripSeparator toolStripSeparator1;
		private ToolStripMenuItem exitToolStripMenuItem;
		private Label label2;
		private Button buttonOpenSequence;
		private Button buttonNewSequence;
		private GroupBox groupBoxSequences;
		private GroupBox groupBoxSystemConfig;
		private ToolStripMenuItem logsToolStripMenuItem;
		private ListView listViewRecentSequences;
		private ColumnHeader columnHeader1;
		private ToolStripMenuItem viewInstalledModulesToolStripMenuItem;
		private Label labelVersion;
		private Button buttonSetupOutputPreviews;
		private PictureBox pictureBox1;
        private ToolStripMenuItem profilesToolStripMenuItem;
		private Label labelDebugVersion;
		private Button buttonSetupDisplay;
		private ToolStripMenuItem systemConfigurationToolStripMenuItem;
		private ToolStripMenuItem setupDisplayToolStripMenuItem;
		private ToolStripMenuItem setupPreviewsToolStripMenuItem;
		private ToolStripSeparator toolStripSeparator2;
		private ToolStripMenuItem oldConfigurationInterfacesToolStripMenuItem;
		private ToolStripMenuItem setupElementsGroupsToolStripMenuItem;
		private ToolStripMenuItem setupControllersToolStripMenuItem;
		private ToolStripMenuItem setupFiltersPatchingToolStripMenuItem;
		private ToolStripSeparator toolStripSeparator3;
		private ToolStripStatusLabel toolStripStatusLabelExecutionLight;
		private ToolStripStatusLabel toolStripStatusLabel1;
		private ToolStripStatusLabel toolStripStatusLabelExecutionState;
		private ToolStripStatusLabel toolStripStatusLabel_memory;
		private StatusStrip statusStrip;
		private Label labelVixen;
	}
}

