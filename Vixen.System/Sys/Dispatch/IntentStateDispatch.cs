﻿using Vixen.Data.Value;

namespace Vixen.Sys.Dispatch
{
	public abstract class IntentStateDispatch : IAnyIntentStateHandler
	{
		public virtual void Handle(IIntentState<ColorValue> obj)
		{
		}

		public virtual void Handle(IIntentState<LightingValue> obj)
		{
		}

		public virtual void Handle(IIntentState<PositionValue> obj)
		{
		}

		public virtual void Handle(IIntentState<CommandValue> obj)
		{
		}

		public virtual void Handle(IIntentState<CustomValue> obj)
		{
		}
	}
}