using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Practices.Prism.Events;

using ${SolutionName}.Base.Mvvm.RibbonUi;
using ${SolutionName}.Base;

namespace ${SolutionName}
{

	public partial class Shell
	{

		private IEventAggregator _eventAggregator;
		

		public Shell(ShellViewModel viewModel, IEventAggregator EventAggregator)
		{
			InitializeComponent();
			_eventAggregator = EventAggregator;
			viewModel.View = this;
		}
		

		void Ribbon_SelectedTabChanged(object sender, SelectionChangedEventArgs e)
		{
			var target = (RibbonTabViewBase)e.AddedItems[0];
			_eventAggregator.GetEvent<RibbonTabChangedEvent>().Publish(target.Header.ToString());
		}
		
	}
	
}