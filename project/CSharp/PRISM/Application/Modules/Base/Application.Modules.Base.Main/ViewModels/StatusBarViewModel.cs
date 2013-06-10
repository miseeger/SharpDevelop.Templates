using System;
using ${SolutionName}.Base.Interfaces.Services;
using ${SolutionName}.Base.Mvvm;
using ${SolutionName}.Modules.Base.Main.ViewModels.Interfaces;

namespace ${SolutionName}.Modules.Base.Main.ViewModels
{

	public class StatusBarViewModel : ViewModelBase, IStatusBarViewModel
	{
		
		private IAuthenticationService _authenticationService;
		
		
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
				}
			}	
		}
		
		
		private string _computerName;
		public string ComputerName
		{
			get { return _computerName; }
			set
			{ 
				if(_computerName != value)
				{
					_computerName = value;
					RaisePropertyChanged(() => ComputerName);
				}
			}	
		}
		
		
		private DateTime _loginTime;
		public DateTime LoginTime
		{
			get { return _loginTime; }
			set
			{ 
				if(_loginTime != value)
				{
					_loginTime = value;
					LoginTimeStr = _loginTime.ToLongDateString();
					RaisePropertyChanged(() => LoginTime);
				}
			}	
		}
		
		
		private string _loginTimeStr;
		public string LoginTimeStr
		{
			get { return _loginTimeStr; }
			private set
			{ 
				if(_loginTimeStr != value)
				{
					_loginTimeStr = value;
					RaisePropertyChanged(() => LoginTimeStr);
				}
			}	
		}
		
		
		private string _databaseName;
		public string DatabaseName
		{
			get { return _databaseName; }
			set
			{ 
				if(_databaseName != value)
				{
					_databaseName = value;
					RaisePropertyChanged(() => DatabaseName);
				}
			}	
		}
		
		
		public StatusBarViewModel(IAuthenticationService authenticationService)
		{
			_authenticationService = authenticationService;
			UserName = _authenticationService.UserName;
			ComputerName = _authenticationService.MachineName;
			DatabaseName = _authenticationService.DatabaseName;
			LoginTime = _authenticationService.Login;
		}
		
	}

}