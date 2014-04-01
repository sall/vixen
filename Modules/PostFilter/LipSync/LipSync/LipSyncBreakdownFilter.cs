using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Vixen.Data.Flow;
using Vixen.Data.Value;
using Vixen.Intent;
//using Vixen.Module;
//using Vixen.Module.OutputFilter;
using Vixen.Sys;
using Vixen.Sys.Dispatch;

namespace VixenModules.OutputFilter.LipSync
{
    internal class LipSyncBreakdownFilter : IntentStateDispatch
    {
        private IIntentState _intentValue;
        private readonly LipSyncBreakdownItem _breakdownItem;

        public LipSyncBreakdownFilter(LipSyncBreakdownItem breakdownItem)
        {
            _breakdownItem = breakdownItem;
        }

        public IIntentState Filter(IIntentState intentValue)
        {
            intentValue.Dispatch(this);
            return _intentValue;
        }

        /*
        public override void Handle(IIntentState<RGBValue> obj)
        {
            else
            {
                // if we're not mixing colors, we need to compare the input color against the filter color -- but only the
                // hue and saturation components; ignore the intensity.
                HSV inputColor = HSV.FromRGB(value.Color);
                if (inputColor.H == _breakdownColorAsHSV.H && inputColor.S == _breakdownColorAsHSV.S)
                {
                    _intentValue = new StaticIntentState<RGBValue>(obj, value);
                }
                else
                {
                    // TODO: return 'null', or some sort of empty intent state here instead. (null isn't handled well, and we don't have an 'empty' state class.)
                    _intentValue = new StaticIntentState<RGBValue>(obj, new RGBValue(Color.Black));
                }
            }
        }
        */

        public override void Handle(IIntentState<LightingValue> obj)
        {
            /*
            LightingValue lightingValue = obj.GetValue();
            if (_mixColors)
            {
                _intentValue = new StaticIntentState<LightingValue>(obj,
                                                                    new LightingValue(_breakdownItem.Color,
                                                                                      lightingValue.Intensity *
                                                                                      _getMaxProportion(lightingValue.HueSaturationOnlyColor)));
            }
            else
            {
                // if we're not mixing colors, we need to compare the input color against the filter color -- but only the
                // hue and saturation components; ignore the intensity.
                if (lightingValue.Hue == _breakdownColorAsHSV.H && lightingValue.Saturation == _breakdownColorAsHSV.S)
                {
                    _intentValue = new StaticIntentState<LightingValue>(obj, lightingValue);
                }
                else
                {
                    // TODO: return 'null', or some sort of empty intent state here instead. (null isn't handled well, and we don't have an 'empty' state class.)
                    _intentValue = new StaticIntentState<LightingValue>(obj, new LightingValue(_breakdownItem.Color, 0));
                }
            }
             */
        }
    }
}
