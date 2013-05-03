using System;
using System.Linq;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using <SolutionName>.Base.Model.Interfaces;
using <SolutionName>.Base.Mvvm.Interfaces;
using <SolutionName>.Base.Names;
using <SolutionName>.Base.Navigation;
using ${ProjectName}.ViewModels;
using ${ProjectName}.ViewModels.Interfaces;
using ${ProjectName}.Views;

// TODO: Replace all occurences of <SolutionName> in this Project with the name of your Solution!
// TODO: Insert references to the Projects "Solution".Base and "Solution".Base.Resource
// TODO: Insert the following Entry into modules.config and change Description and Order
//       <Module Description="" Order="999" StartModule="true/false">${ProjectName}</Module>

namespace ${ProjectName}.Modules.Contacts
{

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
			_container.RegisterType<IRibbonTabViewModel, RibbonTabViewModel>();
			_container.RegisterType<IViewModel1, ViewModel1>();
			_container.RegisterType<IViewModel2, ViewModel2>();
			
			// Registrations for ViewNavigation:
			_container.RegisterTypeForNavigation<View1>();
			_container.RegisterTypeForNavigation<View2>();

			// View Registration for ViewInjection:
			_container.RegisterType(typeof(IView), typeof(RibbonTabView), typeof(RibbonTabView).FullName);

			// View Injection:
			var viewRibbonTab = _container.Resolve(typeof(IView), typeof(RibbonTabView).FullName);
			_regionManager.Regions[RegionNames.RibbonTabRegion].Add(viewRibbonTab);
			
			if (_moduleConfigs.Modules.Any(m => (m.Name == Names.Module && m.StartModule)))
			{
				_regionManager.RequestNavigate(RegionNames.MainRegion,
			    	new Uri(typeof(View1).FullName, UriKind.Relative));
			}
		
		}

	}

}