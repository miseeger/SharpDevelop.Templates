using ${SolutionName}.Modules.Base.Main.ViewModels.Interfaces;

namespace ${SolutionName}.Modules.Base.Main.Views
{

	
	public partial class BackstageExitView
	{
		public BackstageExitView(IBackstageExitViewModel viewModel)
		{
			InitializeComponent();
			viewModel.View = this;
		}
	}
}