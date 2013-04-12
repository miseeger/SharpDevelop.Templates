using System;
using ${SolutionName}.Base.Interfaces.Services;
using ${SolutionName}.Base.Persistence.PetaPoco;

namespace ${SolutionName}.Base.Data
{
 
	public class XmlService : IDataService
	{
		public XmlService()
		{
		}
		
		public IRepository Repo
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
