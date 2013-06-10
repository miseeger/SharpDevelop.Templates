using ${SolutionName}.Modules.Contacts.ViewModels.Interfaces;

namespace ${SolutionName}.Modules.Contacts.Views
{

	public partial class View2
	{

		public View2(IViewModel2 viewModel)
		{
			InitializeComponent();
			viewModel.View = this;
		}

	}

}