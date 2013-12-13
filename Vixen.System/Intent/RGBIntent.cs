using System;
using Vixen.Data.Value;

namespace Vixen.Intent
{
	public class RGBIntent : LinearIntent<RGBAValue>
	{
		public RGBIntent(RGBAValue startValue, RGBAValue endValue, TimeSpan timeSpan)
			: base(startValue, endValue, timeSpan)
		{
		}
	}
}