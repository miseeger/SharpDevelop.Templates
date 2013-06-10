using System;
using System.Collections.Generic;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using ${SolutionName}.Base;
using ${SolutionName}.Base.Business.Model;
using ${SolutionName}.Base.Business.Model.Interfaces;
using ${SolutionName}.Base.Data.Model;
using ${SolutionName}.Base.Extensions;
using ${SolutionName}.Base.Interfaces.Services;
using ${SolutionName}.Base.Mvvm;
using ${SolutionName}.Base.Mvvm.Enums;
using ${SolutionName}.Base.Navigation;
using ${SolutionName}.Modules.Accounts.ViewModels.Interfaces;
using ${SolutionName}.Modules.Accounts.Views;

namespace ${SolutionName}.Modules.Accounts.ViewModels
{

	public class ViewModel2 : ViewModelBase, IViewModel2, IConfirmNavigationRequest, IRegionMemberLifetime
	{
		
		#region ----- Fields --------------------------------------------------
		
		private IEventAggregator _eventAggregator;
		private IRegionManager _regionManager;
		private IBusinessService _businessService;
		private IMessageBoxService _messageBoxService;
		
		private IAccountBus _currentVendor;
		public IAccountBus CurrentVendor
		{
			get { return _currentVendor; }
			set
			{ 
				if(_currentVendor != value)
				{
					_currentVendor = value;
					RaisePropertyChanged(() => CurrentVendor);
				}
			}	
		}
		
		#endregion ------------------------------------------------------------
		
		
		#region ----- .ctor ---------------------------------------------------
		
		public ViewModel2(IEventAggregator eventAggregator, IRegionManager regionManager, IBusinessService businessService,
		                  IMessageBoxService messageBoxService)
		{
			_eventAggregator = eventAggregator;
			_regionManager = regionManager;
			_businessService = businessService;
			_messageBoxService = messageBoxService;

			VmTitle = Names.View2;
		}
		
		#endregion ------------------------------------------------------------
		
		
		#region ----- AddAccountCommand ---------------------------------------

		private RelayCommand _addAccountCommand;
		public RelayCommand AddAccountCommand
		{
			get { return _addAccountCommand ?? (_addAccountCommand = new RelayCommand(AddAccount, CanExecuteAddAccount)); }
		}

		
		private void AddAccount()
		{
			// First "initialize" the new AccountBus-Object and then
			// assign it to CurrentVendor like so:
			CurrentVendor = new AccountBus {
											AccountData = new Account {
																		ObjectStatus=DataObjectStatus.Added
																		, AccountName = "New Vendor"
																		, Type = Names.DataNavTypeVendor
																	   }
											, AddressData = new Address {
																		  ObjectStatus=DataObjectStatus.Added
																		}
										   };
		}

		
		private bool CanExecuteAddAccount()
		{
			return true;
		}
		
		#endregion

		
		#region ----- SaveAccountCommand --------------------------------------
		
		private RelayCommand _saveAccountCommand;
		public RelayCommand SaveAccountCommand
		{
		    get { return _saveAccountCommand ?? (_saveAccountCommand = new RelayCommand(SaveAccount, CanExecuteSaveAccount)); }
		}
		
		private void SaveAccount()
		{
			_businessService.SaveAccount(CurrentVendor);
			_eventAggregator.GetEvent<DataNavRefresh>().Publish(new object[]
			                                                    {
			                                                    	Names.DataNavTypeVendor
			                                                    	, CurrentVendor.AccountData.AccountId
			                                                    });
		}
		
		
		private bool CanExecuteSaveAccount()
		{
			return CurrentVendor != null ? CurrentVendor.IsDirty : false;
		}

		#endregion ------------------------------------------------------------
		
		
		#region ----- RefreshVendorsCommand ---------------------------------

		private RelayCommand _refreshVendorsCommand;
		public RelayCommand RefreshVendorsCommand
		{
		    get { return _refreshVendorsCommand ?? (_refreshVendorsCommand = new RelayCommand(RefreshVendors, CanExecuteRefreshVendors)); }
		}

		
		private void RefreshVendors()
		{
			CurrentVendor.AccountData.ResetStatus();
			_eventAggregator.GetEvent<DataNavRefresh>().Publish(new object[]
			                                                    {
			                                                    	Names.DataNavTypeVendor
			                                                    	, CurrentVendor.AccountData.AccountId
			                                                    });
		}
		
		
		private bool CanExecuteRefreshVendors()
		{
		    return true;
		}

		#endregion ------------------------------------------------------------		
		
		
		#region ----- DeleteAccountCommand ------------------------------------
		
		private RelayCommand _deleteAccountCommand;
		public RelayCommand DeleteAccountCommand
		{
		    get { return _deleteAccountCommand ?? (_deleteAccountCommand = new RelayCommand(DeleteAccount, CanExecuteDeleteAccount)); }
		}
		
		
		private void DeleteAccount()
		{
			if (_messageBoxService.Question("Delete Vendor", "Are you sure to delete this Vendor?"))
		    {
				_businessService.DeleteAccount(CurrentVendor);
				_eventAggregator.GetEvent<DataNavRefresh>().Publish(new object[]
				                                                    {
				                                                    	Names.DataNavTypeVendor
				                                                    	, null
				                                                    });

		    }
		}
		

		private bool CanExecuteDeleteAccount()
		{
			return CurrentVendor != null ? !CurrentVendor.IsDirty : false;
		}
		
		#endregion ------------------------------------------------------
		
		
		#region ----- Navigation Event Handling -------------------------------
		
		private void OnDataNavBeforeChanging(object[] args)
		{
			var leavingId = args[0].ToString();
			var targettingId = args[1].ToString();
			if (leavingId == CurrentVendor.AccountData.AccountId
			    && CurrentVendor.IsDirty)
			{
				if (_messageBoxService.Question("Changes not saved yet", "There are unsaved changes. Do you want to save them?"))
			    {
					_businessService.SaveAccount(CurrentVendor);
					_eventAggregator.GetEvent<DataNavRefresh>().Publish(new object[]
			                                                    {
			                                                    	Names.DataNavTypeVendor
			                                                    	, targettingId
			                                                    });
				}
			}
		}

		
		private void OnDataNavChanged(object id)
		{
			CurrentVendor = _businessService.LoadDetails(_businessService.GetAccount(id));
		}
 		
		
		public bool IsNavigationTarget(NavigationContext navigationContext)
		{
			return true;
		}


		public void OnNavigatedTo(NavigationContext navigationContext)
		{
			// Subscribing to Events
			_eventAggregator.GetEvent<DataNavChanged>().Subscribe(OnDataNavChanged);
			_eventAggregator.GetEvent<DataNavBeforeChanging>().Subscribe(OnDataNavBeforeChanging);
			
			// Initialize data navigation view
			_eventAggregator.GetEvent<ShowDataNavigationEvent>().Publish(true);
			_regionManager.NavigateToDataNavRegion(
				typeof(DataNavigationView)
			    	.CreateNavigationPath(new Dictionary<string,string>
										  {
			                              	  { Names.DataNavTypeName, navigationContext.Parameters[Names.DataNavTypeName] },
			                              	  { Names.SelectedIdName, CurrentVendor != null
			                              	  							? CurrentVendor.AccountData != null 
			                              	  								? CurrentVendor.AccountData.AccountId 
			                              	  								: "" 
			                              	  							: "" }
				                      	  }));
		
			// Assign relay commands to module (composite-)commands
			GlobalCommands.NewCommand.RegisterSingleCommand(AddAccountCommand);
			GlobalCommands.SaveCommand.RegisterSingleCommand(SaveAccountCommand);
			GlobalCommands.RefreshCommand.RegisterSingleCommand(RefreshVendorsCommand);
			GlobalCommands.DeleteCommand.RegisterSingleCommand(DeleteAccountCommand);
			
		}
	
		
		public void OnNavigatedFrom(NavigationContext navigationContext)
		{
			if (CurrentVendor != null && CurrentVendor.IsDirty)
			{
				if (_messageBoxService.Question("Changes not saved yet", "There are unsaved changes. Do you want to save them?"))
			    {
					_businessService.SaveAccount(CurrentVendor);
				}
			}
			
			// Unsubscribing Events
			_eventAggregator.GetEvent<DataNavChanged>().Unsubscribe(OnDataNavChanged);
			_eventAggregator.GetEvent<DataNavBeforeChanging>().Unsubscribe(OnDataNavBeforeChanging);
		}
		
		
		public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
		{
			bool result = true;
			
			// TODO: Implement code to set result ...
			
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