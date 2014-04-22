﻿using Vixen.Commands;
using Vixen.Data.Value;
using Vixen.Sys;

namespace Vixen.Data.Evaluator
{
	public class _64BitEvaluator : Evaluator
	{
		public override void Handle(IIntentState<RGBValue> obj)
		{
			System.UInt64 level = (System.UInt64)(System.UInt64.MaxValue * obj.GetValue().Intensity);
			EvaluatorValue = new _64BitCommand(level);
		}

		public override void Handle(IIntentState<LightingValue> obj)
		{
			EvaluatorValue = new _64BitCommand((ulong)(ulong.MaxValue * obj.GetValue().Intensity));
		}

		public override void Handle(IIntentState<PositionValue> obj)
		{
			EvaluatorValue = new _64BitCommand((ulong)(ulong.MaxValue * obj.GetValue().Position));
		}
        
        public override void Handle(IIntentState<PhonemeValue> obj)
        {
            EvaluatorValue = new _64BitCommand((byte)(byte.MaxValue * obj.GetValue().Intensity));
        }
	}
}