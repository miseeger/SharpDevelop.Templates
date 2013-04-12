using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

using ${SolutionName}.Modules.Accounts.ViewModels.Interfaces;

namespace ${SolutionName}.Modules.Accounts.Views
{

	public partial class View1
	{

		public View1(IViewModel1 viewModel)
		{
			InitializeComponent();
			ViewModel = viewModel;
		}

	}

}