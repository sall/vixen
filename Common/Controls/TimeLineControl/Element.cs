﻿using System;
using System.Drawing;
using System.Collections.Generic;
using Common.Controls.ControlsEx;
using Vixen.Module.Editor;
using Vixen.Module.Effect;
using Vixen.Sys;

namespace Common.Controls.Timeline
{
	[Serializable]
	public class Element : IComparable<Element>, ITimePeriod, IDisposable
	{
		private TimeSpan m_startTime;
		private TimeSpan m_duration;
		private static Color m_backColor = Color.FromArgb(0, 0, 0, 0);
		private static Color m_borderColor = Color.Black;
		private bool m_selected = false;
		private static Font m_textFont = new Font("Arial", 7);
		private static Color m_textColor = Color.FromArgb(255, 255, 255);
		private static Brush infoBrush = new SolidBrush(Color.FromArgb(128,0,0,0));
		private static System.Object drawLock = new System.Object();

		public Element()
		{
			CachedCanvasIsCurrent = false;
		}

		/// <summary>
		/// Copy constructor. Creates a shallow copy of other.
		/// </summary>
		/// <param name="other">The element to copy.</param>
		public Element(Element other)
		{
			m_startTime = other.m_startTime;
			m_duration = other.m_duration;
			m_selected = other.m_selected;
		}

		#region Begin/End update

		private TimeSpan m_origStartTime, m_origDuration;

		///<summary>Suspends raising events until EndUpdate is called.</summary>
		public void BeginUpdate()
		{
			m_origStartTime = this.StartTime;
			m_origDuration = this.Duration;
		}

		public void EndUpdate()
		{
			if ((StartTime != m_origStartTime) || (Duration != m_origDuration)) {
				Changed = true;
				OnTimeChanged();
			}
		}

		#endregion

		#region Properties

		/// <summary>
		/// Display top for the last version of this element. Not reliable when the element is part of multiple rows.
		/// I.E. grouping. Use Row.DisplayTop for the containing row and RowTopOffset.
		/// </summary>
		public int DisplayTop { get; set; }
		public int RowTopOffset { get; set; }
		public int DisplayHeight { get; set; }
		public Rectangle DisplayRect { get; set; }
		public bool MouseCaptured { get; set; }

		[NonSerializedAttribute]
		public EffectNode _effectNode;

		public EffectNode EffectNode
		{
			get { return _effectNode; }
			set { _effectNode = value; }
		}

		/// <summary>
		/// This is the last row that this element was associated with. This element can be part of more than one row if it is part of multiple groups
		/// So do not trust it. Already leads to a issue if the rows I belong to are different heights.
		/// The element will not be drawn correctly because the cached image height is based on this
		/// </summary>
		public Row Row { get; set; }

		/// <summary>
		/// Gets or sets the starting time of this element (left side).
		/// </summary>
		public TimeSpan StartTime
		{
			get { return m_startTime; }
			set
			{
				if (value < TimeSpan.Zero)
					value = TimeSpan.Zero;

				if (m_startTime == value)
					return;

				m_startTime = value;
				OnTimeChanged();
			}
		}

		/// <summary>
		/// Gets or sets the time duration of this element (width).
		/// </summary>
		public TimeSpan Duration
		{
			get { return m_duration; }
			set
			{
				if (m_duration == value)
					return;

				m_duration = value;
				OnTimeChanged();
			}
		}

		/// <summary>
		/// Gets or sets the ending time of this element (right side).
		/// Changing this value adjusts the duration. The start time is unaffected.
		/// </summary>
		public TimeSpan EndTime
		{
			get { return StartTime + Duration; }
			set { Duration = (value - StartTime); }
		}

		public bool Selected
		{
			get { return m_selected; }
			set
			{
				if (m_selected == value)
					return;

				m_selected = value;
				//m_redraw = true;
				//CachedCanvasIsCurrent = false;
				OnSelectedChanged();
			}
		}

		private bool _changed = true;

		public bool Changed
		{
			set
			{
				if (value) {
					CachedCanvasIsCurrent = false;
				}
				_changed = value;
			}
			get { return _changed; }
		}

		#endregion

		#region Events

		/// <summary>
		/// Occurs when some of this element's other content changes.
		/// </summary>
		public event EventHandler ContentChanged;

		/// <summary>
		/// Occurs when this element's Selected state changes.
		/// </summary>
		public event EventHandler SelectedChanged;

		/// <summary>
		/// Occurs when one of this element's time propeties changes.
		/// </summary>
		public event EventHandler TimeChanged;

		#endregion

		#region Virtual Methods

		/// <summary>
		/// Raises the ContentChanged event.
		/// </summary>
		protected virtual void OnContentChanged()
		{
			if (ContentChanged != null)
				ContentChanged(this, EventArgs.Empty);
		}

		/// <summary>
		/// Raises the SelectedChanged event.
		/// </summary>
		protected virtual void OnSelectedChanged()
		{
			if (SelectedChanged != null)
				SelectedChanged(this, EventArgs.Empty);
		}

		/// <summary>
		/// Raises the TimeChanged event.
		/// </summary>
		protected virtual void OnTimeChanged()
		{
			if (TimeChanged != null)
				TimeChanged(this, EventArgs.Empty);
		}

		#endregion

		#region Methods

		public int CompareTo(Element other)
		{
			int rv = StartTime.CompareTo(other.StartTime);
			if (rv != 0)
				return rv;
			else
				return EndTime.CompareTo(other.EndTime);
		}

		public void MoveStartTime(TimeSpan offset)
		{
			if (m_startTime + offset < TimeSpan.Zero)
				offset = -m_startTime;

			m_duration -= offset;
			StartTime += offset;
		}

		#endregion

		#region Drawing

		private Bitmap CachedElementCanvas { get; set; }

		private bool _cachedCanvasIsCurrent=false;
		public bool CachedCanvasIsCurrent {
			get
			{
				return _cachedCanvasIsCurrent;
			}
			set
			{
				_cachedCanvasIsCurrent=value;
			}
		}

		protected virtual Bitmap SetupCanvas(Size imageSize)
		{
			Bitmap result = new Bitmap(imageSize.Width, imageSize.Height);
			using (Graphics g = Graphics.FromImage(result)) {
				using (Brush b = new SolidBrush(m_backColor)) {
					g.FillRectangle(b, 0, 0, imageSize.Width, imageSize.Height);
				}
			}
			return result;	
		}

		protected virtual void AddSelectionOverlayToCanvas(Graphics g, bool drawSelected)
		{
			// Width - bold if selected
			int b_wd = drawSelected ? 3 : 1;

			// Adjust the rect such that the border is completely inside it.
			Rectangle b_rect = new Rectangle(
				(b_wd/2),
				(b_wd/2),
				(int) g.VisibleClipBounds.Width - b_wd,
				(int) g.VisibleClipBounds.Height - b_wd
				);

			// Draw it!
			using (Pen border = new Pen(m_borderColor)) {
				border.Width = b_wd;
				g.DrawRectangle(border, b_rect);
			}
		}

		protected virtual void DrawCanvasContent(Graphics graphics)
		{
		}

		public Bitmap SetupCachedImage(Size imageSize)
		{
			CachedCanvasIsCurrent=false;
			EffectNode.Effect.Render(); //ensure the effect is rendered outside of the locking.
			lock (drawLock) {
				Bitmap bitmap = new Bitmap(imageSize.Width, imageSize.Height);
				using(Graphics g = Graphics.FromImage(bitmap)){
					DrawCanvasContent(g);
					AddSelectionOverlayToCanvas(g, false);
					if (CachedElementCanvas != null)
					{
						CachedElementCanvas.Dispose();
					}
					
					CachedElementCanvas = bitmap;
					Changed = false;
					CachedCanvasIsCurrent = true;
					
				}
			}
			
			return CachedElementCanvas;
		}

		public void DrawPlaceholder(Graphics g)
		{
			g.FillRectangle(new SolidBrush(Color.FromArgb(122, 122, 122)),
			                new Rectangle((int) g.VisibleClipBounds.Left, (int) g.VisibleClipBounds.Top,
			                              (int) g.VisibleClipBounds.Width, (int) g.VisibleClipBounds.Height));
		}

		public void DrawInfo(Graphics g, Rectangle rect) 
		{
			const int margin = 2;
			if (MouseCaptured)
			{
				// add text describing the effect
				using (Brush b = new SolidBrush(m_textColor))
				{
					g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

					string s;

					s = string.Format("{0} \r\n Start: {1} \r\n Length: {2}", 
						EffectNode.Effect.EffectName,
						EffectNode.StartTime.ToString(@"m\:ss\.fff"),
						EffectNode.TimeSpan.ToString(@"m\:ss\.fff"));

					SizeF textSize = g.MeasureString(s, m_textFont);
					Rectangle destRect = new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
					if (rect.Y < destRect.Height)
					{
						// Display the text below the effect
						destRect.Y += rect.Height + margin - 8;
					}
					else
					{
						// Display the text above the effect
						destRect.Y -= (int)textSize.Height + margin - 4;
					}

					//Check to make sure we are on the screen. 
					if (g.VisibleClipBounds.X > destRect.X)
					{
						destRect.X = (int)g.VisibleClipBounds.X + 5;
					}

					// Full size info box. Comment out next two lines to clip
					destRect.Width = (int)textSize.Width + margin;
					destRect.Height = (int)textSize.Height + margin;
					
					g.FillRectangle(infoBrush, new Rectangle(destRect.Left, destRect.Top, (int)Math.Min(textSize.Width + margin, destRect.Width), (int)Math.Min(textSize.Height + margin, destRect.Height)));
					g.DrawString(s, m_textFont, b, new Rectangle(destRect.Left + margin/2, destRect.Top + margin/2, destRect.Width - margin, destRect.Height - margin));
				}
			}
		}

		public Bitmap Draw(Size imageSize)
		{
			lock (drawLock)
			{
				if (CachedElementCanvas==null)
				{
					Bitmap b = SetupCanvas(imageSize);
					using (Graphics g = Graphics.FromImage(b))
					{
						DrawPlaceholder(g);
						AddSelectionOverlayToCanvas(g, m_selected);
						CachedElementCanvas = b;
						CachedCanvasIsCurrent = false; 
					}
					return b;
				} else if (m_selected)
				{
					Bitmap b = new Bitmap(CachedElementCanvas);
					using (Graphics g = Graphics.FromImage(b))
					{
						AddSelectionOverlayToCanvas(g, true);
					}
					
					return b;
				}
			}
			return CachedElementCanvas;
		}

		#endregion

		~Element()
		{
			Dispose(false);
		}

		protected void Dispose(bool disposing)
		{
			if (disposing) {
				if (CachedElementCanvas != null)
				{
					CachedElementCanvas.Dispose();
					CachedElementCanvas = null;
				}
			}
			
			
		}

		public void Dispose()
		{
			Dispose(true);
		}
	}


	public class ElementTimeInfo : ITimePeriod
	{
		public ElementTimeInfo(Element elem)
		{
			StartTime = elem.StartTime;
			Duration = elem.Duration;
		}

		public TimeSpan StartTime { get; set; }
		public TimeSpan Duration { get; set; }

		public TimeSpan EndTime
		{
			get { return StartTime + Duration; }
		}

		public static void SwapTimes(ITimePeriod lhs, ITimePeriod rhs)
		{
			TimeSpan temp;

			temp = lhs.StartTime;
			lhs.StartTime = rhs.StartTime;
			rhs.StartTime = temp;

			temp = lhs.Duration;
			lhs.Duration = rhs.Duration;
			rhs.Duration = temp;
		}
	}

	public interface ITimePeriod
	{
		TimeSpan StartTime { get; set; }
		TimeSpan Duration { get; set; }
	}
}