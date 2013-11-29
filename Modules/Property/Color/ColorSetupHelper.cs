﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Vixen.Data.Flow;
using Vixen.Module.Effect;
using Vixen.Module.OutputFilter;
using Vixen.Module.Property;
using Vixen.Rule;
using Vixen.Services;
using Vixen.Sys;
using System.Linq;
using VixenModules.OutputFilter.ColorBreakdown;

namespace VixenModules.Property.Color
{
	public partial class ColorSetupHelper : Form, IElementSetupHelper
	{
		private static NLog.Logger Logging = NLog.LogManager.GetCurrentClassLogger();

		public ColorSetupHelper()
		{
			InitializeComponent();
		}

		public string HelperName
		{
			get { return "Color Handling"; }
		}

		public bool Perform(IEnumerable<ElementNode> selectedNodes)
		{
			DialogResult dr = ShowDialog();
			if (dr != DialogResult.OK)
				return false;

			// note: the color property can only be applied to leaf nodes.

			// pull out the new data settings from the form elements
			ElementColorType colorType;
			string colorSetName = "";
			System.Drawing.Color singleColor = System.Drawing.Color.Black;

			if (radioButtonOptionSingle.Checked) {
				colorType = ElementColorType.SingleColor;
				singleColor = colorPanelSingleColor.Color;
			}
			else if (radioButtonOptionMultiple.Checked) {
				colorType = ElementColorType.MultipleDiscreteColors;
				colorSetName = comboBoxColorSet.SelectedItem.ToString();
			}
			else if (radioButtonOptionFullColor.Checked) {
				colorType = ElementColorType.FullColor;
			}
			else {
				Logging.Warn("Unexpected radio option selected");
				colorType = ElementColorType.SingleColor;
			}


			// PROPERTY SETUP
			// go through all elements, making a color property for each one.
			// (If any has one already, check with the user as to what they want to do.)
			IEnumerable<ElementNode> leafElements = selectedNodes.SelectMany(x => x.GetLeafEnumerator()).Distinct();
			List<ElementNode> leafElementList = leafElements.ToList();

			bool askedUserAboutExistingProperties = false;
			bool overrideExistingProperties = false;

			int colorPropertiesAdded = 0;
			int colorPropertiesConfigured = 0;
			int colorPropertiesSkipped = 0;

			foreach (ElementNode leafElement in leafElementList) {
				bool skip = false;
				ColorModule existingProperty = null;

				if (leafElement.Properties.Contains(ColorDescriptor.ModuleId)) {
					if (!askedUserAboutExistingProperties) {
						DialogResult mbr =
							MessageBox.Show("Some elements already have color properties set up. Should these be overwritten?",
							                "Color Setup", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
						overrideExistingProperties = (mbr == DialogResult.Yes);
						askedUserAboutExistingProperties = true;
					}

					skip = !overrideExistingProperties;
					existingProperty = leafElement.Properties.Get(ColorDescriptor.ModuleId) as ColorModule;
				}
				else {
					existingProperty = leafElement.Properties.Add(ColorDescriptor.ModuleId) as ColorModule;
					colorPropertiesAdded++;
				}

				if (!skip) {
					if (existingProperty == null) {
						Logging.Error("Null color property for element " + leafElement.Name);
					}
					else {
						existingProperty.ColorType = colorType;
						existingProperty.SingleColor = singleColor;
						existingProperty.ColorSetName = colorSetName;
						colorPropertiesConfigured++;
					}
				}
				else {
					colorPropertiesSkipped++;
				}
			}


			// PATCHING
			// go through each element, walking the tree of patches, building up a list.  If any are a 'color
			// breakdown' already, warn/check with the user if it's OK to overwrite them.  Make a new breakdown
			// filter for each 'leaf' of the patching process. If it's fully patched to an output, ignore it.

			List<IDataFlowComponentReference> leafOutputs = new List<IDataFlowComponentReference>();
			foreach (ElementNode leafElement in leafElementList.Where(x => x.Element != null)) {
				leafOutputs.AddRange(_FindLeafOutputsOrBreakdownFilters(VixenSystem.DataFlow.GetComponent(leafElement.Element.Id)));
			}

			bool askedUserAboutExistingFilters = false;
			bool overrideExistingFilters = false;
			ColorBreakdownModule breakdown = null;

			int colorFiltersAdded = 0;
			int colorFiltersConfigured = 0;
			int colorFiltersSkipped = 0;

			foreach (IDataFlowComponentReference leaf in leafOutputs) {
				bool skip = false;

				if (leaf.Component is ColorBreakdownModule) {
					if (!askedUserAboutExistingFilters) {
						DialogResult mbr =
							MessageBox.Show("Some elements are already patched to color filters. Should these be overwritten?",
							                "Color Setup", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
						overrideExistingFilters = (mbr == DialogResult.Yes);
						askedUserAboutExistingFilters = true;
					}

					skip = !overrideExistingFilters;
					breakdown = leaf.Component as ColorBreakdownModule;
				}
				else if (leaf.Component.OutputDataType == DataFlowType.None) {
					// if it's a dead-end -- ie. most likely a controller output -- skip it
					skip = true;
				}
				else {
					// doesn't exist? make a new module and assign it
					breakdown =
						ApplicationServices.Get<IOutputFilterModuleInstance>(ColorBreakdownDescriptor.ModuleId) as ColorBreakdownModule;
					VixenSystem.DataFlow.SetComponentSource(breakdown, leaf);
					VixenSystem.Filters.AddFilter(breakdown);
					colorFiltersAdded++;
				}

				if (!skip) {
					List<ColorBreakdownItem> newBreakdownItems = new List<ColorBreakdownItem>();
					bool mixColors = false;
					ColorBreakdownItem cbi;

					switch (colorType) {
						case ElementColorType.FullColor:
							mixColors = true;

							// TODO: really, RGB isn't the only option for 'full color': some people use white as well.  We should allow for that.
							cbi = new ColorBreakdownItem();
							cbi.Color = System.Drawing.Color.Red;
							cbi.Name = "Red";
							newBreakdownItems.Add(cbi);

							cbi = new ColorBreakdownItem();
							cbi.Color = System.Drawing.Color.Lime;
							cbi.Name = "Green";
							newBreakdownItems.Add(cbi);

							cbi = new ColorBreakdownItem();
							cbi.Color = System.Drawing.Color.Blue;
							cbi.Name = "Blue";
							newBreakdownItems.Add(cbi);

							break;

						case ElementColorType.MultipleDiscreteColors:
							mixColors = false;

							ColorStaticData csd = ApplicationServices.GetModuleStaticData(ColorDescriptor.ModuleId) as ColorStaticData;

							if (!csd.ContainsColorSet(colorSetName)) {
								Logging.Error("Color sets doesn't contain " + colorSetName);
							}
							else {
								ColorSet cs = csd.GetColorSet(colorSetName);
								foreach (var c in cs) {
									cbi = new ColorBreakdownItem();
									cbi.Color = c;
									// heh heh, this can be.... creative.
									cbi.Name = c.Name;
									newBreakdownItems.Add(cbi);
								}
							}

							break;

						case ElementColorType.SingleColor:
							mixColors = false;
							cbi = new ColorBreakdownItem();
							cbi.Color = singleColor;
							newBreakdownItems.Add(cbi);
							break;
					}

					breakdown.MixColors = mixColors;
					breakdown.BreakdownItems = newBreakdownItems;

					colorFiltersConfigured++;

				}
				else {
					colorFiltersSkipped++;
				}
			}

			MessageBox.Show("Color Properties:  " + colorPropertiesAdded + " added, " +
							colorPropertiesConfigured + " configured, " + colorPropertiesSkipped + " skipped. " +
			                "Color Filters:  " + colorFiltersAdded + " added, " + colorFiltersConfigured + " configured, " +
			                colorFiltersSkipped + " skipped.");

			return true;
		}

		private IEnumerable<IDataFlowComponentReference> _FindLeafOutputsOrBreakdownFilters(IDataFlowComponent component)
		{
			if (component == null) {
				yield break;
			}

			if (component is ColorBreakdownModule) {
				yield return new DataFlowComponentReference(component, -1);
					// this is a bit iffy -- -1 as a component output index -- but hey.
			}

			if (component.Outputs == null || component.OutputDataType == DataFlowType.None) {
				yield break;
			}

			for (int i = 0; i < component.Outputs.Length; i++) {
				IEnumerable<IDataFlowComponent> children = VixenSystem.DataFlow.GetDestinationsOfComponentOutput(component, i);

				if (!children.Any()) {
					yield return new DataFlowComponentReference(component, i);
				}
				else {
					foreach (IDataFlowComponent child in children) {
						foreach (IDataFlowComponentReference result in _FindLeafOutputsOrBreakdownFilters(child)) {
							yield return result;
						}
					}
				}
			}
		}



		private void ColorSetupHelper_Load(object sender, EventArgs e)
		{
			PopulateColorSetsComboBox();

		}

		private void PopulateColorSetsComboBox()
		{
			comboBoxColorSet.BeginUpdate();
			comboBoxColorSet.Items.Clear();

			foreach (string colorSetName in (ApplicationServices.GetModuleStaticData(ColorDescriptor.ModuleId) as ColorStaticData).GetColorSetNames()) {
				comboBoxColorSet.Items.Add(colorSetName);
			}

			if (comboBoxColorSet.SelectedIndex < 0) {
				comboBoxColorSet.SelectedIndex = 0;
			}

			comboBoxColorSet.EndUpdate();
		}

		private void AnyRadioButtonCheckedChanged(object sender, EventArgs e)
		{
			colorPanelSingleColor.Enabled = radioButtonOptionSingle.Checked;
			comboBoxColorSet.Enabled = radioButtonOptionMultiple.Checked;
			buttonColorSetsSetup.Enabled = radioButtonOptionMultiple.Checked;

			buttonOk.Enabled = radioButtonOptionSingle.Checked || radioButtonOptionMultiple.Checked || radioButtonOptionFullColor.Checked;
		}

		private void buttonColorSetsSetup_Click(object sender, EventArgs e)
		{
			using (ColorSetsSetupForm cssf = new ColorSetsSetupForm(ApplicationServices.GetModuleStaticData(ColorDescriptor.ModuleId) as ColorStaticData)) {
				cssf.ShowDialog();
				PopulateColorSetsComboBox();
			}
		}


	}
}
