﻿using System;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Regions;
using ${SolutionName}.Base.Interfaces.Services;
using ${SolutionName}.Base.Mvvm;
using ${SolutionName}.Base.Mvvm.Interfaces;
using ${SolutionName}.Base.Names;
using ${SolutionName}.Base.Navigation;
using ${SolutionName}.Modules.Accounts.ViewModels.Interfaces;
using ${SolutionName}.Modules.Accounts.Views;

namespace ${SolutionName}.Modules.Accounts.ViewModels
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
			_regionManager.RequestNavigate(RegionNames.ModuleNavigationRegion, 
			                               new Uri(typeof(NavigationView).FullName, UriKind.Relative));
		}
		
	}

}