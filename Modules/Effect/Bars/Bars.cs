﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using Common.Controls.ColorManagement.ColorModels;
using Vixen.Attributes;
using Vixen.Module;
using Vixen.Sys.Attribute;
using VixenModules.App.ColorGradients;
using VixenModules.App.Curves;
using VixenModules.Effect.Pixel;
using VixenModules.EffectEditor.EffectDescriptorAttributes;

namespace VixenModules.Effect.Bars
{
	public class Bars:PixelEffectBase
	{
		private BarsData _data;
		
		public Bars()
		{
			_data = new BarsData();
		}

		public override bool IsDirty
		{
			get
			{
				if(Colors.Any(x => !x.CheckLibraryReference()))
				{
					base.IsDirty = true;
				}

				return base.IsDirty;
			}
			protected set { base.IsDirty = value; }
		}

		#region Setup

		[Value]
		public override StringOrientation StringOrientation
		{
			get { return _data.Orientation; }
			set
			{
				_data.Orientation = value;
				IsDirty = true;
				OnPropertyChanged();
			}
		}

		#endregion

		#region Config properties

		[Value]
		[ProviderCategory(@"Config", 1)]
		[ProviderDisplayName(@"Direction")]
		[ProviderDescription(@"Direction")]
		[PropertyOrder(0)]
		public BarDirection Direction
		{
			get { return _data.Direction; }
			set
			{
				_data.Direction = value;
				IsDirty = true;
				OnPropertyChanged();
			}
		}

		[Value]
		[ProviderCategory(@"Config", 1)]
		[ProviderDisplayName(@"Iterations")]
		[ProviderDescription(@"Iterations")]
		[PropertyEditor("SliderEditor")]
		[NumberRange(1, 20, 1)]
		[PropertyOrder(1)]
		public int Speed
		{
			get { return _data.Speed; }
			set
			{
				_data.Speed = value;
				IsDirty = true;
				OnPropertyChanged();
			}
		}

		[Value]
		[ProviderCategory(@"Config", 1)]
		[ProviderDisplayName(@"Repeat")]
		[ProviderDescription(@"Repeat")]
		[PropertyEditor("SliderEditor")]
		[NumberRange(1, 10, 1)]
		[PropertyOrder(2)]
		public int Repeat
		{
			get { return _data.Repeat; }
			set
			{
				_data.Repeat = value;
				IsDirty = true;
				OnPropertyChanged();
			}
		}

		[Value]
		[ProviderCategory(@"Config", 1)]
		[ProviderDisplayName(@"Highlight")]
		[ProviderDescription(@"Highlight")]
		[PropertyOrder(4)]
		public bool Highlight
		{
			get { return _data.Highlight; }
			set
			{
				_data.Highlight = value;
				IsDirty = true;
				OnPropertyChanged();
			}
		}

		[Value]
		[ProviderCategory(@"Config", 1)]
		[ProviderDisplayName(@"Show3D")]
		[ProviderDescription(@"Show3D")]
		[PropertyOrder(5)]
		public bool Show3D
		{
			get { return _data.Show3D; }
			set
			{
				_data.Show3D = value;
				IsDirty = true;
				OnPropertyChanged();
			}
		}

		#endregion

		#region Color properties


		[Value]
		[ProviderCategory(@"Color", 2)]
		[ProviderDisplayName(@"ColorGradients")]
		[ProviderDescription(@"Color")]
		[PropertyOrder(1)]
		public List<ColorGradient> Colors
		{
			get { return _data.Colors; }
			set
			{
				_data.Colors = value;
				IsDirty = true;
				OnPropertyChanged();
			}
		}

		#endregion

		#region Level properties

		[Value]
		[ProviderCategory(@"Brightness", 3)]
		[ProviderDisplayName(@"Brightness")]
		[ProviderDescription(@"Brightness")]
		public Curve LevelCurve
		{
			get { return _data.LevelCurve; }
			set
			{
				_data.LevelCurve = value;
				IsDirty = true;
				OnPropertyChanged();
			}
		}

		#endregion

		public override IModuleDataModel ModuleData
		{
			get { return _data; }
			set
			{
				_data = value as BarsData;
				IsDirty = true;
			}
		}

		protected override void SetupRender()
		{
			//Nothing to setup
		}

		protected override void CleanUpRender()
		{
			//Nothing to clean up
		}

		protected override void RenderEffect(int frame, ref PixelFrameBuffer frameBuffer)
		{
			int x, y, n, colorIdx;
			int colorcnt = Colors.Count();
			int barCount = Repeat * colorcnt;
			double position = (GetEffectTimeIntervalPosition(frame) * Speed) % 1;
			if (barCount < 1) barCount = 1;


			if (Direction < BarDirection.Left || Direction == BarDirection.AlternateUp || Direction == BarDirection.AlternateDown)
			{
				int barHt = BufferHt / barCount+1;
				if (barHt < 1) barHt = 1;
				int halfHt = BufferHt / 2;
				int blockHt = colorcnt * barHt;
				if (blockHt < 1) blockHt = 1;
				int fOffset = (int) (position*blockHt*Repeat);// : Speed * frame / 4 % blockHt);
				if(Direction == BarDirection.AlternateUp || Direction == BarDirection.AlternateDown)
				{
					fOffset = (int)(Math.Floor(position*barCount)*barHt);
				}
				int indexAdjust = 1;
				
				for (y = 0; y < BufferHt; y++)
				{
					n = y + fOffset;
					colorIdx = ((n + indexAdjust) % blockHt) / barHt;
					//we need the integer division here to make things work
					double colorPosition = ((double)(n + indexAdjust) / barHt) - ((n + indexAdjust) / barHt);
					Color c = Colors[colorIdx].GetColorAt(colorPosition);
					var hsv = HSV.FromRGB(c);
					if (Highlight && (n + indexAdjust) % barHt == 0) hsv.S = 0.0f;
					if (Show3D) hsv.V *= (float)(barHt - (n + indexAdjust) % barHt - 1) / barHt;

					hsv.V = hsv.V * LevelCurve.GetValue(GetEffectTimeIntervalPosition(frame) * 100) / 100;

					switch (Direction)
					{
						case BarDirection.Down:
						case BarDirection.AlternateDown:
							// down
							for (x = 0; x < BufferWi; x++)
							{
								frameBuffer.SetPixel(x, y, hsv);
							}
							break;
						case BarDirection.Expand:
							// expand
							if (y <= halfHt)
							{
								for (x = 0; x < BufferWi; x++)
								{
									frameBuffer.SetPixel(x, y, hsv);
									frameBuffer.SetPixel(x, BufferHt - y - 1, hsv);
								}
							}
							break;
						case BarDirection.Compress:
							// compress
							if (y >= halfHt)
							{
								for (x = 0; x < BufferWi; x++)
								{
									frameBuffer.SetPixel(x, y, hsv);
									frameBuffer.SetPixel(x, BufferHt - y - 1, hsv);
								}
							}
							break;
						default:
							// up & AlternateUp
							for (x = 0; x < BufferWi; x++)
							{
								frameBuffer.SetPixel(x, BufferHt - y - 1, hsv);
							}
							break;
					}
				}
			}
			else
			{
				int barWi = BufferWi / barCount+1;
				if (barWi < 1) barWi = 1;
				int halfWi = BufferWi / 2;
				int blockWi = colorcnt * barWi;
				if (blockWi < 1) blockWi = 1;
				int fOffset = (int)(position * blockWi * Repeat);
				if (Direction > BarDirection.AlternateDown)
				{
					fOffset = (int)(Math.Floor(position * barCount) * barWi);
				} 
				
				for (x = 0; x < BufferWi; x++)
				{
					n = x + fOffset;
					colorIdx = ((n + 1) % blockWi) / barWi;
					//we need the integer division here to make things work
					double colorPosition = ((double)(n + 1) / barWi) - ((n + 1) / barWi);
					Color c = Colors[colorIdx].GetColorAt( colorPosition );
					var hsv = HSV.FromRGB(c);
					if (Highlight && n % barWi == 0) hsv.S = 0.0f;
					if (Show3D) hsv.V *= (float)(barWi - n % barWi - 1) / barWi;
					hsv.V = hsv.V * LevelCurve.GetValue(GetEffectTimeIntervalPosition(frame) * 100) / 100;
					switch (Direction)
					{
						case BarDirection.Right:
						case BarDirection.AlternateRight:
							// right
							for (y = 0; y < BufferHt; y++)
							{
								frameBuffer.SetPixel(BufferWi - x - 1, y, hsv);
							}
							break;
						case BarDirection.HExpand:
							// H-expand
							if (x <= halfWi)
							{
								for (y = 0; y < BufferHt; y++)
								{
									frameBuffer.SetPixel(x, y, hsv);
									frameBuffer.SetPixel(BufferWi - x - 1, y, hsv);
								}
							}
							break;
						case BarDirection.HCompress:
							// H-compress
							if (x >= halfWi)
							{
								for (y = 0; y < BufferHt; y++)
								{
									frameBuffer.SetPixel(x, y, hsv);
									frameBuffer.SetPixel(BufferWi - x - 1, y, hsv);
								}
							}
							break;
						default:
							// left & AlternateLeft
							for (y = 0; y < BufferHt; y++)
							{
								frameBuffer.SetPixel(x, y, hsv);
							}
							break;
					}
				}
			}
		}
	}
}
