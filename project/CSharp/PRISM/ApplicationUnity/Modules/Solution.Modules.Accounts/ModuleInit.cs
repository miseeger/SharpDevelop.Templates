using System;
using System.Linq;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using ${SolutionName}.Base.Model.Interfaces;
using ${SolutionName}.Base.Mvvm.Interfaces;
using ${SolutionName}.Base.Names;
using ${SolutionName}.Base.Navigation;
using ${SolutionName}.Modules.Accounts.ViewModels;
using ${SolutionName}.Modules.Accounts.ViewModels.Interfaces;
using ${SolutionName}.Modules.Accounts.Views;

namespace ${SolutionName}.Modules.Accounts
{

	/// <summary>
	/// (Initializing) Module class for ${SolutionName}.Modules.Accounts
	/// having UnityContainer and Regionmanager injected
	/// </summary>
	
	// Module will be initialized "as available" (by default)
	// Use this attribute when the module is rarely used:
	//[Module(ModuleName=Names.Module, OnDemand=true)] 
	[Module(ModuleName=Names.Module)]
	public class ModuleInit : IModule
	{
		
		IUnityContainer _container;
		IRegionManager _regionManager;
		IModuleConfigs _moduleConfigs;
		

		public ModuleInit(IUnityContainer container, IRegionManager regionManager)
		{
			_container = container;
			_regionManager = regionManager;
			_moduleConfigs = container.Resolve<IModuleConfigs>("ModuleConfigs");
		}
		

		public void Initialize()
		{
			// ViewModel Registration:
			_container.RegisterType<ITaskViewModel, TaskViewModel>();
			_container.RegisterType<INavigationViewModel, NavigationViewModel>();
			_container.RegisterType<IViewModel1, ViewModel1>();
			_container.RegisterType<IViewModel2, ViewModel2>();			
			
			// Registrations for ViewNavigation:
			_container.RegisterTypeForNavigation<NavigationView>();
			_container.RegisterTypeForNavigation<View1>();
			_container.RegisterTypeForNavigation<View2>();

			// View Registration for ViewInjection:
			_container.RegisterType(typeof(IView), typeof(TaskView), typeof(TaskView).FullName);

			// View Injection:
			var viewTask = _container.Resolve(typeof(IView), typeof(TaskView).FullName);
			_regionManager.Regions[RegionNames.ModuleRegion].Add(viewTask);
			
			if (_moduleConfigs.Modules.Any(m => (m.Name == Names.Module && m.StartModule)))
			{
				_regionManager.RequestNavigate(RegionNames.ModuleNavigationRegion,
			    	new Uri(typeof(NavigationView).FullName, UriKind.Relative));
				((IView)viewTask).ViewModel.IsActive = true;
			}
		}

	}

}