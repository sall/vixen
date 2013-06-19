namespace VixenModules.EffectEditor.AlternatingEditor
{
    partial class AlternatingEffectEditorControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.levelTypeEditorControl1 = new VixenModules.EffectEditor.LevelTypeEditor.LevelTypeEditorControl();
            this.colorTypeEditorControl1 = new VixenModules.EffectEditor.ColorTypeEditor.ColorTypeEditorControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.levelTypeEditorControl2 = new VixenModules.EffectEditor.LevelTypeEditor.LevelTypeEditorControl();
            this.colorTypeEditorControl2 = new VixenModules.EffectEditor.ColorTypeEditor.ColorTypeEditorControl();
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            this.trackBarInterval = new System.Windows.Forms.TrackBar();
            this.numChangeInterval = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numGroupEffects = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numChangeInterval)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGroupEffects)).BeginInit();
            this.SuspendLayout();
            // 
            // levelTypeEditorControl1
            // 
            this.levelTypeEditorControl1.EffectParameterValues = new object[] {
        ((object)(1D))};
            this.levelTypeEditorControl1.LevelValue = 1D;
            this.levelTypeEditorControl1.Location = new System.Drawing.Point(63, 19);
            this.levelTypeEditorControl1.Name = "levelTypeEditorControl1";
            this.levelTypeEditorControl1.Size = new System.Drawing.Size(90, 39);
            this.levelTypeEditorControl1.TabIndex = 18;
            this.levelTypeEditorControl1.TargetEffect = null;
            // 
            // colorTypeEditorControl1
            // 
            this.colorTypeEditorControl1.ColorValue = System.Drawing.Color.Empty;
            this.colorTypeEditorControl1.EffectParameterValues = new object[] {
        ((object)(System.Drawing.Color.Empty))};
            this.colorTypeEditorControl1.Location = new System.Drawing.Point(17, 19);
            this.colorTypeEditorControl1.Name = "colorTypeEditorControl1";
            this.colorTypeEditorControl1.Size = new System.Drawing.Size(40, 40);
            this.colorTypeEditorControl1.TabIndex = 17;
            this.colorTypeEditorControl1.TargetEffect = null;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.levelTypeEditorControl1);
            this.groupBox1.Controls.Add(this.colorTypeEditorControl1);
            this.groupBox1.Location = new System.Drawing.Point(3, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(159, 78);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Color 1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.levelTypeEditorControl2);
            this.groupBox2.Controls.Add(this.colorTypeEditorControl2);
            this.groupBox2.Location = new System.Drawing.Point(168, 14);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(159, 78);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Color 2";
            // 
            // levelTypeEditorControl2
            // 
            this.levelTypeEditorControl2.EffectParameterValues = new object[] {
        ((object)(1D))};
            this.levelTypeEditorControl2.LevelValue = 1D;
            this.levelTypeEditorControl2.Location = new System.Drawing.Point(63, 19);
            this.levelTypeEditorControl2.Name = "levelTypeEditorControl2";
            this.levelTypeEditorControl2.Size = new System.Drawing.Size(90, 39);
            this.levelTypeEditorControl2.TabIndex = 18;
            this.levelTypeEditorControl2.TargetEffect = null;
            // 
            // colorTypeEditorControl2
            // 
            this.colorTypeEditorControl2.ColorValue = System.Drawing.Color.Empty;
            this.colorTypeEditorControl2.EffectParameterValues = new object[] {
        ((object)(System.Drawing.Color.Empty))};
            this.colorTypeEditorControl2.Location = new System.Drawing.Point(17, 19);
            this.colorTypeEditorControl2.Name = "colorTypeEditorControl2";
            this.colorTypeEditorControl2.Size = new System.Drawing.Size(40, 40);
            this.colorTypeEditorControl2.TabIndex = 17;
            this.colorTypeEditorControl2.TargetEffect = null;
            // 
            // chkEnabled
            // 
            this.chkEnabled.AutoSize = true;
            this.chkEnabled.Location = new System.Drawing.Point(20, 114);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkEnabled.Size = new System.Drawing.Size(120, 17);
            this.chkEnabled.TabIndex = 22;
            this.chkEnabled.Text = "Make Display Static";
            this.chkEnabled.UseVisualStyleBackColor = true;
            // 
            // trackBarInterval
            // 
            this.trackBarInterval.Dock = System.Windows.Forms.DockStyle.Top;
            this.trackBarInterval.Location = new System.Drawing.Point(3, 16);
            this.trackBarInterval.Maximum = 5000;
            this.trackBarInterval.Minimum = 100;
            this.trackBarInterval.Name = "trackBarInterval";
            this.trackBarInterval.Size = new System.Drawing.Size(153, 45);
            this.trackBarInterval.TabIndex = 24;
            this.trackBarInterval.Value = 100;
            this.trackBarInterval.ValueChanged += new System.EventHandler(this.trackBarInterval_ValueChanged);
            // 
            // numChangeInterval
            // 
            this.numChangeInterval.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.numChangeInterval.Location = new System.Drawing.Point(3, 49);
            this.numChangeInterval.Name = "numChangeInterval";
            this.numChangeInterval.Size = new System.Drawing.Size(153, 20);
            this.numChangeInterval.TabIndex = 25;
            this.numChangeInterval.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.numChangeInterval);
            this.groupBox3.Controls.Add(this.trackBarInterval);
            this.groupBox3.Location = new System.Drawing.Point(168, 98);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(159, 72);
            this.groupBox3.TabIndex = 28;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Change Interval";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.numGroupEffects);
            this.groupBox4.Location = new System.Drawing.Point(6, 147);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(159, 71);
            this.groupBox4.TabIndex = 29;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Grouping && Display";
            this.groupBox4.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Element(s)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Group Effect to ";
            // 
            // numGroupEffects
            // 
            this.numGroupEffects.Location = new System.Drawing.Point(95, 20);
            this.numGroupEffects.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numGroupEffects.Name = "numGroupEffects";
            this.numGroupEffects.Size = new System.Drawing.Size(48, 20);
            this.numGroupEffects.TabIndex = 0;
            this.numGroupEffects.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // AlternatingEffectEditorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.chkEnabled);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "AlternatingEffectEditorControl";
            this.Size = new System.Drawing.Size(331, 175);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numChangeInterval)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGroupEffects)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LevelTypeEditor.LevelTypeEditorControl levelTypeEditorControl1;
        private ColorTypeEditor.ColorTypeEditorControl colorTypeEditorControl1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private LevelTypeEditor.LevelTypeEditorControl levelTypeEditorControl2;
        private ColorTypeEditor.ColorTypeEditorControl colorTypeEditorControl2;
        private System.Windows.Forms.CheckBox chkEnabled;
        private System.Windows.Forms.TrackBar trackBarInterval;
        private System.Windows.Forms.NumericUpDown numChangeInterval;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numGroupEffects;
    }
}
