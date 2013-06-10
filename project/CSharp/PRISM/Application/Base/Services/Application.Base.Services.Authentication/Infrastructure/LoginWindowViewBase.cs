using System.Windows;
using System.Windows.Controls;
using ${SolutionName}.Base.Mvvm.Interfaces;

namespace ${SolutionName}.Base.Services.Authentication.Infrastructure
{

	public class LoginWindowViewBase: Window, ILoginViewDialog
	{

		public IViewModel ViewModel
		{
			get
			{
				return (IViewModel)DataContext;
			}
			set
			{
				DataContext = value;
			}
		}

		
		public bool? ShowAsDialog()
		{
			return ShowDialog();
		}
		

		public void ShowAsWindow()
		{
			Show();
		}
		
		
		private bool? _result;
		public bool? Result
		{
			get
			{
				return _result;
			}

			set
			{
				_result = value;
				DialogResult = value;
			}
		}

		
		private PasswordBox _loginPwdBox;
		public PasswordBox LoginPwdBox
		{
			get
			{
				return _loginPwdBox;
			}
			set
			{
				_loginPwdBox = value;
			}
		}

	}

}
