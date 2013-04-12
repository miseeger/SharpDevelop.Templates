using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace ${SolutionName}
{

	public partial class Shell
	{

		public Shell(ShellViewModel viewModel)
		{
			InitializeComponent();
			ViewModel = viewModel;
		}
		
	}
}