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

namespace VixenModules.Effect.Fire
{
	public class Fire:PixelEffectBase
	{
		private FireData _data;
		private int[] _fireBuffer = new int[1];
		private readonly Random _random = new Random();
		
		public Fire()
		{
			_data = new FireData();
		}

		[Value]
		[ProviderCategory(@"Config", 1)]
		[ProviderDisplayName(@"Orientation")]
		[ProviderDescription(@"Orientation")]
		[Browsable(false)]
		public StringOrientation Orientation
		{
			get { return _data.Orientation; }
			set
			{
				_data.Orientation = value;
				StringOrientation = value;
				IsDirty = true;
				OnPropertyChanged();
			}
		}

		[Value]
		[ProviderCategory(@"Config", 1)]
		[ProviderDisplayName(@"Location")]
		[ProviderDescription(@"Location")]
		[PropertyOrder(0)]
		public FireDirection Location
		{
			get { return _data.Location; }
			set
			{
				_data.Location = value;
				IsDirty = true;
				OnPropertyChanged();
			}
		}
		
		[Value]
		[ProviderCategory(@"Config", 1)]
		[ProviderDisplayName(@"Height")]
		[ProviderDescription(@"Height")]
		[PropertyOrder(1)]
		public Curve Height
		{
			get { return _data.Height; }
			set
			{
				_data.Height = value;
				IsDirty = true;
				OnPropertyChanged();
			}
		}

		[Value]
		[ProviderCategory(@"Color", 2)]
		[ProviderDisplayName(@"HueShift")]
		[ProviderDescription(@"Color")]
		[PropertyEditor("SliderEditor")]
		[NumberRange(0, 100, 1)]
		[PropertyOrder(1)]
		public int HueShift
		{
			get { return _data.HueShift; }
			set
			{
				_data.HueShift = value;
				IsDirty = true;
				OnPropertyChanged();
			}
		}

		[Browsable(false)]
		public override StringOrientation StringOrientation
		{
			get
			{
				return StringOrientation.Vertical;
			}
			set
			{
				//Read only
			}
		}

		public override IModuleDataModel ModuleData
		{
			get { return _data; }
			set
			{
				_data = value as FireData;
				IsDirty = true;
			}
		}
		// 0 <= x < BufferWi
		// 0 <= y < BufferHt
		private void SetFireBuffer(int x, int y, int paletteIdx, int maxWi, int maxHt)
		{
			if (x >= 0 && x < maxWi && y >= 0 && y < maxHt)
			{
				_fireBuffer[y * maxWi + x] = paletteIdx;
			}
		}

		// 0 <= x < BufferWi
		// 0 <= y < BufferHt
		private int GetFireBuffer(int x, int y, int maxWi, int maxHt)
		{
			if (x >= 0 && x < maxWi && y >= 0 && y < maxHt)
			{
				return _fireBuffer[y * maxWi + x];
			}
			return -1;
		}

		private int Rand()
		{
			return _random.Next();
		}

		protected override void RenderEffect(int frame, ref PixelFrameBuffer frameBuffer)
		{
			if (_fireBuffer.Count() != BufferWi * BufferHt)
			{
				Array.Resize(ref _fireBuffer, BufferWi * BufferHt);
			}

			int x, y;
			if (frame == 0)
			{
				int i;
				for (i = 0; i < _fireBuffer.Count(); i++)
				{
					_fireBuffer[i] = 0;
				}
			}

			int maxHt = BufferHt;
			int maxWi = BufferWi;
			if (Location == FireDirection.Left || Location == FireDirection.Right)
			{
				maxHt = BufferWi;
				maxWi = BufferHt;
			}

			// build fire
			for (x = 0; x < maxWi; x++)
			{
				var r = x % 2 == 0 ? 190 + (Rand() % 10) : 100 + (Rand() % 50);
				SetFireBuffer(x, 0, r, maxWi, maxHt);
			}

			double interval = GetEffectTimeIntervalPosition(frame);

			int h = (int)Height.GetValue(interval*100);

			
			if(h <= 0)
			{
				h = 1;
			}
			int step = 255 * 100 / maxHt / h;

			for (y = 1; y < maxHt; y++)
			{
				for (x = 0; x < maxWi; x++)
				{
					var v1 = GetFireBuffer(x - 1, y - 1, maxWi, maxHt);
					var v2 = GetFireBuffer(x + 1, y - 1, maxWi, maxHt);
					var v3 = GetFireBuffer(x, y - 1, maxWi, maxHt);
					var v4 = GetFireBuffer(x, y - 1, maxWi, maxHt);
					var n = 0;
					var sum = 0;
					if (v1 >= 0)
					{
						sum += v1;
						n++;
					}
					if (v2 >= 0)
					{
						sum += v2;
						n++;
					}
					if (v3 >= 0)
					{
						sum += v3;
						n++;
					}
					if (v4 >= 0)
					{
						sum += v4;
						n++;
					}
					var newIndex = n > 0 ? sum / n : 0;
					if (newIndex > 0)
					{
						newIndex += (Rand() % 100 < 20) ? step : -step;
						if (newIndex < 0) newIndex = 0;
						if (newIndex >= FirePalette.Count()) newIndex = FirePalette.Count() - 1;
					}
					SetFireBuffer(x, y, newIndex, maxWi, maxHt);
				}
			}
			for (y = 0; y < maxHt; y++)
			{
				for (x = 0; x < maxWi; x++)
				{
					int xp = x;
					int yp = y;
					if (Location == FireDirection.Top || Location == FireDirection.Right)
					{
						yp = maxHt - y - 1;
					}
					if (Location == FireDirection.Left || Location == FireDirection.Right)
					{
						int t = xp;
						xp = yp;
						yp = t;
					}


					Color color = FirePalette.GetColor(GetFireBuffer(x, y, maxWi, maxHt));
					if (HueShift > 0)
					{
						HSV hsv = HSV.FromRGB(color);
						hsv.H = hsv.H + (HueShift / 100.0f);
						color = hsv.ToRGB().ToArgb();
					}

					if (color.R == 0 && color.G == 0 && color.B == 0)
						color = Color.Transparent;
					frameBuffer.SetPixel(xp, yp, color);
				}
			}
		}

		
	}
}
