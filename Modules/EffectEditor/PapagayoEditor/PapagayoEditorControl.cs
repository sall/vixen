using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Vixen.Module.EffectEditor;
using Vixen.Module.Effect;
using Vixen.Sys;

namespace VixenModules.EffectEditor.PapagayoEditor
{
    public partial class  PapagayoEditorControl : UserControl, IEffectEditorControl
    {
        private static NLog.Logger Logging = NLog.LogManager.GetCurrentClassLogger();
        public PapagayoEditorControl()
        {
            InitializeComponent();
        }


        public object[] EffectParameterValues
        {
            get
            {
                return new object[]
				       	{
				       	};
            }
            set
            {
                
            }
        }


        private IEffect _targetEffect;
        public IEffect TargetEffect
        {
            get { return _targetEffect; }
            set
            {
                _targetEffect = value;
                
            }
        }
    }
}
