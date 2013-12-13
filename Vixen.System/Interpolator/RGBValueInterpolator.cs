using System.Drawing;
using Vixen.Data.Value;

namespace Vixen.Interpolator
{
	[Vixen.Sys.Attribute.Interpolator(typeof (RGBAValue))]
	internal class RGBValueInterpolator : Interpolator<RGBAValue>
	{
		protected override RGBAValue InterpolateValue(double percent, RGBAValue startValue, RGBAValue endValue)
		{
			RGBAValue rv;
			rv.R = (byte)(startValue.R + (endValue.R - startValue.R)*percent);
			rv.G = (byte)(startValue.G + (endValue.G - startValue.G)*percent);
			rv.B = (byte)(startValue.B + (endValue.B - startValue.B)*percent);
			rv.A = (byte)(startValue.A + (endValue.A - startValue.A) * percent);
			return rv;
		}
	}
}