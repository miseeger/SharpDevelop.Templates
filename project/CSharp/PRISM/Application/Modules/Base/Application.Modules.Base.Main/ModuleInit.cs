using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using ${SolutionName}.Base.Mvvm.Interfaces;
using ${SolutionName}.Base.Names;
using ${SolutionName}.Modules.Base.Main.ViewModels;
using ${SolutionName}.Modules.Base.Main.ViewModels.Interfaces;
using ${SolutionName}.Modules.Base.Main.Views;

namespace ${SolutionName}.Modules.Base.Main
{

	// Module will be initialized "as available" (by default)
	// Use this attribute when the module is rarely used:
	//[Module(ModuleName="${SolutionName}.Modules.Main", OnDemand=true)] 
	[Module(ModuleName="${SolutionName}.Modules.Main")]
	public class ModuleInit : IModule
	{
		
		IUnityContainer _container;
		IRegionManager _regionManager;
		

		public ModuleInit(IUnityContainer container, IRegionManager regionManager)
		{
			_container = container;
			_regionManager = regionManager;
		}
		

		public void Initialize()
		{

			// ViewModel Registration:
			_container.RegisterType<IBackstageExitViewModel, BackstageExitViewModel>();
			_container.RegisterType<IStatusBarViewModel, StatusBarViewModel>();
			
			// View Registration for ViewInjection:
			_container.RegisterType(typeof(IView), typeof(BackstageExitView), typeof(BackstageExitView).FullName);
			_container.RegisterType(typeof(IView), typeof(StatusBarView), typeof(StatusBarView).FullName);

			// View Injection:
			var vExitItem = _container.Resolve(typeof(IView), typeof(BackstageExitView).FullName);
			var vStatusBar = _container.Resolve(typeof(IView), typeof(StatusBarView).FullName);
			_regionManager.Regions[RegionNames.RibbonBackstageRegion].Add(vExitItem);
			_regionManager.Regions[RegionNames.StatusbarRegion].Add(vStatusBar);
			
		}

	}

}