using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Common.Controls.ColorManagement.ColorModels;

namespace Vixen.Data.Value
{
	public struct RGBAValue : IIntentDataType
	{
		public byte R;
		public byte G;
		public byte B;
		public byte A;
		public RGBAValue(Color color)
		{
			if (color.A == 0) {
			A = R = G = B = 0;
			} else {
				R = color.R;
				G = color.G;
				B = color.B;
				A = color.A;
			}
		}

		/// <summary>
		/// Gets the color as an opaque color: ie. 100% alpha channel.
		/// </summary>
		public Color Color
		{
			get { return Color.FromArgb(A,R, G, B); }
			set
			{
				R = value.R;
				G = value.G;
				B = value.B;
				//A = value.A;
			}
		}

		/// <summary>
		/// Gets the color value with the alpha channel interpreted as the 'brightness' of the color.
		/// </summary>
		public Color ColorWithAplha
		{
			get
			{
				return Color.FromArgb(A, R, G, B);
			}
		}

		/// <summary>
		/// the intensity or brightness of this color in the range 0.0 -> 1.0 (from 0% to 100%).
		/// </summary>
		public double Intensity
		{
			get
			{
				//return HSV.FromRGB(Color).V;
				return (double)((double)A / (double)byte.MaxValue);
			}
		}


		public static Color ConvertToGrayscale(Color color)
		{
			return _BasicGrayscaleLuma(color);
		}

		public static byte GetGrayscaleLevel(Color color)
		{
			return ConvertToGrayscale(color).R;
		}

		private static Color _BasicGrayscaleLuma(Color color)
		{
			byte grayLevel = (byte) (color.R*0.3 + color.G*0.59 + color.B*0.11);
			return Color.FromArgb(grayLevel, grayLevel, grayLevel);
		}
	}
}
