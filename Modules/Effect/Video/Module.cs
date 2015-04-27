using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using Vixen.Commands;
using Vixen.Data.Value;
using Vixen.Intent;
using Vixen.Module;
using Vixen.Module.Effect;
using Vixen.Sys;
using Vixen.Sys.Attribute;

namespace Video
{
    public class Module : EffectModuleInstanceBase
    {
        private EffectIntents _elementData = null;
        private Data _data;

        public Module()
        {
            _data = new Data();
        }

        protected override void TargetNodesChanged()
        {

        }

        protected override void _PreRender(CancellationTokenSource cancellationToken = null)
        {
            _elementData = new EffectIntents();

            var value = new CommandValue(new StringCommand(string.Format("{0}|{1}|{2}|{3}|{4}|{5}", "Video", _data.VideoFileName, _data.Repeat, _data.StartTime, _data.EndTime, _data.Volume)));
            var value2 = new CommandValue(new StringCommand(string.Format("{0}|{1}|{2}|{3}|{4}|{5}", "VideoStop", _data.VideoFileName, _data.Repeat, _data.StartTime, _data.EndTime, _data.Volume)));

            var targetNodes = TargetNodes.AsParallel();

            if (cancellationToken != null)
                targetNodes = targetNodes.WithCancellation(cancellationToken.Token);

            targetNodes.ForAll(node =>
            {
                IIntent i = new CommandIntent(value, TimeSpan);
                _elementData.AddIntentForElement(node.Element.Id, i, TimeSpan.Zero);
                IIntent ii = new CommandIntent(value, TimeSpan);
                _elementData.AddIntentForElement(node.Element.Id, ii, TimeSpan.Add(TimeSpan.FromMilliseconds(-100)));

            });

        }

        protected override Vixen.Sys.EffectIntents _Render()
        {
            return _elementData;
        }
        public override IModuleDataModel ModuleData
        {
            get { return _data; }
            set { _data = value as Data; }
        }
        public override bool ForceGenerateVisualRepresentation { get { return true; } }

        public override void GenerateVisualRepresentation(System.Drawing.Graphics g, System.Drawing.Rectangle clipRectangle)
        {
            try
            {

                string DisplayValue = string.Format("Video - {0}", Description);

                Font AdjustedFont = Vixen.Common.Graphics.GetAdjustedFont(g, DisplayValue, clipRectangle, "Vixen.Fonts.DigitalDream.ttf");
                using (var StringBrush = new SolidBrush(Color.DarkBlue))
                {
                    using (var backgroundBrush = new SolidBrush(Color.Yellow))
                    {
                        g.FillRectangle(backgroundBrush, clipRectangle);
                    }
                    g.DrawString(DisplayValue, AdjustedFont, StringBrush, 4, 4);
                 }

            }
            catch (Exception e)
            {

                Console.WriteLine(e.ToString());
            }
        }

        [Value]
        public string Description
        {
            get
            {
                return _data.Description;
            }
            set
            {
                _data.Description = value;
                IsDirty = true;
            }
        }
        [Value]
        public string FileName
        {
            get
            {
                return _data.VideoFileName;
            }
            set
            {
                _data.VideoFileName = value;
                IsDirty = true;
            }
        }

        [Value]
        public bool Repeat
        {
            get { return _data.Repeat; }
            set
            {
                _data.Repeat = value;
                IsDirty = true;
            }
        }



        [Value]
        public double StartTime
        {
            get
            {
                return _data.StartTime;
            }
            set
            {
                _data.StartTime = value;
                IsDirty = true;
            }
        }

        [Value]
        public double EndTime
        {
            get { return _data.EndTime; }
            set
            {
                _data.StartTime = value;
                IsDirty = true;
            }
        }

        [Value]
        public double Volume
        {
            get { return _data.Volume; }
            set
            {
                _data.Volume = value;
                IsDirty = true;
            }
        }
    }
}
