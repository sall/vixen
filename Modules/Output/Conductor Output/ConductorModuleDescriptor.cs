using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vixen;
using Vixen.Module;
using Vixen.Module.Controller;
using Vixen.Sys;

namespace VixenModules.Output.ConductorOutput
{
    //  Authored by Charles Strang on 10-29-2013
    //  Additional contributions by Tony Eberle
    //  code can be used and distributed freely
    //  Please give the coders their proper acknowledgement.

    public class ConductorModuleDescriptor : ControllerModuleDescriptorBase

    {
        public override string Author
        {
            get { return "Charles Strang"; }
        }

        public override string Description
        {
            get { return "Lynx Conductor Output Controller 1.0"; }
        }

        public override Type ModuleClass
        {
            get { return typeof(ConductorModuleInstance); }
        }

        public override Guid TypeId
        {
            get { return Guid.Parse("5970a41f-9c8b-4731-a93e-9c75d9b69406"); }
        }

        public override string TypeName
        {
            get { return "Conductor Output Module"; }
        }

        public override string Version
        {
            get { return "1.0.0.0"; }
        }

        public override Type ModuleDataClass
        {
            get
            {
                return typeof(ConductorModuleData);
            }
        }
  
    }
}
