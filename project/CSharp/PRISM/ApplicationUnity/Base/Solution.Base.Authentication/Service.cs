using System;
using System.Collections.Generic;
using System.Windows;
using Microsoft.Practices.Unity;
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

		public IEnumerable<string> Servers {get;  set;}
		public IEnumerable<string> Databases {get;  set;}
		public string UserId {get;  set;}
		public string UserName {get;  set;}
		public string Password {get;  set;}
		public string MachineName {get;  set;}
		public string DatabaseName {get;  set;}
		public string IpAddress {get;  set;}
		public DateTime Login {get;  set;}
		public bool IsAuthenticated {get;  set;}
		 
		
		public Service(IDataService DataService, IMessageBoxService MessageBoxService, IAppResourceService ResourceService)
		{
			_dataService = DataService;
			_messageBoxService = MessageBoxService;
			_resourceService = ResourceService;
			
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
			
			loginViewModel.CancelImage = _resourceService.GetPng16("cross");
			loginViewModel.OkImage = _resourceService.GetPng16("tick");
			loginViewModel.VmImage = _resourceService.GetPngMisc("Login");

			// Fill Database-List and default-Setting
			loginViewModel.Databases.Add("MyDatabase");
			loginViewModel.Databases.Add("YourDatabase");
			loginViewModel.Database = "MyDatabase";
			
			bool? loginResult = true;
			IsAuthenticated = false;

			while (loginResult == true && IsAuthenticated == false)
			{
				var loginView = new DatabaseLogin();
				loginViewModel.ViewDialog = loginView;
				loginViewModel.PwdBox = loginView.pwdBox;
				
				loginResult = loginView.ShowAsDialog();

				if (loginResult == true)
				{
					IsAuthenticated = (loginViewModel.PwdBox.Password == "12345!");
				}

				loginView = null;
			}
			
			if (IsAuthenticated == true) 
			{
				Login = DateTime.Now;
				UserName = loginViewModel.UserName;
				DatabaseName = loginViewModel.Database;
			}
			
		}

	}

}
