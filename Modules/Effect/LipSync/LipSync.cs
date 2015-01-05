﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using System.Resources;
using Vixen.Commands;
using Vixen.Data.Value;
using Vixen.Intent;
using Vixen.Module;
using Vixen.Module.App;
using Vixen.Module.Effect;
using Vixen.Services;
using Vixen.Sys;
using Vixen.Sys.Attribute;
using VixenModules.App.LipSyncApp;
using VixenModules.Effect;

namespace VixenModules.Effect.LipSync
{

    public class LipSync : EffectModuleInstanceBase
    {
        private LipSyncData _data;
        private EffectIntents _elementData = null;
        static Dictionary<string, Bitmap> _phonemeBitmaps = null;
        private LipSyncMapLibrary _library = null;


        public LipSync()
        {
            _data = new LipSyncData();
            LoadResourceBitmaps();
            _library = ApplicationServices.Get<IAppModuleInstance>(LipSyncMapDescriptor.ModuleID) as LipSyncMapLibrary;
        }

        protected override void TargetNodesChanged()
        {

        }

        protected override void _PreRender(CancellationTokenSource cancellationToken = null)
        {
            _elementData = new EffectIntents();
            var targetNodes = TargetNodes.AsParallel();

            if (cancellationToken != null)
                targetNodes = targetNodes.WithCancellation(cancellationToken.Token);

            targetNodes.ForAll(node =>
            {
                if (node != null)
                    RenderNode(node);
            });
        }

        // renders the given node to the internal ElementData dictionary. If the given node is
        // not a element, will recursively descend until we render its elements.
        private void RenderNode(ElementNode node)
        {
            EffectIntents result;
            LipSyncMapData mapData = null;
            List<ElementNode> renderNodes = TargetNodes.SelectMany(x => x.GetNodeEnumerator()).ToList();

            if (_data.PhonemeMapping != null) 
            {
                if (!_library.Library.ContainsKey(_data.PhonemeMapping)) 
                {
                    _data.PhonemeMapping = _library.DefaultMappingName;
                }

                PhonemeType phoneme = (PhonemeType)System.Enum.Parse(typeof(PhonemeType), _data.StaticPhoneme); 
                if (_library.Library.TryGetValue(_data.PhonemeMapping, out mapData))
                {

                    renderNodes.ForEach(delegate(ElementNode element)
                    {
                        LipSyncMapItem item = mapData.FindMapItem(element.Name);
                        if (item != null)
                        {
                            if (mapData.PhonemeState(element.Name, _data.StaticPhoneme, item))
                            {
                                var level = new SetLevel.SetLevel();
                                level.TargetNodes = new ElementNode[] { element };
                                level.Color = mapData.ConfiguredColor(element.Name, phoneme, item);
                                level.IntensityLevel = mapData.ConfiguredIntensity(element.Name, phoneme, item);
                                level.TimeSpan = TimeSpan;
                                result = level.Render();
                                _elementData.Add(result);
                            }

                        }
                    });

                }
                    
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
        public String PhonemeMapping
        {
            get { return _data.PhonemeMapping;  }
            set
            {
                _data.PhonemeMapping = value;
                IsDirty = true;
            }
        }

        [Value]
        public String LyricData
        {
            get { return _data.LyricData; }
            set
            {
                _data.LyricData = value;
                IsDirty = true;
            }
        }

        private void LoadResourceBitmaps()
        {
            if (_phonemeBitmaps == null)
            {
                Assembly assembly = Assembly.Load("LipSyncApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
                if (assembly != null)
                {
                    ResourceManager rm = new ResourceManager("VixenModules.App.LipSyncApp.LipSyncResources", assembly);
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
        }

        public override string EffectName
        {
            get { return ((IEffectModuleDescriptor)Descriptor).EffectName + " - " + StaticPhoneme; }
        }

        public override bool ForceGenerateVisualRepresentation { get { return true; } }

        public override void GenerateVisualRepresentation(System.Drawing.Graphics g, System.Drawing.Rectangle clipRectangle)
        {
            try
            {
                if (StaticPhoneme == "")
                {
                    StaticPhoneme = "REST";
                }

                string DisplayValue = string.IsNullOrWhiteSpace(LyricData) ? "-" : LyricData;
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

        public void MakeDirty()
        {
            this.IsDirty = true;
        }
    }
}