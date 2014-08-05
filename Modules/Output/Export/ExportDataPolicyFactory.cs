using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vixen.Sys;

namespace VixenModules.Output.Export
{
    class ExportDataPolicyFactory : IDataPolicyFactory
    {
        public IDataPolicy CreateDataPolicy()
        {
            return new ExportDataPolicy();
        }
    }
}
