using ${ProjectName}.ViewModels.Interfaces;

namespace ${ProjectName}.Views
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