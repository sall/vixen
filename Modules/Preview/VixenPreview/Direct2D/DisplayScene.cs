﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using D2D = Microsoft.WindowsAPICodePack.DirectX.Direct2D1;
using DWrite = Microsoft.WindowsAPICodePack.DirectX.DirectWrite;
using WIC = Microsoft.WindowsAPICodePack.DirectX.WindowsImagingComponent;
using System.Threading.Tasks;
using System.Drawing;
using System.Timers;
using System.Diagnostics;
using System.Drawing.Imaging;
using Vixen.Sys;
using System.Threading;
using VixenModules.Preview.VixenPreview.Shapes;
using Vixen.Data.Value;
using Common.Controls.Direct2D;
using System.Drawing.Drawing2D;
using System.IO;

namespace VixenModules.Preview.VixenPreview.Direct2D {
	public sealed class DisplayScene : AnimatedScene {

		private DWrite.TextFormat textFormat;
		private DWrite.DWriteFactory writeFactory;
		private const int PIXEL_SIZE = 2;
		// These are used for tracking an accurate frames per second
		private DateTime time;
		private long frameCount;
		private long fps;
		private Guid DisplayID = Guid.Empty;
		public DisplayScene(System.Drawing.Image backgroundImage)
			: base(100) {
			this.writeFactory = DWrite.DWriteFactory.CreateFactory();


			BackgroundImage = backgroundImage;


		}
		private VixenPreviewData _data;
		public VixenPreviewData Data {
			set {
				_data = value;

			}
			get { return _data; }
		}

		protected override void Dispose(bool disposing) {
			if (disposing) {
				this.writeFactory.Dispose();

			}

			base.Dispose(disposing);
		}

		protected override void OnCreateResources() {
			// We don't need to free any resources because the base class will
			// call OnFreeResources if necessary before calling this method.

			this.textFormat = this.writeFactory.CreateTextFormat("Arial", 10);

			base.OnCreateResources(); // Call this last to start the animation
		}

		protected override void OnFreeResources() {
			base.OnFreeResources(); // Call this first to stop the animation

		}
		System.Drawing.Image backgroundImage;
		int? imageHash;
		int? lastImageHash;

		public System.Drawing.Image BackgroundImage {
			get { return backgroundImage; }
			set {
				backgroundImage = value;
				if (value != null)
					imageHash = value.GetHashCode();
				else
					imageHash = null;

				if (imageHash != lastImageHash)
					isDirty = true;
			}
		}

		bool isDirty = true;
		D2D.D2DBitmap background = null;

		public D2D.D2DBitmap Background {
			get {
				TryCreateBackgroundBitmap();
				return background;
			}
		}


		public void ConvertImageToStreamAndAdjustBrightness(Image image, int value, MemoryStream stream) {

			if (value == 0) // No change, so just return
				return;

			using (Image img = image) {

				float sb = (((float)value - 255) / 255F) * .7f;

				float[][] colorMatrixElements =
				  {
                        new float[] {1,  0,  0,  0, 0},
                        new float[] {0,  1,  0,  0, 0},
                        new float[] {0,  0,  1,  0, 0},
                        new float[] {0,  0,  0,  1, 0},
                        new float[] {sb, sb, sb, 1, 1}

                  };

				ColorMatrix cm = new ColorMatrix(colorMatrixElements);

				using (ImageAttributes imgattr = new ImageAttributes()) {
					Rectangle rc = new Rectangle(0, 0, img.Width, img.Height);
					using (Graphics g = Graphics.FromImage(img)) {
						g.InterpolationMode = InterpolationMode.HighQualityBicubic;
						imgattr.SetColorMatrix(cm);
						g.DrawImage(img, rc, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgattr);
					}
				}

				img.Save(stream, img.RawFormat);
			}
		}

		private void TryCreateBackgroundBitmap() {
			if (RenderTarget != null && (imageHash != lastImageHash) && ((isDirty && BackgroundImage != null) || (background == null && BackgroundImage != null))) {
				Console.WriteLine("TryCreateBackgroundBitmap");
				using (var ms = new System.IO.MemoryStream()) {

					ConvertImageToStreamAndAdjustBrightness(BackgroundImage, Data.BackgroundAlpha, ms);
					ms.Position = 0;

					using (var factory = WIC.ImagingFactory.Create()) {
						using (WIC.BitmapDecoder decoder = factory.CreateDecoderFromStream(ms, WIC.DecodeMetadataCacheOption.OnDemand)) {
							using (WIC.BitmapFrameDecode source = decoder.GetFrame(0)) {
								using (WIC.FormatConverter converter = factory.CreateFormatConverter()) {
									converter.Initialize(source.ToBitmapSource(), WIC.PixelFormats.Pbgra32Bpp, WIC.BitmapDitherType.None, WIC.BitmapPaletteType.MedianCut);
									background = RenderTarget.CreateBitmapFromWicBitmap(converter.ToBitmapSource());
									isDirty = false;
									lastImageHash = imageHash;
								}
							}
						}
					}
				}
			}
		}

		public static ConcurrentDictionary<Guid, ElementNode> ElementNodeCache = new ConcurrentDictionary<Guid, ElementNode>();

		protected override void OnRender() { }
		Queue<long> updatTimes = new Queue<long>();
		long maxUpdateTime = 0;
		TimeSpan maxUpdateTimeposition = TimeSpan.Zero;

		public Queue<long> UpdateTimes {
			get { return updatTimes; }
			set {
				updatTimes = value;
				while (updatTimes.Count() > 1000) {
					var i = updatTimes.Dequeue();
				}
			}
		}
		public void Update(ElementIntentStates ElementStates) {

			try {
				Stopwatch w = Stopwatch.StartNew();

				// Calculate our actual frame rate
				this.frameCount++;

				if (DateTime.UtcNow.Subtract(this.time).TotalSeconds >= 1) {
					this.fps = this.frameCount;
					this.frameCount = 0;
					this.time = DateTime.UtcNow;
				}
				int iPixels = 0;

				this.RenderTarget.BeginDraw();

				this.RenderTarget.Clear(new D2D.ColorF(0, 0, 0, 1f));
				//this.RenderTarget.Clear();

				if (Background != null) {
					this.RenderTarget.DrawBitmap(Background);
				}
				if (NodeToPixel.Count == 0)
					Reload();

				CancellationTokenSource tokenSource = new CancellationTokenSource();



				try {

					ElementStates.AsParallel().WithCancellation(tokenSource.Token).ForAll(channelIntentState => {

						//foreach (var channelIntentState in ElementStates) {

						ElementNode node;

						if (!ElementNodeCache.TryGetValue(channelIntentState.Key, out node)) {
							Element element = VixenSystem.Elements.GetElement(channelIntentState.Key);
							if (element != null) {

								node = VixenSystem.Elements.GetElementNodeForElement(element);
								if (node != null)
									ElementNodeCache.TryAdd(channelIntentState.Key, node);
							}
						}


						if (node != null) {
							Color pixColor;


							//TODO: Discrete Colors
							pixColor = Vixen.Intent.ColorIntent.GetAlphaColorForIntents(channelIntentState.Value);

							if (pixColor.A > 0)
								using (var brush = this.RenderTarget.CreateSolidColorBrush(pixColor.ToColorF())) {

									List<PreviewPixel> pixels;
									if (NodeToPixel.TryGetValue(node, out pixels)) {
										//if (pixels.Count < 50)
										iPixels += pixels.Count;
										pixels.ForEach(p => RenderPixel(channelIntentState, p, brush));

										//else {
										//	//Console.WriteLine("Element {1}, Pixels: {0}",pixels.Count, element.Name);
										//	pixels.AsParallel().WithCancellation(tokenSource.Token).ForAll(p => RenderPixel(channelIntentState, p, brush));
										//}
									}
								}
						}

						//}
					});

				}
				catch (Exception e) {
					tokenSource.Cancel();
					//Console.WriteLine(e.Message);
				}






				w.Stop();
				// Draw a little FPS in the top left corner
				var ms = w.ElapsedMilliseconds;
				UpdateTimes.Enqueue(ms);
				if (maxUpdateTime < ms) {
					maxUpdateTime = ms;
				}
				string text = string.Format("FPS {0} \nPoints {1} \nPixels {3} \nRender Time:{2} \nMax Render: {4} \nAvg Render: {5}", this.fps, ElementStates.Count(), w.ElapsedMilliseconds, iPixels, maxUpdateTime, UpdateTimes.Average().ToString("0.00"));

				using (var textBrush = this.RenderTarget.CreateSolidColorBrush(Color.White.ToColorF())) {
					this.RenderTarget.DrawText(text, this.textFormat, new D2D.RectF(10, 10, 100, 20), textBrush);
				}

				// All done!

				this.RenderTarget.EndDraw();




			}
			catch (Exception e) {

				Console.WriteLine(e.Message);
			}
		}

		private void RenderPixel(KeyValuePair<Guid, IIntentStates> channelIntentState, PreviewPixel p, D2D.SolidColorBrush brush) {
			if (p.PixelSize <= 4)
				this.RenderTarget.DrawLine(new D2D.Point2F(p.X, p.Y), new D2D.Point2F(p.X + 1, p.Y + 1), brush, p.PixelSize);
			else
				this.RenderTarget.FillEllipse(new D2D.Ellipse() { Point = new D2D.Point2F(p.X, p.Y), RadiusX = p.PixelSize / 2, RadiusY = p.PixelSize / 2 }, brush);

		}


		#region Old Update Stuff

		public ConcurrentDictionary<ElementNode, List<PreviewPixel>> NodeToPixel = new ConcurrentDictionary<ElementNode, List<PreviewPixel>>();
		public List<DisplayItem> DisplayItems {
			get {
				if (Data != null) {
					return Data.DisplayItems;
				}
				else {
					return null;
				}
			}
		}

		public void Reload() {
			//lock (PreviewTools.renderLock)
			//{
			if (NodeToPixel == null)
				throw new System.ArgumentException("PreviewBase.NodeToPixel == null");

			NodeToPixel.Clear();

			if (DisplayItems == null)
				throw new System.ArgumentException("DisplayItems == null");

			if (DisplayItems != null)
				foreach (DisplayItem item in DisplayItems) {
					if (item.Shape.Pixels == null)
						throw new System.ArgumentException("item.Shape.Pixels == null");

					foreach (PreviewPixel pixel in item.Shape.Pixels) {
						if (pixel.Node != null) {
							List<PreviewPixel> pixels;
							if (NodeToPixel.TryGetValue(pixel.Node, out pixels)) {
								if (!pixels.Contains(pixel)) {
									pixels.Add(pixel);
								}
							}
							else {
								pixels = new List<PreviewPixel>();
								pixels.Add(pixel);
								NodeToPixel.TryAdd(pixel.Node, pixels);
							}
						}
					}
				}

			if (System.IO.File.Exists(Data.BackgroundFileName))
				BackgroundImage = Image.FromFile(Data.BackgroundFileName);
			else
				BackgroundImage = null;

			//LoadBackground();
			//}

		}


		#endregion


	}
}
