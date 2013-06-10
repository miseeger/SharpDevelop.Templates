using System.Windows.Controls;
using ${SolutionName}.Base.Mvvm.Interfaces;

namespace ${SolutionName}.Base.Services.Authentication.Infrastructure
{

	public interface ILoginViewDialog : IViewDialog
	{
		PasswordBox LoginPwdBox { get; set; }
	}

}
