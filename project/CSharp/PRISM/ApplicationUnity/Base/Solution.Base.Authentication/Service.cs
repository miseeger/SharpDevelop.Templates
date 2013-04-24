using System;
using System.Collections.Generic;
using System.Windows;
using Microsoft.Practices.Unity;
using ${SolutionName}.Base.Authentication.Infrastructure;
using ${SolutionName}.Base.Enum;
using ${SolutionName}.Base.Interfaces.Services;
using ${SolutionName}.Base.Mvvm;
using ${SolutionName}.Base.Mvvm.Interfaces;

namespace ${SolutionName}.Base.Authentication
{

	public class Service : IAuthenticationService
	{

		private IDataService _dataService;
		private IMessageBoxService _messageBoxService;
		private IAppResourceService _resourceService;

		public IEnumerable<string> Servers { get; set; }
		public IEnumerable<string> Databases { get; set; }
		public string UserId { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public string MachineName { get; set; }
		public string DatabaseName { get; set; }
		public string IpAddress { get; set; }
		public DateTime Login { get; set; }
		public bool IsAuthenticated { get; set; }
		public AuthenticationType Type { get; set; }
		 
		
		public Service(IDataService DataService, IMessageBoxService MessageBoxService, IAppResourceService ResourceService)
		{
			_dataService = DataService;
			_messageBoxService = MessageBoxService;
			_resourceService = ResourceService;
			
			Type = AuthenticationType.SingleSignOn;
			Servers = new List<string>();
			Databases = new List<string>();
			UserId = "";
			UserName = System.Environment.UserName;
			Password = "";
			MachineName = System.Environment.MachineName;
			DatabaseName = "None";
			IpAddress = GetIp();
			Login = DateTime.Now;
			IsAuthenticated = false;

		}
		
		
		private string GetIp()
		{
			string strHostName = "";
			strHostName = System.Net.Dns.GetHostName();
			var ipEntry = System.Net.Dns.GetHostEntry(strHostName);
			var addr = ipEntry.AddressList;
			return addr[addr.Length-1].ToString();
		}
		

		public void Authenticate()
		{
			var loginViewModel = new LoginViewModel(this);
			
			loginViewModel.Database = "MyDatabase";
			loginViewModel.CancelImage = _resourceService.GetPng16("cross");
			loginViewModel.OkImage = _resourceService.GetPng16("tick");
			loginViewModel.VmImage = _resourceService.GetPngMisc("Login");

			IsAuthenticated = false;
			
			if (Type == AuthenticationType.Login || Type == AuthenticationType.DatabaseLogin) 
			{
			
				if (Type == AuthenticationType.DatabaseLogin) 
				{
					loginViewModel.Databases.Add("MyDatabase");
					loginViewModel.Databases.Add("YourDatabase");
				}

				ILoginViewDialog loginView= null;
				bool? loginResult = true;

				while (loginResult == true && IsAuthenticated == false)
				{
					
					loginView = (Type == AuthenticationType.DatabaseLogin)
								? (new DatabaseLogin()) as ILoginViewDialog
								: (new Login()) as ILoginViewDialog;
				
					loginViewModel.PwdBox = loginView.LoginPwdBox;
					
					loginViewModel.ViewDialog = loginView as IViewDialog;
					loginResult = loginView.ShowAsDialog();

					if (loginResult == true)
					{
						IsAuthenticated = (loginViewModel.PwdBox.Password == "12345!");
					}

					loginView = null;
				}

			}
			else
			{
				IsAuthenticated = true;
			}
			
			if (IsAuthenticated == true) 
			{
				Login = DateTime.Now;
				UserName = loginViewModel.UserName;
				DatabaseName = loginViewModel.Database;
				
				if (loginViewModel.PwdBox != null) 
				{
					Password = loginViewModel.PwdBox.Password;
				}
			}
			
		}

	}

}
