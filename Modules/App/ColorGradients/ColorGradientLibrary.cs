﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vixen.Module;
using Vixen.Module.App;

namespace VixenModules.App.ColorGradients
{
	internal class ColorGradientLibrary : AppModuleInstanceBase, IEnumerable<KeyValuePair<string, ColorGradient>>
	{
		private ColorGradientLibraryStaticData _data;

		public override void Loading()
		{
		}

		public override void Unloading()
		{
		}

		public override Vixen.Sys.IApplication Application
		{
			set { }
		}

		public override IModuleDataModel StaticModuleData
		{
			get { return _data; }
			set { _data = value as ColorGradientLibraryStaticData; }
		}


		public Dictionary<string, ColorGradient> Library
		{
			get { return _data.Library; }
		}

		public bool Contains(string name)
		{
			return Library.ContainsKey(name);
		}

		public ColorGradient GetColorGradient(string name)
		{
			if (Library.ContainsKey(name))
				return Library[name];
			else
				return null;
		}

		public bool AddColorGradient(string name, ColorGradient cg)
		{
			if (name == string.Empty)
				return false;

			bool inLibrary = Contains(name);
			if (inLibrary) {
				Library[name].IsCurrentLibraryGradient = false;
			}
			cg.IsCurrentLibraryGradient = true;
			cg.LibraryReferenceName = string.Empty;
			Library[name] = cg;
			return inLibrary;
		}

		public bool RemoveColorGradient(string name)
		{
			if (!Contains(name))
				return false;

			Library[name].IsCurrentLibraryGradient = false;
			Library.Remove(name);

			return true;
		}

		public bool EditLibraryItem(string name)
		{
			ColorGradient cg = GetColorGradient(name);
			if (cg == null)
				return false;

			ColorGradientEditor editor = new ColorGradientEditor(cg, false, null);
			editor.LibraryItemName = name;

			if (editor.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
				RemoveColorGradient(name);
				AddColorGradient(name, editor.Gradient);
				return true;
			}

			return false;
		}

		public IEnumerator<KeyValuePair<string, ColorGradient>> GetEnumerator()
		{
			return Library.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return Library.GetEnumerator();
		}
	}
}