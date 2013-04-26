using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Xml.Linq;
using ${SolutionName}.Base.Model;
using ${SolutionName}.Base.Model.Interfaces;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.UnityExtensions;
using ${SolutionName}.Base.Events;

namespace ${SolutionName}
{

	public class SortedModuleInitializer : IModuleInitializer
	{
		
		private ILoggerFacade _logger; 
		private IEventAggregator _eventAggregator;
		
		public bool initialModuleLoadCompleted = false;
		public IModuleInitializer defaultInitializer = null;
		public ModuleConfigs moduleConfigs;
		

		public SortedModuleInitializer(IUnityContainer Container, ILoggerFacade Logger, IEventAggregator EventAggregator)
		{
			_logger = Logger;
			_eventAggregator = EventAggregator;
			defaultInitializer = Container.Resolve<IModuleInitializer>("defaultModuleInitializer");
			moduleConfigs = LoadModuleConfig();
			Container.RegisterInstance<IModuleConfigs>("ModuleConfigs", moduleConfigs);
		}
			

		public ModuleConfigs LoadModuleConfig()
		{
			ModuleConfigs result = new ModuleConfigs();

			try 
			{
				result.Modules = (from module in XElement.Load(@".\modules.config").Elements("Module")
						  		  select new ModuleConfig
									 {
										 Name = (string)module.Value
										 , Description = (string)module.Attribute("Description").Value
										 , Order = Convert.ToInt32(module.Attribute("Order").Value.ToString())
									 	 , StartModule = (string)module.Attribute("StartModule").Value.ToUpper() == "TRUE" ? true : false
									 }).ToList();
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
			if(initialModuleLoadCompleted || moduleInfo.ModuleName.EndsWith("Modules.Splash"))
			{
				defaultInitializer.Initialize(moduleInfo);
				return;
			}
			
			moduleConfigs.Modules.FirstOrDefault(mc => mc.Name == moduleInfo.ModuleName).Module = moduleInfo;
			
			// All modules pre-loaded?
			if (!(moduleConfigs.Modules.Any(mc => mc.Module == null)))
			{
				// Sort modules
		    	moduleConfigs.Modules = moduleConfigs.Modules.OrderBy(mc => mc.Order).ToList();

				foreach (var config in moduleConfigs.Modules)
				{
					_eventAggregator.GetEvent<SplashInfoUpdateEvent>().Publish(new SplashInfoUpdateEvent {Info = config.Description});
					Thread.Sleep(1000); // Uncomment as you need ;-)
					defaultInitializer.Initialize(config.Module);
				}

				initialModuleLoadCompleted = true;
			}
		}
	
	}

}
