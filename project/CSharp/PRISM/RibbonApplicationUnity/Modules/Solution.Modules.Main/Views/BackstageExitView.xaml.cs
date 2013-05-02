using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

using ${SolutionName}.Modules.Main.ViewModels.Interfaces;

namespace ${SolutionName}.Modules.Main.Views
{

	
	public partial class BackstageExitView
	{
		public BackstageExitView(IBackstageExitViewModel viewModel)
		{
			InitializeComponent();
			viewModel.View = this;
		}
	}
}