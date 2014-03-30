using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using Vixen.Data.Value;
using Vixen.Intent;
using Vixen.Module;
using Vixen.Module.Effect;
using Vixen.Sys;
using Vixen.Sys.Attribute;
using System.Threading.Tasks;

namespace VixenModules.Effect.Papagayo
{

    public class Papagayo : EffectModuleInstanceBase
    {
        private PapagayoData _data;
        private EffectIntents _elementData = null;

        public Papagayo()
        {
            _data = new PapagayoData();
        }

        protected override void TargetNodesChanged()
        {

        }

        protected override void _PreRender(CancellationTokenSource cancellationToken = null)
        {



        }


        protected override EffectIntents _Render()
        {
            return _elementData;
        }

        public override IModuleDataModel ModuleData
        {
            get { return _data; }
            set { _data = value as PapagayoData; }
        }

        [Value]
        public String PGOFilename 			
        {
            get { return _data.PGOFilename; }
			set
			{
				_data.PGOFilename = value;
				IsDirty = true;
			}
		}
    }
}