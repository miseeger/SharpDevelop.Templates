using Microsoft.Practices.Prism.Modularity;
using ${SolutionName}.Base.Data.Model.Interfaces.System;

namespace ${SolutionName}.Base.Data.Model.System
{
 
	public class ModuleConfig : IModuleConfig
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public int Order { get; set; }
		public bool StartModule { get; set; }
		public ModuleInfo Module { get; set; }
	}

}
