using System;
using System.Collections.Generic;
using ${SolutionName}.Base.Enums;

namespace ${SolutionName}.Base.Interfaces.Services
{

	public interface IAuthenticationService
	{
		IEnumerable<string> Servers { get; set;}
		IEnumerable<string> Databases { get; set;}

		string UserId { get; set; }
		string UserName { get; set; }
		string Password { get; set; }
		string MachineName { get; set; }
		string DatabaseName { get; set; }
		string IpAddress { get; set; }
		DateTime Login { get; set; }
		bool IsAuthenticated { get; set; }
		AuthenticationType Type { get; set; }
		
		void Authenticate();
	}

}
