using System;
using ${SolutionName}.Base.Persistence.PetaPoco;

namespace ${SolutionName}.Base.Interfaces.Services
{

	public interface IDataService
	{
		IRepository Repo { get; }
	}

}
