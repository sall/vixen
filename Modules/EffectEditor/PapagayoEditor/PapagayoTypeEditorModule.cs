using Vixen.Module.EffectEditor;

namespace VixenModules.EffectEditor.PapagayoEditor
{
    public class PapagayoEditorModule : EffectEditorModuleInstanceBase
    {
        public override IEffectEditorControl CreateEditorControl()
        {
            return new PapagayoEditorControl();
        }
    }
}