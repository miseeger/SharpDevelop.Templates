using System;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Prism.Logging;
using ${SolutionName}.Base.Interfaces.Services;

namespace ${SolutionName}.Base.Business
{
	/// <summary>
	/// Description of Service.
	/// </summary>
	public class Service : IBusinessService
	{

		IDataService _dataService;
		public IDataService DataService 
		{
			get 
			{ 
				return _dataService; 
			}
			
			private set 
			{ 
				_dataService = value; 
			}
		
		}
		
		ILoggerFacade _logger;
		public ILoggerFacade Logger 
		{
			get 
			{ 
				return _logger; 
			}
			
			private set 
			{ 
				_logger = value; 
			}
		}


		public Service()
		{
			DataService = ServiceLocator.Current.GetInstance<IDataService>();
			Logger = ServiceLocator.Current.GetInstance<ILoggerFacade>();
			Logger.Log("${SolutionName} BusinessService was successfully retrieved from container.",
				Category.Info, Priority.None);
			Logger.Log("${SolutionName} Logger for BusinessService was successfully retrieved from container.",
				Category.Info, Priority.None);			
		}

	}

}
