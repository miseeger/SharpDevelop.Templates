using System;
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
			VmTitle = Names.TaskTitle;
			VmImage = _appResourceService.GetPng16("users");
		}
		
		
		private RelayCommand _navigateCommand;
		public RelayCommand NavigateCommand
		{
			get { return _navigateCommand ?? (_navigateCommand = new RelayCommand(Navigate)); }
		}
		
		private void Navigate()
		{
			_regionManager.RequestNavigate(RegionNames.MainRegion, 
			                               new Uri(typeof(ModuleView).FullName, UriKind.Relative));
		}
		
	}

}