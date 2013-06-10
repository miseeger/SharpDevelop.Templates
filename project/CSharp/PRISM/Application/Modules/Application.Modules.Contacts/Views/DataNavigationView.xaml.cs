using ${SolutionName}.Modules.Contacts.ViewModels.Interfaces;

namespace ${SolutionName}.Modules.Contacts.Views
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