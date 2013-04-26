using System;
using Microsoft.Practices.Prism.Regions;
using ${SolutionName}.Base.Mvvm;
using ${SolutionName}.Modules.Contacts.ViewModels.Interfaces;

namespace ${SolutionName}.Modules.Contacts.ViewModels
{

	public class ModuleViewModel : ViewModelBase, IModuleViewModel, IConfirmNavigationRequest, IRegionMemberLifetime
	{
		
		public ModuleViewModel()
		{
			VmTitle = Names.ModuleView;
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