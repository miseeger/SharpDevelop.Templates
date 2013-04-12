using System;
using ${SolutionName}.Base.Interfaces.Services;
using ${SolutionName}.Base.Names;
using ${SolutionName}.Base.Persistence.PetaPoco;

namespace ${SolutionName}.Base.Data
{
	
	public class DbService : IDataService
	{
		
		IRepository _repo;
		
		public IRepository Repo {
			
			get
			{ 
				return _repo; 
			}
			
			private set
			{ 
				_repo = value; 
			}
		
		}
		

		public DbService()
		{
			Repo = new Repository(Names.ConnectionString);
		}
		
	}
	
}
