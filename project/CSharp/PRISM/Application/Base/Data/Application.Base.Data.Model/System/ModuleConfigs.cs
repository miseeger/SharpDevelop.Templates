using System.Collections.Generic;
using ${SolutionName}.Base.Data.Model.Interfaces.System;

namespace ${SolutionName}.Base.Data.Model.System
{

	public class ModuleConfigs : IModuleConfigs
	{

		public List<IModuleConfig> Modules { get; set; }

		public ModuleConfigs()
		{
			Modules = new List<IModuleConfig>();
		}

	}

}
