﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Common.Controls.ColorManagement.ColorModels;
using Vixen.Module;
using Vixen.Module.Effect;
using Vixen.Sys;
using Vixen.Sys.Attribute;
using VixenModules.App.ColorGradients;
using VixenModules.App.Curves;
using VixenModules.Effect.Pulse;
using VixenModules.Property.Location;
using System.Drawing;
using VixenModules.Property.Color;

namespace VixenModules.Effect.Wipe {
	public class WipeModule : EffectModuleInstanceBase {
		public WipeModule() {

		}
		WipeData _data = new WipeData();
		private EffectIntents _elementData = null;

		protected override void TargetNodesChanged()
		{
			CheckForInvalidColorData();
		}

		protected override void _PreRender(CancellationTokenSource tokenSource = null) {

			_elementData = new EffectIntents();

			IEnumerable<IGrouping<int, ElementNode>> renderNodes = null;

			var enumerator =  TargetNodes.SelectMany(x => x.GetLeafEnumerator());
			var b = enumerator;
			switch (_data.Direction) {
				case WipeDirection.Up:
					renderNodes = TargetNodes
												.SelectMany(x => x.GetLeafEnumerator())
												.OrderByDescending(x => {
													var prop = x.Properties.Get(VixenModules.Property.Location.LocationDescriptor._typeId);
													if (prop != null) {
														return ((LocationData)prop.ModuleData).Y;
													}
													else
														return 1;
												})
												.ThenBy(x => {
													var prop = x.Properties.Get(VixenModules.Property.Location.LocationDescriptor._typeId);
													if (prop != null) {
														return ((LocationData)prop.ModuleData).X;
													}
													else
														return 1;
												})
												.GroupBy(x => {
													var prop = x.Properties.Get(VixenModules.Property.Location.LocationDescriptor._typeId);
													if (prop != null) {
														return ((LocationData)prop.ModuleData).Y;
													}
													else
														return 1;
												})
												.Distinct();
					break;
				case WipeDirection.Down:

					renderNodes = TargetNodes.SelectMany(x => x.GetLeafEnumerator())
												.OrderBy(x => {
													var prop = x.Properties.Get(VixenModules.Property.Location.LocationDescriptor._typeId);
													if (prop != null) {
														return ((LocationData)prop.ModuleData).Y;
													}
													else
														return 1;
												})
												.ThenBy(x => {
													var prop = x.Properties.Get(VixenModules.Property.Location.LocationDescriptor._typeId);
													if (prop != null) {
														return ((LocationData)prop.ModuleData).X;
													}
													else
														return 1;
												})
												.GroupBy(x => {
													var prop = x.Properties.Get(VixenModules.Property.Location.LocationDescriptor._typeId);
													if (prop != null) {
														return ((LocationData)prop.ModuleData).Y;
													}
													else
														return 1;
												})
												.Distinct();
					break;
				case WipeDirection.Right:

					renderNodes = TargetNodes.SelectMany(x => x.GetLeafEnumerator())
												.OrderBy(x => {
													var prop = x.Properties.Get(VixenModules.Property.Location.LocationDescriptor._typeId);
													if (prop != null) {
														return ((LocationData)prop.ModuleData).X;
													}
													else
														return 1;
												})
												.ThenBy(x => {
													var prop = x.Properties.Get(VixenModules.Property.Location.LocationDescriptor._typeId);
													if (prop != null) {
														return ((LocationData)prop.ModuleData).Y;
													}
													else
														return 1;
												})
												.GroupBy(x => {
													var prop = x.Properties.Get(VixenModules.Property.Location.LocationDescriptor._typeId);
													if (prop != null) {
														return ((LocationData)prop.ModuleData).X;
													}
													else
														return 1;
												})
												.Distinct();
					break;
				case WipeDirection.Left:

					renderNodes = TargetNodes.SelectMany(x => x.GetLeafEnumerator())
												.OrderByDescending(x => {
													var prop = x.Properties.Get(VixenModules.Property.Location.LocationDescriptor._typeId);
													if (prop != null) {
														return ((LocationData)prop.ModuleData).X;
													}
													return 1;
												})
												.ThenBy(x => {
													var prop = x.Properties.Get(VixenModules.Property.Location.LocationDescriptor._typeId);
													if (prop != null) {
														return ((LocationData)prop.ModuleData).Y;
													}
													return 1;
												})
												.GroupBy(x => {
													var prop = x.Properties.Get(VixenModules.Property.Location.LocationDescriptor._typeId);
													if (prop != null) {
														return ((LocationData)prop.ModuleData).X;
													}
													return 1;
												})

												.Distinct();
					break;
				default:
					break;
			}

			if (renderNodes != null && renderNodes.Count()>0) {
				TimeSpan effectTime = TimeSpan.Zero;
				if (WipeByCount) {
					int count = 0;
					double pulseSegment = (TimeSpan.TotalMilliseconds / PassCount) * (PulsePercent / 100) ;
					TimeSpan intervalTime = TimeSpan.FromMilliseconds((TimeSpan.TotalMilliseconds - pulseSegment) / (renderNodes.Count() * PassCount));
					TimeSpan segmentPulse = TimeSpan.FromMilliseconds(pulseSegment);

					while (count < PassCount) {
						foreach (var item in renderNodes)
						{
							if (tokenSource != null && tokenSource.IsCancellationRequested) return;
							EffectIntents result;

							foreach (ElementNode element in item) {

								if (tokenSource != null && tokenSource.IsCancellationRequested)
									return;
								if (element != null) {
									var pulse = new Pulse.Pulse();
									pulse.TargetNodes = new ElementNode[] { element };
									pulse.TimeSpan = segmentPulse;
									pulse.ColorGradient = _data.ColorGradient;
									pulse.LevelCurve = _data.Curve;
									result = pulse.Render();
									result.OffsetAllCommandsByTime(effectTime);
									_elementData.Add(result);
								}
							}
							effectTime += intervalTime;
							
						}
						count++;
						
					}
				} else {
					double intervals = (double)PulseTime / (double)renderNodes.Count();
					var intervalTime = TimeSpan.FromMilliseconds(intervals);
					// the calculation above blows up render time/memory as count goes up, try this.. 
					// also fails if intervals is less than half a ms and intervalTime then gets 0
					intervalTime = TimeSpan.FromMilliseconds( Math.Max( intervalTime.TotalMilliseconds, 50));
					TimeSpan segmentPulse = TimeSpan.FromMilliseconds(PulseTime);
					while (effectTime < TimeSpan) {
						foreach (var item in renderNodes) {
							EffectIntents result;

							if (tokenSource != null && tokenSource.IsCancellationRequested)
								return;
							foreach (ElementNode element in item) {
								if (element != null) {

									if (tokenSource != null && tokenSource.IsCancellationRequested)
										return;
									var pulse = new Pulse.Pulse();
									pulse.TargetNodes = new ElementNode[] { element };
									pulse.TimeSpan = segmentPulse;
									pulse.ColorGradient = _data.ColorGradient;
									pulse.LevelCurve = _data.Curve;
									result = pulse.Render();
									 
									result.OffsetAllCommandsByTime(effectTime);
									_elementData.Add(result);
								}
							}
							effectTime += intervalTime;
							if (effectTime >= TimeSpan)
								return;
						}
					}
				}
				
			}
		}

		protected override EffectIntents _Render() {
			return _elementData;
		}

		public override IModuleDataModel ModuleData {
			get { return _data; }
			set { _data = value as WipeData; }
		}

		

		private void CheckForInvalidColorData()
		{
			HashSet<Color> validColors = new HashSet<Color>();
			validColors.AddRange(TargetNodes.SelectMany(x => ColorModule.getValidColorsForElementNode(x, true)));
			if (validColors.Any() && !_data.ColorGradient.GetColorsInGradient().IsSubsetOf(validColors))
			{
				//Our color is not valid for any elements we have.
				//Try to set a default color gradient from our available colors
				_data.ColorGradient = new ColorGradient(validColors.First());
			}
		}

		[Value]
		public ColorGradient ColorGradient {
			get {
				return _data.ColorGradient; 
			}
			set {
				_data.ColorGradient = value;
				IsDirty = true;
			}
		}

		[Value]
		public WipeDirection Direction {
			get { return _data.Direction; }
			set {
				_data.Direction = value;
				IsDirty = true;
			}
		}

		[Value]
		public Curve Curve {
			get { return _data.Curve; }
			set {
				_data.Curve = value;
				IsDirty = true;
			}
		}

		[Value]
		public int PulseTime {
			get { return _data.PulseTime; }
			set {
				_data.PulseTime = value;
				IsDirty = true;
			}
		}

		[Value]
		public bool WipeByCount
		{
			get { return _data.WipeByCount; }
			set
			{
				_data.WipeByCount = value;
				IsDirty = true;
			}
		}

		[Value]
		public int PassCount {
			get { return _data.PassCount; }
			set {
				_data.PassCount = value;
				IsDirty = true;
			}
		}

		[Value]
		public double PulsePercent
		{
			get { return _data.PulsePercent; }
			set
			{
				_data.PulsePercent = value;
				IsDirty = true;
			}
		}

	}
}
