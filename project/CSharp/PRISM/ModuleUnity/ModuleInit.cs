using System;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using <SolutionName>.Base.Names;
using <SolutionName>.Base.Navigation;
using <SolutionName>.Base.Mvvm.Interfaces;
using ${ProjectName}.ViewModels;
using ${ProjectName}.ViewModels.Interfaces;
using ${ProjectName}.Views;

namespace ${ProjectName}
{

	/// <summary>
	/// (Initializing) Module class for ${ProjectName}
	/// having UnityContainer and Regionmanager injected
	/// </summary>
	
	// Module will be initialized "as available" (by default)
	// Use this attribute when the module is rarely used:
	//[Module(ModuleName="${ProjectName}", OnDemand=true)] 
	[Module(ModuleName="${ProjectName}")]
	public class ModuleInit : IModule
	{
		
		IUnityContainer _container;
		IRegionManager _regionManager;
		

		public ModuleInit(IUnityContainer container, IRegionManager regionManager)
		{
			// Get conatiner and region manager
			_container = container;
			_regionManager = regionManager;
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
			
			// TODO: Replace all occurences of <SolutionName> in this Project with the name of your Solution!
			// TODO: Insert references to the Projects "Solution".Base and "Solution".Base.Resource

		}

	}

}