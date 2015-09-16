﻿using System;
using System.Globalization;
using System.Windows.Data;
using VixenModules.App.Curves;

namespace VixenModules.Editor.EffectEditor.Converters
{
	public class CurveToImageConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is Curve)
			{
				Curve curve = (Curve) value;
				return BitmapImageConverter.BitmapToMediaImage(curve.GenerateGenericCurveImage(new System.Drawing.Size(25, 25)));
			}

			return BitmapImageConverter.BitmapToMediaImage(new Curve().GenerateGenericCurveImage(new System.Drawing.Size(25, 25), true));
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}