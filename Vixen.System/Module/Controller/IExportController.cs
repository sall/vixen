﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vixen.Module.Controller
{
    public interface IExportController
    {
        Dictionary<string,string> ExportFileTypes { get; }
        string OutFileName { get; set; }
    }
}
