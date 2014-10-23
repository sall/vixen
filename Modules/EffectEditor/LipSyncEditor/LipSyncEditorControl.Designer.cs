namespace VixenModules.EffectEditor.LipSyncEditor
{
    partial class LipSyncEditorControl
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
            this.components = new System.ComponentModel.Container();
            VixenModules.App.Curves.Curve curve1 = new VixenModules.App.Curves.Curve();
            VixenModules.App.ColorGradients.ColorGradient colorGradient1 = new VixenModules.App.ColorGradients.ColorGradient();
            this.staticPhoneMeCombo = new System.Windows.Forms.ComboBox();
            this.imageListView = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.mappingComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lyricDataTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.curveTypeEditorControl1 = new VixenModules.EffectEditor.CurveTypeEditor.CurveTypeEditorControl();
            this.gradientOverrideCheckBox = new System.Windows.Forms.CheckBox();
            this.colorGradientTypeEditorControl1 = new VixenModules.EffectEditor.ColorGradientTypeEditor.ColorGradientTypeEditorControl();
            this.SuspendLayout();
            // 
            // staticPhoneMeCombo
            // 
            this.staticPhoneMeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.staticPhoneMeCombo.FormattingEnabled = true;
            this.staticPhoneMeCombo.Location = new System.Drawing.Point(218, 13);
            this.staticPhoneMeCombo.MaxDropDownItems = 12;
            this.staticPhoneMeCombo.Name = "staticPhoneMeCombo";
            this.staticPhoneMeCombo.Size = new System.Drawing.Size(64, 21);
            this.staticPhoneMeCombo.TabIndex = 6;
            this.staticPhoneMeCombo.SelectedIndexChanged += new System.EventHandler(this.staticPhoneMeCombo_SelectedIndexChanged);
            // 
            // imageListView
            // 
            this.imageListView.LargeImageList = this.imageList1;
            this.imageListView.Location = new System.Drawing.Point(18, 40);
            this.imageListView.Name = "imageListView";
            this.imageListView.Size = new System.Drawing.Size(300, 312);
            this.imageListView.SmallImageList = this.imageList1;
            this.imageListView.TabIndex = 7;
            this.imageListView.UseCompatibleStateImageBehavior = false;
            this.imageListView.SelectedIndexChanged += new System.EventHandler(this.imageListView_SelectedIndexChanged);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // mappingComboBox
            // 
            this.mappingComboBox.FormattingEnabled = true;
            this.mappingComboBox.Location = new System.Drawing.Point(355, 13);
            this.mappingComboBox.Name = "mappingComboBox";
            this.mappingComboBox.Size = new System.Drawing.Size(121, 21);
            this.mappingComboBox.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(298, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Mapping:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(160, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Phoneme";
            // 
            // lyricDataTextBox
            // 
            this.lyricDataTextBox.Location = new System.Drawing.Point(65, 12);
            this.lyricDataTextBox.Name = "lyricDataTextBox";
            this.lyricDataTextBox.Size = new System.Drawing.Size(81, 20);
            this.lyricDataTextBox.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Lyric";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(333, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Intensity";
            // 
            // curveTypeEditorControl1
            // 
            curve1.IsCurrentLibraryCurve = false;
            curve1.LibraryReferenceName = "";
            this.curveTypeEditorControl1.CurveValue = curve1;
            this.curveTypeEditorControl1.EffectParameterValues = new object[] {
        ((object)(curve1))};
            this.curveTypeEditorControl1.Location = new System.Drawing.Point(327, 78);
            this.curveTypeEditorControl1.Name = "curveTypeEditorControl1";
            this.curveTypeEditorControl1.Size = new System.Drawing.Size(149, 81);
            this.curveTypeEditorControl1.TabIndex = 15;
            this.curveTypeEditorControl1.TargetEffect = null;
            // 
            // gradientOverrideCheckBox
            // 
            this.gradientOverrideCheckBox.AutoSize = true;
            this.gradientOverrideCheckBox.Location = new System.Drawing.Point(336, 199);
            this.gradientOverrideCheckBox.Name = "gradientOverrideCheckBox";
            this.gradientOverrideCheckBox.Size = new System.Drawing.Size(109, 17);
            this.gradientOverrideCheckBox.TabIndex = 18;
            this.gradientOverrideCheckBox.Text = "Gradient Override";
            this.gradientOverrideCheckBox.UseVisualStyleBackColor = true;
            this.gradientOverrideCheckBox.CheckedChanged += new System.EventHandler(this.gradientOverrideCheckBox_CheckedChanged);
            // 
            // colorGradientTypeEditorControl1
            // 
            colorGradient1.Gammacorrected = false;
            colorGradient1.IsCurrentLibraryGradient = false;
            colorGradient1.LibraryReferenceName = "";
            colorGradient1.Title = null;
            this.colorGradientTypeEditorControl1.ColorGradientValue = colorGradient1;
            this.colorGradientTypeEditorControl1.EffectParameterValues = new object[] {
        ((object)(colorGradient1))};
            this.colorGradientTypeEditorControl1.Location = new System.Drawing.Point(336, 231);
            this.colorGradientTypeEditorControl1.Name = "colorGradientTypeEditorControl1";
            this.colorGradientTypeEditorControl1.Size = new System.Drawing.Size(140, 73);
            this.colorGradientTypeEditorControl1.TabIndex = 19;
            this.colorGradientTypeEditorControl1.TargetEffect = null;
            // 
            // LipSyncEditorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.colorGradientTypeEditorControl1);
            this.Controls.Add(this.gradientOverrideCheckBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.curveTypeEditorControl1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lyricDataTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.mappingComboBox);
            this.Controls.Add(this.imageListView);
            this.Controls.Add(this.staticPhoneMeCombo);
            this.Name = "LipSyncEditorControl";
            this.Size = new System.Drawing.Size(509, 421);
            this.Load += new System.EventHandler(this.LipSyncEditorControl_Load);
            this.Leave += new System.EventHandler(this.LipSyncEditorControl_Leave);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox staticPhoneMeCombo;
        private System.Windows.Forms.ListView imageListView;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ComboBox mappingComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox lyricDataTextBox;
        private System.Windows.Forms.Label label3;
        private CurveTypeEditor.CurveTypeEditorControl curveTypeEditorControl1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox gradientOverrideCheckBox;
        private ColorGradientTypeEditor.ColorGradientTypeEditorControl colorGradientTypeEditorControl1;
    }
}
