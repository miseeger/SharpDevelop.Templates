using System; 
using Microsoft.Practices.Prism.Logging;

namespace ${SolutionName}.Base.Interfaces.Services
{
	
	public interface IBusinessService
	{
		IDataService DataService { get; }
		ILoggerFacade Logger { get; }
	}
	
}
