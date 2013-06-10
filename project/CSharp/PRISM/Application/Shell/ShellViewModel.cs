using System.Windows.Media;
using Microsoft.Practices.Prism.Events;
using ${SolutionName}.Base;
using ${SolutionName}.Base.Interfaces.Services;
using ${SolutionName}.Base.Mvvm;

namespace ${SolutionName}
{

	public class ShellViewModel : ViewModelBase
	{
		
		public ImageSource MainImage { get; set; }
		
		private bool _dataNavVisibility;
		public bool DataNavVisibility
		{
			get { return _dataNavVisibility; }
			set
			{
				if(_dataNavVisibility != value)
				{
					_dataNavVisibility = value;
					RaisePropertyChanged(() => DataNavVisibility);
				}
			}
		}
		
		
		public ShellViewModel(IAppResourceService resourceService, IEventAggregator eventAggregator)
		{
			MainImage = resourceService.GetPngMisc("Shell");
			eventAggregator.GetEvent<ShowDataNavigationEvent>().Subscribe(OnDataNavShow);
			
		}
		
		
		public void OnDataNavShow(bool visibility)
  		{
  			DataNavVisibility = visibility;
        }

	}

}
