using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Vixen.Data.Value;
using Vixen.Intent;
using Vixen.Module;
using Vixen.Module.Effect;
using Vixen.Sys;
using Vixen.Sys.Attribute;
using System.Threading.Tasks;

namespace VixenModules.Effect.Alternating
{
    public class Alternating : EffectModuleInstanceBase
    {
        private AlternatingData _data;
        private EffectIntents _elementData = null;

        public Alternating()
        {
            _data = new AlternatingData();
        }

        protected override void _PreRender()
        {
            _elementData = new EffectIntents();

            foreach (ElementNode node in TargetNodes)
            {
                if (node != null)
                    RenderNode(node);
            }
        }

        protected override EffectIntents _Render()
        {
            return _elementData;
        }

        public override IModuleDataModel ModuleData
        {
            get { return _data; }
            set { _data = value as AlternatingData; }
        }

        [Value]
        public double IntensityLevel1
        {
            get { return _data.Level1; }
            set { _data.Level1 = value; IsDirty = true; }
        }

        [Value]
        public Color Color1
        {
            get { return _data.Color1; }
            set { _data.Color1 = value; IsDirty = true; }
        }
        [Value]
        public double IntensityLevel2
        {
            get { return _data.Level2; }
            set { _data.Level2 = value; IsDirty = true; }
        }

        [Value]
        public Color Color2
        {
            get { return _data.Color2; }
            set { _data.Color2 = value; IsDirty = true; }
        }

        [Value]
        public int Interval
        {
            get { return _data.Interval; }
            set { _data.Interval = value; IsDirty = true; }
        }

        [Value]
        public bool Enable
        {
            get { return _data.Enable; }
            set { _data.Enable = value; IsDirty = true; }
        }

        // renders the given node to the internal ElementData dictionary. If the given node is
        // not a element, will recursively descend until we render its elements.
        private void RenderNode(ElementNode node)
        {
            bool altColor = false;
            bool startingColor = false;
            long intervals = 1;
            long rem = 0;

            if (Enable)
            {
                intervals = Math.DivRem((long)TimeSpan.TotalMilliseconds, (long)Interval, out rem);
            }
            TimeSpan startTime = TimeSpan.Zero;
            for (int i = 0; i < intervals; i++)
            {
                altColor = startingColor;
                var intervalTime = intervals == 1 ? TimeSpan : TimeSpan.FromMilliseconds(i == intervals - 1 ? Interval + rem : Interval);

                foreach (Element element in node)
                {
                    LightingValue lightingValue;
                    if (altColor)
                    {
                        lightingValue = new LightingValue(Color1, (float)IntensityLevel1);
                    }
                    else
                    {
                        lightingValue = new LightingValue(Color2, (float)IntensityLevel2);
                    }
                    IIntent intent = new LightingIntent(lightingValue, lightingValue, intervalTime);
                    _elementData.AddIntentForElement(element.Id, intent, startTime);

                    altColor = !altColor;
                }

                startTime += intervalTime;
                startingColor = !startingColor;
            }
        }

    }
}
