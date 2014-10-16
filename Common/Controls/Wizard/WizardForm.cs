﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Common.Controls.Wizard
{
	// Wizard displaying form. Expects to be given a Wizard to work with, and just wraps it with a UI and
	// the needed navigation. Calls into the wizard to get the current stage, back/forward movement, etc.
	public partial class WizardForm : Form
	{
		private readonly Wizard _wizard;
		private WizardStage _currentStage;

		public WizardForm(Wizard wizard)
		{
			InitializeComponent();
			Icon = Resources.Properties.Resources.Icon_Vixen3;
			_wizard = wizard;
		}

		private void WizardForm_Load(object sender, EventArgs e)
		{
			_changeDisplayToCurrentStage();
			_currentStage.StageStart();
		}

		private void buttonPrevious_Click(object sender, EventArgs e)
		{
			_wizard.MovePrevious();
			_changeDisplayToCurrentStage();
			_currentStage.StageStart();
		}

		private void buttonNext_Click(object sender, EventArgs e)
		{
			_currentStage.StageEnd();

			if (_wizard.IsFinalStage) {
				DialogResult = DialogResult.OK;
				Close();
				_wizardFinished();
				return;
			}

			_wizard.MoveNext();
			_changeDisplayToCurrentStage();
			_currentStage.StageStart();
		}

		private void _changeDisplayToCurrentStage()
		{
			if (_currentStage != null)
				_currentStage.WizardStageChanged -= CurrentStage_WizardStageChanged;
			panelContent.Controls.Clear();

			_currentStage = _wizard.CurrentStage;

			panelContent.Controls.Add(_currentStage);
			_currentStage.WizardStageChanged += CurrentStage_WizardStageChanged;

			buttonNext.Text = _wizard.IsFinalStage ? "Finish" : "Next >>";

			_updateButtons();
		}

		private void CurrentStage_WizardStageChanged(object sender, EventArgs e)
		{
			_updateButtons();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
			_wizardFinished();
		}

		private void _updateButtons()
		{
			buttonNext.Enabled = _wizard.CanMoveNext;
			buttonPrevious.Enabled = _wizard.CanMovePrevious;
		}

		public event EventHandler WizardFormFinished;

		private void _wizardFinished()
		{
			if (WizardFormFinished != null)
				WizardFormFinished(this, EventArgs.Empty);
		}
	}
}