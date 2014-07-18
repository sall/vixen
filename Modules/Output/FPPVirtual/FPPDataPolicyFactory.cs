using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vixen.Sys;

namespace VixenModules.Output.FPPVirtual
{
    class FPPDataPolicyFactory : IDataPolicyFactory
    {
        public IDataPolicy CreateDataPolicy()
        {
            return new FPPDataPolicy();
        }
    }
}
