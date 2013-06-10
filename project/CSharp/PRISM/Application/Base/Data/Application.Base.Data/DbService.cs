using ${SolutionName}.Base.Interfaces.Services;
using ${SolutionName}.Base.Services.Persistence.Interfaces;
using ${SolutionName}.Base.Services.Persistence.PetaPoco;

namespace ${SolutionName}.Base.Data
{
	
	public class DbService : IDataService
	{
	    public IRepository Repo { get; private set; }


	    public DbService()
		{
			Repo = new Repository(Names.ConnectionString);
		}
		
	}
	
}
