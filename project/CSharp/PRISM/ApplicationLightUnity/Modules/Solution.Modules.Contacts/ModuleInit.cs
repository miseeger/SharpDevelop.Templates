using System;
using System.Linq;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using ${SolutionName}.Base.Model.Interfaces;
using ${SolutionName}.Base.Mvvm.Interfaces;
using ${SolutionName}.Base.Names;
using ${SolutionName}.Base.Navigation;
using ${SolutionName}.Modules.Contacts.ViewModels;
using ${SolutionName}.Modules.Contacts.ViewModels.Interfaces;
using ${SolutionName}.Modules.Contacts.Views;

// TODO: Replace all occurences of <SolutionName> in this Project with the name of your Solution!
// TODO: Insert references to the Projects "Solution".Base and "Solution".Base.Resource
// TODO: Insert the following Entry into modules.config and change Description and Order
//       <Module Description="" Order="999" StartModule="true/false">${ProjectName}</Module>

namespace ${SolutionName}.Modules.Contacts
{

	/// <summary>
	/// (Initializing) Module class for ${ProjectName}
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
			_container.RegisterType<IModuleViewModel, ModuleViewModel>();
			
			// Registrations for ViewNavigation:
			_container.RegisterTypeForNavigation<ModuleView>();

			// View Registration for ViewInjection:
			_container.RegisterType(typeof(IView), typeof(TaskView), typeof(TaskView).FullName);

			// View Injection:
			var viewTask = _container.Resolve(typeof(IView), typeof(TaskView).FullName);
			_regionManager.Regions[RegionNames.ModuleNavigationRegion].Add(viewTask);
			
			if (_moduleConfigs.Modules.Any(m => (m.Name == Names.Module && m.StartModule)))
			{
				_regionManager.RequestNavigate(RegionNames.MainRegion,
			    	new Uri(typeof(ModuleView).FullName, UriKind.Relative));
				((IView)viewTask).ViewModel.IsActive = true;
			}
		}

	}

}