﻿using System;
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

namespace Launcher
{
	public class Module : EffectModuleInstanceBase
	{
		private EffectIntents _elementData = null;
		private Data _data;
        private static NLog.Logger Logging = NLog.LogManager.GetCurrentClassLogger();
		
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

			var value = new CommandValue(new StringCommand(string.Format("{0}|{1},{2}", "Launcher", _data.Executable, _data.Arguments)));
			
			var targetNodes = TargetNodes.AsParallel();
			
			if (cancellationToken != null)
				targetNodes = targetNodes.WithCancellation(cancellationToken.Token);
			
			targetNodes.ForAll(node => {
                try
                {
                    IIntent i = new CommandIntent(value, TimeSpan);
                    _elementData.AddIntentForElement(node.Element.Id, i, TimeSpan.Zero);
                }
                catch (Exception e)
                {
                    Logging.ErrorException(e.Message, e);
                }
	
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
			try {

				string DisplayValue = string.Format("Launcher - {0}", Description);

				Font AdjustedFont =  Vixen.Common.Graphics.GetAdjustedFont(g, DisplayValue, clipRectangle, "Vixen.Fonts.DigitalDream.ttf");
				using (var StringBrush = new SolidBrush(Color.White)) {
					using (var backgroundBrush = new SolidBrush(Color.DarkBlue)) {
						g.FillRectangle(backgroundBrush, clipRectangle);
					}
					g.DrawString(DisplayValue, AdjustedFont, StringBrush, 4, 4);
					//base.GenerateVisualRepresentation(g, clipRectangle);
				}

			} catch (Exception e) {

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
				_data.Description=value;
				IsDirty=true;
			}
		}
		[Value]
		public string Executable
		{
			get
			{
				return _data.Executable;
			}
			set
			{
				_data.Executable=value;
				IsDirty=true;
			}
		}

		[Value]
		public string Arguments
		{
			get { return _data.Arguments; }
			set
			{
				_data.Arguments=value;
				IsDirty=true;
			}
		}

	

	}
}
