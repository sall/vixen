using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Vixen.Data.Flow;
using Vixen.Module.Effect;
using Vixen.Module.OutputFilter;
using Vixen.Module.Property;
using Vixen.Rule;
using Vixen.Services;
using Vixen.Sys;
using Vixen.Module;


namespace VixenModules.OutputFilter.LipSyncBreakdown
{
    public partial class LipSyncBreakdownSetup : Form, IElementSetupHelper
    {
        private  LipSyncBreakdownData _data;
        private static NLog.Logger Logging = NLog.LogManager.GetCurrentClassLogger();

        public LipSyncBreakdownSetup()
		{
			InitializeComponent();
		}

        public LipSyncBreakdownSetup(LipSyncBreakdownData breakdownData)
        {
            InitializeComponent();
            _data = breakdownData;
        }

        public List<LipSyncBreakdownItem> BreakdownItems
        {
            get
            {
                return
                    tableLayoutPanelControls.Controls.OfType<LipSyncBreakdownItemControl>().Select(
                        itemControl => itemControl.LipSyncBreakdownItem).ToList();
            }
        }

        public string HelperName
        {
            get { return "Phoneme Mapping"; }
        }

        private IEnumerable<IDataFlowComponentReference> _FindLeafOutputsOrDimmingCurveFilters(IDataFlowComponent component)
        {
            if (component == null)
            {
                yield break;
            }

            if (component.Outputs == null || component.OutputDataType == DataFlowType.None)
            {
                yield break;
            }

            for (int i = 0; i < component.Outputs.Length; i++)
            {
                IEnumerable<IDataFlowComponent> children = VixenSystem.DataFlow.GetDestinationsOfComponentOutput(component, i);

                if (!children.Any())
                {
                    yield return new DataFlowComponentReference(component, i);
                }
                else
                {
                    foreach (IDataFlowComponent child in children)
                    {
                        foreach (IDataFlowComponentReference result in _FindLeafOutputsOrDimmingCurveFilters(child))
                        {
                            yield return result;
                        }
                    }
                }
            }
        }

        public bool Perform(IEnumerable<ElementNode> selectedNodes)
        {
            DialogResult dr = ShowDialog();
            if (dr != DialogResult.OK)
                return false;

            IEnumerable<ElementNode> leafElements = selectedNodes.SelectMany(x => x.GetLeafEnumerator()).Distinct();
            int modulesCreated = 0;
            int modulesConfigured = 0;
            int modulesSkipped = 0;

            foreach (ElementNode leafNode in leafElements)
            {

                // get the leaf 'things' to deal with -- ie. either existing dimming curves on a filter branch, or data component outputs
                // (if we're adding new ones, ignore any existing dimming curves: always go to the outputs and we'll add new ones)
                IDataFlowComponent elementComponent = VixenSystem.DataFlow.GetComponent(leafNode.Element.Id);
                IEnumerable<IDataFlowComponentReference> references = _FindLeafOutputsOrDimmingCurveFilters(elementComponent);

                foreach (IDataFlowComponentReference reference in references)
                {
                    int outputIndex = reference.OutputIndex;
/*
                    if (reference.Component is LipSyncBreakdownModule)
                    {
                        switch (existingBehaviour)
                        {
                            case ExistingBehaviour.DoNothing:
                                modulesSkipped++;
                                continue;

                            case ExistingBehaviour.UpdateExisting:
                                (reference.Component as DimmingCurveModule).DimmingCurve = _curve;
                                modulesConfigured++;
                                continue;

                            case ExistingBehaviour.AddNew:
                                outputIndex = 0;
                                break;
                        }
                    }
*/
                    outputIndex = 0;

                    // assuming we're making a new one and going from there
                    LipSyncBreakdownModule breakdownModule = ApplicationServices.Get<IOutputFilterModuleInstance>(LipSyncBreakdownDescriptor.ModuleId) as LipSyncBreakdownModule;
                    VixenSystem.DataFlow.SetComponentSource(breakdownModule, reference.Component, outputIndex);
                    VixenSystem.Filters.AddFilter(breakdownModule);

                    modulesCreated++;
                    modulesConfigured++;
                }
            }

            MessageBox.Show(modulesCreated + " Phoneme Curves created, " + modulesConfigured + " configured, and " + modulesSkipped + " skipped.");
            return true;
        }

        private void LipSyncBreakdownSetup_Load(object sender, EventArgs e)
        {
//            tableLayoutPanelControls.Controls.Clear();
//
//            foreach (LipSyncBreakdownItem breakdownItem in _data.BreakdownItems)
//            {
//                addControl(new LipSyncBreakdownItemControl(breakdownItem));
//            }

            // let's just make up some hardcoded templates. Can expand on this later; probably don't need to,
            // people can request new ones and stuff if they want.
            comboBoxTemplates.Items.Clear();
            comboBoxTemplates.Items.Add("HolidayCoro");
            comboBoxTemplates.SelectedIndex = 0;
        }

        private void buttonAddString_Click(object sender, EventArgs e)
        {
            addControl(new LipSyncBreakdownItemControl());
        }

        private void control_DeleteRequested(object sender, EventArgs e)
        {
            LipSyncBreakdownItemControl control = sender as LipSyncBreakdownItemControl;
            if (control == null)
                return;

            removeControl(control);
        }

        private void removeControl(LipSyncBreakdownItemControl control)
        {
            if (!tableLayoutPanelControls.Controls.Contains(control))
                return;

            tableLayoutPanelControls.Controls.Remove(control);
            control.DeleteRequested -= control_DeleteRequested;
        }

        private void addControl(LipSyncBreakdownItemControl control)
        {
            control.DeleteRequested += control_DeleteRequested;
            tableLayoutPanelControls.Controls.Add(control);
        }

        private void buttonApplyTemplate_Click(object sender, EventArgs e)
        {
            foreach (LipSyncBreakdownItemControl control in tableLayoutPanelControls.Controls.OfType<LipSyncBreakdownItemControl>())
            {
				removeControl(control);
			}

			tableLayoutPanelControls.Controls.Clear();

			string template = comboBoxTemplates.SelectedItem.ToString();
			switch (template) 
            {
				case "HolidayCoro":
                    LipSyncBreakdownItem outlineItem = new LipSyncBreakdownItem();
                    outlineItem.Name = "Outline";
                    outlineItem.PhonemeList.Clear();
                    outlineItem.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("AI", true));
                    outlineItem.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("E", true));
                    outlineItem.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("O", true));
                    outlineItem.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("U", true));
                    outlineItem.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("FV", true));
                    outlineItem.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("L", true));
                    outlineItem.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("MBP", true));
                    outlineItem.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("WQ", true));
                    outlineItem.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("etc", true));
                    outlineItem.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("Rest", true));
                    addControl(new LipSyncBreakdownItemControl(outlineItem));

                    LipSyncBreakdownItem eyesTop = new LipSyncBreakdownItem();
                    eyesTop.Name = "Eyes Top";
                    eyesTop.PhonemeList.Clear();
                    eyesTop.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("AI", true));
                    eyesTop.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("E", true));
                    eyesTop.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("O", true));
                    eyesTop.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("U", true));
                    eyesTop.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("FV", true));
                    eyesTop.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("L", true));
                    eyesTop.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("MBP", true));
                    eyesTop.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("WQ", true));
                    eyesTop.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("etc", true));
                    eyesTop.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("Rest", true));
                    addControl(new LipSyncBreakdownItemControl(eyesTop));

                    LipSyncBreakdownItem eyesBottom = new LipSyncBreakdownItem();
                    eyesBottom.Name = "Eyes Bottom";
                    eyesBottom.PhonemeList.Clear();
                    eyesBottom.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("AI", true));
                    eyesBottom.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("E", true));
                    eyesBottom.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("O", true));
                    eyesBottom.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("U", true));
                    eyesBottom.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("FV", true));
                    eyesBottom.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("L", true));
                    eyesBottom.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("MBP", true));
                    eyesBottom.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("WQ", true));
                    eyesBottom.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("etc", true));
                    eyesBottom.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("Rest", true));
                    addControl(new LipSyncBreakdownItemControl(eyesBottom));

                    LipSyncBreakdownItem mouthTop = new LipSyncBreakdownItem();
                    mouthTop.Name = "Mouth Top";
                    mouthTop.PhonemeList.Clear();
                    mouthTop.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("AI", true));
                    mouthTop.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("E", true));
                    mouthTop.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("O", false));
                    mouthTop.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("U", false));
                    mouthTop.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("FV", false));
                    mouthTop.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("L", true));
                    mouthTop.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("MBP", true));
                    mouthTop.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("WQ", false));
                    mouthTop.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("etc", false));
                    mouthTop.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("Rest", false));
                    addControl(new LipSyncBreakdownItemControl(mouthTop));

                    LipSyncBreakdownItem mouthMiddle = new LipSyncBreakdownItem();
                    mouthMiddle.Name = "Mouth Middle";
                    mouthMiddle.PhonemeList.Clear();
                    mouthMiddle.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("AI", false));
                    mouthMiddle.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("E", true));
                    mouthMiddle.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("O", false));
                    mouthMiddle.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("U", false));
                    mouthMiddle.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("FV", false));
                    mouthMiddle.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("L", true));
                    mouthMiddle.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("MBP", false));
                    mouthMiddle.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("WQ", false));
                    mouthMiddle.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("etc", false));
                    mouthMiddle.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("Rest", false));
                    addControl(new LipSyncBreakdownItemControl(mouthMiddle));

                    LipSyncBreakdownItem mouthBottom = new LipSyncBreakdownItem();
                    mouthBottom.Name = "Mouth Bottom";
                    mouthBottom.PhonemeList.Clear();
                    mouthBottom.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("AI", true));
                    mouthBottom.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("E", false));
                    mouthBottom.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("O", false));
                    mouthBottom.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("U", false));
                    mouthBottom.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("FV", false));
                    mouthBottom.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("L", false));
                    mouthBottom.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("MBP", false));
                    mouthBottom.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("WQ", false));
                    mouthBottom.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("etc", false));
                    mouthBottom.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("Rest", false));
                    addControl(new LipSyncBreakdownItemControl(mouthBottom));

                    LipSyncBreakdownItem mouthNarrow = new LipSyncBreakdownItem();
                    mouthNarrow.Name = "Mouth Narrow";
                    mouthNarrow.PhonemeList.Clear();
                    mouthNarrow.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("AI", false));
                    mouthNarrow.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("E", false));
                    mouthNarrow.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("O", false));
                    mouthNarrow.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("U", false));
                    mouthNarrow.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("FV", true));
                    mouthNarrow.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("L", false));
                    mouthNarrow.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("MBP", false));
                    mouthNarrow.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("WQ", false));
                    mouthNarrow.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("etc", true));
                    mouthNarrow.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("Rest", false));
                    addControl(new LipSyncBreakdownItemControl(mouthNarrow));

                    LipSyncBreakdownItem mouthO = new LipSyncBreakdownItem();
                    mouthO.Name = "Mouth O";
                    mouthO.PhonemeList.Clear();
                    mouthO.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("AI", false));
                    mouthO.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("E", false));
                    mouthO.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("O", true));
                    mouthO.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("U", true));
                    mouthO.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("FV", false));
                    mouthO.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("L", false));
                    mouthO.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("MBP", false));
                    mouthO.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("WQ", true));
                    mouthO.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("etc", true));
                    mouthO.PhonemeList.Add(new LipSyncBreakdownItemPhoneme("Rest", false));
                    addControl(new LipSyncBreakdownItemControl(mouthO));
                    break;

				default:
					Logging.Error("Color Breakdown Setup: got an unknown template to apply: " + template);
					MessageBox.Show("Error applying template: Unknown template.");
					break;
			}
		}
    }
}
