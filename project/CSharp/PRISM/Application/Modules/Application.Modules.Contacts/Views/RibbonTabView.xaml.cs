using ${SolutionName}.Modules.Contacts.ViewModels.Interfaces;

namespace ${SolutionName}.Modules.Contacts.Views
{

	public partial class RibbonTabView
	{

		public RibbonTabView(IRibbonTabViewModel viewModel)
		{
			InitializeComponent();
			viewModel.View = this;
		}

	}

}