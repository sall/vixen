namespace VixenModules.App.LipSyncMap
{
    partial class LipSyncMapSelector
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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.listViewMappings = new System.Windows.Forms.ListView();
            this.buttonEditMap = new System.Windows.Forms.Button();
            this.buttonDeleteMap = new System.Windows.Forms.Button();
            this.buttonNewMap = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(386, 273);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(80, 25);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(300, 273);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(80, 25);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // listViewMappings
            // 
            this.listViewMappings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewMappings.Location = new System.Drawing.Point(12, 12);
            this.listViewMappings.Name = "listViewMappings";
            this.listViewMappings.Size = new System.Drawing.Size(454, 249);
            this.listViewMappings.TabIndex = 6;
            this.listViewMappings.UseCompatibleStateImageBehavior = false;
            this.listViewMappings.ItemActivate += new System.EventHandler(this.listViewMappings_ItemActivate);
            this.listViewMappings.SelectedIndexChanged += new System.EventHandler(this.listViewMappings_SelectedIndexChanged);
            this.listViewMappings.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listViewMappings_KeyDown);
            this.listViewMappings.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewMappings_MouseDoubleClick);
            // 
            // buttonEditMap
            // 
            this.buttonEditMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonEditMap.Enabled = false;
            this.buttonEditMap.Location = new System.Drawing.Point(88, 273);
            this.buttonEditMap.Name = "buttonEditMap";
            this.buttonEditMap.Size = new System.Drawing.Size(70, 25);
            this.buttonEditMap.TabIndex = 7;
            this.buttonEditMap.Text = "Edit Map";
            this.buttonEditMap.UseVisualStyleBackColor = true;
            this.buttonEditMap.Click += new System.EventHandler(this.buttonEditMap_Click);
            // 
            // buttonDeleteMap
            // 
            this.buttonDeleteMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDeleteMap.Enabled = false;
            this.buttonDeleteMap.Location = new System.Drawing.Point(164, 273);
            this.buttonDeleteMap.Name = "buttonDeleteMap";
            this.buttonDeleteMap.Size = new System.Drawing.Size(70, 25);
            this.buttonDeleteMap.TabIndex = 8;
            this.buttonDeleteMap.Text = "Delete Map";
            this.buttonDeleteMap.UseVisualStyleBackColor = true;
            this.buttonDeleteMap.Click += new System.EventHandler(this.buttonDeleteMapping_Click);
            // 
            // buttonNewMap
            // 
            this.buttonNewMap.Location = new System.Drawing.Point(12, 273);
            this.buttonNewMap.Name = "buttonNewMap";
            this.buttonNewMap.Size = new System.Drawing.Size(70, 25);
            this.buttonNewMap.TabIndex = 9;
            this.buttonNewMap.Text = "New Map";
            this.buttonNewMap.UseVisualStyleBackColor = true;
            this.buttonNewMap.Click += new System.EventHandler(this.buttonNewMap_Click);
            // 
            // LipSyncMapSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 310);
            this.Controls.Add(this.buttonNewMap);
            this.Controls.Add(this.buttonDeleteMap);
            this.Controls.Add(this.buttonEditMap);
            this.Controls.Add(this.listViewMappings);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "LipSyncMapSelector";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "LipSync Map Library";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LipSyncMapSelector_FormClosing);
            this.Load += new System.EventHandler(this.LipSyncMapSelector_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LipSyncMapSelector_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.ListView listViewMappings;
        private System.Windows.Forms.Button buttonEditMap;
        private System.Windows.Forms.Button buttonDeleteMap;
        private System.Windows.Forms.Button buttonNewMap;
    }
}