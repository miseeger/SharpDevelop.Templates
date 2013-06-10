using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Xml.Linq;
using ${SolutionName}.Base.Data.Model.System;
using ${SolutionName}.Base.Data.Model.Interfaces.System;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using ${SolutionName}.Base.Events;

namespace ${SolutionName}
{

	public class SortedModuleInitializer : IModuleInitializer
	{
		
		private ILoggerFacade _logger; 
		private IEventAggregator _eventAggregator;
		
		public bool InitialModuleLoadCompleted = false;
		public IModuleInitializer DefaultInitializer = null;
		public ModuleConfigs ModuleConfigs;
		

		public SortedModuleInitializer(IUnityContainer container, ILoggerFacade logger, IEventAggregator eventAggregator)
		{
			_logger = logger;
			_eventAggregator = eventAggregator;
			DefaultInitializer = container.Resolve<IModuleInitializer>("defaultModuleInitializer");
			ModuleConfigs = LoadModuleConfig();
			container.RegisterInstance<IModuleConfigs>("ModuleConfigs", ModuleConfigs);
		}
			

		public ModuleConfigs LoadModuleConfig()
		{
			ModuleConfigs result = new ModuleConfigs();

			try 
			{
				result.Modules = new List<IModuleConfig>((from module in XElement.Load(@".\modules.config").Elements("Module")
				                           select new ModuleConfig
				                               {
				                                   Name = module.Value
				                                   , Description = module.Attribute("Description").Value
				                                   , Order = Convert.ToInt32(module.Attribute("Order").Value)
				                                   , StartModule = module.Attribute("StartModule").Value.ToUpper() == "TRUE"
				                               }).ToList());
			} 
			catch (Exception) 
			{
				_logger.Log("SortedModuleInitializer: Unable to create modules configuraiton list. " +
				            "Check modules.config in application path.", Category.Exception, Priority.High);
				throw;
			}
			
  			return result;
		}

		
		public void Initialize(ModuleInfo moduleInfo)
		{
			if(InitialModuleLoadCompleted || moduleInfo.ModuleName.EndsWith("Modules.Splash"))
			{
				DefaultInitializer.Initialize(moduleInfo);
				return;
			}
			
			ModuleConfigs.Modules.FirstOrDefault(mc => mc.Name == moduleInfo.ModuleName).Module = moduleInfo;
			
			// All modules pre-loaded?
			if (!(ModuleConfigs.Modules.Any(mc => mc.Module == null)))
			{
				// Sort modules
		    	ModuleConfigs.Modules = ModuleConfigs.Modules.OrderBy(mc => mc.Order).ToList();

				foreach (var config in ModuleConfigs.Modules)
				{
					_eventAggregator.GetEvent<SplashInfoUpdateEvent>().Publish(new SplashInfoUpdateEvent {Info = config.Description});
					Thread.Sleep(1000); // Uncomment as you need ;-)
					DefaultInitializer.Initialize(config.Module);
				}

				InitialModuleLoadCompleted = true;
			}
		}
	
	}

}
