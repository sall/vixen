﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Runtime.Serialization;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Xml.Serialization;
using Vixen.Sys;
using Vixen.Data.Value;
using Vixen.Execution.Context;
using System.Collections.Concurrent;
using System.Threading;

namespace VixenModules.Preview.VixenPreview.Shapes
{
    [DataContract]
    public abstract class PreviewBitmapShape : PreviewBaseShape
	{
		[OnDeserialized]
		public void OnDeserialized(StreamingContext context)
		{
//			ResizePixels();
		}



        public override DisplayItemBaseControl GetSetupControl()
		{
			Shapes.DisplayItemBaseControl setupControl = null;
            
            if (GetType().ToString() == "VixenModules.Preview.VixenPreview.Shapes.PreviewLipSync") {
                setupControl = new Shapes.PreviewLipSyncControl(this);
            }

			return setupControl;
		}


	}
}