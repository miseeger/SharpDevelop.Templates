using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using ${SolutionName}.Base.Business.Model.Interfaces;
using ${SolutionName}.Base.Interfaces.Services;
using ${SolutionName}.Base.Mvvm;
using ${SolutionName}.Modules.Accounts.ViewModels.Interfaces;

namespace ${SolutionName}.Modules.Accounts.ViewModels
{

	public class DataNavigationViewModel : ViewModelBase, IDataNavigationViewModel, IConfirmNavigationRequest, IRegionMemberLifetime
	{
		
		#region ----- Fields --------------------------------------------------	
		
		IBusinessService _businessService;
		IEventAggregator _eventAggregator;
		IAppResourceService _resourceService;

	    private ObservableCollection<IAccountBus> _accounts;
		public ObservableCollection<IAccountBus> Accounts
		{
			get { return _accounts; }
			set
			{
				if(_accounts != value)
				{
					_accounts = value;
					RaisePropertyChanged(() => Accounts);
				}
			}
		}
		
		private IAccountBus _selectedAccount;
		public IAccountBus SelectedAccount
		{
			get { return _selectedAccount; }
			set
			{
				if(_selectedAccount != value)
				{
					if (_selectedAccount != null && value != null) 
					{
						_eventAggregator.GetEvent<DataNavBeforeChanging>().Publish(new object[]
			                                                    {
			                                                    	_selectedAccount.AccountData.AccountId
			                                                    	, value.AccountData.AccountId
			                                                    });
					}
					
					_selectedAccount = value;
					RaisePropertyChanged(() => SelectedAccount);
					_eventAggregator.GetEvent<DataNavChanged>().Publish(value != null ? value.AccountData.AccountId : "");
				}
			}
		}
		
		
		private ImageSource _icon;
		public ImageSource Icon
		{
			get { return _icon; }
			set
			{
				if(_icon != value)
				{
					_icon = value;
					RaisePropertyChanged(() => Icon);
				}
			}
		}
		
		#endregion ------------------------------------------------------------
		
		
		#region ----- .ctor ---------------------------------------------------

		public DataNavigationViewModel(IBusinessService businessService, IEventAggregator eventAggregator,
		                               IAppResourceService resourceService)
		{
			_businessService = businessService;
			_eventAggregator = eventAggregator;
			_resourceService = resourceService;
		    _eventAggregator.GetEvent<DataNavRefresh>().Subscribe(OnDataNavRefresh);
		}
		
		#endregion ------------------------------------------------------------
		
		
		#region ----- Common Event Handling -----------------------------------

		private void OnDataNavRefresh(object[] args)
		{
			var navType = args[0].ToString();
			
			Icon = navType == Names.DataNavTypeVendor 
					? Icon = _resourceService.GetPng32("building") 
					: Icon = Icon = _resourceService.GetPng32("group");
			
			Accounts = _businessService.GetAccountsByType(navType);
			
			SelectedAccount = (args[1] != null) 
				? Accounts != null ? Accounts.FirstOrDefault(a => a.AccountData.AccountId == args[1].ToString()) : null
				: Accounts != null ? Accounts.FirstOrDefault() : null;
		}
		
		#endregion ------------------------------------------------------------
		
		
		#region ----- Navigation Event Handling -------------------------------

		public bool IsNavigationTarget(NavigationContext navigationContext)
		{
			return true;
		}
		

		public void OnNavigatedTo(NavigationContext navigationContext)
		{
			VmTitle = navigationContext.Parameters[Names.DataNavTypeName];
			var selectedId = navigationContext.Parameters[Names.SelectedIdName];
			OnDataNavRefresh(new object[]
                             {
                             	navigationContext.Parameters[Names.DataNavTypeName]
                           		, selectedId != "" ? selectedId : null
			                 });
		}
		
		
		public void OnNavigatedFrom(NavigationContext navigationContext)
		{
			// TODO: Impelemtn code ...
		}
		
		
		public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
		{
			bool result = true;
			
			// TODO: Implement code to set result
			
			continuationCallback(result);
		}
		
		#endregion ------------------------------------------------------------
		

		#region ----- Member Lifetime------------------------------------------			

		public bool KeepAlive
		{
			get
			{
				return true;
			}
		}
		
		#endregion ------------------------------------------------------------
	
	}

}