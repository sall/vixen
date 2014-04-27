using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Resources;
using Vixen.Commands;
using Vixen.Data.Value;
using Vixen.Intent;
using Vixen.Module;
using Vixen.Module.Effect;
using Vixen.Sys;
using Vixen.Sys.Attribute;


namespace VixenModules.Effect.LipSync
{

    public class LipSync : EffectModuleInstanceBase
    {
        private LipSyncData _data;
        private EffectIntents _elementData = null;
        static Dictionary<string, Bitmap> _phonemeBitmaps = null;

        public LipSync()
        {
            _data = new LipSyncData();
            LoadResourceBitmaps();
        }

        protected override void TargetNodesChanged()
        {

        }

		protected override void _PreRender(CancellationTokenSource tokenSource = null)
		{
			_elementData = new EffectIntents();
			
			foreach (ElementNode node in TargetNodes) {
				if (tokenSource != null && tokenSource.IsCancellationRequested)
					return;
				
				if (node != null)
					RenderNode(node);
			}
		}

        // renders the given node to the internal ElementData dictionary. If the given node is
        // not a element, will recursively descend until we render its elements.
        private void RenderNode(ElementNode node)
        {
            foreach (ElementNode elementNode in node.GetLeafEnumerator())
            {
                PhonemeValue phonemeValue = new PhonemeValue(StaticPhoneme, Color.White);
                IIntent intent = new PhonemeIntent(phonemeValue,TimeSpan);
                _elementData.AddIntentForElement(elementNode.Element.Id, intent, TimeSpan.Zero);
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

        [Value]
        public String ColorGroup
        {
            get { return _data.ColorGroup; }
            set
            {
                _data.ColorGroup = value;
                IsDirty = true;
            }
        }


        private void LoadResourceBitmaps()
        {
            if (_phonemeBitmaps == null)
            {
                ResourceManager rm = LipSyncResources.ResourceManager;
                _phonemeBitmaps = new Dictionary<string, Bitmap>();
                _phonemeBitmaps.Add("AI", (Bitmap)rm.GetObject("AI"));
                _phonemeBitmaps.Add("E", (Bitmap)rm.GetObject("E"));
                _phonemeBitmaps.Add("ETC", (Bitmap)rm.GetObject("etc"));
                _phonemeBitmaps.Add("FV", (Bitmap)rm.GetObject("FV"));
                _phonemeBitmaps.Add("L", (Bitmap)rm.GetObject("L"));
                _phonemeBitmaps.Add("MBP", (Bitmap)rm.GetObject("MBP"));
                _phonemeBitmaps.Add("O", (Bitmap)rm.GetObject("O"));
                _phonemeBitmaps.Add("PREVIEW", (Bitmap)rm.GetObject("Preview"));
                _phonemeBitmaps.Add("REST", (Bitmap)rm.GetObject("rest"));
                _phonemeBitmaps.Add("U", (Bitmap)rm.GetObject("U"));
                _phonemeBitmaps.Add("WQ", (Bitmap)rm.GetObject("WQ"));

            }
        }

        public override bool ForceGenerateVisualRepresentation { get { return true; } }

        public override void GenerateVisualRepresentation(System.Drawing.Graphics g, System.Drawing.Rectangle clipRectangle)
        {
            try
            {

                string DisplayValue = StaticPhoneme;
                Bitmap displayImage = null;
                Bitmap scaledImage = null;
                if (_phonemeBitmaps.TryGetValue(StaticPhoneme, out displayImage) == true)
                {
                    scaledImage = new Bitmap(displayImage, 
                                            Math.Min(clipRectangle.Height,clipRectangle.Width), 
                                            clipRectangle.Height);
                    g.DrawImage(scaledImage, clipRectangle.X,clipRectangle.Y);
                }
                if ((scaledImage != null) && (scaledImage.Width < clipRectangle.Width))
                {
                    clipRectangle.X += scaledImage.Width;
                    clipRectangle.Width -= scaledImage.Width;
                    Font AdjustedFont = Vixen.Common.Graphics.GetAdjustedFont(g, DisplayValue, clipRectangle, "Vixen.Fonts.DigitalDream.ttf");
                    using (var StringBrush = new SolidBrush(Color.Yellow))
                    {
                        using (var backgroundBrush = new SolidBrush(Color.Green))
                        {
                            g.FillRectangle(backgroundBrush, clipRectangle);
                        }
                        g.DrawString(DisplayValue, AdjustedFont, StringBrush, 4 + scaledImage.Width, 4);
                    }
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

    }
}