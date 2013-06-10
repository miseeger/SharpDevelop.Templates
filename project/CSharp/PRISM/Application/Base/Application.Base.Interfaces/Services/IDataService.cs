using ${SolutionName}.Base.Services.Persistence.Interfaces;

namespace ${SolutionName}.Base.Interfaces.Services
{

	public interface IDataService
	{
		IRepository Repo { get; }
	}

}
