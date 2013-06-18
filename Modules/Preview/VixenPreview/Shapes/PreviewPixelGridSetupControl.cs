﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VixenModules.Preview.VixenPreview.Shapes
{
    public partial class PreviewPixelGridSetupControl : DisplayItemBaseControl
    {
        public PreviewPixelGridSetupControl(PreviewBaseShape shape): base(shape)
        {
            InitializeComponent();
            propertyGrid.SelectedObject = Shape;
            Shape.OnPropertiesChanged += OnPropertiesChanged;
        }

        ~PreviewPixelGridSetupControl()
        {
            Shape.OnPropertiesChanged -= OnPropertiesChanged;
        }

        private void OnPropertiesChanged(object sender, PreviewBaseShape shape)
        {
            propertyGrid.Refresh();
        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            Shapes.PreviewTools.ShowHelp(Properties.Settings.Default.Help_BasicShapes);
        }
    }
}
