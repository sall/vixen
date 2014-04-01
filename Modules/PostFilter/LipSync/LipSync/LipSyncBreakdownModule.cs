using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Vixen.Data.Flow;
using Vixen.Module;
using Vixen.Module.OutputFilter;


namespace VixenModules.OutputFilter.LipSync
{

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
}