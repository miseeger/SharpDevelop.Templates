using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using ${ProjectName}.ViewModels.Interfaces;

namespace ${ProjectName}.Views
{

	public partial class ModuleView
	{

		public ModuleView(IModuleViewModel viewModel)
		{
			InitializeComponent();
			viewModel.View = this;
		}

	}

}