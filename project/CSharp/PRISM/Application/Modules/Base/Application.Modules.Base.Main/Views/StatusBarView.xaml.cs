using ${SolutionName}.Modules.Base.Main.ViewModels.Interfaces;

namespace ${SolutionName}.Modules.Base.Main.Views
{

	public partial class StatusBarView
	{

		public StatusBarView(IStatusBarViewModel viewModel)
		{
			InitializeComponent();
			viewModel.View = this;
		}

	}

}