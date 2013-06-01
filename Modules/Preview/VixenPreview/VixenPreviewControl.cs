﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using Vixen.Data.Value;
using Vixen.Execution.Context;
using Vixen.Sys;
using VixenModules.Preview.VixenPreview.Shapes;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;

namespace VixenModules.Preview.VixenPreview
{

	public partial class VixenPreviewControl : UserControl
	{
		bool UseFloods = false;

		#region "Variables"
		public VixenPreviewSetupElementsDocument elementsForm;
		public VixenPreviewSetupPropertiesDocument propertiesForm;
		private bool _paused = false;
		//private BackgroundWorker renderBackgroundWorker = new BackgroundWorker();
		private BufferedGraphicsContext context;
		private BufferedGraphics bufferedGraphics;
		public static double averageUpdateTime = 0;
		public static double updateCount = 0;
		public static double totalUpdateTime = 0;
		public static double lastUpdateTime = 0;
		public double lastRenderUpdateTime = 0;

		private Tools _currentTool = Tools.Select;
		public enum Tools
		{
			Select,
			String,
			Arch,
			Rectangle,
			Ellipse,
			Single,
			Triangle,
			MegaTree,
			Net,
			Flood,
			Star,
			Cane
		}

		//private List<DisplayItem> selectedDisplayItems = new List<DisplayItem>();
		private Point dragStart;
		private Point dragCurrent;
		private int changeX;
		private int changeY;
		private DisplayItem _selectedDisplayItem = null;
		//private PreviewBaseShape _copiedShape;
		private bool _editMode = false;

		private Bitmap _background;
		private Bitmap _alphaBackground;
		private Bitmap _blankAlphaBackground;

		private VixenPreviewData _data;

		private List<ElementNode> _highlightedElements = new List<ElementNode>();
		private List<DisplayItem> _selectedDisplayItems;

		Rectangle _bandRect = new Rectangle();

		// For debug, status line, etc.
		private Random random = new Random();
		Stopwatch renderTimer = new Stopwatch();

		TextureBrush _backgroundBrush;

		#endregion

		#region "Events"
		public delegate void SelectDisplayItemEventHandler(object sender, DisplayItem displayItem);
		public event SelectDisplayItemEventHandler OnSelectDisplayItem;

		public delegate void DeSelectDisplayItemEventHandler(object sender, DisplayItem displayItem);
		public event DeSelectDisplayItemEventHandler OnDeSelectDisplayItem;

		public ConcurrentDictionary<ElementNode, List<PreviewPixel>> NodeToPixel = new ConcurrentDictionary<ElementNode, List<PreviewPixel>>();

		public ISequenceContext vixenContext = null;

		#endregion

		public List<ElementNode> HighlightedElements
		{
			get { return _highlightedElements; }
		}

		public List<DisplayItem> SelectedDisplayItems
		{
			get
			{
				if (_selectedDisplayItems == null)
					_selectedDisplayItems = new List<DisplayItem>();
				return _selectedDisplayItems;
			}
		}

		public int BackgroundAlpha
		{
			get { return Data.BackgroundAlpha; }
			set
			{
				if (Data != null)
				{
					Data.BackgroundAlpha = value;
					SetupBackgroundAlphaImage();
				}
			}
		}

		public bool EditMode
		{
			set
			{
				_editMode = value;
				_selectedDisplayItem = null;
				if (DisplayItems != null)
				{
					foreach (DisplayItem item in DisplayItems)
					{
						item.Shape.Deselect();
					}
				}
			}
			get { return _editMode; }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public VixenPreviewData Data
		{
			get
			{
				if (DesignMode)
					_data = new VixenPreviewData();
				return _data;
			}
			set
			{
				_data = value;
			}
		}

		private List<DisplayItem> DisplayItems
		{
			get
			{
				if (Data != null)
				{
					return Data.DisplayItems;
				}
				else
				{
					return null;
				}
			}
		}

		public int PixelCount
		{
			get
			{
				int count = 0;
				foreach (DisplayItem displayItem in DisplayItems)
				{
					count += displayItem.Shape.Pixels.Count;
				}
				return count;
			}
		}

		public VixenPreviewControl()
			: base()
		{
			InitializeComponent();

			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.DoubleBuffer, true);

			context = BufferedGraphicsManager.Current;
			AllocateGraphicsBuffer();



			//renderBackgroundWorker.DoWork += new DoWorkEventHandler(DoRenderWork);
			//renderBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(RenderWorkCompleted);
		}

		private void VixenPreviewControl_Load(object sender, EventArgs e)
		{
			InitializeGraphics();
			//StartRefresh();
		}

		public Bitmap Background
		{
			get { return _background; }
		}

		public void LoadBackground(string fileName)
		{
			//lock (PreviewTools.renderLock)
			//{
			if (System.IO.File.Exists(fileName))
			{
				try
				{
					//_background = Image.FromFile(fileName);
					using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
					{
						using (Bitmap loadedBitmap = new Bitmap(fs))
						{
							_background = loadedBitmap.Clone(new Rectangle(0, 0, loadedBitmap.Width, loadedBitmap.Height), PixelFormat.Format32bppPArgb);
						}
					}
					//Bitmap loadedBitmap = new Bitmap(fileName);
					Console.WriteLine("Load: " + fileName);
				}
				catch (Exception ex)
				{
					_background = new Bitmap(640, 480);
					MessageBox.Show("There was an error loading the background image: " + ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
				}
			}
			else
			{
				_background = new Bitmap(640, 480);
			}
			//}

			SetupBackgroundAlphaImage();
		}

		public void LoadBackground()
		{
			if (Data.BackgroundFileName != null)
			{
				LoadBackground(Data.BackgroundFileName);
			}
		}

		private void SetupBackgroundAlphaImage()
		{

			if (_background != null)
			{
				AllocateGraphicsBuffer();
				//_alphaBackground = new Bitmap(_background.Width, _background.Height);
				_alphaBackground = new Bitmap(_background.Width, _background.Height, PixelFormat.Format32bppPArgb);

				//bitmapFinal = bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), PixelFormat.Format32bppPArgb);bitmapFinal = bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), PixelFormat.Format32bppPArgb);

				using (Graphics gfx = Graphics.FromImage(_alphaBackground))

				using (SolidBrush brush = new SolidBrush(Color.FromArgb(255 - BackgroundAlpha, 0, 0, 0)))
				{
					gfx.DrawImage(_background, 0, 0, _background.Width, _background.Height);
					gfx.FillRectangle(brush, 0, 0, _alphaBackground.Width, _alphaBackground.Height);
				}

				_blankAlphaBackground = new Bitmap(_background.Width, _background.Height, PixelFormat.Format32bppPArgb);
				Graphics g = Graphics.FromImage(_blankAlphaBackground);
				g.Clear(Color.FromArgb(255 - BackgroundAlpha, 0, 0, 0));
				_backgroundBrush = new TextureBrush(_blankAlphaBackground);
			}
		}

		private void InitializeGraphics()
		{
			context = BufferedGraphicsManager.Current;
			AllocateGraphicsBuffer();
		}

		private void AllocateGraphicsBuffer()
		{
			if (!Disposing)
			{
				//lock (PreviewTools.renderLock)
				//{

				if (context != null)
				{
					context.MaximumBuffer = new Size(this.Width + 1, this.Height + 1);

					if (bufferedGraphics != null)
					{
						//lock (bufferedGraphics)
						//{
						bufferedGraphics.Dispose();
						bufferedGraphics = null;
						bufferedGraphics = context.Allocate(this.CreateGraphics(),
							new Rectangle(0, 0, this.Width + 1, this.Height + 1));
						//}
					}
					else
					{
						bufferedGraphics = context.Allocate(this.CreateGraphics(),
						new Rectangle(0, 0, this.Width + 1, this.Height + 1));
					}
				}
				//}
			}
		}

		public void AddDisplayItem(DisplayItem displayItem)
		{
			DisplayItems.Add(displayItem);
		}

		public bool MouseOverSelectedDisplayItems(int X, int Y)
		{
			foreach (DisplayItem item in SelectedDisplayItems)
			{
				if (item.Shape.PointInShape(new PreviewPoint(X, Y)))
				{
					return true;
				}
			}
			return false;
		}

		private bool _mouseCaptured = false;
		private bool _banding = false;
		private void VixenPreviewControl_MouseDown(object sender, MouseEventArgs e)
		{
			if (_editMode)
			{
				if (e.Button == System.Windows.Forms.MouseButtons.Left)
				{
					PreviewPoint point = new PreviewPoint(e.X, e.Y);

					if (_currentTool == Tools.Select)
					{
						// Is there a single dislay item selected?
						if (_selectedDisplayItem != null)
						{
							//if (_selectedDisplayItem.GetType().ToString().Contains("PreviewCustom"))
							//{
							//    Console.WriteLine(_selectedDisplayItem.GetType().ToString());
							//}
							// Lets see if we've got a drag point.
							PreviewPoint selectedPoint = _selectedDisplayItem.Shape.PointInSelectPoint(point);
							if (selectedPoint != null)
							{
								dragStart.X = e.X;
								dragStart.Y = e.Y;
								_selectedDisplayItem.Shape.SetSelectPoint(selectedPoint);
								Capture = true;
								_mouseCaptured = true;
							}
							// If we're not resizing, see if we're moving a single shape
							else if (_selectedDisplayItem.Shape.PointInShape(point))
							{
								StartMove(e.X, e.Y);
							}
							// If we get here, we're outside the shape, deselect
							else
							{
								DeSelectSelectedDisplayItem();
							}
						}
						// Are there multiple items selected?
						// If so, we're moving, can't resize them...
						else if (SelectedDisplayItems.Count > 1)
						{
							if (MouseOverSelectedDisplayItems(e.X, e.Y))
							{
								//_selectedDisplayItem.Shape.SetSelectPoint(null);
								StartMove(e.X, e.Y);
							}
							else
							{
								SelectedDisplayItems.Clear();
							}
						}

						if (!_mouseCaptured)
						{
							_selectedDisplayItem = DisplayItemAtPoint(point);
							if (_selectedDisplayItem != null)
							{
								_selectedDisplayItem.Shape.Select(true);
								OnSelectDisplayItem(this, _selectedDisplayItem);
							}
						}

						// If we get this far, and we've got nothing selected, we're drawing a rubber band!
						if (_selectedDisplayItem == null && SelectedDisplayItems.Count == 0)
						{
							// Capture the mouse in case we want to draw a rubber band
							dragStart.X = e.X;
							dragStart.Y = e.Y;
							Capture = true;
							_mouseCaptured = true;
							SelectedDisplayItems.Clear();
							_bandRect.Width = 0; _bandRect.Height = 0;
							_banding = true;
						}
					}
					// If we're not Selecting items, we're drawing them
					else
					{
						DisplayItem newDisplayItem = null;
						if (_currentTool == Tools.String)
						{
							newDisplayItem = new DisplayItem();
							newDisplayItem.Shape = new PreviewLine(new PreviewPoint(e.X, e.Y), new PreviewPoint(e.X, e.Y), 50, elementsForm.SelectedNode);
						}
						else if (_currentTool == Tools.Arch)
						{
							newDisplayItem = new DisplayItem();
							newDisplayItem.Shape = new PreviewArch(new PreviewPoint(e.X, e.Y), elementsForm.SelectedNode);
						}
						else if (_currentTool == Tools.Rectangle)
						{
							newDisplayItem = new DisplayItem();
							newDisplayItem.Shape = new PreviewRectangle(new PreviewPoint(e.X, e.Y), elementsForm.SelectedNode);
						}
						else if (_currentTool == Tools.Single)
						{
							newDisplayItem = new DisplayItem();
							newDisplayItem.Shape = new PreviewSingle(new PreviewPoint(e.X, e.Y), elementsForm.SelectedNode);
						}
						else if (_currentTool == Tools.Ellipse)
						{
							newDisplayItem = new DisplayItem();
							newDisplayItem.Shape = new PreviewEllipse(new PreviewPoint(e.X, e.Y), 50, elementsForm.SelectedNode);
						}
						else if (_currentTool == Tools.Triangle)
						{
							newDisplayItem = new DisplayItem();
							newDisplayItem.Shape = new PreviewTriangle(new PreviewPoint(e.X, e.Y), elementsForm.SelectedNode);
						}
						else if (_currentTool == Tools.Net)
						{
							newDisplayItem = new DisplayItem();
							newDisplayItem.Shape = new PreviewNet(new PreviewPoint(e.X, e.Y), elementsForm.SelectedNode);
						}
						else if (_currentTool == Tools.Cane)
						{
							newDisplayItem = new DisplayItem();
							newDisplayItem.Shape = new PreviewCane(new PreviewPoint(e.X, e.Y), elementsForm.SelectedNode);
						}
						else if (_currentTool == Tools.Star)
						{
							newDisplayItem = new DisplayItem();
							newDisplayItem.Shape = new PreviewStar(new PreviewPoint(e.X, e.Y), elementsForm.SelectedNode);
						}
						else if (_currentTool == Tools.Flood)
						{
							newDisplayItem = new DisplayItem();
							newDisplayItem.Shape = new PreviewFlood(new PreviewPoint(e.X, e.Y), elementsForm.SelectedNode);
						}
						else if (_currentTool == Tools.MegaTree)
						{
							newDisplayItem = new DisplayItem();
							newDisplayItem.Shape = new PreviewMegaTree(new PreviewPoint(e.X, e.Y), elementsForm.SelectedNode);
						}

						// Now add the newely created display item to the screen.
						if (newDisplayItem != null)
						{
							AddDisplayItem(newDisplayItem);
							_selectedDisplayItem = newDisplayItem;
							_selectedDisplayItem.Shape.PixelSize = 3;
							_selectedDisplayItem.Shape.Select(true);
							_selectedDisplayItem.Shape.SelectDefaultSelectPoint();
							dragStart.X = e.X;
							dragStart.Y = e.Y;
							Capture = true;
							_mouseCaptured = true;
						}
					}
				}
				else if (e.Button == System.Windows.Forms.MouseButtons.Right)
				{

				}
			}
		}

		private void StartMove(int x, int y)
		{
			dragStart.X = x;
			dragStart.Y = y;
			if (_selectedDisplayItem != null)
			{
				_selectedDisplayItem.Shape.SetSelectPoint(null);
			}
			else if (SelectedDisplayItems.Count > 0)
			{
				foreach (DisplayItem item in SelectedDisplayItems)
				{
					item.Shape.SetSelectPoint(null);
				}
			}
			Capture = true;
			_mouseCaptured = true;
		}

		private void VixenPreviewControl_MouseMove(object sender, MouseEventArgs e)
		{
			if (_editMode)
			{
				PreviewPoint point = new PreviewPoint(e.X, e.Y);

				dragCurrent.X = e.X;
				dragCurrent.Y = e.Y;
				changeX = e.X - dragStart.X;
				changeY = e.Y - dragStart.Y;

				// Are we moving a single display item?
				if (_mouseCaptured && _selectedDisplayItem != null)
				{
					_selectedDisplayItem.Shape.MouseMove(dragCurrent.X, dragCurrent.Y, changeX, changeY);
				}
				// If we get here, we're drwing a rubber band
				else if (_banding)
				{
					_bandRect.Location = dragStart;
					_bandRect.Width = changeX;
					_bandRect.Height = changeY;
					foreach (DisplayItem item in DisplayItems)
					{
						if (item.Shape.ShapeInRect(_bandRect))
						{
							if (!SelectedDisplayItems.Contains(item))
							{
								SelectedDisplayItems.Add(item);
							}
						}
						else if (SelectedDisplayItems.Contains(item))
						{
							SelectedDisplayItems.Remove(item);
						}
					}
				}
				// Are we moving a group of display items?
				else if (_mouseCaptured && _selectedDisplayItem == null && SelectedDisplayItems.Count > 1)
				{
					foreach (DisplayItem item in SelectedDisplayItems)
					{
						item.Shape.MouseMove(dragCurrent.X, dragCurrent.Y, changeX, changeY);
					}
				}
				else
				{
					if (_selectedDisplayItem != null)
					{
						PreviewPoint selectPoint = _selectedDisplayItem.Shape.PointInSelectPoint(point);
						if (selectPoint != null)
						{
							Cursor.Current = Cursors.Cross;
						}
						else if (_selectedDisplayItem.Shape.PointInShape(point))
						{
							Cursor.Current = Cursors.SizeAll;
						}
						else
						{
							Cursor.Current = Cursors.Default;
						}
					}
					else if (SelectedDisplayItems.Count > 0)
					{
						if (MouseOverSelectedDisplayItems(e.X, e.Y))
						{
							Cursor.Current = Cursors.SizeAll;
						}
					}
				}
			}
		}

		private void VixenPreviewControl_MouseUp(object sender, MouseEventArgs e)
		{
			HighlightedElements.Clear();
			if (_mouseCaptured)
			{
				if (_currentTool != Tools.Select)
				{
					// If control is pressed, deselect the shape and immediately allow drawing another shape
					if ((Control.ModifierKeys & Keys.Control) != 0)
					{
						_selectedDisplayItem.Shape.MouseUp(sender, e);
						DeSelectSelectedDisplayItem();
					}
					else
					{
						_currentTool = Tools.Select;
					}
				}

				if (_selectedDisplayItem != null)
				{
					_selectedDisplayItem.Shape.MouseUp(sender, e);
					OnSelectDisplayItem(this, _selectedDisplayItem);
				}
				// Ok, if this is true, we've got a rubber band and something is selected, now what?
				else if (SelectedDisplayItems.Count > 0)
				{
					// If we only have one item, just select it and go on.
					if (SelectedDisplayItems.Count == 1)
					{
						_selectedDisplayItem = SelectedDisplayItems[0];
						_selectedDisplayItem.Shape.Select(true);
						OnSelectDisplayItem(this, _selectedDisplayItem);
						SelectedDisplayItems.Clear();
					}
					else
					{
						// Do nothing...
					}
				}
			}
			ResetMouse();
		}

		private void ResetMouse()
		{
			_banding = false;
			Capture = false;
			_mouseCaptured = false;
			_bandRect.Width = 0; _bandRect.Height = 0;
		}

		public DisplayItem DisplayItemAtPoint(PreviewPoint point)
		{
			foreach (DisplayItem displayItem in DisplayItems)
			{
				if (displayItem.Shape.PointInShape(point))
				{
					return displayItem;
				}
			}
			return null;
		}

		public Tools CurrentTool
		{
			get { return _currentTool; }
			set
			{
				_currentTool = value;
				if (_selectedDisplayItem != null)
				{
					DeSelectSelectedDisplayItem();
				}
			}
		}

		private void VixenPreviewControl_Resize(object sender, EventArgs e)
		{
			AllocateGraphicsBuffer();
		}

		private void VixenPreviewControl_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				Delete();
			}
			// Copy
			else if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control)
			{
				Copy();
			}
			else if (e.KeyCode == Keys.X && e.Modifiers == Keys.Control)
			{
				Cut();
			}
			else if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
			{
				if (Paste())
				{
					// move the prop to the mouse position
					Point moveToPoint = PointToClient(MousePosition);
					_selectedDisplayItem.Shape.MoveTo(moveToPoint.X, moveToPoint.Y);

					StartMove(moveToPoint.X, moveToPoint.Y);
				}
			}
			else if (e.KeyCode == Keys.Up)
			{
				if (_selectedDisplayItem != null)
					_selectedDisplayItem.Shape.Nudge(0, -1);
				else if (SelectedDisplayItems.Count > 0)
				{
					foreach (DisplayItem item in DisplayItems)
					{
						if (SelectedDisplayItems.Contains(item))
							item.Shape.Nudge(0, -1);
					}
				}
			}
			else if (e.KeyCode == Keys.Down)
			{
				if (_selectedDisplayItem != null)
					_selectedDisplayItem.Shape.Nudge(0, 1);
				else if (SelectedDisplayItems.Count > 0)
				{
					foreach (DisplayItem item in DisplayItems)
					{
						if (SelectedDisplayItems.Contains(item))
							item.Shape.Nudge(0, 1);
					}
				}
			}
			else if (e.KeyCode == Keys.Right)
			{
				if (_selectedDisplayItem != null)
					_selectedDisplayItem.Shape.Nudge(1, 0);
				else if (SelectedDisplayItems.Count > 0)
				{
					foreach (DisplayItem item in DisplayItems)
					{
						if (SelectedDisplayItems.Contains(item))
							item.Shape.Nudge(1, 0);
					}
				}
			}
			else if (e.KeyCode == Keys.Left)
			{
				if (_selectedDisplayItem != null)
					_selectedDisplayItem.Shape.Nudge(-1, 0);
				else if (SelectedDisplayItems.Count > 0)
				{
					foreach (DisplayItem item in DisplayItems)
					{
						if (SelectedDisplayItems.Contains(item))
							item.Shape.Nudge(-1, 0);
					}
				}
			}
			else if (e.KeyCode == Keys.Escape)
			{
				// not sure how to handle this yet...
				// would work fine if we were always live moving.
				// if we are standard mving, we don't want to delete the item when escape is pressed!

				//if (_mouseCaptured) {
				//Capture = false;
				//_mouseCaptured = false;
				//DisplayItems.Remove(_selectedDisplayItem);
				//DeSelectSelectedDisplayItem();
			}
		}

		public void DeSelectSelectedDisplayItem()
		{
			if (_selectedDisplayItem != null)
			{
				OnDeSelectDisplayItem(this, _selectedDisplayItem);
				_selectedDisplayItem.Shape.Deselect();
				_selectedDisplayItem = null;
			}
		}

		public void DeSelectSelectedDisplayItemNoNotify()
		{
			if (_selectedDisplayItem != null)
			{
				_selectedDisplayItem.Shape.Deselect();
				_selectedDisplayItem = null;
			}
		}

		public void DrawDisplayItemsInBackground()
		{
			//Thread drawThread = new Thread(() => DrawDisplayItems(bufferedGraphics.Graphics));
			//drawThread.Start();
		}

		public void UpdateColors(ElementNode node, Color newColor)
		{
			List<PreviewPixel> pixels;
			if (NodeToPixel.TryGetValue(node, out pixels))
			{
				foreach (PreviewPixel pixel in pixels)
				{
					pixel.PixelColor = newColor;
				}
			}
		}

		public void ResetColors()
		{
			foreach (List<PreviewPixel> pixels in NodeToPixel.Values)
			{
				foreach (PreviewPixel pixel in pixels)
				{
					if (_editMode)
					{
						pixel.PixelColor = Color.White;
					}
					else
					{
						pixel.PixelColor = Color.Transparent;
					}
				}
			}
		}

		public void Reload()
		{
			//lock (PreviewTools.renderLock)
			//{
			Console.WriteLine("Reload");
			if (NodeToPixel == null) PreviewTools.Throw("PreviewBase.NodeToPixel == null");
			NodeToPixel.Clear();

			if (DisplayItems == null) PreviewTools.Throw("DisplayItems == null");
			foreach (DisplayItem item in DisplayItems)
			{
				if (item.Shape.Pixels == null) PreviewTools.Throw("item.Shape.Pixels == null");
				foreach (PreviewPixel pixel in item.Shape.Pixels)
				{
					if (pixel.Node != null)
					{
						List<PreviewPixel> pixels;
						if (NodeToPixel.TryGetValue(pixel.Node, out pixels))
						{
							if (!pixels.Contains(pixel))
							{
								pixels.Add(pixel);
							}
						}
						else
						{
							pixels = new List<PreviewPixel>();
							pixels.Add(pixel);
							NodeToPixel.TryAdd(pixel.Node, pixels);

						}
					}
				}
			}
			LoadBackground();
			//}
		}

		public bool Paused
		{
			set
			{
				_paused = value;
				if (_paused)
				{
				}
				else
				{
				}
			}
			get { return _paused; }
		}

		#region Clipboard

		public void Cut()
		{
			Copy();
			Delete();
		}


		public bool Paste()
		{
			string xml = Clipboard.GetText();
			DisplayItem newDisplayItem = (DisplayItem)PreviewTools.DeSerializeToObject(xml, typeof(DisplayItem));
			if (newDisplayItem != null)
			{
				DeSelectSelectedDisplayItem();
				Console.WriteLine("Pasted: " + newDisplayItem.Shape.GetType().ToString());
				AddDisplayItem(newDisplayItem);
				_selectedDisplayItem = newDisplayItem;
				_selectedDisplayItem.Shape.Select(true);
				_selectedDisplayItem.Shape.SetSelectPoint(null);
				OnSelectDisplayItem(this, _selectedDisplayItem);
				return true;
			}
			else
				return false;
		}

		public void Delete()
		{
			if (_selectedDisplayItem != null)
			{
				DisplayItems.Remove(_selectedDisplayItem);
				DeSelectSelectedDisplayItem();
			}
		}

		public void Copy()
		{
			if (_selectedDisplayItem != null)
			{
				string xml = PreviewTools.SerializeToString(_selectedDisplayItem);
				Clipboard.SetData(DataFormats.Text, xml);
				Console.WriteLine("Copied: " + _selectedDisplayItem.Shape.GetType().ToString());
			}
			else
			{
				Console.WriteLine("Selected Display Item = null");
			}
		}

		#endregion

		public void ResizeBackground(int width, int height)
		{
			double aspect = (double)width / (double)_background.Width;
			//Console.WriteLine("Resizing background to: " + width + ", " + height);
			Bitmap newBackground = PreviewTools.ResizeBitmap(new Bitmap(_background), new Size(width, height));
			// Copy the file to the Vixen folder
			string imageFileName = Guid.NewGuid().ToString() + ".jpg";
			var destFileName = System.IO.Path.Combine(VixenPreviewDescriptor.ModulePath, imageFileName);
			newBackground.Save(destFileName, ImageFormat.Jpeg);
			Data.BackgroundFileName = destFileName;
			LoadBackground(destFileName);

			foreach (Shapes.DisplayItem item in DisplayItems)
			{
				item.Shape.Resize(aspect);
			}
		}

		/// <summary>
		/// This is used in edit mode only!!
		/// Need to make it go away so we only have one render engine
		/// </summary>
		public void RenderInForeground()
		{
			renderTimer.Reset();
			renderTimer.Start();

			if (_background != null)
			{
				//FastPixel fp = new FastPixel(_background.Width, _background.Height);
				FastPixel fp = new FastPixel(new Bitmap(_alphaBackground));
				Bitmap floodBG = null;
				if (UseFloods)
				{
					floodBG = PreviewTools.Copy32BPPBitmapSafe(_blankAlphaBackground);
					//floodBG = new Bitmap(_blankAlphaBackground);
					//floodBG = new Bitmap(_blankAlphaBackground.Width, _blankAlphaBackground.Height);
					//Graphics g = Graphics.FromImage(floodBG);
					//g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
					//SolidBrush brush = new SolidBrush(Color.FromArgb(50, 255, 0, 0));
					//g.FillEllipse(brush, new Rectangle(200, 200, 300, 300));
					//g.DrawImage(_blankAlphaBackground, 0, 0);
					//g.FillRectangle(_backgroundBrush, new Rectangle(0, 0, _blankAlphaBackground.Width, _blankAlphaBackground.Height));
				}
				fp.Lock();
				foreach (DisplayItem displayItem in DisplayItems)
				{
					//if (UseFloods && displayItem.Shape.StringType == PreviewBaseShape.StringTypes.Flood)
					//{
					//    displayItem.Shape.Draw(floodBG, _editMode, null);
					//}
					//else
					//{
					if (_editMode)
						displayItem.Draw(fp, true, HighlightedElements, SelectedDisplayItems.Contains(displayItem));
					else
						displayItem.Draw(fp, false, null, false);
					//}
				}
				fp.Unlock(true);

				// Finally, are we drawing a banded rectangle?
				if (_mouseCaptured && _selectedDisplayItem == null)
				{
					Graphics g = Graphics.FromImage(fp.Bitmap);
					g.DrawRectangle(Pens.White, _bandRect);
				}

				//lock (renderLock)
				//{
				// First, draw our background image opaque
				bufferedGraphics.Graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
				//bufferedGraphics.Graphics.DrawImage(_background, 0, 0, _background.Width, _background.Height);
				if (UseFloods)
				{
					bufferedGraphics.Graphics.DrawImage(_background, 0, 0, _background.Width, _background.Height);
				}
				else
				{
					//bufferedGraphics.Graphics.DrawImage(_alphaBackground, 0, 0, _alphaBackground.Width, _alphaBackground.Height);
				}

				//bufferedGraphics.Graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
				if (UseFloods)
				{
					bufferedGraphics.Graphics.DrawImage(floodBG, 0, 0, _blankAlphaBackground.Width, _blankAlphaBackground.Height);
				}
				// Now, draw our "pixel" image using alpha blending
				bufferedGraphics.Graphics.DrawImage(fp.Bitmap, 0, 0, fp.Width, fp.Height);
				//}

			}

			bufferedGraphics.Render(Graphics.FromHwnd(this.Handle));
			renderTimer.Stop();
			lastRenderUpdateTime = renderTimer.ElapsedMilliseconds;
		}

		public void ProcessUpdate(ElementIntentStates elementStates)
		{
			renderTimer.Reset();
			renderTimer.Start();
			if (!_paused)
			{
				//FastPixel fp = new FastPixel(_background.Width, _background.Height);
				FastPixel fp = new FastPixel(new Bitmap(_alphaBackground));
				Bitmap floodBG = null;
				if (UseFloods)
				{
					floodBG = PreviewTools.Copy32BPPBitmapSafe(_blankAlphaBackground);
					//floodBG = new Bitmap(_blankAlphaBackground);
					//floodBG = new Bitmap(_blankAlphaBackground.Width, _blankAlphaBackground.Height);
					//Graphics g = Graphics.FromImage(floodBG);
					//g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
					//SolidBrush brush = new SolidBrush(Color.FromArgb(50, 255, 0, 0));
					//g.FillEllipse(brush, new Rectangle(200, 200, 300, 300));
					//g.DrawImage(_blankAlphaBackground, 0, 0);
					//g.FillRectangle(_backgroundBrush, new Rectangle(0, 0, _blankAlphaBackground.Width, _blankAlphaBackground.Height));
				}
				fp.Lock();

				foreach (var channelIntentState in elementStates)
				{
					var elementId = channelIntentState.Key;
					Element element = VixenSystem.Elements.GetElement(elementId);
					if (element == null) continue;
					ElementNode node = VixenSystem.Elements.GetElementNodeForElement(element);
					if (node == null) continue;

					//foreach (IIntentState intentState in channelIntentState.Value)
					//{
					//    intentState.Dispatch(DisplayItems[0]);
					//}

					foreach (IIntentState<LightingValue> intentState in channelIntentState.Value)
					{

						if (_background != null)
						{
							List<PreviewPixel> pixels;
							if (NodeToPixel.TryGetValue(node, out pixels))
							{
								foreach (PreviewPixel pixel in pixels)
								{

									//Color.FromArgb((int)(Intensity * byte.MaxValue), Color.R, Color.G, Color.B);
									//LightingValue v = intentState.GetValue();
									//Color c = v.GetOpaqueIntensityAffectedColor();
									//Color c = v.GetAlphaChannelIntensityAffectedColor();
									Color c = intentState.GetValue().GetAlphaChannelIntensityAffectedColor();
									//Color c = Color.White;
									pixel.Draw(fp, c);
								}
							}
						}
					}
				}

				//if (UseFloods)
				//{
				//    foreach (DisplayItem displayItem in DisplayItems)
				//    {
				//        if (displayItem.Shape.StringType == PreviewBaseShape.StringTypes.Flood)
				//        {
				//            Color c = displayItem.Shape._pixels[0].PixelColor;
				//            int alpha = c.A;
				//            //int maxAlpha = 255 - BackgroundAlpha;
				//            //if (maxAlpha < 255)
				//            //{

				//            //}
				//            displayItem.Shape._pixels[0].PixelColor = Color.FromArgb(alpha, c.R, c.G, c.B);
				//            displayItem.Shape.Draw(floodBG, false, null);
				//            displayItem.Shape._pixels[0].PixelColor = Color.FromArgb(255 - BackgroundAlpha, 0, 0, 0);
				//        }
				//    }
				//}
				fp.Unlock(true);

				// First, draw our background image opaque
				bufferedGraphics.Graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
				//if (UseFloods)
				//{
				//    bufferedGraphics.Graphics.DrawImage(_background, 0, 0, _background.Width, _background.Height);
				//}
				//else
				//{
				//    bufferedGraphics.Graphics.DrawImage(_alphaBackground, 0, 0, _alphaBackground.Width, _alphaBackground.Height);
				//}

				// Now, draw our "pixel" image using alpha blending
				///bufferedGraphics.Graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
				if (UseFloods)
				{
					bufferedGraphics.Graphics.DrawImage(floodBG, 0, 0, _blankAlphaBackground.Width, _blankAlphaBackground.Height);
				}
				bufferedGraphics.Graphics.DrawImage(fp.Bitmap, 0, 0, fp.Width, fp.Height);

				if (!this.Disposing && bufferedGraphics != null)
					bufferedGraphics.Render(Graphics.FromHwnd(this.Handle));

				fp = null;
			}

			renderTimer.Stop();
			lastRenderUpdateTime = renderTimer.ElapsedMilliseconds;
		}
		public void ProcessUpdateParallel(ElementIntentStates elementStates)
		{
			renderTimer.Reset();
			renderTimer.Start();
			CancellationTokenSource tokenSource = new CancellationTokenSource();
			if (!_paused)
			{
				//FastPixel fp = new FastPixel(_background.Width, _background.Height);
				using (FastPixel fp = new FastPixel(new Bitmap(_alphaBackground)))
				{
					try
					{


						var floodBDTask = Task.Factory.StartNew<Bitmap>(() =>
						{

							Bitmap floodBG = null;
							if (UseFloods)
							{
								floodBG = PreviewTools.Copy32BPPBitmapSafe(_blankAlphaBackground);
								//floodBG = new Bitmap(_blankAlphaBackground);
								//floodBG = new Bitmap(_blankAlphaBackground.Width, _blankAlphaBackground.Height);
								//Graphics g = Graphics.FromImage(floodBG);
								//g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
								//SolidBrush brush = new SolidBrush(Color.FromArgb(50, 255, 0, 0));
								//g.FillEllipse(brush, new Rectangle(200, 200, 300, 300));
								//g.DrawImage(_blankAlphaBackground, 0, 0);
								//g.FillRectangle(_backgroundBrush, new Rectangle(0, 0, _blankAlphaBackground.Width, _blankAlphaBackground.Height));
							}
							return floodBG;
						}, tokenSource.Token);

						fp.Lock();
						elementStates.AsParallel().WithCancellation(tokenSource.Token).ForAll(channelIntentState =>
						{
							//foreach (var channelIntentState in elementStates)
							//{
							var elementId = channelIntentState.Key;
							Element element = VixenSystem.Elements.GetElement(elementId);
							if (element != null)
							{
								ElementNode node = VixenSystem.Elements.GetElementNodeForElement(element);
								if (node != null)
								{

									//foreach (IIntentState intentState in channelIntentState.Value)
									//{
									//    intentState.Dispatch(DisplayItems[0]);
									//}
									
									foreach (IIntentState<LightingValue> intentState in channelIntentState.Value)
									{
										if (_background != null)
										{

											List<PreviewPixel> pixels;
											if (NodeToPixel.TryGetValue(node, out pixels))
											{
												pixels.AsParallel().WithCancellation(tokenSource.Token).ForAll(pixel =>
												{

													//Color.FromArgb((int)(Intensity * byte.MaxValue), Color.R, Color.G, Color.B);
													//LightingValue v = intentState.GetValue();
													//Color c = v.GetOpaqueIntensityAffectedColor();
													//Color c = v.GetAlphaChannelIntensityAffectedColor();
													Color c = ((IIntentState<LightingValue>)intentState).GetValue().GetAlphaChannelIntensityAffectedColor();
													//Color c = Color.White;
													pixel.Draw(fp, c);
												});
											}

										}
									}


								}
							}
							//}
							
							
						});
						//if (UseFloods)
						//{
						//    foreach (DisplayItem displayItem in DisplayItems)
						//    {
						//        if (displayItem.Shape.StringType == PreviewBaseShape.StringTypes.Flood)
						//        {
						//            Color c = displayItem.Shape._pixels[0].PixelColor;
						//            int alpha = c.A;
						//            //int maxAlpha = 255 - BackgroundAlpha;
						//            //if (maxAlpha < 255)
						//            //{

						//            //}
						//            displayItem.Shape._pixels[0].PixelColor = Color.FromArgb(alpha, c.R, c.G, c.B);
						//            displayItem.Shape.Draw(floodBG, false, null);
						//            displayItem.Shape._pixels[0].PixelColor = Color.FromArgb(255 - BackgroundAlpha, 0, 0, 0);
						//        }
						//    }
						//}
						fp.Unlock(true);

						RenderBufferedGraphics(fp, floodBDTask.Result);
					}
					catch (Exception e)
					{
						tokenSource.Cancel();
						Console.WriteLine(e.Message);
					}

				}

			}

			renderTimer.Stop();
			lastRenderUpdateTime = renderTimer.ElapsedMilliseconds;

		}
		private object lockObject = new object();
		delegate void RenderBufferedGraphicsDelgate(FastPixel fp, Bitmap floodBG);
		private void RenderBufferedGraphics(FastPixel fp, Bitmap floodBG)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new RenderBufferedGraphicsDelgate(RenderBufferedGraphics), fp, floodBG);
			}
			else
				lock (lockObject)
				{
					// First, draw our background image opaque
					bufferedGraphics.Graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
					//if (UseFloods)
					//{
					//    bufferedGraphics.Graphics.DrawImage(_background, 0, 0, _background.Width, _background.Height);
					//}
					//else
					//{
					//    bufferedGraphics.Graphics.DrawImage(_alphaBackground, 0, 0, _alphaBackground.Width, _alphaBackground.Height);
					//}

					// Now, draw our "pixel" image using alpha blending
					///bufferedGraphics.Graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
					if (UseFloods)
					{
						bufferedGraphics.Graphics.DrawImage(floodBG, 0, 0, _blankAlphaBackground.Width, _blankAlphaBackground.Height);
					}
					bufferedGraphics.Graphics.DrawImage(fp.Bitmap, 0, 0, fp.Width, fp.Height);

					if (!this.Disposing && bufferedGraphics != null)
						bufferedGraphics.Render(Graphics.FromHwnd(this.Handle));
				}
		}


		//public void ResetNodeToPixelDictionary()
		//{
		//    Console.WriteLine("ResetNodeToPixelDictionary");
		//    if (NodeToPixel == null)
		//        NodeToPixel = new Dictionary<ElementNode, List<PreviewPixel>>();
		//    else
		//        NodeToPixel.Clear();

		//    foreach (DisplayItem item in DisplayItems)
		//    {

		//        foreach (PreviewPixel pixel in item.Shape._pixels)
		//        {
		//            if (pixel.Node != null)
		//            {
		//                List<PreviewPixel> pixels;
		//                if (NodeToPixel.TryGetValue(pixel.Node, out pixels))
		//                {
		//                    if (!pixels.Contains(pixel))
		//                    {
		//                        pixels.Add(pixel);
		//                    }
		//                }
		//                else
		//                {
		//                    pixels = new List<PreviewPixel>();
		//                    pixels.Add(pixel);
		//                    NodeToPixel.Add(pixel.Node, pixels);
		//                }
		//            }
		//        }
		//    }
		//}

		#region Templates

		public List<PreviewBaseShape> SelectedShapes()
		{
			List<PreviewBaseShape> shapes = new List<PreviewBaseShape>();
			foreach (DisplayItem item in SelectedDisplayItems)
			{
				shapes.Add(item.Shape);
			}
			return shapes;
		}

		public void GroupItems()
		{
			DisplayItem newDisplayItem;
			newDisplayItem = new DisplayItem();
			newDisplayItem.Shape = new PreviewCustom(new PreviewPoint(100, 100), SelectedShapes());
			AddDisplayItem(newDisplayItem);
			Console.Write("Grouped");
			foreach (DisplayItem item in SelectedDisplayItems)
			{
				DisplayItems.Remove(item);
			}

			_selectedDisplayItem = newDisplayItem;
			if (_selectedDisplayItem != null)
			{
				PreviewCustomCreateForm f = new PreviewCustomCreateForm();
				if (f.ShowDialog() == DialogResult.OK)
				{
					_selectedDisplayItem.Shape.Name = f.TemplateName;
					_selectedDisplayItem.Shape.Select(true);

					string xml = PreviewTools.SerializeToString(_selectedDisplayItem);
					string destFileName = PreviewTools.TemplateWithFolder(f.TemplateName + ".xml");
					System.IO.File.WriteAllText(destFileName, xml);

					OnSelectDisplayItem(this, _selectedDisplayItem);
				}
			}
		}

		public void AddTtemplateToPreview(string fileName)
		{
			if (System.IO.File.Exists(fileName))
			{
				// Read the entire template file (stoopid waste of resources, but how else?)
				string xml = System.IO.File.ReadAllText(fileName);
				DisplayItem newDisplayItem = PreviewTools.DeSerializeToObject(xml, typeof(DisplayItem));
				if (newDisplayItem != null)
				{
					DeSelectSelectedDisplayItem();
					AddDisplayItem(newDisplayItem);
					_selectedDisplayItem = newDisplayItem;
					OnSelectDisplayItem(this, _selectedDisplayItem);
					_selectedDisplayItem.Shape.MoveTo(10, 10);
					_selectedDisplayItem.Shape.Select(true);
					_selectedDisplayItem.Shape.SetSelectPoint(null);
				}
			}
		}

		#endregion
	}
}
