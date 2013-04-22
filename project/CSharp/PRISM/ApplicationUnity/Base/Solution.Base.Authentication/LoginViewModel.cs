using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Practices.ServiceLocation;
using $<SolutionName>.Base.Interfaces.Services;
using $<SolutionName>.Base.Mvvm;

namespace $<SolutionName>.Base.Authentication
{

	public class LoginViewModel : ViewModelBase
	{
	
		private IAuthenticationService _authenticationService;
		
		public PasswordBox PwdBox { get; set; }
		
		public ImageSource OkImage { get; set; }
		public ImageSource CancelImage { get; set; }
		
		private ObservableCollection<string> _databases;
		public ObservableCollection<string> Databases
		{
			get { return _databases; }
			set
			{
				if(_databases != value)
				{
					_databases = value;
					RaisePropertyChanged(() => Databases);
				}
			}
		}
		
		private string _userName;
		public string UserName
		{
			get { return _userName; }
			set
			{
				if(_userName != value)
				{
					_userName = value;
					RaisePropertyChanged(() => UserName);
					RaiseCanExecutes();
				}
			}
		}

		private string _database;
		public string Database
		{
			get { return _database; }
			set
			{
				if(_database != value)
				{
					_database = value;
					RaisePropertyChanged(() => Database);
					RaiseCanExecutes();
				}
			}
		}
		
		
		public LoginViewModel(IAuthenticationService AuthenticationService)
		{
			_authenticationService = AuthenticationService;
			Databases = new ObservableCollection<string>();
			UserName = _authenticationService.UserName;
		}
		
		
		private void RaiseCanExecutes()
		{	
			if (_okCommand != null)
			{
				_okCommand.RaiseCanExecuteChanged();
			}

			if (_cancelCommand != null)
			{
				_cancelCommand.RaiseCanExecuteChanged();				
			}
		}
		
		
		private RelayCommand _okCommand;
		public ICommand OkCommand
		{
		    get { return _okCommand ?? (_okCommand = new RelayCommand(Ok, CanExecuteOk)); }
		}
		
		private void Ok()
		{
		    _authenticationService.UserName = UserName;
		    _authenticationService.DatabaseName = Database;
		    _authenticationService.Password = PwdBox.Password;
		    ViewDialog.Result = true;
		}
		
		private bool CanExecuteOk()
		{
			return (UserName != "" && PwdBox.Password != "" && Database != "");
		}
		
		
		private RelayCommand _cancelCommand;
		public ICommand CancelCommand
		{
		    get { return _cancelCommand ?? (_cancelCommand = new RelayCommand(Cancel, CanExecuteCancel)); }
		}
		
		private void Cancel()
		{
		    _authenticationService.UserName = String.Empty;
		    _authenticationService.DatabaseName = String.Empty;
		    _authenticationService.Password = String.Empty;
   		    ViewDialog.Result = false;
		}
		
		private bool CanExecuteCancel()
		{
		    return true;
		}
	
	}
}
