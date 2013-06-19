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
            this.label1 = new System.Windows.Forms.Label();
            this.trackBarInterval = new System.Windows.Forms.TrackBar();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
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
            this.groupBox1.Location = new System.Drawing.Point(16, 24);
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
            this.groupBox2.Location = new System.Drawing.Point(201, 24);
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
            this.chkEnabled.Location = new System.Drawing.Point(33, 129);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Size = new System.Drawing.Size(53, 17);
            this.chkEnabled.TabIndex = 22;
            this.chkEnabled.Text = "Static";
            this.chkEnabled.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(151, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Interval";
            // 
            // trackBarInterval
            // 
            this.trackBarInterval.Location = new System.Drawing.Point(154, 129);
            this.trackBarInterval.Maximum = 5000;
            this.trackBarInterval.Minimum = 100;
            this.trackBarInterval.Name = "trackBarInterval";
            this.trackBarInterval.Size = new System.Drawing.Size(206, 45);
            this.trackBarInterval.TabIndex = 24;
            this.trackBarInterval.Value = 100;
            this.trackBarInterval.ValueChanged += new System.EventHandler(this.trackBarInterval_ValueChanged);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(166, 154);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 25;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // AlternatingEffectEditorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.trackBarInterval);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkEnabled);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "AlternatingEffectEditorControl";
            this.Size = new System.Drawing.Size(376, 203);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackBarInterval;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
    }
}
