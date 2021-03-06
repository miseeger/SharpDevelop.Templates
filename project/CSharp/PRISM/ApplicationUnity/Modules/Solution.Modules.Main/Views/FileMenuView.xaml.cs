﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ${SolutionName}.Base.Mvvm.Interfaces;
using ${SolutionName}.Base.Mvvm;
using ${SolutionName}.Modules.Main.ViewModels;
using ${SolutionName}.Modules.Main.ViewModels.Interfaces;

namespace ${SolutionName}.Modules.Main.Views
{

	public partial class FileMenuView : UserControlViewBase
	{
		
		public FileMenuView(IFileMenuViewModel viewModel)
		{
			InitializeComponent();
			viewModel.View = this;
		}

	}

}