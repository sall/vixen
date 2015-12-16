﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using Common.Controls;
using Common.Controls.Theme;
using Common.Controls.ControlsEx.ListControls;
using Common.Controls.Timeline;
using Vixen.Data.Value;
using Vixen.Execution.Context;
using Vixen.Sys;
using VixenModules.Editor.VixenPreviewSetup3.Undo;
using VixenModules.Preview.VixenPreview.Shapes;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;
using VixenModules.Property.Location;
using DisplayItem = VixenModules.Preview.VixenPreview.Shapes.DisplayItem;
using Element = Vixen.Sys.Element;

namespace VixenModules.Preview.VixenPreview
{
	public partial class VixenPreviewControl : UserControl
	{
		private static NLog.Logger Logging = NLog.LogManager.GetCurrentClassLogger();
		
		#region "Variables"

		public VixenPreviewSetupElementsDocument elementsForm;
		public VixenPreviewSetupPropertiesDocument propertiesForm;
		private bool _paused = false;
		private BufferedGraphicsContext context;
		private BufferedGraphics bufferedGraphics;
		public static double averageUpdateTime = 0;
		public static double updateCount = 0;
		public static double totalUpdateTime = 0;
		public static double lastUpdateTime = 0;
		public double lastRenderUpdateTime = 0;
		private bool DefaultBackground = true;
		private Point zoomTo;
		private bool _displayItemsLoaded = false;
		public static string modifyType;
		private static bool ClipboardPopulated = false;

		private Tools _currentTool = Tools.Select;


		public DisplayMoveType Type { get; private set; }

		public enum DisplayMoveType
		{
			Move,
			Resize,
			Align,
			Distribute
		}

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
			Cane,
			PixelGrid,
			StarBurst,
			Icicle,
			PolyLine,
			MultiString
		}

		private Point dragStart;
		private Point dragCurrent;
		private int changeX;
		private int changeY;
		public static DisplayItem _selectedDisplayItem = null;
		private bool _editMode = false;

		private Bitmap _background;
		private Bitmap _alphaBackground;

		private VixenPreviewData _data;

		private List<ElementNode> _highlightedElements = new List<ElementNode>();
		public List<DisplayItem> _selectedDisplayItems;

		private Rectangle _bandRect = new Rectangle();

		// For debug, status line, etc.
		private Random random = new Random();
		private Stopwatch renderTimer = new Stopwatch();

		#endregion

		#region "Events"

		public delegate void ElementsChangedEventHandler(object sender, EventArgs e);

		public event ElementsChangedEventHandler OnElementsChanged;

		public delegate void SelectDisplayItemEventHandler(object sender, DisplayItem displayItem);

		public event SelectDisplayItemEventHandler OnSelectDisplayItem;

		public delegate void DeSelectDisplayItemEventHandler(object sender, DisplayItem displayItem);

		public event DeSelectDisplayItemEventHandler OnDeSelectDisplayItem;

		public delegate void ChangeZoomLevelEventHandler(object sender, double zoomLevel);

		public event ChangeZoomLevelEventHandler OnChangeZoomLevel;

		public ConcurrentDictionary<ElementNode, List<PreviewPixel>> NodeToPixel =
			new ConcurrentDictionary<ElementNode, List<PreviewPixel>>();

		public ISequenceContext vixenContext = null;

		public static event EventHandler<PreviewItemMoveEventArgs> PreviewItemsMovedNew;

		public static event EventHandler<PreviewItemResizingEventArgs> PreviewItemsResizingNew;

		#endregion

		public void vixenpreviewControl_PreviewItemsResizingNew(object sender, PreviewItemResizingEventArgs e)
		{
			var action = new PreviewItemsResizeUndoAction(this, e.PreviousSize);
			UndoManager.AddUndoAction(action);
		}

		public void vixenpreviewControl_PreviewItemsMovedNew(object sender, PreviewItemMoveEventArgs e)
		{
			var action = new PreviewItemsMoveUndoAction(this, e.PreviousMove);
			UndoManager.AddUndoAction(action);
		}

		internal UndoManager UndoManager { get; set; }

		public bool ShowInfo { get; set; }

		private double _zoomLevel = 1;

		public double ZoomLevel
		{
			get { return _zoomLevel; }
			set
			{
				double ZoomMax = 4;
				if (IntPtr.Size != 8)
				{
					ZoomMax = 2;
				}
				const double ZoomMin = .25;

				if (value >= ZoomMin && value <= ZoomMax)
					_zoomLevel = value;
				else if (value < ZoomMin)
					_zoomLevel = ZoomMin;
				else if (value > ZoomMax)
					_zoomLevel = ZoomMax;

				if (DisplayItems != null)
				{
					foreach (DisplayItem item in DisplayItems)
					{
						item.ZoomLevel = _zoomLevel;
					}
				}
				SetupBackgroundAlphaImage();
				if (OnChangeZoomLevel != null) OnChangeZoomLevel(this, _zoomLevel);

				SetBackgroundPosition(zoomTo, MousePosition);
			}
		}

		public void SetBackgroundPosition(Point zoomToPoint, Point mousePosition)
		{
			// Set the new background position based on the mouse position
			Point backgroundPoint = ZoomPointToBackgroundPoint(zoomToPoint);
			Point mp = PointToClient(mousePosition);
			int newHValue = backgroundPoint.X - mp.X;
			;
			if (newHValue > 0 && newHValue <= hScroll.Maximum)
			{
				hScroll.Value = newHValue;
			}
			int newYValue = backgroundPoint.Y - mp.Y;
			;
			if (newYValue > 0 && newYValue <= vScroll.Maximum)
			{
				vScroll.Value = newYValue;
			}
		}

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
			set { _selectedDisplayItems = value; }
		}

		public int BackgroundAlpha
		{
			get
			{
				if (Data != null)
				{
					return Data.BackgroundAlpha;
				}
				else
				{
					return 0;
				}
			}
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
			set { _data = value; }
		}

		public List<DisplayItem> DisplayItems
		{
			get
			{
				if (!_displayItemsLoaded && Data != null && Data.DisplayItems != null)
				{
					_displayItemsLoaded = true;
					ZoomLevel = 1;
				}
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
			contextMenuStrip1.Renderer = new ThemeToolStripRenderer();
			PreviewItemsResizingNew += vixenpreviewControl_PreviewItemsResizingNew;
			PreviewItemsMovedNew += vixenpreviewControl_PreviewItemsMovedNew;
			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.DoubleBuffer, true);
			_selectedDisplayItem = null;
		}

		private VScrollBar vScroll = new VScrollBar();
		private HScrollBar hScroll = new HScrollBar();

		private void VixenPreviewControl_Load(object sender, EventArgs e)
		{
			MouseWheel += VixenPreviewControl_MouseWheel;
			Controls.Add(vScroll);
			Controls.Add(hScroll);
			ZoomLevel = 1;
			LayoutProps();
		}

		public void LayoutProps()
		{
			if (DisplayItems != null)
			{
				foreach (DisplayItem item in DisplayItems)
				{
					item.Shape.Layout();
				}
			}
		}

		public Bitmap Background
		{
			get { return _background; }
			set
			{
				_background = value;
				if (_background == null)
				{
					DefaultBackground = true;
					_background = new Bitmap(Width, Height, PixelFormat.Format32bppPArgb);

					Graphics gfx = Graphics.FromImage(_background);
					gfx.Clear(Color.Black);
					gfx.Dispose();
				}
				else
				{
					DefaultBackground = false;
					_background = value;
				}
				SetupBackgroundAlphaImage();
			}
		}

		public void LoadBackground(string file)
		{

			var fileName = Path.Combine(VixenPreviewDescriptor.ModulePath, file);
			if (File.Exists(fileName))
			{
				try
				{
					using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
					{
						using (var loadedBitmap = new Bitmap(fs))
						{
							Background = loadedBitmap.Clone(new Rectangle(0, 0, loadedBitmap.Width, loadedBitmap.Height),
								PixelFormat.Format32bppPArgb);
						}
						fs.Close();
						DefaultBackground = false;
					}
				}
				catch (Exception ex)
				{
					Background = null;
					Logging.Error("There was error loading the preview background image.", ex);
					//messageBox Arguments are (Text, Title, No Button Visible, Cancel Button Visible)
					MessageBoxForm.msgIcon = SystemIcons.Error; //this is used if you want to add a system icon to the message form.
					var messageBox = new MessageBoxForm("There was an error loading the background image: " + ex.Message, @"Error", false, true);
					messageBox.ShowDialog();
				}
			}
			else
			{
				Background = null;
			}

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
				int newWidth = Convert.ToInt32(_background.Width*ZoomLevel);
				int newHeight = Convert.ToInt32(_background.Height*ZoomLevel);

				_alphaBackground = new Bitmap(newWidth, newHeight, PixelFormat.Format32bppPArgb);

				using (Graphics gfx = Graphics.FromImage(_alphaBackground))
				{
					Color c = Color.FromArgb(BackgroundAlpha, 0, 0, 0);
					if (!DefaultBackground)
					{
						c = Color.FromArgb(255 - BackgroundAlpha, 0, 0, 0);
					}
					using (SolidBrush brush = new SolidBrush(c))
					{
						gfx.DrawImage(_background, 0, 0, newWidth, newHeight);
						gfx.FillRectangle(brush, 0, 0, _alphaBackground.Width, _alphaBackground.Height);
					}
				}

				SetupScrollBars();

				if (bufferedGraphics != null) bufferedGraphics.Graphics.Clear(Color.Black);
			}
		}

		private int lastWidth = 0, lastHeight = 0;

		private void AllocateGraphicsBuffer(bool forceAllocation)
		{
			if (!Disposing)
			{
				context = BufferedGraphicsManager.Current;
				if (context != null)
				{
					if (this.Width > 0 && this.Height > 0 && (this.Height != lastHeight || this.Width != lastWidth || forceAllocation))
					{
						lastHeight = this.Height;
						lastWidth = this.Width;

						context.MaximumBuffer = new Size(this.Width + 1, this.Height + 1);

						if (bufferedGraphics != null)
						{
							bufferedGraphics.Dispose();
							bufferedGraphics = null;
							bufferedGraphics = context.Allocate(this.CreateGraphics(),
								new Rectangle(0, 0, this.Width + 1, this.Height + 1));
						}
						else
						{
							bufferedGraphics = context.Allocate(this.CreateGraphics(),
								new Rectangle(0, 0, this.Width + 1, this.Height + 1));
						}
					}
				}
			}
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

		public void SelectItemUnderPoint(PreviewPoint point, bool addToSelection)
		{
			if (!_mouseCaptured)
			{
				// First, see if we have an item already selected, but want to add to it
				if (addToSelection)
				{
					if (_selectedDisplayItem != null)
						SelectedDisplayItems.Add(_selectedDisplayItem);
					DeSelectSelectedDisplayItem();
					DisplayItem item = DisplayItemAtPoint(point);
					if (item != null)
						SelectedDisplayItems.Add(item);
				}
				else
				{
					// First, deselect any currently selected item
					DeSelectSelectedDisplayItem();

					_selectedDisplayItem = DisplayItemAtPoint(point);
					if (_selectedDisplayItem != null)
					{
						_selectedDisplayItem.Shape.Select(true);
						OnSelectDisplayItem(this, _selectedDisplayItem);
					}
				}
			}
		}

		public void selectItem(PreviewPoint point)
		{
			_selectedDisplayItem = DisplayItemAtPoint(point);
			if (_selectedDisplayItem != null)
			{
				_selectedDisplayItem.Shape.Select(true);
				OnSelectDisplayItem(this, _selectedDisplayItem);
			}
		}

		private bool _mouseCaptured = false;
		private bool _banding = false;

		private void VixenPreviewControl_MouseDown(object sender, MouseEventArgs e)
		{
			if (_editMode)
			{
				bool controlPressed = (Control.ModifierKeys == Keys.Control);
				PreviewPoint translatedPoint = new PreviewPoint(e.X + hScroll.Value, e.Y + vScroll.Value);
				if (e.Button == System.Windows.Forms.MouseButtons.Left)
				{
					if (_currentTool == Tools.Select)
					{
						if (controlPressed)
						{
							DisplayItem item = DisplayItemAtPoint(translatedPoint);
							if (item != null && SelectedDisplayItems.Contains(item))
							{
								SelectedDisplayItems.Remove(item);
							}
							else
							{
								SelectItemUnderPoint(translatedPoint, controlPressed);
							}
							return;
						}

						// Is there a single dislay item selected?
						if (_selectedDisplayItem != null && !controlPressed)
						{
							// Lets see if we've got a drag point.
							PreviewPoint selectedPoint = _selectedDisplayItem.Shape.PointInSelectPoint(translatedPoint);
							if (selectedPoint != null)
							{
								modifyType = "Resize";
								beginResize_Move(false); //Starts the Undo Process
								dragStart = translatedPoint.ToPoint();
								_selectedDisplayItem.Shape.SetSelectPoint(selectedPoint);
								Capture = true;
								_mouseCaptured = true;
							}
								// If we're not resizing, see if we're moving a single shape
							else if (_selectedDisplayItem.Shape.PointInShape(translatedPoint))
							{
								modifyType = "Move";
								beginResize_Move(false); //Starts the Undo Process

								StartMove(translatedPoint.X, translatedPoint.Y);
							}
								// If we get here, we're outside the shape, deselect
							else
							{
								DeSelectSelectedDisplayItem();
							}
						}
							// Are there multiple items selected?
							// If so, we're moving, can't resize them...
						else if (SelectedDisplayItems.Count > 1 && !controlPressed)
						{
							//if (MouseOverSelectedDisplayItems(e.X, e.Y)) {
							//    StartMove(e.X, e.Y);
							//}
							//else {
							//    SelectedDisplayItems.Clear();
							//}
							if (MouseOverSelectedDisplayItems(translatedPoint.X, translatedPoint.Y))
							{
								modifyType = "Move";
								beginResize_Move(true); //Starts the Undo Process
								StartMove(translatedPoint.X, translatedPoint.Y);
							}
							else
							{
								SelectedDisplayItems.Clear();
							}
						}

						SelectItemUnderPoint(translatedPoint, controlPressed);

						// If we get this far, and we've got nothing selected, we're drawing a rubber band!
						if (_selectedDisplayItem == null && SelectedDisplayItems.Count == 0)
						{
							// Capture the mouse in case we want to draw a rubber band
							dragStart = translatedPoint.ToPoint();
							Capture = true;
							_mouseCaptured = true;
							SelectedDisplayItems.Clear();
							_bandRect.Width = 0;
							_bandRect.Height = 0;
							_banding = true;
						}
					}

					else if (_selectedDisplayItem != null && _selectedDisplayItem.Shape.PointInShape(translatedPoint) &&
					         !_selectedDisplayItem.Shape.Creating)
					{
						StartMove(translatedPoint.X, translatedPoint.Y);
					}

						// If we're not Selecting items, we're drawing them
					else if (_currentTool == Tools.PolyLine && _mouseCaptured)
					{
						return;
					}
					else if (_currentTool == Tools.MultiString && _mouseCaptured)
					{
						return;
					}
					else
					{
						DeSelectSelectedDisplayItem();

						DisplayItem newDisplayItem = null;
						if (_currentTool == Tools.String)
						{
							newDisplayItem = new DisplayItem();
							newDisplayItem.Shape = new PreviewLine(translatedPoint, translatedPoint, 50,
								elementsForm.SelectedNode, ZoomLevel);
						}
						else if (_currentTool == Tools.Arch)
						{
							newDisplayItem = new DisplayItem();
							newDisplayItem.Shape = new PreviewArch(translatedPoint, elementsForm.SelectedNode, ZoomLevel);
						}
						else if (_currentTool == Tools.Rectangle)
						{
							newDisplayItem = new DisplayItem();
							newDisplayItem.Shape = new PreviewRectangle(translatedPoint, elementsForm.SelectedNode, ZoomLevel);
						}
						else if (_currentTool == Tools.Single)
						{
							newDisplayItem = new DisplayItem
							{
								Shape = new PreviewSingle(translatedPoint, elementsForm.SelectedNode, ZoomLevel)
							};
						}
						else if (_currentTool == Tools.Ellipse)
						{
							newDisplayItem = new DisplayItem();
							newDisplayItem.Shape = new PreviewEllipse(translatedPoint, 50, elementsForm.SelectedNode, ZoomLevel);
						}
						else if (_currentTool == Tools.Triangle)
						{
							newDisplayItem = new DisplayItem();
							newDisplayItem.Shape = new PreviewTriangle(translatedPoint, elementsForm.SelectedNode, ZoomLevel);
						}
						else if (_currentTool == Tools.Net)
						{
							newDisplayItem = new DisplayItem();
							newDisplayItem.Shape = new PreviewNet(translatedPoint, elementsForm.SelectedNode, ZoomLevel);
						}
						else if (_currentTool == Tools.Cane)
						{
							newDisplayItem = new DisplayItem();
							newDisplayItem.Shape = new PreviewCane(translatedPoint, elementsForm.SelectedNode, ZoomLevel);
						}
						else if (_currentTool == Tools.Star)
						{
							newDisplayItem = new DisplayItem();
							newDisplayItem.Shape = new PreviewStar(translatedPoint, elementsForm.SelectedNode, ZoomLevel);
						}
						else if (_currentTool == Tools.StarBurst)
						{
							newDisplayItem = new DisplayItem();
							newDisplayItem.Shape = new PreviewStarBurst(translatedPoint, elementsForm.SelectedNode, ZoomLevel);
						}
						else if (_currentTool == Tools.Flood)
						{
							newDisplayItem = new DisplayItem();
							newDisplayItem.Shape = new PreviewFlood(translatedPoint, elementsForm.SelectedNode);
						}
						else if (_currentTool == Tools.MegaTree)
						{
							newDisplayItem = new DisplayItem();
							newDisplayItem.Shape = new PreviewMegaTree(translatedPoint, elementsForm.SelectedNode, ZoomLevel);
						}
						else if (_currentTool == Tools.PixelGrid)
						{
							newDisplayItem = new DisplayItem();
							newDisplayItem.Shape = new PreviewPixelGrid(translatedPoint, elementsForm.SelectedNode, ZoomLevel);
						}
						else if (_currentTool == Tools.Icicle)
						{
							newDisplayItem = new DisplayItem();
							newDisplayItem.Shape = new PreviewIcicle(translatedPoint, translatedPoint,
								elementsForm.SelectedNode, ZoomLevel);
						}
						else if (_currentTool == Tools.PolyLine)
						{
							newDisplayItem = new DisplayItem();
							newDisplayItem.Shape = new PreviewPolyLine(translatedPoint, translatedPoint,
								elementsForm.SelectedNode, ZoomLevel);
						}
						else if (_currentTool == Tools.MultiString)
						{
							newDisplayItem = new DisplayItem();
							newDisplayItem.Shape = new PreviewMultiString(translatedPoint, translatedPoint,
								elementsForm.SelectedNode, ZoomLevel);
						}
						// Now add the newely created display item to the screen.
						if (newDisplayItem != null)
						{
							AddDisplayItem(newDisplayItem);
							newDisplayItem.ZoomLevel = ZoomLevel;
							_selectedDisplayItem = newDisplayItem;
							_selectedDisplayItem.Shape.PixelSize = 3;
							_selectedDisplayItem.Shape.Select(true);
							_selectedDisplayItem.Shape.SelectDefaultSelectPoint();
							dragStart = translatedPoint.ToPoint();
							Capture = true;
							_mouseCaptured = true;

							modifyType = "AddNew";
						}
					}
				}
				else if (e.Button == System.Windows.Forms.MouseButtons.Right)
				{
					contextMenuStrip1.Items.Clear();
					SelectItemUnderPoint(translatedPoint, false);

					if (_selectedDisplayItem != null)
					{
						PreviewPoint selectedPoint = _selectedDisplayItem.Shape.PointInSelectPoint(translatedPoint);
						if (_selectedDisplayItem.Shape.PointInShape(translatedPoint))
						{
							if (_selectedDisplayItem.Shape.GetType().ToString().Contains("PreviewCustom"))
							{
								contextMenuStrip1.Items.Add(new ToolStripMenuItem
								{
									Text = "Separate Template Items",
									Tag = "Separate",
									Image = Common.Resources.Properties.Resources.Split
								});
							}
						}
					}
					else if (SelectedDisplayItems.Count > 1)
					{
						contextMenuStrip1.Items.Add(new ToolStripMenuItem
						{
							Text = "Create Group...",
							Tag = "CreateGroup",
							Image = Common.Resources.Properties.Resources.group
						});
						contextMenuStrip1.Items.Add(new ToolStripSeparator());
						contextMenuStrip1.Items.Add(new ToolStripMenuItem
						{
							Text = "Create Template...",
							Tag = "CreateTemplate",
							Image = Common.Resources.Properties.Resources.document_font
						});
					}
					if (_selectedDisplayItem != null)
					{
						contextMenuStrip1.Items.Add(new ToolStripMenuItem
						{
							Text = "Cut",
							Tag = "Cut",
							Image = Common.Resources.Properties.Resources.cut
						});
						contextMenuStrip1.Items.Add(new ToolStripMenuItem
						{
							Text = "Copy",
							Tag = "Copy",
							Image = Common.Resources.Properties.Resources.page_copy
						});
					}
					if (ClipboardPopulated)
					{
						if (_selectedDisplayItem == null & SelectedDisplayItems.Count > 1)
						{
							contextMenuStrip1.Items.Add(new ToolStripSeparator());
						}
						contextMenuStrip1.Items.Add(new ToolStripMenuItem
						{
							Text = "Paste",
							Tag = "Paste",
							Image = Common.Resources.Properties.Resources.paste_plain
						});
					}
					if (_selectedDisplayItem != null)
					{
						contextMenuStrip1.Items.Add(new ToolStripMenuItem
						{
							Text = "Delete",
							Tag = "Delete",
							Image = Common.Resources.Properties.Resources.delete
						});
					}
					if (UndoManager.NumUndoable > 0)
					{
						contextMenuStrip1.Items.Add(new ToolStripSeparator());
						contextMenuStrip1.Items.Add(new ToolStripMenuItem
						{
							Text = "Undo",
							Tag = "Undo",
							Image = Common.Resources.Properties.Resources.arrow_undo
						});
					}
					if (UndoManager.NumRedoable > 0)
					{
						if (UndoManager.NumUndoable < 1)
							contextMenuStrip1.Items.Add(new ToolStripSeparator());
						contextMenuStrip1.Items.Add(new ToolStripMenuItem
						{
							Text = "Redo",
							Tag = "Redo",
							Image = Common.Resources.Properties.Resources.arrow_redo
						});
					}
					if (_selectedDisplayItem != null)
					{
						contextMenuStrip1.Items.Add(new ToolStripSeparator());

						if (Data.SaveLocations & _selectedDisplayItem != null)
						{
							// Z location menu
							contextMenuStrip1.Items.Add(new ToolStripMenuItem {Text = "Set Z Location to"});
							// Z sub menu items
							(contextMenuStrip1.Items[contextMenuStrip1.Items.Count - 1] as ToolStripMenuItem).DropDownItems.Add(
								new ToolStripMenuItem {Text = "0 Front", Tag = "0"});
							(contextMenuStrip1.Items[contextMenuStrip1.Items.Count - 1] as ToolStripMenuItem).DropDownItems.Add(
								new ToolStripMenuItem {Text = "1", Tag = "1"});
							(contextMenuStrip1.Items[contextMenuStrip1.Items.Count - 1] as ToolStripMenuItem).DropDownItems.Add(
								new ToolStripMenuItem {Text = "2", Tag = "2"});
							(contextMenuStrip1.Items[contextMenuStrip1.Items.Count - 1] as ToolStripMenuItem).DropDownItems.Add(
								new ToolStripMenuItem {Text = "3", Tag = "3"});
							(contextMenuStrip1.Items[contextMenuStrip1.Items.Count - 1] as ToolStripMenuItem).DropDownItems.Add(
								new ToolStripMenuItem {Text = "4 Middle", Tag = "4"});
							(contextMenuStrip1.Items[contextMenuStrip1.Items.Count - 1] as ToolStripMenuItem).DropDownItems.Add(
								new ToolStripMenuItem {Text = "5", Tag = "5"});
							(contextMenuStrip1.Items[contextMenuStrip1.Items.Count - 1] as ToolStripMenuItem).DropDownItems.Add(
								new ToolStripMenuItem {Text = "6", Tag = "6"});
							(contextMenuStrip1.Items[contextMenuStrip1.Items.Count - 1] as ToolStripMenuItem).DropDownItems.Add(
								new ToolStripMenuItem {Text = "7", Tag = "7"});
							(contextMenuStrip1.Items[contextMenuStrip1.Items.Count - 1] as ToolStripMenuItem).DropDownItems.Add(
								new ToolStripMenuItem {Text = "8", Tag = "8"});
							(contextMenuStrip1.Items[contextMenuStrip1.Items.Count - 1] as ToolStripMenuItem).DropDownItems.Add(
								new ToolStripMenuItem {Text = "9 Back", Tag = "9"});
						}
					}
					if (contextMenuStrip1.Items.Count > 0)
					{
						contextMenuStrip1.Show(this, e.Location);
					}
				}
				else if (e.Button == System.Windows.Forms.MouseButtons.Middle)
				{
					// Pan
					zoomTo = MousePointToZoomPoint(e.Location);

				}
			}
		}

		private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			var tag = e.ClickedItem.Tag;
			if (e.ClickedItem.Tag == null)
				return;
			switch (tag.ToString())
			{
				case "CreateTemplate":
					_selectedDisplayItem = CreateTemplate();
					if (_selectedDisplayItem != null)
					{
						_selectedDisplayItem.Shape.Select(true);
					}
					break;
				case "CreateGroup":
					_selectedDisplayItem = CreateGroup();
					if (_selectedDisplayItem != null)
					{
						_selectedDisplayItem.Shape.Select(true);
					}
					break;
				case "Separate":
					if (_selectedDisplayItem != null)
						SeparateTemplateItems(_selectedDisplayItem);
					break;
				case "Cut":
					Cut();
					break;
				case "Copy":
					Copy();
					break;
				case "Paste":
					Paste();
					break;
				case "Delete":
					Delete();
					break;
				case "Undo":
					UndoManager.Undo();
					break;
				case "Redo":

					UndoManager.Redo();
					break;
				case "0":
				case "1":
				case "2":
				case "3":
				case "4":
				case "5":
				case "6":
				case "7":
				case "8":
				case "9":
					SetZForSelectedItems(Convert.ToInt32(tag));
					break;
			}
		}

		private void SetZForSelectedItems(int pos)
		{
			if (SelectedDisplayItems.Count > 0)
			{
				foreach (DisplayItem displayItem in SelectedDisplayItems)
				{
					SetDisplayItemZ(displayItem, pos);
				}
				SaveLocations(SelectedDisplayItems);
			}
			else if (_selectedDisplayItem != null)
			{
				SetDisplayItemZ(_selectedDisplayItem, pos);
				SelectedDisplayItems.Add(_selectedDisplayItem);
				SaveLocations(SelectedDisplayItems);
				SelectedDisplayItems.Clear();
			}
		}

		private void SetDisplayItemZ(DisplayItem displayItem, int pos)
		{
			foreach (PreviewPixel p in displayItem.Shape.Pixels)
			{
				p.Z = pos;
			}
		}

		private void StartMove(int x, int y)
		{
			dragStart.X = x;
			dragStart.Y = y;
			if (SelectedDisplayItems.Count() > 0)
			{
				foreach (DisplayItem item in SelectedDisplayItems)
				{
					item.Shape.SetSelectPoint(null);
				}
			}
			else if (_selectedDisplayItem != null)
			{
				_selectedDisplayItem.Shape.SetSelectPoint(null);
			}
			Capture = true;
			_mouseCaptured = true;
		}

		private void VixenPreviewControl_MouseMove(object sender, MouseEventArgs e)
		{
			if (_editMode)
			{
				PreviewPoint translatedPoint = new PreviewPoint(e.X + hScroll.Value, e.Y + vScroll.Value);
				Point zoomPoint = PointToZoomPoint(translatedPoint.ToPoint());
				if (e.Button == System.Windows.Forms.MouseButtons.Middle)
				{
					// Woo hoo... we're panning with the middle mouse button
					// Set the new background position based on the mouse position
					SetBackgroundPosition(zoomTo, MousePosition);
				}
				else
				{
					dragCurrent.X = translatedPoint.X;
					dragCurrent.Y = translatedPoint.Y;
					changeX = translatedPoint.X - dragStart.X;
					changeY = translatedPoint.Y - dragStart.Y;

					// Are we moving a single display item?
					if (_mouseCaptured && _selectedDisplayItem != null)
					{
						_selectedDisplayItem.Shape.MouseMove(dragCurrent.X, dragCurrent.Y, changeX, changeY);
					}
						// If we get here, we're drwing a rubber band
					else if (_banding)
					{
						int X1 = Math.Min(dragStart.X, dragStart.X + changeX);
						int Y1 = Math.Min(dragStart.Y, dragStart.Y + changeY);

						_bandRect.Location = new Point(X1, Y1);
						_bandRect.Width = Math.Abs(changeX);
						_bandRect.Height = Math.Abs(changeY);

						foreach (DisplayItem item in DisplayItems)
						{
							if (
								(changeX < 0 && item.Shape.ShapeInRect(_bandRect)) ||
								(changeX > 0 && item.Shape.ShapeAllInRect(_bandRect))
								)
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
					else if (_mouseCaptured && _selectedDisplayItem == null && SelectedDisplayItems.Count() > 0)
					{
						foreach (DisplayItem item in SelectedDisplayItems)
						{
							item.Shape.MouseMove(zoomPoint.X, zoomPoint.Y, changeX, changeY);
						}
					}

					if (_selectedDisplayItem != null)
					{
						PreviewPoint selectPoint = _selectedDisplayItem.Shape.PointInSelectPoint(translatedPoint);
						if (selectPoint != null)
						{
							Cursor.Current = Cursors.Cross;
						}
						else if (_selectedDisplayItem.Shape.PointInShape(translatedPoint))
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
						if (MouseOverSelectedDisplayItems(translatedPoint.X, translatedPoint.Y))
						{
							Cursor.Current = Cursors.SizeAll;
						}
					}
				}
			}
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				Delete();
				e.Handled = true;
			}
				// Copy
			else if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control)
			{
				Copy();
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.X && e.Modifiers == Keys.Control)
			{
				Cut();
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
			{
				Paste();
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.Z && e.Modifiers == Keys.Control)
			{
				if (UndoManager.NumUndoable > 0)
				{
					UndoManager.Undo();
				}
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.Y && e.Modifiers == Keys.Control)
			{
				if (UndoManager.NumRedoable > 0)
				{
					UndoManager.Redo();
				}
				e.Handled = true;
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
				e.Handled = true;
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
				e.Handled = true;
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
				e.Handled = true;
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
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.Escape)
			{
				if (_selectedDisplayItem != null && _selectedDisplayItem.Shape != null)
				{
					if (_selectedDisplayItem.Shape is PreviewPolyLine && _selectedDisplayItem.Shape.Creating)
					{
						PreviewItemAddAction();
						(_selectedDisplayItem.Shape as PreviewPolyLine).EndCreation();
						OnSelectDisplayItem(this, _selectedDisplayItem);
						DeSelectSelectedDisplayItem();
						ResetMouse();
					}
					else if (_selectedDisplayItem.Shape is PreviewMultiString && _selectedDisplayItem.Shape.Creating)
					{
						PreviewItemAddAction();
						(_selectedDisplayItem.Shape as PreviewMultiString).EndCreation();
						OnSelectDisplayItem(this, _selectedDisplayItem);
						DeSelectSelectedDisplayItem();
						ResetMouse();
					}
					else
					{
						CurrentTool = Tools.Select;
					}
				}
				else
				{
					CurrentTool = Tools.Select;
				}
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.Oemplus && Control.ModifierKeys == Keys.Control)
			{
				if (ZoomLevel < 4)
				{
					ZoomLevel += .25;
				}
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.OemMinus && Control.ModifierKeys == Keys.Control)
			{
				if (ZoomLevel > .25)
				{
					ZoomLevel -= .25;
				}
				e.Handled = true;
			}

			base.OnKeyDown(e);
		}

		public void VixenPreviewControl_MouseUp(object sender, MouseEventArgs e)
		{
			if (_mouseCaptured)
			{
				if (_currentTool != Tools.Select)
				{
					// If control is pressed, deselect the shape and immediately allow drawing another shape
					//if ((Control.ModifierKeys & Keys.Shift) != 0) {
					//    _selectedDisplayItem.Shape.MouseUp(sender, e);
					//    DeSelectSelectedDisplayItem();
					//}
					//else 
					if (_selectedDisplayItem != null &&
					    _currentTool == Tools.PolyLine &&
					    e.Button == System.Windows.Forms.MouseButtons.Left &&
					    _selectedDisplayItem.Shape.Creating)
					{
						// If we are drawing a PolyLine, we want all the mouse events to be passed to the shape
						_selectedDisplayItem.Shape.MouseUp(sender, e);
						return;
					}
					else if (_selectedDisplayItem != null &&
					         _currentTool == Tools.MultiString &&
					         e.Button == System.Windows.Forms.MouseButtons.Left &&
					         _selectedDisplayItem.Shape.Creating)
					{
						// If we are drawing a MultiString, we want all the mouse events to be passed to the shape
						_selectedDisplayItem.Shape.MouseUp(sender, e);
						return;
					}
					else
					{
						//_currentTool = Tools.Select;
					}
				}

				if (_selectedDisplayItem != null)
				{
					_selectedDisplayItem.Shape.MouseUp(sender, e);
					OnSelectDisplayItem(this, _selectedDisplayItem);
					if ((PreviewItemsMovedNew != null | PreviewItemsResizingNew != null) && m_previewItemResizeMoveInfo != null | modifyType == "AddNew")
					{
						switch (modifyType)
						{
							case "Resize":
								PreviewItemsResizingNew(this, new PreviewItemResizingEventArgs(m_previewItemResizeMoveInfo));
								break;
							case "Move":
								PreviewItemsMovedNew(this, new PreviewItemMoveEventArgs(m_previewItemResizeMoveInfo));
								break;
							case "AddNew":
								PreviewItemAddAction(); //starts Undo_Redo Action
								DeSelectSelectedDisplayItem();
								break;
						}
						modifyType = "None";
						m_previewItemResizeMoveInfo = null;
					}
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
						if (PreviewItemsMovedNew != null && m_previewItemResizeMoveInfo != null)
							PreviewItemsMovedNew(this, new PreviewItemMoveEventArgs(m_previewItemResizeMoveInfo));
					}
				}
			}
			ResetMouse();
		}

		public void ResetMouse()
		{
			_banding = false;
			Capture = false;
			_mouseCaptured = false;
			_bandRect.Width = 0;
			_bandRect.Height = 0;
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
			if (!DesignMode) Logging.Debug("Preview:Resize");
			if (DefaultBackground)
			{
				Background = null;
			}
			SetupScrollBars();
		}

		private void SetupScrollBars()
		{
			if (_alphaBackground != null)
			{
				vScroll.Left = Width - vScroll.Width;
				vScroll.Top = 0;
				vScroll.Height = Height - hScroll.Height;
				vScroll.Minimum = 0;
				vScroll.Maximum = _alphaBackground.Height;
				if (_alphaBackground.Height > Height - hScroll.Height)
				{
					vScroll.Maximum = _alphaBackground.Height - (Height - hScroll.Height);
				}
				else
				{
					vScroll.Maximum = 0;
				}

				hScroll.Left = 0;
				hScroll.Top = Height - hScroll.Height;
				hScroll.Width = Width - vScroll.Width;
				hScroll.Minimum = 0;
				if (_alphaBackground.Width > Width - vScroll.Width)
				{
					hScroll.Maximum = _alphaBackground.Width - (Width - vScroll.Width);
				}
				else
				{
					hScroll.Maximum = 0;
				}

				if (Width - _alphaBackground.Width == 0 || Height - _alphaBackground.Height == 0)
				{
					vScroll.Hide();
					hScroll.Hide();
				}
				else if (!vScroll.Visible || !hScroll.Visible)
				{
					vScroll.Show();
					hScroll.Show();
				}
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

		public void Reload()
		{
			if (NodeToPixel == null) NodeToPixel = new ConcurrentDictionary<ElementNode, List<PreviewPixel>>();
			NodeToPixel.Clear();

			if (DisplayItems == null)
				throw new System.ArgumentException("DisplayItems == null");

			foreach (DisplayItem item in DisplayItems)
			{
				if (item.Shape.Pixels == null)
					throw new System.ArgumentException("item.Shape.Pixels == null");


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


		public void Paste()
		{
			string xml = Clipboard.GetText();
			SelectedDisplayItems = (List<DisplayItem>) PreviewTools.DeSerializeToDisplayItemList(xml);
			List<DisplayItem> selected = new List<DisplayItem>(SelectedDisplayItems.ToArray());
			if (SelectedDisplayItems.Count() > 0)
			{
				var action = new PreviewItemsPasteUndoAction(this, selected);//Start Undo Action.
				UndoManager.AddUndoAction(action);

				DeSelectSelectedDisplayItem();
				foreach (DisplayItem newDisplayItem in SelectedDisplayItems)
				{
					AddDisplayItem(newDisplayItem);
					newDisplayItem.ZoomLevel = ZoomLevel;
				}

				// move the prop to the mouse position
				Point mousePoint = PointToClient(MousePosition);
				mousePoint.X += hScroll.Value;
				mousePoint.Y += vScroll.Value;
				Point moveToPoint = PointToZoomPoint(mousePoint);

				int top = int.MaxValue;
				int left = int.MaxValue;
				foreach (DisplayItem item in SelectedDisplayItems)
				{
					top = Math.Min(top, item.Shape.Top);
					left = Math.Min(left, item.Shape.Left);
				}
				int deltaY = top - moveToPoint.Y;
				int deltaX = left - moveToPoint.X;
				foreach (DisplayItem item in SelectedDisplayItems)
				{
					item.Shape.Left -= deltaX;
					item.Shape.Top -= deltaY;
				}
				StartMove(mousePoint.X, mousePoint.Y);
			}
		}

		public void Delete()
		{
			List<DisplayItem> selected = new List<DisplayItem>(SelectedDisplayItems.ToArray());
			if (_selectedDisplayItem != null)
			{
				var action = new PreviewItemsRemovedUndoAction(this, new List<DisplayItem> { _selectedDisplayItem });//Start Undo Action.
				UndoManager.AddUndoAction(action);

				RemoveDisplayItem(_selectedDisplayItem);
				DeSelectSelectedDisplayItem();
			}
			else if (SelectedDisplayItems != null && SelectedDisplayItems.Count > 0)
			{
				var action = new PreviewItemsRemovedUndoAction(this, selected);//Start Undo Action.
				UndoManager.AddUndoAction(action);

				foreach (DisplayItem item in SelectedDisplayItems)
				{
					RemoveDisplayItem(item);
					DeSelectSelectedDisplayItem();
				}
				SelectedDisplayItems.Clear();
			}
		}

		public void RemoveDisplayItem(DisplayItem _selectedDisplayItem)
		{
			DisplayItems.Remove(_selectedDisplayItem);
		}

		public void AddDisplayItem(DisplayItem displayItem)
		{
			DisplayItems.Add(displayItem);
		}

		public void Copy()
		{
			if (SelectedDisplayItems.Count() > 0)
			{
				string xml = PreviewTools.SerializeToString(SelectedDisplayItems);
				Clipboard.SetData(DataFormats.Text, xml);
			}
			else if (_selectedDisplayItem != null)
			{
				SelectedDisplayItems.Add(_selectedDisplayItem);
				string xml = PreviewTools.SerializeToString(SelectedDisplayItems);
				Clipboard.SetData(DataFormats.Text, xml);
			}
			ClipboardPopulated = true;
		}
#endregion

		#region PreviewItem Resize/Move Undo/Redo Action

		public void Resize_MoveSwapPlaces(Dictionary<DisplayItem, PreviewItemPositionInfo> ChangedPreviewItems)
		{
			Resize_MoveSwapElementPlacement(ChangedPreviewItems);
		}

		public void Resize_MoveSwapElementPlacement(Dictionary<DisplayItem, PreviewItemPositionInfo> ChangedPreviewItems)
		{
			foreach (KeyValuePair<DisplayItem, PreviewItemPositionInfo> e in ChangedPreviewItems)
			{
				// Key is reference to actual element. Value is class with previous object data.
				// Swap the element's Display data with the saved data from before the move, so we can restore them later in redo.
				Resize_MoveSwapPlaces(e.Key, e.Value);
				e.Key.Shape.ZoomLevel = ZoomLevel;
				if (_selectedDisplayItem != null)
				propertiesForm.ShowSetupControl(_selectedDisplayItem.Shape.GetSetupControl());
			}
		}

		public static void Resize_MoveSwapPlaces(DisplayItem lhs, PreviewItemPositionInfo rhs)
		{
			var displayItemTemp = PreviewTools.DeSerializeToDisplayItemList(rhs.OriginalPreviewItem[0]);
			foreach (var temp1 in displayItemTemp)
			{
				var temp = lhs;
				var newObject = temp;
				rhs.OriginalPreviewItem.Clear();
				List<DisplayItem> newObjectList = new List<DisplayItem>();
				newObjectList.Add(newObject);
				rhs.OriginalPreviewItem.Add(PreviewTools.SerializeToString(newObjectList));
				lhs.Shape = temp1.Shape;
			}
		}
		#endregion

		public void ResizeBackground(int width, int height)
		{
			double aspect = width/(double) _background.Width;
			Bitmap newBackground = PreviewTools.ResizeBitmap(new Bitmap(_background), new Size(width, height));
			// Copy the file to the Vixen folder
			string imageFileName = Guid.NewGuid() + ".jpg";
			var destFileName = Path.Combine(VixenPreviewDescriptor.ModulePath, imageFileName);
			newBackground.Save(destFileName, ImageFormat.Jpeg);
			Data.BackgroundFileName = imageFileName;
			LoadBackground();

			foreach (DisplayItem item in DisplayItems)
			{
				item.Shape.Resize(aspect);
			}
			EraseScreen();
		}

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

		public DisplayItem CreateGroup()
		{
			if (!SelectedDisplayItems.Any()) return null;

			foreach (DisplayItem item in SelectedDisplayItems)
			{
				if (item.Shape.GetType().ToString().Contains("PreviewCustom"))
				{
					//messageBox Arguments are (Text, Title, No Button Visible, Cancel Button Visible)
					MessageBoxForm.msgIcon = SystemIcons.Error; //this is used if you want to add a system icon to the message form.
					var messageBox = new MessageBoxForm("You cannot create a group or a template with an item that is already grouped or a template item. First, separate the items and then re-group all the items you would like.",
						"Grouping Error", false, true);
					messageBox.ShowDialog();
					return null;
				}
			}
			DisplayItem newDisplayItem;

			AddNewGroup(out newDisplayItem, null);
			SelectedDisplayItems.Clear();
			var action = new PreviewItemsGroupAddedUndoAction(this, newDisplayItem);//Start Undo Action.
			UndoManager.AddUndoAction(action);
			return newDisplayItem;
		}

		internal void AddNewGroup(out DisplayItem newDisplayItem, List<DisplayItem> selectedShapes)
		{
			//Sets the start TopLeft point of the new group
			List<int> topPoint = new List<int>();
			List<int> leftPoint = new List<int>();
			foreach (DisplayItem shape in SelectedDisplayItems)
			{
				topPoint.Add(shape.Shape.Top);
				leftPoint.Add(shape.Shape.Left);
			}
			newDisplayItem = new DisplayItem();
			newDisplayItem.Shape = new PreviewCustom(new PreviewPoint(leftPoint.Min(), topPoint.Min()), SelectedShapes());
			AddDisplayItem(newDisplayItem);

			foreach (DisplayItem item in SelectedDisplayItems)
			{
				DisplayItems.Remove(item);
			}
		}

		public DisplayItem CreateTemplate()
		{
			DisplayItem newDisplayItem = CreateGroup();
			if (newDisplayItem != null)
			{
				PreviewCustomCreateForm f = new PreviewCustomCreateForm();
				if (f.ShowDialog() == DialogResult.OK)
				{
					newDisplayItem.Shape.Name = f.TemplateName;
					newDisplayItem.Shape.Select(true);

					string xml = PreviewTools.SerializeToString(newDisplayItem);
					string destFileName = PreviewTools.TemplateWithFolder(f.TemplateName + ".xml");
					System.IO.File.WriteAllText(destFileName, xml);
				}
			}
			return newDisplayItem;
		}

		public void AddTtemplateToPreview(string fileName)
		{
			if (System.IO.File.Exists(fileName))
			{
				// Read the entire template file (stoopid waste of resources, but how else?)
				string xml = System.IO.File.ReadAllText(fileName);
				DisplayItem newDisplayItem = PreviewTools.DeSerializeToDisplayItem(xml, typeof (DisplayItem));
				if (newDisplayItem != null)
				{
					DeSelectSelectedDisplayItem();

					AddDisplayItem(newDisplayItem);
					_selectedDisplayItem = newDisplayItem;
					OnSelectDisplayItem(this, _selectedDisplayItem);
					_selectedDisplayItem.Shape.MoveTo(10, 10);
					_selectedDisplayItem.Shape.Select(true);
					_selectedDisplayItem.Shape.SetSelectPoint(null);
					List<DisplayItem> selected = new List<DisplayItem> {_selectedDisplayItem};

					PreviewItemAddAction(); //starts Undo_Redo Action
				}
			}
		}

		public void SeparateTemplateItems(DisplayItem displayItem)
		{
			foreach (PreviewBaseShape shape in displayItem.Shape._strings)
			{
				DisplayItem newDisplayItem;
				newDisplayItem = new DisplayItem();
				newDisplayItem.Shape = shape;
				DisplayItems.Add(newDisplayItem);
			}
			if (_selectedDisplayItem != null)
			{
				DisplayItems.Remove(_selectedDisplayItem);
				DeSelectSelectedDisplayItem();
			}
			else if (SelectedDisplayItems != null && SelectedDisplayItems.Count > 0)
			{
				foreach (DisplayItem item in SelectedDisplayItems)
				{
					DisplayItems.Remove(item);
					DeSelectSelectedDisplayItem();
				}
				SelectedDisplayItems.Clear();
			}
		}

		#endregion

		#region Alignment Tools

		public void AlignLeft()
		{
			
			foreach (PreviewBaseShape shape in SelectedShapes())
			{
				
				if (shape != SelectedShapes()[0])
				{
					shape.Left = SelectedShapes()[0].Left;
				}
			}

		}

		public void AlignRight()
		{
			foreach (PreviewBaseShape shape in SelectedShapes())
			{
				if (shape != SelectedShapes()[0])
					shape.Left = SelectedShapes()[0].Right - (shape.Right - shape.Left);
			}
		}

		public void AlignTop()
		{
			foreach (PreviewBaseShape shape in SelectedShapes())
			{
				if (shape != SelectedShapes()[0])
					shape.Top = SelectedShapes()[0].Top;
			}
		}

		public void AlignBottom()
		{
			foreach (PreviewBaseShape shape in SelectedShapes())
			{
				if (shape != SelectedShapes()[0])
					shape.Top = SelectedShapes()[0].Bottom - (shape.Bottom - shape.Top);
			}
		}

		public void AlignHorizontal()
		{
			foreach (PreviewBaseShape shape in SelectedShapes())
			{
				if (shape != SelectedShapes()[0])
				{
					int matchMidPoint = SelectedShapes()[0].Top + ((SelectedShapes()[0].Bottom - SelectedShapes()[0].Top)/2);
					shape.Top = matchMidPoint - ((shape.Bottom - shape.Top)/2);
				}
			}
		}

		public void AlignVertical()
		{
			foreach (PreviewBaseShape shape in SelectedShapes())
			{
				if (shape != SelectedShapes()[0])
				{
					int matchMidPoint = SelectedShapes()[0].Left + ((SelectedShapes()[0].Right - SelectedShapes()[0].Left)/2);
					shape.Left = matchMidPoint - ((shape.Right - shape.Left)/2);
				}
			}
		}

		public void DistributeHorizontal()
		{
			List<PreviewBaseShape> shapes = SelectedShapes().OrderBy(o => o.Left).ToList();
			int shapeCount = shapes.Count;
			if (shapeCount >= 3)
			{
				int totalSpace = shapes[shapeCount - 1].Left - shapes[0].Right;
				int spaceToFill = totalSpace;
				for (int shapeNum = 1; shapeNum < shapeCount - 1; shapeNum++)
				{
					spaceToFill -= shapes[shapeNum].Right - shapes[shapeNum].Left;
				}

				if (spaceToFill > 0)
				{
					float shapeSpacing = (float) spaceToFill/(float) (shapeCount - 1);
					int propSpaceSoFar = 0;
					for (int shapeNum = 1; shapeNum < shapeCount - 1; shapeNum++)
					{
						shapes[shapeNum].Left = shapes[0].Right + propSpaceSoFar + (Convert.ToInt32(shapeSpacing*(float) shapeNum));
						propSpaceSoFar += shapes[shapeNum].Right - shapes[shapeNum].Left;
					}
				}
			}
		}

		public void DistributeVertical()
		{
			List<PreviewBaseShape> shapes = SelectedShapes().OrderBy(o => o.Top).ToList();
			int shapeCount = shapes.Count;
			if (shapeCount >= 3)
			{
				int totalSpace = shapes[shapeCount - 1].Top - shapes[0].Bottom;
				int spaceToFill = totalSpace;
				for (int shapeNum = 1; shapeNum < shapeCount - 1; shapeNum++)
				{
					spaceToFill -= shapes[shapeNum].Bottom - shapes[shapeNum].Top;
				}

				if (spaceToFill > 0)
				{
					float shapeSpacing = (float) spaceToFill/(float) (shapeCount - 1);
					int propSpaceSoFar = 0;
					for (int shapeNum = 1; shapeNum < shapeCount - 1; shapeNum++)
					{
						shapes[shapeNum].Top = shapes[0].Bottom + propSpaceSoFar + (Convert.ToInt32(shapeSpacing*(float) shapeNum));
						propSpaceSoFar += shapes[shapeNum].Bottom - shapes[shapeNum].Top;
					}
				}
			}
		}

        public void MatchProperties()
        {
            if (SelectedShapes().Count >= 2) { 
                foreach (PreviewBaseShape shape in SelectedShapes())
                {
                    if (shape.GetType().ToString() != SelectedShapes()[0].GetType().ToString())
                    {
						//messageBox Arguments are (Text, Title, No Button Visible, Cancel Button Visible)
						MessageBoxForm.msgIcon = SystemIcons.Error; //this is used if you want to add a system icon to the message form.
						var messageBox = new MessageBoxForm("You can only match the properties of like shapes.", "Match Properties", false, true);
						messageBox.ShowDialog();
                        return;
                    }
                }
                foreach (PreviewBaseShape shape in SelectedShapes())
                {
                    if (shape != SelectedShapes()[0])
                    {
                        shape.Match(SelectedShapes()[0]);
                    }
                }
            }
        }

		#endregion

		//#region "Update in a BeginInvoke"
		//public void ProcessUpdate(ElementIntentStates elementStates)
		//{
		//    renderTimer.Reset();
		//    renderTimer.Start();
		//    if (!_paused)
		//    {
		//        FastPixel fp = new FastPixel(new Bitmap(_alphaBackground));

		//        fp.Lock();
		//        Color c;
		//        foreach (var channelIntentState in elementStates)
		//        {
		//            var elementId = channelIntentState.Key;
		//            Element element = VixenSystem.Elements.GetElement(elementId);
		//            if (element == null) continue;
		//            ElementNode node = VixenSystem.Elements.GetElementNodeForElement(element);
		//            if (node == null) continue;

		//            foreach (IIntentState<LightingValue> intentState in channelIntentState.Value)
		//            {
		//                c = intentState.GetValue().GetAlphaChannelIntensityAffectedColor();
		//                if (_background != null)
		//                {
		//                    List<PreviewPixel> pixels;
		//                    if (NodeToPixel.TryGetValue(node, out pixels))
		//                    {
		//                        foreach (PreviewPixel pixel in pixels)
		//                        {
		//                            pixel.Draw(fp, c);
		//                        }
		//                    }
		//                }
		//            }
		//        }

		//        fp.Unlock(true);

		//        // First, draw our background image opaque
		//        bufferedGraphics.Graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
		//        bufferedGraphics.Graphics.DrawImage(fp.Bitmap, 0, 0, fp.Width, fp.Height);

		//        if (!this.Disposing && bufferedGraphics != null)
		//            bufferedGraphics.Render(Graphics.FromHwnd(this.Handle));

		//        fp = null;
		//    }

		//    renderTimer.Stop();
		//    lastRenderUpdateTime = renderTimer.ElapsedMilliseconds;
		//}
		//#endregion

		public void ProcessUpdateParallel( /*Vixen.Preview.PreviewElementIntentStates elementStates*/)
		{
			renderTimer.Reset();
			renderTimer.Start();
			CancellationTokenSource tokenSource = new CancellationTokenSource();
			if (!_paused)
			{
				Bitmap clone = (Bitmap) _alphaBackground.Clone();
				using (FastPixel.FastPixel fp = new FastPixel.FastPixel(clone))
				{
					try
					{
						fp.Lock();

						Vixen.Preview.PreviewElementIntentStates elementStates =
							new Vixen.Preview.PreviewElementIntentStates(VixenSystem.Elements.ToDictionary(x => x, x => x.State));

						elementStates.AsParallel().WithCancellation(tokenSource.Token).ForAll(channelIntentState =>
						{
							Element element = channelIntentState.Key;
							if (element != null)
							{
								ElementNode node = VixenSystem.Elements.GetElementNodeForElement(element);
								if (node != null)
								{
									List<PreviewPixel> pixels;
									if (NodeToPixel.TryGetValue(node, out pixels))
									{
										foreach (PreviewPixel pixel in pixels)
										{
											pixel.Draw(fp, channelIntentState.Value);
										}
									}
								}
							}
						});
						fp.Unlock(true);
						RenderBufferedGraphics(fp);
					}
					catch (Exception)
					{
						tokenSource.Cancel();
					}
				}
			}

			renderTimer.Stop();
			lastRenderUpdateTime = renderTimer.ElapsedMilliseconds;
		}

		private object lockObject = new object();

		private delegate void RenderBufferedGraphicsDelgate(FastPixel.FastPixel fp /*, Bitmap floodBG*/);

		private void RenderBufferedGraphics(FastPixel.FastPixel fp /*, Bitmap floodBG*/)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new RenderBufferedGraphicsDelgate(RenderBufferedGraphics), fp /*, floodBG*/);
			}
			else
				// No, this doesn't allocate every time. It first checks to see if the screen is 
				// resized or the graphics buffer is not allocated. So it is checked for validity every time
				// and re-allocated only if the something changed.
				AllocateGraphicsBuffer(false);

			// First, draw our background image opaque
			bufferedGraphics.Graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
			bufferedGraphics.Graphics.DrawImage(fp.Bitmap, 0, 0, fp.Width, fp.Height);
			if (!this.Disposing && bufferedGraphics != null)
				bufferedGraphics.Render(Graphics.FromHwnd(this.Handle));
		}

		#region "Foreground updates"

		/// <summary>
		/// This is used in edit mode only!!
		/// Need to make it go away so we only have one render engine
		/// </summary>
		public void RenderInForeground()
		{
			renderTimer.Reset();
			renderTimer.Start();

			AllocateGraphicsBuffer(false);
			if (Background != null)
			{
				FastPixel.FastPixel fp = new FastPixel.FastPixel(new Bitmap(_alphaBackground));
				fp.Lock();
				foreach (DisplayItem displayItem in DisplayItems)
				{
					if (_editMode)
					{
						displayItem.Draw(fp, true, HighlightedElements, SelectedDisplayItems.Contains(displayItem), false);
					}
					else
					{
						displayItem.Draw(fp, false, null, false, true);
					}
				}
				fp.Unlock(true);

				if (ShowInfo && _editMode)
				{
					foreach (DisplayItem displayItem in DisplayItems)
					{
						Graphics g = Graphics.FromImage(fp.Bitmap);
						displayItem.DrawInfo(g);
					}
				}

				// Finally, are we drawing a banded rectangle?
				if (_mouseCaptured && _selectedDisplayItem == null)
				{
					Graphics g = Graphics.FromImage(fp.Bitmap);
					g.DrawRectangle(Pens.White, _bandRect);
				}

				bufferedGraphics.Graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;

				// Now, draw our "pixel" image using alpha blending
				if (vScroll.Visible && hScroll.Visible)
				{
					int drawWidth = Width - vScroll.Width + hScroll.Value;
					int drawHeight = Height - hScroll.Height + vScroll.Value;
					int drawX = -hScroll.Value;
					int drawY = -vScroll.Value;
					Rectangle dest = new Rectangle(0, 0, drawWidth, drawHeight);
					Rectangle src = new Rectangle(hScroll.Value, vScroll.Value, drawWidth, drawHeight);
					bufferedGraphics.Graphics.DrawImage(fp.Bitmap, dest, src, GraphicsUnit.Pixel);
				}
				else
				{
					Rectangle dest = new Rectangle(0, 0, Width, Height);
					Rectangle src = new Rectangle(0, 0, Width, Height);
					bufferedGraphics.Graphics.DrawImage(fp.Bitmap, dest, src, GraphicsUnit.Pixel);
				}
			}

			bufferedGraphics.Render(Graphics.FromHwnd(this.Handle));
			renderTimer.Stop();
			lastRenderUpdateTime = renderTimer.ElapsedMilliseconds;
		}

		public void EraseScreen()
		{
			bufferedGraphics.Graphics.Clear(Color.Black);
		}

		#endregion

		public void SaveLocations(List<DisplayItem> displayItems)
		{
			foreach (DisplayItem displayItem in displayItems)
			{
				foreach (var p in displayItem.Shape.Pixels.Where(pi => pi != null && pi.Node != null))
				{
					if (!p.Node.Properties.Contains(LocationDescriptor._typeId))
						p.Node.Properties.Add(LocationDescriptor._typeId);
					var prop = p.Node.Properties.Get(LocationDescriptor._typeId);
					((LocationData) prop.ModuleData).X = p.X;
					((LocationData) prop.ModuleData).Y = p.Y;
					((LocationData) prop.ModuleData).Y = p.Z;
				}
			}
		}

		public Point PointToZoomPoint(Point p)
		{
			int xDif = p.X - Convert.ToInt32(p.X/ZoomLevel);
			int yDif = p.Y - Convert.ToInt32(p.Y/ZoomLevel);
			Point newP = new Point(p.X - xDif, p.Y - yDif);
			return newP;
		}

		public Point MousePointToZoomPoint(Point p)
		{
			int x = p.X + hScroll.Value;
			int y = p.Y + vScroll.Value;
			int xDif = p.X - Convert.ToInt32(x/ZoomLevel);
			int yDif = p.Y - Convert.ToInt32(y/ZoomLevel);
			Point newP = new Point(p.X - xDif, p.Y - yDif);
			return newP;
		}

		public Point ZoomPointToBackgroundPoint(Point p)
		{
			int x = Convert.ToInt32(p.X*ZoomLevel);
			int y = Convert.ToInt32(p.Y*ZoomLevel);
			Point newP = new Point(x, y);
			return newP;
		}

		private void VixenPreviewControl_MouseWheel(object sender, MouseEventArgs e)
		{
			double delta = Convert.ToDouble(e.Delta)/1000;

			// Zoom to the pointer location
			zoomTo = MousePointToZoomPoint(e.Location);

			ZoomLevel += delta;
		}

		private void VixenPreviewControl_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (e.KeyCode == Keys.Left ||
			    e.KeyCode == Keys.Right ||
			    e.KeyCode == Keys.Up ||
			    e.KeyCode == Keys.Down)
			{
				e.IsInputKey = true;
			}
		}

		public void PreviewItemAddAction()
		{
			var action = new PreviewItemsAddedUndoAction(this, new List<DisplayItem> { _selectedDisplayItem });//Start Undo Action.
			UndoManager.AddUndoAction(action);
		}

		#region [Mouse Drag] (Move/Resize)

		public PreviewItemResizeMoveInfo m_previewItemResizeMoveInfo;

		///<summary>Called when any operation that moves element times (namely drag-move and hresize).
		///Saves the pre-move information and begins update on all selected elements.</summary>
		public void beginResize_Move(bool multi)
		{
			if (!multi)
				SelectedDisplayItems.Add(_selectedDisplayItem);
			m_previewItemResizeMoveInfo = new PreviewItemResizeMoveInfo(SelectedDisplayItems);
			if (!multi)
				SelectedDisplayItems.Clear();
		}

		public class PreviewItemResizeMoveInfo
		{
			public PreviewItemResizeMoveInfo(List<DisplayItem> modifyingElements)
			{
				OriginalPreviewItem = new Dictionary<DisplayItem, PreviewItemPositionInfo>();

				foreach (var previewItem in modifyingElements)
				{
					OriginalPreviewItem.Add(previewItem, new PreviewItemPositionInfo(previewItem));
				}
			}

			///<summary>The point on the grid where the mouse first went down.</summary>
			//		public PreviewPoint InitialGridLocation { get; private set; }

			///<summary>All elements being modified and their original parameters.</summary>
			public Dictionary<DisplayItem, PreviewItemPositionInfo> OriginalPreviewItem { get; private set; }
		}

		public class PreviewItemPositionInfo
		{
			public PreviewItemPositionInfo(DisplayItem previewItem)
			{
				TopPosition = previewItem.Shape.Top;
				LeftPosition = previewItem.Shape.Left;
				List<DisplayItem> temp = new List<DisplayItem>();
				temp.Add(previewItem);
				OriginalPreviewItem = new List<string>();
				OriginalPreviewItem.Add(PreviewTools.SerializeToString(temp));
			}

			public List<string> OriginalPreviewItem { get; set; }

			public int TopPosition { get; set; }
			public int LeftPosition { get; set; }
		}
	}

	#endregion
}