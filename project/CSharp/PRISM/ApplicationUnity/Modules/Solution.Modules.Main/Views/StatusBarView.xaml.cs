using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

using ${SolutionName}.Base.Mvvm;
using ${SolutionName}.Modules.Main.ViewModels.Interfaces;

namespace ${SolutionName}.Modules.Main.Views
{

	public partial class StatusBarView : UserControlViewBase
	{

		public StatusBarView(IStatusBarViewModel viewModel)
		{
			InitializeComponent();
			viewModel.View = this;
		}

	}

}