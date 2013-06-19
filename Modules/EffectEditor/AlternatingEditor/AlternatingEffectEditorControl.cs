using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Vixen.Module.EffectEditor;
using Vixen.Module.Effect;
using Common.Controls.ColorManagement.ColorModels;
using Vixen.Sys;

namespace VixenModules.EffectEditor.AlternatingEditor
{
    public partial class AlternatingEffectEditorControl : UserControl, IEffectEditorControl
    {
        public AlternatingEffectEditorControl()
        {
            InitializeComponent();
            numChangeInterval.Maximum = trackBarInterval.Maximum;
            numChangeInterval.Minimum = trackBarInterval.Minimum;

        }

        public object[] EffectParameterValues
        {
            get
            {
                return new object[] {
                    Level1,
                    Color1,
                    Level2,
                    Color2,
                    Interval,
                    DepthOfEffect, 
                    GroupInterval,
                    Enabled
				};
            }
            set
            {
                if (value.Length != 8)
                {
                    VixenSystem.Logging.Warning("Alternating effect parameters set with " + value.Length + " parameters");
                    return;
                }
                var val = value;
                Level1 = (double)value[0];
                Color1 = (Color)value[1];
                Level2 = (double)value[2];
                Color2 = (Color)value[3];
                Interval = (int)value[4];
                DepthOfEffect = (int)value[5];
                GroupInterval = (int)value[6];
                Enabled = (bool)value[7];
            }
        }


        IEffect _targetEffect;
        public IEffect TargetEffect
        {
            get { return _targetEffect; }
            set { _targetEffect = value; }
        }

        public int Interval
        {
            get { return trackBarInterval.Value; }
            set
            {
                trackBarInterval.Value = value > trackBarInterval.Maximum ? trackBarInterval.Maximum : value;
            }
        }
        public Color Color1 { get { return colorTypeEditorControl1.ColorValue; } set { colorTypeEditorControl1.ColorValue = value; } }
        public Color Color2 { get { return colorTypeEditorControl2.ColorValue; } set { colorTypeEditorControl2.ColorValue = value; } }
        public double Level1 { get { return levelTypeEditorControl1.LevelValue; } set { levelTypeEditorControl1.LevelValue = value; } }
        public double Level2 { get { return levelTypeEditorControl2.LevelValue; } set { levelTypeEditorControl2.LevelValue = value; } }
        public bool Enabled { get { return !this.chkEnabled.Checked; } set { this.chkEnabled.Checked = !value; } }
        public int GroupInterval
        {
            get { return (int)this.numGroupEffects.Value; }
            set
            {
                this.numGroupEffects.Value = value <= 1 ? 1 : value;
            }
        }
        public int DepthOfEffect { get; set; }
        private void trackBarInterval_ValueChanged(object sender, EventArgs e)
        {
            numChangeInterval.Value = trackBarInterval.Value;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            trackBarInterval.Value = (int)numChangeInterval.Value;
        }





    }
}
