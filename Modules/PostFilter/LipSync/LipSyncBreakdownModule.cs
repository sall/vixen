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
                CreateOutputs();
            }
        }


        public List<LipSyncBreakdownItem> BreakdownItems
        {
            get { return _data.BreakdownItems; }
            set
            {
                _data.BreakdownItems = value;
                CreateOutputs();
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
                    CreateOutputs();
                    return true;
                }
            }
            return false;
        }

        public void CreateOutputs()
        {
            if (_data.BreakdownItems != null)
            {
                _outputs = _data.BreakdownItems.Select(x => new LipSyncBreakdownOutput(x)).ToArray();
            }
            else
            {
                _data = null;
            }
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

        public override void Handle(IIntentState<PhonemeValue> obj)
        {
            bool state = false;
            PhonemeValue value = obj.GetValue();
            PhonemeValue newValue;
            
            bool success = _breakdownItem.PhonemeList.TryGetValue(value.Phoneme, out state);
            if ((success == true) && (state == true) && (value.ColorGroup >= 0))
            {
                newValue = value;
                _breakdownItem.ActiveColorIndex = value.ColorGroup;
                newValue.FullColor = _breakdownItem.ActiveColor; 
            }
            else
            {
                newValue = new PhonemeValue(value.Phoneme, value.FullColor, 0);
            }
            _intentValue = new StaticIntentState<PhonemeValue>(obj, newValue);
        }

        public override void Handle(IIntentState<RGBValue> obj)
		{
			RGBValue value = obj.GetValue();
            _intentValue = new StaticIntentState<RGBValue>(obj, value);
		}

        public override void Handle(IIntentState<LightingValue> obj)
        {
            LightingValue value = obj.GetValue();
            _intentValue = new StaticIntentState<LightingValue>(obj, value);
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
            PhonemeList = new Dictionary<string,Boolean>();
            ActiveColorIndex = 0;
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int ActiveColorIndex { get; set; }

        [DataMember]
        public Dictionary<string,Boolean> PhonemeList { get; set; }

        [DataMember]
        public List<Color> ColorList { get; set; }

        public Color ActiveColor
        {
            get
            {
                return ColorList[ActiveColorIndex];
            }
        }
    }
/*
     [DataContract]
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
     
        [DataMember]
        public string phonemeName { get; set; }

        [DataMember]
        public bool isChecked { get; set; }
    }
*/
}