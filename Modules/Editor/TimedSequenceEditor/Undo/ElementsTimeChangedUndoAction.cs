using System;
using System.Collections.Generic;
using Common.Controls;
using Common.Controls.Timeline;

namespace VixenModules.Editor.TimedSequenceEditor.Undo
{
	public class ElementsTimeChangedUndoAction : UndoAction
	{
		private Dictionary<Element, ElementTimeInfo> m_changedElements;
		private ElementMoveType m_moveType;
		private TimedSequenceEditorForm m_form;

		public ElementsTimeChangedUndoAction(TimedSequenceEditorForm form, Dictionary<Element, ElementTimeInfo> changedElements, ElementMoveType moveType)
			: base()
		{
			m_changedElements = changedElements;
			m_moveType = moveType;
			m_form = form;
		}


		public override void Undo()
		{
			m_form.SwapPlaces(m_changedElements);

			base.Undo();
		}

		public override void Redo()
		{
			m_form.SwapPlaces(m_changedElements);

			base.Redo();
		}

		public override string Description
		{
			get
			{
				string s = (m_changedElements.Count == 1 ? string.Empty : "s");
				switch (m_moveType) {
					case ElementMoveType.Move:
						return string.Format("Moved {0} effect{1}", m_changedElements.Count, s);
					case ElementMoveType.Resize:
						return string.Format("Resize {0} effect{1}", m_changedElements.Count, s);
					case ElementMoveType.AlignStart:
						return string.Format("Start Align {0} effect{1}", m_changedElements.Count, s);
					case ElementMoveType.AlignEnd:
						return string.Format("End Align {0} effect{1}", m_changedElements.Count, s);
					case ElementMoveType.AlignBoth:
						return string.Format("Align {0} effect{1}", m_changedElements.Count, s);
					case ElementMoveType.AlignDurations:
						return string.Format("Duration Align {0} effect{1}", m_changedElements.Count, s);
					case ElementMoveType.AlignStartToEnd:
						return string.Format("Start to End Align {0} effect{1}", m_changedElements.Count, s);
					case ElementMoveType.AlignEndToStart:
						return string.Format("End to Start Align {0} effect{1}", m_changedElements.Count, s);
					case ElementMoveType.AlignCenters:
						return string.Format("Center Align {0} effect{1}", m_changedElements.Count, s);
					case ElementMoveType.Distribute:
						return string.Format("Distributed {0} effect{1}", m_changedElements.Count, s);
					default:
						throw new Exception("Unknown ElementMoveType!");
				}
			}
		}
	}
}