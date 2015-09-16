﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using Vixen.Execution.Context;
using Vixen.Sys;
using VixenModules.Editor.EffectEditor;
using VixenModules.Editor.TimedSequenceEditor.Undo;
using WeifenLuo.WinFormsUI.Docking;
using Application = System.Windows.Application;
using Element = Common.Controls.Timeline.Element;
using PropertyValueChangedEventArgs = VixenModules.Editor.EffectEditor.PropertyValueChangedEventArgs;
using Timer = System.Timers.Timer;

namespace VixenModules.Editor.TimedSequenceEditor
{
	public partial class FormEffectEditor : DockContent
	{
		private readonly List<Element> _elements = new List<Element>();
		private readonly TimedSequenceEditorForm _sequenceEditorForm;
		private LiveContext _previewContext;
		private readonly Timer _previewLoopTimer = new Timer();
		private readonly EffectPropertyEditorGrid _effectPropertyEditorGridEffectEffectPropertiesEditor;
		private bool _previewState;
		private ElementHost host;

		public FormEffectEditor(TimedSequenceEditorForm sequenceEditorForm)
		{
			
			if (Application.Current == null)
			{
				// create the Application object
				new Application();
			}
			ResourceDictionary dict = new ResourceDictionary
			{
				Source = new Uri("/EffectEditor;component/Themes/Theme.xaml", UriKind.Relative)
			};


			Application.Current.Resources.MergedDictionaries.Add(dict);
			InitializeComponent();
			
			_sequenceEditorForm = sequenceEditorForm;
			host = new ElementHost { Dock = DockStyle.Fill };

			_effectPropertyEditorGridEffectEffectPropertiesEditor = new EffectPropertyEditorGrid
			{
				ShowReadOnlyProperties = true,
				PropertyFilterVisibility = Visibility.Hidden
			};

			_effectPropertyEditorGridEffectEffectPropertiesEditor.KeyDown += Editor_OnKeyDown;

			host.Child = _effectPropertyEditorGridEffectEffectPropertiesEditor;

			Controls.Add(host);

			sequenceEditorForm.TimelineControl.SelectionChanged += timelineControl_SelectionChanged;
			_effectPropertyEditorGridEffectEffectPropertiesEditor.PropertyValueChanged += EffectPropertyEditorValueChanged;
			_effectPropertyEditorGridEffectEffectPropertiesEditor.PreviewChanged += EditorPreviewStateChanged;
			_previewLoopTimer.Elapsed += PreviewLoopTimerOnElapsed;
		}

		internal IEnumerable<Element> Elements
		{
			get { return _elements; }
			set
			{
				_elements.Clear();
				_elements.AddRange(value);
				SetPreviewState();
				
				_effectPropertyEditorGridEffectEffectPropertiesEditor.SelectedObjects = _elements.Select(x => x.EffectNode.Effect).ToArray();
			} 
		}

		#region Events
		
		private void timelineControl_SelectionChanged(object sender, EventArgs e)
		{
			Elements = _sequenceEditorForm.TimelineControl.SelectedElements;
		}

		private void EditorPreviewStateChanged(object sender, PreviewStateEventArgs e)
		{
			_previewState = e.State;
			TogglePreviewState();
		}


		private void EffectPropertyEditorValueChanged(object sender, PropertyValueChangedEventArgs e)
		{
			Dictionary<Element, Tuple<Object, PropertyDescriptor>> elementValues = new Dictionary<Element, Tuple<object, PropertyDescriptor>>();

			int i = 0;
			foreach (var element in _elements)
			{
				element.UpdateNotifyContentChanged();
				elementValues.Add(element, new Tuple<object, PropertyDescriptor>(e.OldValue[i], e.Property.UnderLyingPropertyDescriptor(i)));
				i++;
			}

			var undo = new EffectsPropertyModifiedUndoAction(elementValues);
			_sequenceEditorForm.AddEffectsModifiedToUndo(undo);
		}

		
		private void Editor_OnKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
		{
			if (e.Key == Key.Space)
			{
				_sequenceEditorForm.HandleSpacebarAction();
			}
		}

		#endregion

		#region Preview

		private void SetPreviewState()
		{
			PreviewStop();
			if (_elements.Any() && _previewState)
			{
				PreviewPlay();
			}
		}

		
		private void PreviewLoopTimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
		{
			if (InvokeRequired)
			{
				if (_previewState && _elements.Any())
				{
					BeginInvoke(new Action(PreviewPlay));	
				}
				
			}

		}

		private void PreviewPlay()
		{
			if (_previewContext == null)
			{
				_previewContext = VixenSystem.Contexts.CreateLiveContext("Effect Preview");
				_previewContext.Start();

			}
			IEnumerable<EffectNode> orderedNodes = Elements.Select(x => x.EffectNode).OrderBy(x => x.StartTime).ToList();
			if (orderedNodes.Any())
			{
				TimeSpan startOffset = orderedNodes.First().StartTime;
				TimeSpan duration = orderedNodes.Last().EndTime - startOffset;
				List<EffectNode> nodesToPlay =
					orderedNodes.Select(effectNode => new EffectNode(effectNode.Effect, effectNode.StartTime - startOffset)).ToList();
				_previewContext.Execute(nodesToPlay);
				_previewLoopTimer.Interval = duration.TotalMilliseconds;
				_previewLoopTimer.Start();
			}
		}

		public void PreviewStop()
		{
			_previewLoopTimer.Stop();
			if (_previewContext != null)
			{
				_previewContext.Clear();
			}
		}

		public void ResumePreview()
		{
			TogglePreviewState();
		}

		private void TogglePreviewState()
		{
			if (_previewState)
			{
				if (_elements.Any())
				{
					PreviewPlay();
				}
			}
			else
			{
				PreviewStop();
			}
		}

		#endregion
	}

}
