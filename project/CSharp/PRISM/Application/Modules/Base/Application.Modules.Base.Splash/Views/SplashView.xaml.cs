using ${SolutionName}.Modules.Base.Splash.ViewModels.Interfaces;

namespace ${SolutionName}.Modules.Base.Splash.Views
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
