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

	public class BackstageExitViewModel : ViewModelBase, IBackstageExitViewModel
	{
		
		private IDataService _dataService;
		private IBusinessService _businessService;
		private IAppResourceService _appResourceService;
		private IMessageBoxService _messageBoxService;
		
		public BackstageExitViewModel(IDataService DataService, 
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
			
			VmTitle = "Exit";
			VmImage = _appResourceService.GetPng32("door_open");
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