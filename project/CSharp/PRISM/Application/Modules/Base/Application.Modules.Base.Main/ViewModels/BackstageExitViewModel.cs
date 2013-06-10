using System.Windows;
using ${SolutionName}.Base.Interfaces.Services;
using ${SolutionName}.Base.Mvvm;
using ${SolutionName}.Modules.Base.Main.ViewModels.Interfaces;

namespace ${SolutionName}.Modules.Base.Main.ViewModels
{

	public class BackstageExitViewModel : ViewModelBase, IBackstageExitViewModel
	{
		
		private IAppResourceService _appResourceService;
		private IMessageBoxService _messageBoxService;
		
		public BackstageExitViewModel(IAppResourceService appResourceService, 
		                         	  IMessageBoxService messageBoxService)
		{
			_appResourceService = appResourceService;
			_messageBoxService = messageBoxService;

			
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