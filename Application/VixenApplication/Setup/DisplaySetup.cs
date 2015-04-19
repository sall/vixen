﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Vixen.Rule;
using Vixen.Sys;
using VixenApplication.Setup;
using VixenApplication.Setup.ElementTemplates;

namespace VixenApplication
{
	public partial class DisplaySetup : Form
	{
		private SetupElementsTree _setupElementsTree;

		private SetupPatchingSimple _setupPatchingSimple;
		private SetupPatchingGraphical _setupPatchingGraphical;

		private SetupControllersSimple _setupControllersSimple;

		private ISetupElementsControl _currentElementControl;
		private ISetupPatchingControl _currentPatchingControl;
		private ISetupControllersControl _currentControllersControl;

		private IElementTemplate[] _elementTemplates;
		private IElementSetupHelper[] _elementSetupHelpers;

		public DisplaySetup()
		{
			InitializeComponent();

			Icon = Common.Resources.Properties.Resources.Icon_Vixen3;
			buttonHelp.Image = Common.Resources.Tools.GetIcon(Common.Resources.Properties.Resources.help, 16);

			_elementTemplates = Vixen.Services.ApplicationServices.GetAllElementTemplates();
			_elementSetupHelpers = Vixen.Services.ApplicationServices.GetAllElementSetupHelpers();
		}

		private void DisplaySetup_Load(object sender, EventArgs e)
		{
			_setupElementsTree = new SetupElementsTree(_elementTemplates, _elementSetupHelpers);
			_setupElementsTree.Dock = DockStyle.Fill;
			_setupElementsTree.MasterForm = this;

			_setupPatchingSimple = new SetupPatchingSimple();
			_setupPatchingSimple.Dock = DockStyle.Fill;
			_setupPatchingSimple.MasterForm = this;
			_setupPatchingGraphical = new SetupPatchingGraphical();
			_setupPatchingGraphical.Dock = DockStyle.Fill;
			_setupPatchingGraphical.MasterForm = this;

			_setupControllersSimple = new SetupControllersSimple();
			_setupControllersSimple.Dock = DockStyle.Fill;
			_setupControllersSimple.MasterForm = this;

			activateControllersControl(_setupControllersSimple);
			activateElementControl(_setupElementsTree);

			radioButtonPatchingSimple.Checked = true;
		}


		private void activateElementControl(ISetupElementsControl control)
		{
			if (_currentElementControl != null) {
				_currentElementControl.ElementSelectionChanged -=  control_ElementSelectionChanged;
				_currentElementControl.ElementsChanged -= control_ElementsChanged;
			}

			_currentElementControl = control;

			control.ElementSelectionChanged +=  control_ElementSelectionChanged;
			control.ElementsChanged += control_ElementsChanged;

			tableLayoutPanelElementSetup.Controls.Clear();
			tableLayoutPanelElementSetup.Controls.Add(control.SetupElementsControl);

			control.UpdatePatching();
		}

		void control_ElementsChanged(object sender, EventArgs e)
		{
			if (_currentPatchingControl != null) {
				_currentPatchingControl.UpdateElementDetails(_currentElementControl.SelectedElements);
			}

			// TODO: this is iffy, should really redo the events for this system
			if (_currentControllersControl != null) {
				_currentControllersControl.UpdatePatching();
			}
		}

		void control_ElementSelectionChanged(object sender, ElementNodesEventArgs e)
		{
			if (_currentPatchingControl != null) {
				_currentPatchingControl.UpdateElementSelection(e.ElementNodes);
			}
		}



		private void activatePatchingControl(ISetupPatchingControl control)
		{
			if (_currentPatchingControl != null) {
				_currentPatchingControl.FiltersAdded-=  control_FiltersAdded;
				_currentPatchingControl.PatchingUpdated -= control_PatchingUpdated;
			}

			_currentPatchingControl = control;

			control.FiltersAdded += control_FiltersAdded;
			control.PatchingUpdated += control_PatchingUpdated;

			tableLayoutPanelPatchingSetup.Controls.Clear();
			tableLayoutPanelPatchingSetup.Controls.Add(control.SetupPatchingControl);


			if (_currentControllersControl == null) {
				control.UpdateControllerSelection(new ControllersAndOutputsSet());
			} else {
				control.UpdateControllerSelection(_currentControllersControl.SelectedControllersAndOutputs);				
			}
			if (_currentElementControl == null) {
				control.UpdateElementSelection(Enumerable.Empty<Vixen.Sys.ElementNode>());
			} else {
				control.UpdateElementSelection(_currentElementControl.SelectedElements);				
			}
		}

		void control_PatchingUpdated(object sender, EventArgs e)
		{
			if (_currentElementControl != null) {
				_currentElementControl.UpdatePatching();
			}

			if (_currentControllersControl != null) {
				_currentControllersControl.UpdatePatching();
			}
		}

		void control_FiltersAdded(object sender, FiltersEventArgs e)
		{
		}



		private void activateControllersControl(ISetupControllersControl control)
		{
			if (_currentControllersControl != null) {
				_currentControllersControl.ControllerSelectionChanged -=  control_ControllerSelectionChanged;
				_currentControllersControl.ControllersChanged -= control_ControllersChanged;
			}

			_currentControllersControl = control;

			control.ControllerSelectionChanged += control_ControllerSelectionChanged;
			control.ControllersChanged += control_ControllersChanged;

			tableLayoutPanelControllerSetup.Controls.Clear();
			tableLayoutPanelControllerSetup.Controls.Add(control.SetupControllersControl);

			control.UpdatePatching();
		}

		void control_ControllersChanged(object sender, EventArgs e)
		{
			if (_currentPatchingControl != null) {
				_currentPatchingControl.UpdateControllerDetails(_currentControllersControl.SelectedControllersAndOutputs);
			}

			// TODO: this is iffy, should really redo the events for this system
			if (_currentElementControl != null) {
				_currentElementControl.UpdatePatching();
			}
		}

		void control_ControllerSelectionChanged(object sender, ControllerSelectionEventArgs e)
		{
			if (_currentPatchingControl != null) {
				_currentPatchingControl.UpdateControllerSelection(e.ControllersAndOutputs);
			}
		}

		private void radioButtonPatchingSimple_CheckedChanged(object sender, EventArgs e)
		{
			if ((sender as RadioButton).Checked)
				activatePatchingControl(_setupPatchingSimple);
		}

		private void radioButtonPatchingGraphical_CheckedChanged(object sender, EventArgs e)
		{
			if ((sender as RadioButton).Checked)
				activatePatchingControl(_setupPatchingGraphical);
		}

		private void buttonHelp_Click(object sender, EventArgs e)
		{
			Common.VixenHelp.VixenHelp.ShowHelp(Common.VixenHelp.VixenHelp.HelpStrings.Setup_Main);
		}

		public void SelectElements(IEnumerable<ElementNode> elements)
		{
			_currentElementControl.SelectedElements = elements;
		}

		public void SelectControllersAndOutputs(ControllersAndOutputsSet controllersAndOutputs)
		{
			_currentControllersControl.SelectedControllersAndOutputs = controllersAndOutputs;
		}

		private void buttonOk_Click(object sender, EventArgs e)
		{
			//This is an attempt to band aid up a issue that the graphical designer has with making filters and then abandoning them
			//Until we can fix up a better way to visualize unconnected filters, we will just clean them up from here.
			//Just doing it in Ok as if we cancel it reloads the system anyway.
			VixenSystem.Filters.RemoveOrphanedFilters();
		}
	}
}
