using ${ProjectName}.ViewModels.Interfaces;

namespace ${ProjectName}.Views
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