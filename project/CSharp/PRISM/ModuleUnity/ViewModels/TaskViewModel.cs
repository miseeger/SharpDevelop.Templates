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
		
		private bool _isActive;
		public bool IsActive
		{
			get { return _isActive; }
			set
			{
				if(_isActive != value)
				{
					_isActive = value;
					RaisePropertyChanged(() => IsActive);
				}
			}
		}

		
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
			_regionManager.RequestNavigate(RegionNames.ModuleNavigationRegion, 
			                               new Uri(typeof(NavigationView).FullName, UriKind.Relative));
		}
		
	}

}