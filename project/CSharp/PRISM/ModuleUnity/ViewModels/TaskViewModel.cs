using System;
using System.Linq;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Regions;
using <SolutionName>.Base.Interfaces.Services;
using <SolutionName>.Base.Mvvm;
using <SolutionName>.Base.Mvvm.Interfaces;
using <SolutionName>.Base.Names;
using <SolutionName>.Base.Navigation;
using ${ProjectName}.ViewModels.Interfaces;
using ${ProjectName}.Views;

namespace ${ProjectName}.ViewModels
{

	public class TaskViewModel : ViewModelBase, ITaskViewModel
	{

		private IRegionManager _regionManager;
		private IAppResourceService _appResourceService;
		
		public TaskViewModel(IRegionManager regionManager, IAppResourceService appResourceService)
		{
			_regionManager = regionManager;
			_appResourceService = appResourceService;
			VmTitle = Names.TaskItem;
			VmImage = _appResourceService.GetPng16("users");
		}
		
		
		private RelayCommand _navigateCommand;
		public RelayCommand NavigateCommand
		{
			get { return _navigateCommand ?? (_navigateCommand = new RelayCommand(Navigate)); }
		}
		
		private void Navigate()
		{
			var moduleNavigation = new Uri(typeof(NavigationView).FullName, UriKind.Relative);
			var initialViewNavigation = new Uri(typeof(View1).FullName, UriKind.Relative);
			var moduleNavRegion = _regionManager.Regions[RegionNames.ModuleNavigationRegion];
			
			foreach (var view in moduleNavRegion.Views)
			{
				moduleNavRegion.Remove(view);
			}
			
			_regionManager.RequestNavigate(RegionNames.ModuleNavigationRegion, moduleNavigation);
			_regionManager.RequestNavigate(RegionNames.MainRegion, initialViewNavigation);
		}
		
	}

}