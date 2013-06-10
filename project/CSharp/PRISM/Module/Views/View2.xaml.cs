using ${ProjectName}.ViewModels.Interfaces;

namespace ${ProjectName}.Views
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