﻿using System.Collections.Generic;

namespace Vixen.Sys {
	public class IntentStateList : List<IIntentState>, IIntentStates {
		public IntentStateList() {
		}

		public IntentStateList(IEnumerable<IIntentState> states) {
			//foreach(IIntentState intentState in states) {
			//    AddIntentState(intentState);
			//}
			AddRangeIntentState(states); //Faster than for add
		}

		virtual public void AddIntentState(IIntentState intentState) {
			Add(intentState);
		}

		virtual public void AddRangeIntentState(IEnumerable<IIntentState> intentStates)
		{
			AddRange(intentStates);
		}
	}
}
