using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;
using ${SolutionName}.Base.Interfaces.Services;
using ${SolutionName}.Base.Mvvm;
using ${SolutionName}.Base.Navigation;
using ${SolutionName}.Modules.Main.ViewModels.Interfaces;

namespace ${SolutionName}.Modules.Main.ViewModels
{

	public class FileMenuViewModel : ViewModelBase, IFileMenuViewModel
	{
		
		private IDataService _dataService;
		private IBusinessService _businessService;
		private IAppResourceService _appResourceService;
		private IMessageBoxService _messageBoxService;
		
		private NavigationItem _navExit;
		public NavigationItem NavExit
		{
			get { return _navExit; }
			set
			{ 
				if(_navExit != value)
				{
					_navExit = value;
					RaisePropertyChanged(() => NavExit);
				}
			}	
		}
		
		
		public FileMenuViewModel(IDataService DataService, 
		                         IBusinessService BusinessService,
		                         IAppResourceService AppResourceService, 
		                         IMessageBoxService MessageBoxService)
		{
			_dataService = DataService;
			_businessService = BusinessService;
			_appResourceService = AppResourceService;
			_messageBoxService = MessageBoxService;

			
			// DataService-Test:
			// var list = _dataService.Repo.Query<Address>();
			
			NavExit = new NavigationItem {
											  Caption = "Exit"
										      , ItemImage = _appResourceService.GetPng16("door-open-out")
										 };
		}

		
		private RelayCommand _exitCommand;
		public RelayCommand ExitCommand
		{
			get { return _exitCommand ?? (_exitCommand = new RelayCommand(Exit)); }
		}
		
		private void Exit()
		{
			if (_messageBoxService.Question("Are you sure to exit the application?"))
		    {
				Application.Current.Shutdown();
			}
		}

	}

}