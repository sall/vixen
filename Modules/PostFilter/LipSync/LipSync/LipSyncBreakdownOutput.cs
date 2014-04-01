using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vixen.Data.Flow;
using Vixen.Module.OutputFilter;

namespace VixenModules.OutputFilter.LipSync
{
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
}