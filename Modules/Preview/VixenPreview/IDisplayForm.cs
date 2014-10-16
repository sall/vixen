﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VixenModules.Preview.VixenPreview {
	public interface IDisplayForm : IDisposable {
		VixenPreviewData Data { get; set; }
		void Setup();
		void Close();
		void UpdatePreview();
	}
}
