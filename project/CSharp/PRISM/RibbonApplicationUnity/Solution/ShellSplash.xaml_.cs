using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using ${SolutionName}.Base.Interfaces;

namespace ${SolutionName}
{

	public partial class Shell : IShell
	{

		public Shell(ShellViewModel viewModel)
		{
			InitializeComponent();
			viewModel.View = this;
		}
		
	}
}