using ${SolutionName}.Modules.Splash.ViewModels.Interfaces;
namespace ${SolutionName}.Modules.Splash.Views
{

  public partial class SplashView
  {

  	public SplashView(ISplashViewModel viewModel)
    {
      InitializeComponent();
      viewModel.View = this;
    }
  	
  }

}
