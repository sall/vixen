using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vixen.Sys;


namespace VixenModules.Output.ConductorOutput
{
    //  Authored by Charles Strang on 10-29-2013
    //  Additional contributions by Tony Eberle
    //  code can be used and distributed freely
    //  Please give the coders their proper acknowledgement.

    class ConductorOutputDataPolicyFactory : IDataPolicyFactory
    {
        public IDataPolicy CreateDataPolicy()
        {
            return new ConductorModuleDataPolicy();
        }
    }
}
