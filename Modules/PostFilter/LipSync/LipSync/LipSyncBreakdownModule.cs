using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;
using Common.Controls.ColorManagement.ColorModels;
using Vixen.Data.Flow;
using Vixen.Data.Value;
using Vixen.Intent;
using Vixen.Module;
using Vixen.Module.OutputFilter;
using Vixen.Sys;
using Vixen.Sys.Dispatch;

namespace VixenModules.OutputFilter.LipSyncBreakdown
{
    public class LipSyncBreakdownDescriptor : OutputFilterModuleDescriptorBase
    {
        private static readonly Guid _typeId = new Guid("{2CE5BAE0-73C8-461D-9295-EDB3F5B9E41B}");

        public override string TypeName
        {
            get { return "LipSync"; }
        }

        public override Guid TypeId
        {
            get { return _typeId; }
        }

        public static Guid ModuleId
        {
            get { return _typeId; }
        }

        public override Type ModuleClass
        {
            get { return typeof(LipSyncBreakdownModule); }
        }

        public override Type ModuleDataClass
        {
            get { return typeof(LipSyncBreakdownData); }
        }

        public override string Author
        {
            get { return "Ed Brady"; }
        }

        public override string Description
        {
            get { return "An output filter that breaks down Phoneme intents into discrete animation components."; }
        }

        public override string Version
        {
            get { return "1.0"; }
        }
    }


    public class LipSyncBreakdownModule : OutputFilterModuleInstanceBase
    {
        private LipSyncBreakdownData _data;
        private LipSyncBreakdownOutput[] _outputs;

        public override void Handle(IntentsDataFlowData obj)
        {
            foreach (LipSyncBreakdownOutput output in Outputs)
            {
                output.ProcessInputData(obj);
            }
        }

        public override DataFlowType InputDataType
        {
            get { return DataFlowType.MultipleIntents; }
        }

        public override DataFlowType OutputDataType
        {
            get { return DataFlowType.MultipleIntents; }
        }

        public override IDataFlowOutput[] Outputs
        {
            get { return _outputs; }
        }

        public override IModuleDataModel ModuleData
        {
            get { return _data; }
            set
            {
                _data = (LipSyncBreakdownData)value;
                _CreateOutputs();
            }
        }


        public List<LipSyncBreakdownItem> BreakdownItems
        {
            get { return _data.BreakdownItems; }
            set
            {
                _data.BreakdownItems = value;
                _CreateOutputs();
            }
        }

        public override bool HasSetup
        {
            get { return true; }
        }

        public override bool Setup()
        {
            using (LipSyncBreakdownSetup setup = new LipSyncBreakdownSetup(_data))
            {
                if (setup.ShowDialog() == DialogResult.OK)
                {
                    _data.BreakdownItems = setup.BreakdownItems;
                    _CreateOutputs();
                    return true;
                }
            }
            return false;
        }

        private void _CreateOutputs()
        {
            _outputs = _data.BreakdownItems.Select(x => new LipSyncBreakdownOutput(x)).ToArray();
        }
    }

    public class LipSyncBreakdownData : ModuleDataModelBase
    {
        public LipSyncBreakdownData()
        {
            BreakdownItems = new List<LipSyncBreakdownItem>();
        }

        public override IModuleDataModel Clone()
        {
            LipSyncBreakdownData newInstance = new LipSyncBreakdownData();
            newInstance.BreakdownItems = new List<LipSyncBreakdownItem>(BreakdownItems);
            return newInstance;
        }

        [DataMember]
        public List<LipSyncBreakdownItem> BreakdownItems { get; set; }

    }

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

    internal class LipSyncBreakdownOutput : IDataFlowOutput<IntentsDataFlowData>
    {
        private readonly LipSyncBreakdownFilter _filter;
        private readonly LipSyncBreakdownItem _breakdownItem;

        public LipSyncBreakdownOutput(LipSyncBreakdownItem breakdownItem)
        {
            _filter = new LipSyncBreakdownFilter(breakdownItem);
            _breakdownItem = breakdownItem;
        }

        public void ProcessInputData(IntentsDataFlowData data)
        {
            Data = new IntentsDataFlowData(data.Value.Select(_filter.Filter));
        }

        public IntentsDataFlowData Data { get; private set; }

        IDataFlowData IDataFlowOutput.Data
        {
            get { return Data; }
        }

        public string Name
        {
            get { return _breakdownItem.Name; }
        }
    }

    [DataContract]
    public class LipSyncBreakdownItem
    {
        public LipSyncBreakdownItem()
        {
            Name = "Unnamed";
            PhonemeList = new List<LipSyncBreakdownItemPhoneme>();
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public List<LipSyncBreakdownItemPhoneme> PhonemeList { get; set; }
    }

    public class LipSyncBreakdownItemPhoneme
    {
        public LipSyncBreakdownItemPhoneme()
        {
            phonemeName = "";
            isChecked = false;
        }

        public LipSyncBreakdownItemPhoneme(LipSyncBreakdownItemPhoneme item)
        {
            phonemeName = item.phonemeName;
            isChecked = item.isChecked;
        }

        public LipSyncBreakdownItemPhoneme(string phonemeName, bool isChecked)
        {
            this.phonemeName = phonemeName;
            this.isChecked = isChecked;
        }

        public string phonemeName { get; set; }

        public bool isChecked { get; set; }
    }



}