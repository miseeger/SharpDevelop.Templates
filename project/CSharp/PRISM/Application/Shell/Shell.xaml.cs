using System.Windows.Controls;
using Microsoft.Practices.Prism.Events;
using ${SolutionName}.Base;
using ${SolutionName}.Base.Interfaces.Ui;
using ${SolutionName}.Base.Mvvm.RibbonUi;

namespace ${SolutionName}
{

	public partial class Shell : IShell
	{

		private IEventAggregator _eventAggregator;
		

		public Shell(ShellViewModel viewModel, IEventAggregator eventAggregator)
		{
			InitializeComponent();
			_eventAggregator = eventAggregator;
			viewModel.View = this;
		}
		

		void Ribbon_SelectedTabChanged(object sender, SelectionChangedEventArgs e)
		{
			var target = (RibbonTabViewBase)e.AddedItems[0];
			_eventAggregator.GetEvent<RibbonTabChangedEvent>().Publish(target.Header.ToString());
		}
		
	}
	
}