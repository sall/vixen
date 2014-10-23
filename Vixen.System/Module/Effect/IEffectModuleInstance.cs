﻿namespace Vixen.Module.Effect
{
	public interface IEffectModuleInstance : IEffect, IModuleInstance
	{
		 bool ForceGenerateVisualRepresentation { get;   }
		 bool HasRasterOverlay { get;  }
	}
}