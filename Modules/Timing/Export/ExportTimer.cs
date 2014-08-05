using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Vixen.Module;
using Vixen.Module.Timing;

namespace VixenModules.Timing.Export
{
    public class ExportTimer : TimingModuleInstanceBase
    {
        private bool allowUpdate = false;

        public override void Pause()
        {
            allowUpdate = false;
        }

        public override TimeSpan Position { get; set; }

        public override void Resume()
        {
            allowUpdate = true;
        }

        public override void Start()
        {
            allowUpdate = true;
            Position = new TimeSpan(0,0,0,0,1);
        }

        public override void Stop()
        {
            allowUpdate = false;
        }
    }
}
