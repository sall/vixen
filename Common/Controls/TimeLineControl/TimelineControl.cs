﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using VixenModules.Media.Audio;
using System.Threading.Tasks;

namespace Common.Controls.Timeline
{
	[System.ComponentModel.DesignerCategory("")] // Prevent this from showing up in designer.
	public class TimelineControl : TimelineControlBase, IEnumerable<Row>
	{
		private const int InitialSplitterDistance = 200;

		#region Member Controls

		private SplitContainer splitContainer;

		// Left side (Panel 1)
		private RowList timelineRowList;

		// Right side (Panel 2)
		public Ruler ruler;
		public Grid grid;
		private Waveform waveform;

		#endregion

		private bool _sequenceLoading = false;

		public bool SequenceLoading
		{
			get { return _sequenceLoading; }
			set
			{
				_sequenceLoading = value;
				if (grid != null)
					grid.SequenceLoading = value;
			}
		}

		public TimelineControl()
			: base(new TimeInfo()) // This is THE TimeInfo object for the whole control (and all sub-controls).
		{
			TimeInfo.TimePerPixel = TimeSpan.FromTicks(100000);
			TimeInfo.VisibleTimeStart = TimeSpan.Zero;

			InitializeControls();

			// Reasonable defaults
			TotalTime = TimeSpan.FromMinutes(2);

			// Event handlers for Row class static events
			Row.RowToggled += RowToggledHandler;
			Row.RowHeightChanged += RowHeightChangedHandler;
			Row.RowHeightResized += RowHeightResizedHandler;
		}
	 
		public void EnableDisableHandlers(bool enabled = true)
		{
			if (enabled) {
				Row.RowToggled -= RowToggledHandler;
				Row.RowHeightChanged -= RowHeightChangedHandler;
				Row.RowHeightResized -= RowHeightResizedHandler;
				Row.RowToggled += RowToggledHandler;
				Row.RowHeightChanged += RowHeightChangedHandler;
				Row.RowHeightResized += RowHeightResizedHandler;
			} else {
				Row.RowToggled -= RowToggledHandler;
				Row.RowHeightChanged -= RowHeightChangedHandler;
				Row.RowHeightResized -= RowHeightResizedHandler;
			}
			this.timelineRowList.EnableDisableHandlers(enabled);
		}
		#region Initialization
		protected override void Dispose(bool disposing)
		{
			
			Row.RowToggled -= RowToggledHandler;
			Row.RowHeightChanged -= RowHeightChangedHandler;
			Row.RowHeightResized -= RowHeightResizedHandler;
			Vixen.Utility.cEventHelper.RemoveAllEventHandlers(this);
			Vixen.Utility.cEventHelper.RemoveAllEventHandlers(TimeInfo);
			TimeInfo = null;

			if (grid != null) {
				grid.Scroll -= GridScrolledHandler;
				grid.VerticalOffsetChanged -= GridScrollVerticalHandler;
				grid.Dispose();
				Vixen.Utility.cEventHelper.RemoveAllEventHandlers(grid);
				grid = null;
			}
			
			if (timelineRowList != null) {
				timelineRowList.Dispose();
				timelineRowList= null;
			}
			if (waveform != null)
				waveform.Dispose();
			waveform= null;
			base.Dispose(disposing);
		}
		private void InitializeControls()
		{
			this.SuspendLayout();

			// (this) Timeline Control
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.Name = "TimelineControl";


			// Split Container
			splitContainer = new SplitContainer()
			                 	{
			                 		Dock = DockStyle.Fill,
			                 		Orientation = Orientation.Vertical,
			                 		Name = "splitContainer",
			                 		FixedPanel = FixedPanel.Panel1,
			                 		Panel1MinSize = 100,
			                 	};
			this.Controls.Add(this.splitContainer);

			// Split container panels
			splitContainer.BeginInit();
			splitContainer.SuspendLayout();

			InitializePanel1();
			InitializePanel2();

			splitContainer.ResumeLayout(false);
			splitContainer.EndInit();

			this.ResumeLayout(false);
		}

		// Panel 1 - the left side of the splitContainer
		private void InitializePanel1()
		{
			splitContainer.Panel1.SuspendLayout();
			splitContainer.Panel1.BackColor = Color.FromArgb(200, 200, 200);

			// Row List
			timelineRowList = new RowList()
			                  	{
			                  		Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
			                  		DottedLineColor = Color.Black,
			                  		Name = "timelineRowList",
			                  	};
			splitContainer.Panel1.Controls.Add(timelineRowList);

			splitContainer.Panel1.ResumeLayout(false);
			splitContainer.Panel1.PerformLayout();
		}


		// Panel 2 - the right side of the splitContainer
		private void InitializePanel2()
		{
			// Add all timeline-like controls to panel2
			splitContainer.Panel2.SuspendLayout();

			// Grid
			grid = new Grid(TimeInfo)
			       	{
			       		Dock = DockStyle.Fill,
			       	};
			splitContainer.Panel2.Controls.Add(grid); // gets added first - to fill the remains
			grid.Scroll += GridScrolledHandler;
			grid.VerticalOffsetChanged += GridScrollVerticalHandler;

			// Ruler
			ruler = new Ruler(TimeInfo)
			        	{
			        		Dock = DockStyle.Top,
			        		Height = 40,
			        	};
			splitContainer.Panel2.Controls.Add(ruler);

			//WaveForm
			//TODO deal with positioning, can we dock two controls to the top
			//Looks like the last one wins.
			waveform = new Waveform(TimeInfo)
			           	{
			           		Dock = DockStyle.Top,
			           		Height = 50
			           	};

			splitContainer.Panel2.Controls.Add(waveform);

			splitContainer.Panel2.ResumeLayout(false);
			splitContainer.Panel2.PerformLayout();
		}

		#endregion

		#region Properties

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int VerticalOffset
		{
			get
			{
				if (grid != null)
					return grid.VerticalOffset;
				else
					return 0;
			}
			set
			{
				if (grid != null)
					grid.VerticalOffset = value;
			}
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int VisibleHeight
		{
			get { return grid.ClientSize.Height; }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TimeSpan CursorPosition
		{
			get { return grid.CursorPosition; }
			set { grid.CursorPosition = value; }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override TimeSpan VisibleTimeSpan
		{
			get { return grid.VisibleTimeSpan; }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IEnumerable<Element> SelectedElements
		{
			get { return grid.SelectedElements; }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IEnumerable<string> SelectedElementTypes
		{
			get
			{
				List<string> elementTypesList = new List<string>();
				foreach (Element element in grid.SelectedElements.Where(element => !elementTypesList.Contains(element.EffectNode.Effect.EffectName)))
				{
					elementTypesList.Add(element.EffectNode.Effect.EffectName);
				}

				return elementTypesList;
			}
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IEnumerable<Row> Rows
		{
			get { return grid.Rows; }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IEnumerable<Row> SelectedRows
		{
			get { return grid.SelectedRows; }
			set { grid.SelectedRows = value; }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Row SelectedRow
		{
			get { return SelectedRows.FirstOrDefault(); }
			set { SelectedRows = new Row[] {value}; }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Row ActiveRow
		{
			get { return grid.ActiveRow; }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IEnumerable<Row> VisibleRows
		{
			get { return grid.VisibleRows; }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Row TopVisibleRow
		{
			get { return grid.TopVisibleRow; }
		}

		#endregion

		#region Methods

		public void LayoutRows()
		{
			timelineRowList.DoLayout();
		}

		// Zoom in or out (ie. change the visible time span): give a scale < 1.0
		// and it zooms in, > 1.0 and it zooms out.
		public void Zoom(double scale)
		{
			if (scale <= 0.0)
				return;
			grid.BeginDraw();
			if (VisibleTimeSpan.Scale(scale) > TotalTime) {
				TimePerPixel = TimeSpan.FromTicks(TotalTime.Ticks/grid.Width);
				VisibleTimeStart = TimeSpan.Zero;
			}
			else {
				TimePerPixel = TimePerPixel.Scale(scale);
				if (VisibleTimeEnd >= TotalTime)
					VisibleTimeStart = TotalTime - VisibleTimeSpan;
			}
			grid.EndDraw();
			}

		public void ZoomRows(double scale)
		{
			if (scale <= 0.0)
				return;
			grid.BeginDraw();

			foreach (Row r in Rows)
			{
				if (r.Height * scale > grid.Height) continue; //Don't scale a row beyond the grid height. How big do you need it?
				r.Height = (int)(r.Height * scale);
			}

			grid.EndDraw();
		}

		public void ResizeGrid()
		{
			grid.AllowGridResize = true;
			grid.ResizeGridHeight();
		}

		public bool AllowGridResize
		{
			get { return grid.AllowGridResize; }
			set { grid.AllowGridResize = value; }
		}

		private void AddRowToControls(Row row, RowLabel label)
		{
			grid.AddRow(row);
			timelineRowList.AddRowLabel(label);
		}

		private void RemoveRowFromControls(Row row)
		{
			grid.RemoveRow(row);
			timelineRowList.RemoveRowLabel(row.RowLabel);
		}

		// adds a given row to the control, optionally as a child of the given parent
		public void AddRow(Row row, Row parent = null)
		{
			if (parent != null)
				parent.AddChildRow(row);

			AddRowToControls(row, row.RowLabel);
		}

		// adds a new, empty row with a default label with the given name, as a child of the (optional) given parent
		public Row AddRow(string name, Row parent = null, int height = 50)
		{
			Row row = new Row();

			row.Name = name;
			row.Height = height;

			if (parent != null)
				parent.AddChildRow(row);

			AddRowToControls(row, row.RowLabel);

			return row;
		}

		// adds a new, empty row with the given label, as a child of the (optional) given parent
		public Row AddRow(RowLabel label, Row parent = null, int height = 50)
		{
			Row row = new Row(label);

			row.Height = height;

			if (parent != null)
				parent.AddChildRow(row);

			AddRowToControls(row, row.RowLabel);

			return row;
		}

		public Audio Audio
		{
			get { return waveform.Audio; }
			set { waveform.Audio = value; }
		}

		public void AddSnapTime(TimeSpan time, int level, Color color)
		{
			grid.AddSnapPoint(time, level, color);
			ruler.AddSnapPoint(time, level, color);
		}

		public bool RemoveSnapTime(TimeSpan time)
		{
			ruler.RemoveSnapPoint(time);
			return grid.RemoveSnapPoint(time);
		}

		public void ClearAllSnapTimes()
		{
			grid.ClearSnapPoints();
			ruler.ClearSnapPoints();
		}


		public void AlignSelectedElementsLeft()
		{
			grid.AlignSelectedElementsLeft();
		}

		/// <summary>
		/// Clears all elements from the grid, leaving the rows intact.
		/// </summary>
		public void ClearAllElements()
		{
			foreach (Row row in grid) {
				row.ClearRowElements();
			}
		}

		/// <summary>
		/// Clears all rows from the grid; effectively emptying the grid. Will also
		/// clear all elements in the grid as well.
		/// </summary>
		public void ClearAllRows()
		{
			ClearAllElements();
			foreach (Row row in grid.ToArray()) {
				RemoveRowFromControls(row);
			}
		}

		/// <summary>
		/// Selects all elements in the grid.
		/// </summary>
		public void SelectAllElements()
		{
			foreach (Row r in grid) {
				r.SelectAllElements();
			}
		}

		public void SelectElement(Element element)
		{
			grid.SelectElement(element);
		}

		public void DeselectElement(Element element)
		{
			grid.DeselectElement(element);
		}

		public void ToggleElementSelection(Element element)
		{
			grid.ToggleElementSelection(element);
		}

		/// <summary>
		/// Moves all selected elements by the given amount of time.
		/// </summary>
		/// <param name="offset"></param>
		public void MoveSelectedElementsByTime(TimeSpan offset)
		{
			grid.MoveElementsByTime(SelectedElements, offset);
		}


		public IEnumerator<Row> GetEnumerator()
		{
			return grid.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return grid.GetEnumerator();
		}

		#endregion

		#region Events exposed from sub-controls (Grid, Ruler, etc)

		public event EventHandler SelectionChanged
		{
			add { grid.SelectionChanged += value; }
			remove { if (grid != null) grid.SelectionChanged -= value; }
		}

		public event EventHandler<ElementEventArgs> ElementDoubleClicked
		{
			add { grid.ElementDoubleClicked += value; }
			remove { if (grid != null) grid.ElementDoubleClicked -= value; }
		}

		public event EventHandler<MultiElementEventArgs> ElementsFinishedMoving
		{
			add { grid.ElementsFinishedMoving += value; }
			remove { if (grid != null) grid.ElementsFinishedMoving -= value; }
		}

		public event EventHandler<TimeSpanEventArgs> CursorMoved
		{
			add { grid.CursorMoved += value; }
			remove { if (grid != null) grid.CursorMoved -= value; }
		}

		public event EventHandler VerticalOffsetChanged
		{
			add { grid.VerticalOffsetChanged += value; }
			remove { if (grid != null) grid.VerticalOffsetChanged -= value; }
		}

		public event EventHandler<ElementRowChangeEventArgs> ElementChangedRows
		{
			add { grid.ElementChangedRows += value; }
			remove { if (grid != null) grid.ElementChangedRows -= value; }
		}

		public event EventHandler<TimelineDropEventArgs> DataDropped
		{
			add { grid.DataDropped += value; }
			remove { if (grid != null) grid.DataDropped -= value; }
		}

		public event EventHandler<ToolDropEventArgs> ColorDropped
		{
			add { grid.ColorDropped += value; }
			remove { if (grid != null) grid.ColorDropped -= value; }
		}

		public event EventHandler<ToolDropEventArgs> CurveDropped
		{
			add { grid.CurveDropped += value; }
			remove { if (grid != null) grid.CurveDropped -= value; }
		}

		public event EventHandler<ToolDropEventArgs> GradientDropped
		{
			add { grid.GradientDropped += value; }
			remove { if (grid != null) grid.GradientDropped -= value; }
		}

		public event EventHandler<ElementsChangedTimesEventArgs> ElementsMovedNew
		{
			add { grid.ElementsMovedNew += value; }
			remove { if (grid != null) grid.ElementsMovedNew -= value; }
		}

		public event EventHandler<ElementsSelectedEventArgs> ElementsSelected
		{
			add { grid.ElementsSelected += value; }
			remove { if (grid != null) grid.ElementsSelected -= value; }
		}

		public event EventHandler<ContextSelectedEventArgs> ContextSelected
		{
			add { grid.ContextSelected += value; }
			remove { if (grid != null) grid.ContextSelected -= value; }
		}

		public event EventHandler<RulerClickedEventArgs> RulerClicked
		{
			add { ruler.ClickedAtTime += value; }
			remove { ruler.ClickedAtTime -= value; }
		}

		public event EventHandler<MarkMovedEventArgs> MarkMoved
		{
			add { ruler.MarkMoved += value; }
			remove { ruler.MarkMoved -= value; }
		}

		public event EventHandler<DeleteMarkEventArgs> DeleteMark
		{
			add { ruler.DeleteMark += value; }
			remove { ruler.DeleteMark -= value; }
		}

		public event EventHandler RulerBeginDragTimeRange
		{
			add { ruler.BeginDragTimeRange += value; }
			remove { ruler.BeginDragTimeRange -= value; }
		}

		public event EventHandler<ModifierKeysEventArgs> RulerTimeRangeDragged
		{
			add { ruler.TimeRangeDragged += value; }
			remove { ruler.TimeRangeDragged -= value; }
		}

		#endregion
		
		#region Event Handlers

		private void GridScrollVerticalHandler(object sender, EventArgs e)
		{
			if (timelineRowList != null)
				timelineRowList.Top = grid.Top;
			timelineRowList.VerticalOffset = grid.VerticalOffset;

			// I know it's bad to do this, but when we scroll we can get very nasty artifacts
			// and it looks shit in general. So, force an immediate graphical refresh
			Refresh();
		}

		private void GridScrollHorizontalHandler(object sender, EventArgs e)
		{
		}

		private void GridScrolledHandler(object sender, ScrollEventArgs e)
		{
			if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll) {
				//GridScrollHorizontalHandler(sender, e);
			}
			else {
				GridScrollVerticalHandler(sender, e);
			}
		}

		private void RowToggledHandler(object sender, EventArgs e)
		{
			if (timelineRowList != null)
				timelineRowList.VerticalOffset = grid.VerticalOffset;
			Invalidate();
		}

		private void RowHeightChangedHandler(object sender, EventArgs e)
		{
				
		}

		private void RowHeightResizedHandler(object sender, EventArgs e)
		{
			Invalidate();
		}

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			base.OnMouseWheel(e);
			if (Form.ModifierKeys.HasFlag(Keys.Control)) {
				// holding the control key zooms the horizontal axis, by 10% per mouse wheel tick
				Zoom(1.0 - ((double) e.Delta/1200.0));
			}
			else if (Form.ModifierKeys.HasFlag(Keys.Shift)) {
				// holding the skift key moves the horizontal axis, by 10% of the visible time span per mouse wheel tick
				// wheel towards user   --> negative delta --> VisibleTimeStart increases
				// wheel away from user --> positive delta --> VisibleTimeStart decreases
				VisibleTimeStart += VisibleTimeSpan.Scale(-((double) e.Delta/1200.0));
			}
			else {
				// moving the mouse wheel with no modifiers moves the display vertically, 40 pixels per mouse wheel tick
				VerticalOffset += -(e.Delta/3);
			}
		}

		#endregion

		#region Overridden methods (On__)

		protected override void OnLoad(EventArgs e)
		{
			splitContainer.SplitterDistance = InitialSplitterDistance;
			base.OnLoad(e);
		}

		protected override void OnLayout(LayoutEventArgs e)
		{
			//Console.WriteLine("Layout");
			timelineRowList.Top = grid.Top;
			base.OnLayout(e);
		}

		protected override void OnPlaybackCurrentTimeChanged(object sender, EventArgs e)
		{
			// check if the playback cursor position would be over 90% of the grid width: if so, scroll the grid so it would be at 10%
			if (PlaybackCurrentTime.HasValue) {
				if (PlaybackCurrentTime.Value > VisibleTimeStart + VisibleTimeSpan.Scale(0.9))
					VisibleTimeStart = PlaybackCurrentTime.Value - VisibleTimeSpan.Scale(0.1);

				if (PlaybackCurrentTime.Value < VisibleTimeStart)
					VisibleTimeStart = PlaybackCurrentTime.Value - VisibleTimeSpan.Scale(0.1);
			}

			base.OnPlaybackCurrentTimeChanged(sender, e);
		}

		#endregion
	}
}