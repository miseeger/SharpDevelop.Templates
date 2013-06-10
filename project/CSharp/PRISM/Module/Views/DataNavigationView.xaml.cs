using ${ProjectName}.ViewModels.Interfaces;

namespace ${ProjectName}.Views
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