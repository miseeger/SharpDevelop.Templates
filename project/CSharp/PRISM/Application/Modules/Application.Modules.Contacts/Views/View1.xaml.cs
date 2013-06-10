using ${SolutionName}.Modules.Contacts.ViewModels.Interfaces;

namespace ${SolutionName}.Modules.Contacts.Views
{

	public partial class View1
	{

		public View1(IViewModel1 viewModel)
		{
			InitializeComponent();
			viewModel.View = this;
		}

	}

}