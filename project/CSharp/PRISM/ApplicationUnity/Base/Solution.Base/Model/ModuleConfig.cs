using System;
using ${SolutionName}.Base.Model.Interfaces;
using Microsoft.Practices.Prism.Modularity;

namespace ${SolutionName}.Base.Model
{

	public class ModuleConfig
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public int Order { get; set; }
		public bool StartModule { get; set; }
		public ModuleInfo Module { get; set; }
	}

}
