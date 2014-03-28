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

namespace VixenModules.EffectEditor.PapagayoEditor
{
    public partial class PapagayoEditorControl : UserControl, IEffectEditorControl
    {
        public PapagayoEditorControl()
        {
            InitializeComponent();
        }

        public IEffect TargetEffect { get; set; }

        public object[] EffectParameterValues
        {
            get { return new object[] { PGOFilename }; }
            set { PGOFilename = "Dude"; }
        }

        public String PGOFilename
        {
            get { return "Test"; }
            set {  }
        }
    }
}
