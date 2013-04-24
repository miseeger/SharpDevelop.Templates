using System;
using System.ComponentModel;
using Microsoft.Practices.Prism.Events;
using ${SolutionName}.Base.Interfaces.Services;
using ${SolutionName}.Base.Mvvm;
using ${SolutionName}.Base.Events;
using ${SolutionName}.Modules.Splash.ViewModels.Interfaces;

namespace ${SolutionName}.Modules.Splash.ViewModels
{

  public class SplashViewModel : ViewModelBase, ISplashViewModel
  {

  	public SplashViewModel(IEventAggregator EventAggregator, IAppResourceService ResourceService)
    {
		EventAggregator.GetEvent<SplashInfoUpdateEvent>().Subscribe(e => UpdateInfo(e.Info));
		VmTitle = "${SolutionName} V 1.0";
		VmImage = ResourceService.GetPngMisc("Splash");
		Status = "I n i t i a l i z i n g" + Environment.NewLine;
    }


  	private string _status;
  	public string Status
  	{
  		get { return _status; }
  		set
  		{
  			if(_status != value)
  			{
  				_status = value;
  				RaisePropertyChanged(() => Status);
  			}
  		}
  	}


    private void UpdateInfo(string info)
    {
      if (string.IsNullOrEmpty(info))
      {
        return;
      }

      Status += string.Concat(Environment.NewLine, info, "...");
    }

  }

}
