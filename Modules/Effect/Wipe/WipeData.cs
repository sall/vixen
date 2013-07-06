﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Vixen.Module;
using VixenModules.App.ColorGradients;
using VixenModules.App.Curves;
using Common.Controls.ColorManagement.ColorModels;

namespace VixenModules.Effect.Wipe {
	[DataContract]
	public class WipeData : ModuleDataModelBase {

		public WipeData() {
			Curve = new Curve();
			Curve.Points.Clear();
			Curve.Points.Add(1, 1);
			Curve.Points.Add(50, 100);
			Curve.Points.Add(100, 1);
			Direction = WipeDirection.Right;
			ColorGradient = new ColorGradient();
			PulseTime = 1000;
		}

		[DataMember]
		public ColorGradient ColorGradient { get; set; }

		[DataMember]
		public WipeDirection Direction{ get; set; }
		
		[DataMember]
		public Curve Curve { get; set; }

		[DataMember]
		public int PulseTime { get; set; }

 
		public override IModuleDataModel Clone() {
			return (WipeData)MemberwiseClone();
		}
	}

	
}
