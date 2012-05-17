﻿using System;
using System.Collections.Generic;

namespace Vixen.Sys {
	public interface IIntent : IDispatchable {
		TimeSpan TimeSpan { get; }
		IIntentState CreateIntentState(TimeSpan intentRelativeTime);
		void FractureAt(TimeSpan intentRelativeTime);
		void FractureAt(IEnumerable<TimeSpan> intentRelativeTimes);
		void FractureAt(ITimeNode intentRelativeTime);
		IIntent[] DivideAt(TimeSpan intentRelativeTime);
		void ApplyFilter(ISequenceFilterNode sequenceFilterNode, TimeSpan contextAbsoluteIntentStartTime);
		object GetStateAt(TimeSpan timeOffset);
	}
	public interface IIntent<out T> : IIntent {
		T GetStateAt(TimeSpan timeOffset);
	}
}
