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

namespace VixenModules.Effect.LipSync
{

    public class LipSync : EffectModuleInstanceBase
    {
        private LipSyncData _data;
        private EffectIntents _elementData = null;

        public LipSync()
        {
            _data = new LipSyncData();
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
            set { _data = value as LipSyncData; }
        }

        [Value]
        public String StaticPhoneme
        {
            get { return _data.StaticPhoneme; }
            set
            {
                _data.StaticPhoneme = value;
                IsDirty = true;
            }
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