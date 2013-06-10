using System;
using System.Collections.Generic;
using ${SolutionName}.Base.Data.Enums.System;
using ${SolutionName}.Base.Interfaces.Services;
using ${SolutionName}.Base.Services.Authentication.Infrastructure;

namespace ${SolutionName}.Base.Services.Authentication
{

	public class AuthenticationService : IAuthenticationService
	{
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
		 
		
		public AuthenticationService(IAppResourceService resourceService)
		{
		    _resourceService = resourceService;
			
			Type = AuthenticationType.SingleSignOn;
			Servers = new List<string>();
			Databases = new List<string>();
			UserId = "";
			UserName = Environment.UserName;
			Password = "";
			MachineName = Environment.MachineName;
			DatabaseName = "None";
			IpAddress = GetIp();
			Login = DateTime.Now;
			IsAuthenticated = false;

		}
		
		
		private string GetIp()
		{
		    var strHostName = System.Net.Dns.GetHostName();
			var ipEntry = System.Net.Dns.GetHostEntry(strHostName);
			var addr = ipEntry.AddressList;
			return addr[addr.Length-1].ToString();
		}
		

		public void Authenticate()
		{
			var loginViewModel = new LoginViewModel(this)
			    {
			        Database = "MyDatabase",
			        CancelImage = _resourceService.GetPng16("cross"),
			        OkImage = _resourceService.GetPng16("tick"),
			        VmImage = _resourceService.GetPngMisc("Login")
			    };

		    IsAuthenticated = false;
			
			if (Type == AuthenticationType.Login || Type == AuthenticationType.DatabaseLogin) 
			{
			
				if (Type == AuthenticationType.DatabaseLogin) 
				{
					loginViewModel.Databases.Add("MyDatabase");
					loginViewModel.Databases.Add("YourDatabase");
				}

			    bool? loginResult = true;

				while (loginResult == true && IsAuthenticated == false)
				{
					
					var loginView = (Type == AuthenticationType.DatabaseLogin)
					                                 ? new DatabaseLogin()
					                                 : (new Login()) as ILoginViewDialog;
				
					loginViewModel.PwdBox = loginView.LoginPwdBox;
					
					loginViewModel.ViewDialog = loginView;
					loginResult = loginView.ShowAsDialog();

					if (loginResult == true)
					{
						IsAuthenticated = (loginViewModel.PwdBox.Password == "12345!");
					}
				}

			}
			else
			{
				IsAuthenticated = true;
			}
			
			if (IsAuthenticated) 
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
