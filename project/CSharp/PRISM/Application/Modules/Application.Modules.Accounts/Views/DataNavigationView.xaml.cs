using ${SolutionName}.Modules.Accounts.ViewModels.Interfaces;

namespace ${SolutionName}.Modules.Accounts.Views
{

	public partial class DataNavigationView
	{

		public DataNavigationView(IDataNavigationViewModel viewModel)
		{
			InitializeComponent();
			viewModel.View = this;
		}

	}

}