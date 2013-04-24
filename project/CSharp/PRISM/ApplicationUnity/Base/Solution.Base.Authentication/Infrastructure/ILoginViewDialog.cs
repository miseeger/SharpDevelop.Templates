using System;
using System.Windows.Controls;
using ${SolutionName}.Base.Mvvm.Interfaces;

namespace ${SolutionName}.Base.Authentication.Infrastructure
{

	public interface ILoginViewDialog : IViewDialog
	{
		PasswordBox LoginPwdBox { get; set; }
	}

}
