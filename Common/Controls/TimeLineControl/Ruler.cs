﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Diagnostics;

namespace Common.Controls.Timeline
{
	[System.ComponentModel.DesignerCategory("")] // Prevent this from showing up in designer.
	public class Ruler : TimelineControlBase
	{
		private const int minPxBetweenTimeLabels = 10;
		private const int maxDxForClick = 2;


		public Ruler(TimeInfo timeinfo)
			: base(timeinfo)
		{
			BackColor = Color.Gray;
			recalculate();
			StaticSnapPoints = new SortedDictionary<TimeSpan, List<SnapDetails>>();
			SnapStrength = 2;
			//SnapPriorityForElements = 5;
		}

		private Font m_font = null;
		private Brush m_textBrush = null;

		private TimeSpan m_MinorTick;
		private int m_minorTicksPerMajor;

		private TimeSpan MinorTick
		{
			get { return m_MinorTick; }
		}

		private TimeSpan MajorTick
		{
			get { return m_MinorTick.Scale(m_minorTicksPerMajor); }
		}


		protected override Size DefaultSize
		{
			get { return new Size(400, 40); }
		}

		public int StandardNudgeTime { get; set; }

		public int SuperNudgeTime { get; set; }

		public int SnapStrength { get; set; }

		#region Drawing

		protected override void OnPaint(PaintEventArgs e)
		{
			try {
				// Translate the graphics to work the same way the timeline grid does
				// (ie. Drawing coordinates take into account where we start at in time)
				e.Graphics.TranslateTransform(-timeToPixels(VisibleTimeStart), 0);

				drawTicks(e.Graphics, MajorTick, 2, 0.5);
				drawTicks(e.Graphics, MinorTick, 1, 0.25);
				drawTimes(e.Graphics);

				using (Pen p = new Pen(Color.Black, 2)) {
					e.Graphics.DrawLine(p, 0, Height - 1, timeToPixels(TotalTime), Height - 1);
				}

				drawPlaybackIndicators(e.Graphics);

				_drawMarks(e.Graphics);
			}
			catch (Exception ex) {
				MessageBox.Show("Exception in Timeline.Ruler.OnPaint():\n\n\t" + ex.Message + "\n\nBacktrace:\n\n\t" + ex.StackTrace);
			}
		}

		private void drawTicks(Graphics graphics, TimeSpan interval, int width, double height)
		{
			Single pxint = timeToPixels(interval);

			// calculate first tick - (it is the first multiple of interval greater than start)
			// believe it or not, this math is correct :-)
			Single start = timeToPixels(VisibleTimeStart) - (timeToPixels(VisibleTimeStart)%pxint) + pxint;
			Single end = timeToPixels(VisibleTimeEnd);

			Pen p = new Pen(Color.Black);
			p.Width = width;
			p.Alignment = PenAlignment.Right;

			for (Single x = start; x <= end; x += pxint) {	
				graphics.DrawLine(p, x, (Single) (Height*(1.0 - height)), x, Height);
			}
		}

		private void drawTimes(Graphics graphics)
		{
			SizeF stringSize;
			int lastPixel = 0;

			// calculate the width of a single time, and figure out how regularly we will be able
			// to display times without overlapping. Then we can make sure we only use those intervals
			// to draw strings.
			stringSize = graphics.MeasureString(labelString(VisibleTimeEnd), m_font);
			int timeDisplayInterval = (int) ((stringSize.Width + minPxBetweenTimeLabels)/timeToPixels(MajorTick)) + 1;
			TimeSpan drawnInterval = TimeSpan.FromTicks(MajorTick.Ticks*timeDisplayInterval);

			// get the time of the first tick that is: visible, on a major tick interval, and a multiple of the number of interval ticks
			TimeSpan firstMajor =
				TimeSpan.FromTicks(VisibleTimeStart.Ticks - (VisibleTimeStart.Ticks%drawnInterval.Ticks) + drawnInterval.Ticks);

			for (TimeSpan curTime = firstMajor;
			     // start at the first major tick
			     (curTime <= VisibleTimeEnd);
			     // current time is in the visible region
			     curTime += drawnInterval) // increment by the drawnInterval
			{
				string timeStr = labelString(curTime);

				stringSize = graphics.MeasureString(timeStr, m_font);
				Single posOffset = (stringSize.Width/2);
				Single curPixelCentre = timeToPixels(curTime);

				// if drawing the string wouldn't overlap the last, then draw it
				if (lastPixel + minPxBetweenTimeLabels + posOffset < curPixelCentre) {
					graphics.DrawString(timeStr, m_font, m_textBrush, curPixelCentre - posOffset, (Height/4) - (stringSize.Height/2));
					lastPixel = (int) (curPixelCentre + posOffset);
				}
			}
		}

		private const int ArrowBase = 16;
		private const int ArrowLength = 10;

		private void drawPlaybackIndicators(Graphics g)
		{
			// Playback start/end arrows
			if (PlaybackStartTime.HasValue || PlaybackEndTime.HasValue) {
				GraphicsState gstate = g.Save();
				g.TranslateTransform(0, -ArrowBase/2);

				if (PlaybackStartTime.HasValue) {
					// start arrow (faces left)  |<|
					int x = (int) timeToPixels(PlaybackStartTime.Value);
					g.FillPolygon(Brushes.DarkGray, new Point[]
					                                	{
					                                		new Point(x, Height - ArrowBase/2), // left mid point
					                                		new Point(x + ArrowLength, Height - ArrowBase), // right top point
					                                		new Point(x + ArrowLength, Height) // right bottom point
					                                	});
					g.DrawLine(Pens.DarkGray, x, Height - ArrowBase, x, Height);
				}

				if (PlaybackEndTime.HasValue) {
					// end arrow (faces right)   |>|
					int x = (int) timeToPixels(PlaybackEndTime.Value);
					g.FillPolygon(Brushes.DarkGray, new Point[]
					                                	{
					                                		new Point(x, Height - ArrowBase/2), // right mid point
					                                		new Point(x - ArrowLength, Height - ArrowBase), // left top point
					                                		new Point(x - ArrowLength, Height) // left bottom point
					                                	});
					g.DrawLine(Pens.DarkGray, x, Height - ArrowBase, x, Height);
				}

				if (PlaybackStartTime.HasValue && PlaybackEndTime.HasValue) {
					// line between the two
					using (Pen p = new Pen(Color.DarkGray)) {
						p.Width = 4;
						int x1 = (int) timeToPixels(PlaybackStartTime.Value) + ArrowLength;
						int x2 = (int) timeToPixels(PlaybackEndTime.Value) - ArrowLength;
						int y = Height - ArrowBase/2;
						g.DrawLine(p, x1, y, x2, y);
					}
				}

				g.Restore(gstate);
			}

			// Current position arrow
			if (PlaybackCurrentTime.HasValue) {
				int x = (int) timeToPixels(PlaybackCurrentTime.Value);
				g.FillPolygon(Brushes.Green, new Point[]
				                             	{
				                             		new Point(x, ArrowLength), // bottom mid point
				                             		new Point(x - ArrowBase/2, 0), // top left point
				                             		new Point(x + ArrowBase/2, 0), // top right point
				                             	});
			}
		}

		#endregion

		protected override void OnResize(EventArgs e)
		{
			recalculate();
			base.OnResize(e);
		}

		protected override void OnTimePerPixelChanged(object sender, EventArgs e)
		{
			recalculate();
			base.OnTimePerPixelChanged(sender, e);
		}

		protected override void OnVisibleTimeStartChanged(object sender, EventArgs e)
		{
			// not ideal, but looks a *shitload* better.
			Refresh();
		}


		// Adapted from from Audacity, Ruler.cpp
		private void recalculate()
		{
			// Calculate the correct font size based on height
			int desiredPixelHeight = (this.Size.Height/3);

			if (m_font != null)
				m_font.Dispose();
			m_font = new Font("Arial", desiredPixelHeight, GraphicsUnit.Pixel);

			if (m_textBrush != null)
				m_textBrush.Dispose();
			m_textBrush = new SolidBrush(Color.White);


			// As a heuristic, we want at least 10 pixels between each minor tick
			var t = pixelsToTime(10);

			if (t.TotalSeconds > 0.05) {
				if (t.TotalSeconds < 0.1) {
					m_MinorTick = TimeSpan.FromMilliseconds(100);
					m_minorTicksPerMajor = 5;
				}
				else if (t.TotalSeconds < 0.25) {
					m_MinorTick = TimeSpan.FromMilliseconds(250);
					m_minorTicksPerMajor = 4;
				}
				else if (t.TotalSeconds < 0.5) {
					m_MinorTick = TimeSpan.FromMilliseconds(500);
					m_minorTicksPerMajor = 4;
				}
				else if (t.TotalSeconds < 1) {
					m_MinorTick = TimeSpan.FromSeconds(1);
					m_minorTicksPerMajor = 5;
				}
				else if (t.TotalSeconds < 5) {
					m_MinorTick = TimeSpan.FromSeconds(5);
					m_minorTicksPerMajor = 6; //major = 30.0;
				}
				else if (t.TotalSeconds < 10) {
					m_MinorTick = TimeSpan.FromSeconds(10);
					m_minorTicksPerMajor = 6; //major = 60.0;
				}
				else if (t.TotalSeconds < 15) {
					m_MinorTick = TimeSpan.FromSeconds(15);
					m_minorTicksPerMajor = 4; //major = 60.0;
				}
				else if (t.TotalSeconds < 30) {
					m_MinorTick = TimeSpan.FromSeconds(30);
					m_minorTicksPerMajor = 4; //major = 120.0;
				}
				else if (t.TotalMinutes < 1) {
					m_MinorTick = TimeSpan.FromMinutes(1);
					m_minorTicksPerMajor = 5; //major = 300.0;
				}
				else if (t.TotalMinutes < 5) {
					m_MinorTick = TimeSpan.FromMinutes(5);
					m_minorTicksPerMajor = 3; //major = 900.0;
				}
				else if (t.TotalMinutes < 10) {
					m_MinorTick = TimeSpan.FromMinutes(10);
					m_minorTicksPerMajor = 3; //major = 1800.0;
				}
				else if (t.TotalMinutes < 15) {
					m_MinorTick = TimeSpan.FromMinutes(15);
					m_minorTicksPerMajor = 4; //major = 3600.0;
				}
				else if (t.TotalMinutes < 30) {
					m_MinorTick = TimeSpan.FromMinutes(30);
					m_minorTicksPerMajor = 2; //major = 3600.0;
				}
				else if (t.TotalHours < 1) {
					m_MinorTick = TimeSpan.FromHours(1);
					m_minorTicksPerMajor = 6; //major = 6 * 3600.0;
				}
				else if (t.TotalHours < 6) {
					m_MinorTick = TimeSpan.FromHours(6);
					m_minorTicksPerMajor = 4; //major = 24 * 3600.0;
				}
				else if (t.TotalDays < 1) {
					m_MinorTick = TimeSpan.FromDays(1);
					m_minorTicksPerMajor = 7; //major = 7 * 24 * 3600.0;
				}
				else {
					m_MinorTick = TimeSpan.FromDays(7);
					m_minorTicksPerMajor = 1; //major = 24.0 * 7.0 * 3600.0;
				}
			}
			else {
				// Fractional seconds
				double d = 0.000001;
				for (;;) {
					if (t.TotalSeconds < d) {
						m_MinorTick = TimeSpan.FromTicks((long) (TimeSpan.TicksPerMillisecond*1000*d));
						m_minorTicksPerMajor = 5; //major = d * 5.0;
						break;
					}
					d *= 5.0;
					if (t.TotalSeconds < d) {
						m_MinorTick = TimeSpan.FromTicks((long) (TimeSpan.TicksPerMillisecond*1000*d));
						m_minorTicksPerMajor = 5; //major = d * 5.0;
						break;
					}
					d *= 5.0;
					if (t.TotalSeconds < d) {
						m_MinorTick = TimeSpan.FromTicks((long) (TimeSpan.TicksPerMillisecond*1000*d));
						m_minorTicksPerMajor = 4; //major = d * 4.0;
						break;
					}
					d *= 4.0;
				}
			}

			//Debug.WriteLine("update():  t={0}    minor={1}   minPerMaj={2}", t, m_MinorTick, m_minorTicksPerMajor);
		}

		private string labelString(TimeSpan t)
		{
			// Adapted from from Audacity, Ruler.cpp

			string timeFormat = string.Empty;

			if (m_MinorTick >= TimeSpan.FromHours(1)) {
				// Round time to nearest hour
				t = TimeSpan.FromHours((int) t.TotalHours);
				timeFormat = @"h\:mm";
			}
			else if (m_MinorTick >= TimeSpan.FromMinutes(1)) {
				// Round time to nearest minute
				t = TimeSpan.FromMinutes((int) t.TotalMinutes);

				if (t >= TimeSpan.FromHours(1))
					timeFormat = @"h\:mm\:ss";
				else
					timeFormat = @"m\:ss";
			}
			else if (m_MinorTick >= TimeSpan.FromSeconds(1)) {
				// Round time to nearest second
				t = TimeSpan.FromSeconds((int) t.TotalSeconds);

				if (t >= TimeSpan.FromHours(1))
					timeFormat = @"h\:mm\:ss";
				else
					timeFormat = @"m\:ss";
			}
			else if (m_MinorTick >= TimeSpan.FromMilliseconds(100)) {
				if (t >= TimeSpan.FromHours(1))
					timeFormat = @"h\:mm\:ss\.f";
				else if (t >= TimeSpan.FromMinutes(1))
					timeFormat = @"m\:ss\.f";
				else
					timeFormat = @"s\.f";
			}
			else if (m_MinorTick >= TimeSpan.FromMilliseconds(10)) {
				if (t >= TimeSpan.FromHours(1))
					timeFormat = @"h\:mm\:ss\.ff";
				else if (t >= TimeSpan.FromMinutes(1))
					timeFormat = @"m\:ss\.ff";
				else
					timeFormat = @"s\.ff";
			}
			else if (m_MinorTick >= TimeSpan.FromMilliseconds(1)) {
				if (t >= TimeSpan.FromHours(1))
					timeFormat = @"h\:mm\:ss\.fff";
				else if (t >= TimeSpan.FromMinutes(1))
					timeFormat = @"m\:ss\.fff";
				else
					timeFormat = @"s\.fff";
			}
			else {
				if (t >= TimeSpan.FromHours(1))
					timeFormat = @"h\:mm\:ss\.ffffff";
				else if (t >= TimeSpan.FromMinutes(1))
					timeFormat = @"m\:ss\.ffffff";
				else
					timeFormat = @"s\.ffffff";
			}

			return t.ToString(timeFormat);
		}

		#region Mouse

		private enum MouseState
		{
			Normal,
			DragWait,
			Dragging,
			DraggingMark
		}

		private MouseState m_mouseState = MouseState.Normal;

		private int m_mouseDownX;
		private MouseButtons m_button;
		private TimeSpan m_mark;
		private SnapDetails m_markDetails = null;
		public SortedDictionary<TimeSpan, SnapDetails> selectedMarks = new SortedDictionary<TimeSpan, SnapDetails>();
		protected override void OnMouseDown(MouseEventArgs e)
		{
			//Console.WriteLine("Clicks: " + e.Clicks);

			m_button = e.Button;
			m_mouseDownX = e.X;
			if (e.Button != MouseButtons.Left) return;

			// If we're hovering over a mark when left button is clicked, then select/move the mark 
			m_mark = PointTimeToMark(pixelsToTime(e.X) + VisibleTimeStart);
			if (m_mark != TimeSpan.Zero)
			{
				foreach (SnapDetails d in StaticSnapPoints[m_mark])
				{
					if (m_markDetails == null || (d.SnapLevel > m_markDetails.SnapLevel && d.SnapColor != Color.Empty))
						m_markDetails = d;
				}
				m_mouseState = MouseState.DraggingMark;
			}
			else
			{
				ClearSelectedMarks();
				m_mouseState = MouseState.DragWait;
			}
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (m_button == System.Windows.Forms.MouseButtons.Left)
			{
				switch (m_mouseState)
				{
					case MouseState.Normal:
						return;

					case MouseState.DragWait:
						// Move enough to be considered a drag?
						if (Math.Abs(e.X - m_mouseDownX) <= maxDxForClick)
							return;
						m_mouseState = MouseState.Dragging;
						OnBeginDragTimeRange();
						PlaybackStartTime = pixelsToTime(e.X) + VisibleTimeStart;
						PlaybackEndTime = null;
						goto case MouseState.Dragging;

					case MouseState.Dragging:
						int start, end;
						if (e.X > m_mouseDownX)
						{
							// Start @ mouse down, end @ mouse current
							start = m_mouseDownX;
							end = e.X;
						}
						else
						{
							// Start @ mouse current, end @ mouse down
							start = e.X;
							end = m_mouseDownX;
						}

						PlaybackStartTime = pixelsToTime(start) + VisibleTimeStart;
						PlaybackEndTime = pixelsToTime(end) + VisibleTimeStart;
						return;
					case MouseState.DraggingMark:
						Invalidate();
						break;
					default:
						throw new Exception("Invalid MouseState. WTF?!");
				}
			}
			else
			{
				// We'll get to this point if there is no mouse button selected
				if (PointTimeToMark(pixelsToTime(e.X) + VisibleTimeStart) != TimeSpan.Zero)
				{
					Cursor = Cursors.VSplit;
				}
				else
				{
					Cursor = Cursors.Hand;
				}
			}
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (e.Clicks == 2)
			{
				// Add a mark
				OnClickedAtTime(new RulerClickedEventArgs(pixelsToTime(e.X) + VisibleTimeStart, Form.ModifierKeys, m_button));
			}
			else
			{
				if (m_button == System.Windows.Forms.MouseButtons.Left)
				{
					switch (m_mouseState)
					{
						case MouseState.Normal:
							break; // this is okay and will happen
						//throw new Exception("MouseUp in MouseState.Normal - WTF?");

						case MouseState.DragWait:
							// Didn't move enough to be considered dragging. Just a click.
							OnClickedAtTime(new RulerClickedEventArgs(pixelsToTime(e.X) + VisibleTimeStart, Form.ModifierKeys, m_button));
							break;

						case MouseState.Dragging:
							// Finished a time range drag.
							OnTimeRangeDragged(new ModifierKeysEventArgs(Form.ModifierKeys));
							break;
						case MouseState.DraggingMark:
							if (m_mark != TimeSpan.Zero)
							{
								// Did we SELECT the mark?
								if (e.X == m_mouseDownX)
								{
									if (ModifierKeys != Keys.Control)
									{
										ClearSelectedMarks();
									}
 									selectedMarks.Add(m_mark, m_markDetails);
								}
								// Did we MOVE the mark?
								else
								{
									ClearSelectedMarks();
									OnMarkMoved(new MarkMovedEventArgs(m_mark, pixelsToTime(e.X) + VisibleTimeStart, m_markDetails));
								}
							}
							break;
						default:
							throw new Exception("Invalid MouseState. WTF?!");
					}
				}
				else if (m_button == System.Windows.Forms.MouseButtons.Right)
				{
					m_mark = PointTimeToMark(pixelsToTime(e.X) + VisibleTimeStart);
					if (m_mark != TimeSpan.Zero)
					{
						// See if we got a right-click on top of a mark.
						if (e.X == m_mouseDownX)
						{
							ContextMenu c = new ContextMenu();
							c.MenuItems.Add("&Delete Mark", new EventHandler(DeleteMark_Click));
							c.Show(this, new Point(e.X, e.Y));
						}
					}
					// Otherwise, we've added a mark
					else
					{
						OnClickedAtTime(new RulerClickedEventArgs(pixelsToTime(e.X) + VisibleTimeStart, Form.ModifierKeys, m_button));
					}
				}
			}
			m_mouseState = MouseState.Normal;
			m_button = System.Windows.Forms.MouseButtons.None;
			Invalidate();
		}

		public void ClearSelectedMarks()
		{
			selectedMarks.Clear();
		}

		public void NudgeMark(int offset)
		{
			TimeSpan timeOffset;
			timeOffset = TimeSpan.FromMilliseconds(offset);

			SortedDictionary<TimeSpan, SnapDetails> newSelectedMarks = new SortedDictionary<TimeSpan, SnapDetails>();

			foreach (KeyValuePair<TimeSpan, SnapDetails> kvp in selectedMarks)
			{
				newSelectedMarks.Add(kvp.Key + timeOffset, kvp.Value);
				OnMarkMoved(new MarkMovedEventArgs(kvp.Key, kvp.Key + timeOffset, kvp.Value));
			}

			selectedMarks = newSelectedMarks;
		}

		void DeleteMark_Click(object sender, EventArgs e)
		{
			MenuItem mi = sender as MenuItem;
			if (mi != null)
			{
				DeleteSelectedMarks();
			}
		}

		public void DeleteSelectedMarks()
		{
			foreach (TimeSpan mark in selectedMarks.Keys)
			{
				OnDeleteMark(new DeleteMarkEventArgs(mark));
			}
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			Cursor = Cursors.Hand;
			base.OnMouseEnter(e);
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			Cursor = Cursors.Default;
			base.OnMouseLeave(e);
		}

		public event EventHandler<MarkMovedEventArgs> MarkMoved;
		public event EventHandler<DeleteMarkEventArgs> DeleteMark;
		public event EventHandler<RulerClickedEventArgs> ClickedAtTime;
		public event EventHandler<ModifierKeysEventArgs> TimeRangeDragged;
		public event EventHandler BeginDragTimeRange;

		protected virtual void OnMarkMoved(MarkMovedEventArgs e)
		{
			if (MarkMoved != null)
				MarkMoved(this, e);
		}

		protected virtual void OnDeleteMark(DeleteMarkEventArgs e)
		{
			if (DeleteMark != null)
				DeleteMark(this, e);
		}

		protected virtual void OnClickedAtTime(RulerClickedEventArgs e)
		{
			if (ClickedAtTime != null)
				ClickedAtTime(this, e);
		}

		protected virtual void OnTimeRangeDragged(ModifierKeysEventArgs e)
		{
			if (TimeRangeDragged != null)
				TimeRangeDragged(this, e);
		}

		protected virtual void OnBeginDragTimeRange()
		{
			if (BeginDragTimeRange != null)
				BeginDragTimeRange(this, EventArgs.Empty);
		}

		#endregion

		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (m_font != null)
					m_font.Dispose();
				if (m_textBrush != null)
					m_textBrush.Dispose();
				m_font = null;
				m_textBrush = null;
			}
			base.Dispose(disposing);
		}

		#region "Snap Points (Marks)"

		private SortedDictionary<TimeSpan, List<SnapDetails>> StaticSnapPoints { get; set; }

		private SnapDetails CalculateSnapDetailsForPoint(TimeSpan snapTime, int level, Color color)
		{
			SnapDetails result = new SnapDetails();
			result.SnapLevel = level;
			result.SnapTime = snapTime;
			result.SnapColor = color;

			// the start time and end times for specified points are 2 pixels
			// per snap level away from the snap time.
			result.SnapStart = snapTime - TimeSpan.FromTicks(TimePerPixel.Ticks * level * SnapStrength);
			result.SnapEnd = snapTime + TimeSpan.FromTicks(TimePerPixel.Ticks * level * SnapStrength);
			return result;
		}

		public void AddSnapPoint(TimeSpan snapTime, int level, Color color)
		{
			if (!StaticSnapPoints.ContainsKey(snapTime))
				StaticSnapPoints.Add(snapTime, new List<SnapDetails> { CalculateSnapDetailsForPoint(snapTime, level, color) });
			else
				StaticSnapPoints[snapTime].Add(CalculateSnapDetailsForPoint(snapTime, level, color));

			if (!SuppressInvalidate) Invalidate();
		}

		public bool RemoveSnapPoint(TimeSpan snapTime)
		{
			bool rv = StaticSnapPoints.Remove(snapTime);
			if (!SuppressInvalidate) Invalidate();
			return rv;
		}

		public void ClearSnapPoints()
		{
			StaticSnapPoints.Clear();
			if (!SuppressInvalidate) Invalidate();
		}

		private void _drawMarks(Graphics g)
		{
			Pen p;

			// iterate through all snap points, and if it's visible, draw it
			foreach (KeyValuePair<TimeSpan, List<SnapDetails>> kvp in StaticSnapPoints.ToArray())
			{
				if (kvp.Key >= VisibleTimeEnd) break;
				
				if (kvp.Key >= VisibleTimeStart)
				{
					SnapDetails details = null;
					foreach (SnapDetails d in kvp.Value)
					{
						if (details == null || (d.SnapLevel > details.SnapLevel && d.SnapColor != Color.Empty))
							details = d;
					}
					p = new Pen(details.SnapColor);
					Single x = timeToPixels(kvp.Key);
					p.DashPattern = new float[] { details.SnapLevel, details.SnapLevel };
					if (selectedMarks.ContainsKey(kvp.Key))
					{
						p.Width = 3;
					}
					g.DrawLine(p, x, 0, x, Height);
					p.Dispose();
				}
			}

			if (m_button == MouseButtons.Left && m_mark != TimeSpan.Zero)
			{
				p = new Pen(Brushes.Yellow) {DashPattern = new float[] {2, 2}};
				TimeSpan newMarkPosition = pixelsToTime(PointToClient(new Point(MousePosition.X, MousePosition.Y)).X) + VisibleTimeStart;
				Single x = timeToPixels(newMarkPosition);
				g.DrawLine(p, x, 0, x, Height);
				p.Dispose();
			}
		}

		// Looks at a TimeSpan on the timeline and returns a TimeSpan from the mark collection
		// taking into account a differential for pixels. Zero if nothing is close.
		private TimeSpan PointTimeToMark(TimeSpan pointTime)
		{
			int markDifferential = 40;
			foreach (KeyValuePair<TimeSpan, List<SnapDetails>> kvp in StaticSnapPoints)
			{
				TimeSpan markStart = TimeSpan.FromMilliseconds(kvp.Key.TotalMilliseconds - (markDifferential/2));
				TimeSpan markEnd = TimeSpan.FromMilliseconds(kvp.Key.TotalMilliseconds + (markDifferential/2));
				if (pointTime >= markStart && pointTime <= markEnd)
				{
					return kvp.Key;
				}
			}
			return TimeSpan.Zero;
		}

		#endregion

		public void BeginDraw()
		{
			SuppressInvalidate = true;
		}

		public void EndDraw()
		{
			SuppressInvalidate = false;
			Invalidate();
		}

		public bool SuppressInvalidate { get; set; }
	}


	public class TimeRangeDraggedEventArgs : EventArgs
	{
		public TimeRangeDraggedEventArgs(TimeSpan start, TimeSpan end, Keys modifiers)
		{
			StartTime = start;
			EndTime = end;
			ModifierKeys = modifiers;
		}

		public TimeSpan StartTime { get; private set; }
		public TimeSpan EndTime { get; private set; }
		public Keys ModifierKeys { get; private set; }
	}

	public class RulerClickedEventArgs : EventArgs
	{
		public RulerClickedEventArgs(TimeSpan time, Keys modifiers, MouseButtons button)
		{
			Time = time;
			ModifierKeys = modifiers;
			Button = button;
		}

		public TimeSpan Time { get; private set; }
		public Keys ModifierKeys { get; private set; }
		public MouseButtons Button { get; private set; }
	}

	public class MarkMovedEventArgs : EventArgs
	{
		public MarkMovedEventArgs(TimeSpan originalMark, TimeSpan newMark, SnapDetails details)
		{
			OriginalMark = originalMark;
			NewMark = newMark;
			SnapDetails = details;
		}

		public TimeSpan OriginalMark { get; private set; }
		public TimeSpan NewMark { get; private set; }
		public SnapDetails SnapDetails { get; private set; }
	}

	public class DeleteMarkEventArgs : EventArgs
	{
		public DeleteMarkEventArgs(TimeSpan mark)
		{
			Mark = mark;
		}

		public TimeSpan Mark { get; private set; }
	}
}