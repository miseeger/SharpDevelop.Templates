using System;
using System.Collections.Generic;
using ${SolutionName}.Base.Model.Interfaces;

namespace ${SolutionName}.Base.Model
{

	public class ModuleConfigs : IModuleConfigs
	{

		public List<ModuleConfig> Modules { get; set; }

		public ModuleConfigs()
		{
			Modules = new List<ModuleConfig>();
		}

	}

}
