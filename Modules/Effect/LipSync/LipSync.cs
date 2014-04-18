using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using Vixen.Commands;
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

        protected override void _PreRender(CancellationTokenSource tokenSource = null)
        {
           
            _elementData = new EffectIntents();

            if (_data == null)
                return;

            ICommand command = new StringCommand(StaticPhoneme);

            CommandValue value = new CommandValue(command);

            foreach (ElementNode node in TargetNodes)
            {
                if (tokenSource != null && tokenSource.IsCancellationRequested)
                    return;

                IIntent intent = new CommandIntent(value, TimeSpan);
                _elementData.AddIntentForElement(node.Element.Id, intent, TimeSpan.Zero);
            }
           

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

        public override bool ForceGenerateVisualRepresentation { get { return true; } }

        public override void GenerateVisualRepresentation(System.Drawing.Graphics g, System.Drawing.Rectangle clipRectangle)
        {
            try
            {

                string DisplayValue = StaticPhoneme;

                Font AdjustedFont = Vixen.Common.Graphics.GetAdjustedFont(g, DisplayValue, clipRectangle, "Vixen.Fonts.DigitalDream.ttf");
                using (var StringBrush = new SolidBrush(Color.White))
                {
                    using (var backgroundBrush = new SolidBrush(Color.DarkGray))
                    {
                        g.FillRectangle(backgroundBrush, clipRectangle);
                    }
                    g.DrawString(DisplayValue, AdjustedFont, StringBrush, 4, 4);
                    //base.GenerateVisualRepresentation(g, clipRectangle);
                }

            }
            catch (Exception e)
            {

                Console.WriteLine(e.ToString());
            }
        }

    }
}