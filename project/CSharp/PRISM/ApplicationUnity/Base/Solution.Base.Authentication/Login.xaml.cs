using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

using ${SolutionName}.Base.Mvvm.Interfaces;

namespace ${SolutionName}.Base.Authentication
{

	public partial class Login
	{

		public Login()
		{
			InitializeComponent();
			LoginPwdBox = pwdBox;
		}
		
	}

}