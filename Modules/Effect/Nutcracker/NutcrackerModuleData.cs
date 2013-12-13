﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Drawing;
using Common.Controls.ColorManagement.ColorModels;
using Vixen.Module;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using VixenModules.App.Curves;
using ZedGraph;

namespace VixenModules.Effect.Nutcracker
{
	[DataContract, Serializable]
	public class NutcrackerModuleData : ModuleDataModelBase
	{
		private NutcrackerData _nutcrackerData = null;
		
		public NutcrackerModuleData()
		{
			_nutcrackerData = new NutcrackerData();
			
		}

		[DataMember]
		public NutcrackerData NutcrackerData
		{
			get
			{
				if (_nutcrackerData == null) {
					_nutcrackerData = new NutcrackerData();
				}
				return _nutcrackerData;
			}
			set { _nutcrackerData = value; }
		}
		

		public override IModuleDataModel Clone()
		{
			Console.WriteLine("Clone NutcrackerModuleData");
			using (MemoryStream stream = new MemoryStream()) {
				BinaryFormatter formatter = new BinaryFormatter();
				formatter.Serialize(stream, this);
				stream.Position = 0;
				return (NutcrackerModuleData) formatter.Deserialize(stream);
			}
		}
	}
}