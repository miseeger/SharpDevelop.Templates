using Microsoft.Practices.Prism.Modularity;

namespace ${SolutionName}.Base.Data.Model.Interfaces.System
{
	public interface IModuleConfig
	{
		string Name { get; set; }
		string Description { get; set; }
		int Order { get; set; }
		bool StartModule { get; set; }
		ModuleInfo Module { get; set; }
	}
}
