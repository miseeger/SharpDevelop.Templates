using System;
using Microsoft.Practices.Prism.Regions;
using ${SolutionName}.Base.Mvvm;
using ${SolutionName}.Modules.Accounts.ViewModels.Interfaces;

namespace ${SolutionName}.Modules.Accounts.ViewModels
{

	public class ViewModel2 : ViewModelBase, IViewModel2, IConfirmNavigationRequest, IRegionMemberLifetime
	{
		
		public ViewModel2()
		{
			VmTitle = Names.View2;
		}
		
		
				public bool IsNavigationTarget(NavigationContext navigationContext)
		{
			return true;
		}


		public void OnNavigatedTo(NavigationContext navigationContext)
		{
			// TODO: Implement code for entering ...
		}
		
		
		public void OnNavigatedFrom(NavigationContext navigationContext)
		{
			// TODO: Implement code for exiting ...
		}
		
		
		public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
		{
			bool result = true;
			
			// TODO: Implement code to set result ...
			
			continuationCallback(result);
		}
		
		
		public bool KeepAlive
		{
			get
			{
				return true;
			}
		}
	
	}

}