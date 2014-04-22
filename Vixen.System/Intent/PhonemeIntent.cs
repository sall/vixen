using System;
using Vixen.Data.Value;
using Vixen.Interpolator;

namespace Vixen.Intent
{
	public class PhonemeIntent : StaticIntent<PhonemeValue>
	{
		public PhonemeIntent(PhonemeValue phoneme, TimeSpan timeSpan)
			: base(phoneme, timeSpan)
		{
		}
	}
}