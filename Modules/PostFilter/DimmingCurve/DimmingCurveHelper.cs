﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Controls;
using Common.Controls.Theme;
using Common.Resources.Properties;
using Vixen.Data.Flow;
using Vixen.Module.OutputFilter;
using Vixen.Rule;
using Vixen.Services;
using Vixen.Sys;
using VixenModules.App.Curves;

namespace VixenModules.OutputFilter.DimmingCurve
{
	public partial class DimmingCurveHelper : Form, IElementSetupHelper
	{
		private static NLog.Logger Logging = NLog.LogManager.GetCurrentClassLogger();

		public DimmingCurveHelper()
		{
			InitializeComponent();
			ForeColor = ThemeColorTable.ForeColor;
			BackColor = ThemeColorTable.BackgroundColor;
			ThemeUpdateControls.UpdateControls(this);
			_curve = new Curve();
		}

		private void DimmingCurveHelper_Load(object sender, EventArgs e)
		{
			buttonOk.Enabled = false;
		}


		public string HelperName
		{
			get { return "Dimming Curve"; }
		}

		public bool Perform(IEnumerable<ElementNode> selectedNodes)
		{
			DialogResult dr = ShowDialog();
			if (dr != DialogResult.OK)
				return false;

			ExistingBehaviour existingBehaviour = ExistingBehaviour.DoNothing;

			if (radioButtonExistingDoNothing.Checked)
				existingBehaviour = ExistingBehaviour.DoNothing;
			else if (radioButtonExistingUpdate.Checked)
				existingBehaviour = ExistingBehaviour.UpdateExisting;
			else if (radioButtonExistingAddNew.Checked)
				existingBehaviour = ExistingBehaviour.AddNew;
			else
				Logging.Warn("no radio button selected");


			IEnumerable<ElementNode> leafElements = selectedNodes.SelectMany(x => x.GetLeafEnumerator()).Distinct();
			int modulesCreated = 0;
			int modulesConfigured = 0;
			int modulesSkipped = 0;

			foreach (ElementNode leafNode in leafElements) {

				// get the leaf 'things' to deal with -- ie. either existing dimming curves on a filter branch, or data component outputs
				// (if we're adding new ones, ignore any existing dimming curves: always go to the outputs and we'll add new ones)
				IDataFlowComponent elementComponent = VixenSystem.DataFlow.GetComponent(leafNode.Element.Id);
				IEnumerable<IDataFlowComponentReference> references = _FindLeafOutputsOrDimmingCurveFilters(elementComponent, existingBehaviour == ExistingBehaviour.AddNew);

				foreach (IDataFlowComponentReference reference in references) {
					int outputIndex = reference.OutputIndex;

					if (reference.Component is DimmingCurveModule) {
						switch (existingBehaviour) {
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

					// assuming we're making a new one and going from there
					DimmingCurveModule dimmingCurve = ApplicationServices.Get<IOutputFilterModuleInstance>(DimmingCurveDescriptor.ModuleId) as DimmingCurveModule;
					VixenSystem.DataFlow.SetComponentSource(dimmingCurve, reference.Component, outputIndex);
					VixenSystem.Filters.AddFilter(dimmingCurve);

					dimmingCurve.DimmingCurve = _curve;

					modulesCreated++;
					modulesConfigured++;
				}
			}
			//messageBox Arguments are (Text, Title, No Button Visible, Cancel Button Visible)
			var messageBox = new MessageBoxForm(modulesCreated + " Dimming Curves created, " + modulesConfigured + " configured, and " + modulesSkipped + " skipped.", "", false, false);
			messageBox.ShowDialog();

			return true;
		}



		private IEnumerable<IDataFlowComponentReference> _FindLeafOutputsOrDimmingCurveFilters(IDataFlowComponent component, bool skipDimmingCurves)
		{
			if (component == null) {
				yield break;
			}

			if (component is DimmingCurveModule && !skipDimmingCurves) {
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
				} else {
					foreach (IDataFlowComponent child in children) {
						foreach (IDataFlowComponentReference result in _FindLeafOutputsOrDimmingCurveFilters(child, skipDimmingCurves)) {
							yield return result;
						}
					}
				}
			}
		}




		private Curve _curve;
		private void buttonSetupCurve_Click(object sender, EventArgs e)
		{
			using (CurveEditor editor = new CurveEditor(_curve)) {
				if (editor.ShowDialog() == DialogResult.OK) {
					_curve = editor.Curve;
					buttonOk.Enabled = true;
				}
			}
		}


		enum ExistingBehaviour
		{
			DoNothing,
			UpdateExisting,
			AddNew
		}

		private void buttonBackground_MouseHover(object sender, EventArgs e)
		{
			var btn = (Button)sender;
			btn.BackgroundImage = Resources.ButtonBackgroundImageHover;
		}

		private void buttonBackground_MouseLeave(object sender, EventArgs e)
		{
			var btn = (Button)sender;
			btn.BackgroundImage = Resources.ButtonBackgroundImage;

		}
	}
}
