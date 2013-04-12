using System;
using Microsoft.Practices.Prism.Regions;
using ${SolutionName}.Base.Mvvm;
using ${SolutionName}.Modules.Accounts.ViewModels.Interfaces;

namespace ${SolutionName}.Modules.Accounts.ViewModels
{

	public class ViewModel1 : ViewModelBase, IViewModel1, IConfirmNavigationRequest, IRegionMemberLifetime
	{
		
		public ViewModel1()
		{
			VmTitle = Names.View1;
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