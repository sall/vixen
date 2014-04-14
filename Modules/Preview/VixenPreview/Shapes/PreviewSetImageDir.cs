
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Collections.Generic;

namespace VixenModules.Preview.VixenPreview.Shapes
{
    internal class PreviewSetImageDir : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService svc = (IWindowsFormsEditorService)
                                             provider.GetService(typeof(IWindowsFormsEditorService));
            if (svc != null)
            {
                FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
                folderBrowser.ShowDialog();
                return folderBrowser.SelectedPath;
            }
            return null;
        }

    }
}