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

	public class ViewModel1 : ViewModelBase, IViewModel1, IConfirmNavigationRequest, IRegionMemberLifetime
	{
		
		#region ----- Fields --------------------------------------------------
		
		private IEventAggregator _eventAggregator;
		private IRegionManager _regionManager;
		private IBusinessService _businessService;
		private IMessageBoxService _messageBoxService;
		
		private IAccountBus _currentCustomer;
		public IAccountBus CurrentCustomer
		{
			get { return _currentCustomer; }
			set
			{ 
				if(_currentCustomer != value)
				{
					_currentCustomer = value;
					RaisePropertyChanged(() => CurrentCustomer);
				}
			}	
		}
		
		#endregion ------------------------------------------------------------
		
		
		#region ----- .ctor ---------------------------------------------------
		
		public ViewModel1(IEventAggregator eventAggregator, IRegionManager regionManager, IBusinessService businessService,
		                  IMessageBoxService messageBoxService)
		{
			_eventAggregator = eventAggregator;
			_regionManager = regionManager;
			_businessService = businessService;
			_messageBoxService = messageBoxService;
			
			VmTitle = Names.View1;
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
			// assign it to CurrentCustomer like so:
			CurrentCustomer = new AccountBus {
											    AccountData = new Account {
																			ObjectStatus=DataObjectStatus.Added
																			, AccountName = "New Customer"
																			, Type = Names.DataNavTypeCustomer
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
			_businessService.SaveAccount(CurrentCustomer);
			_eventAggregator.GetEvent<DataNavRefresh>().Publish(new object[]
			                                                    {
			                                                    	Names.DataNavTypeCustomer
			                                                    	, CurrentCustomer.AccountData.AccountId
			                                                    });
		}
		
		
		private bool CanExecuteSaveAccount()
		{
			return CurrentCustomer != null ? CurrentCustomer.IsDirty : false;
		}

		#endregion ------------------------------------------------------------
		
		
		#region ----- RefreshCustomersCommand ---------------------------------

		private RelayCommand _refreshCustomersCommand;
		public RelayCommand RefreshCustomersCommand
		{
		    get { return _refreshCustomersCommand ?? (_refreshCustomersCommand = new RelayCommand(RefreshCustomers, CanExecuteRefreshCustomers)); }
		}

		
		private void RefreshCustomers()
		{
			
			if (CurrentCustomer.AccountData != null) 
			{
				CurrentCustomer.AccountData.ResetStatus();
				_eventAggregator.GetEvent<DataNavRefresh>().Publish(new object[]
				                                                    {
				                                                    	Names.DataNavTypeCustomer
				                                                    	, CurrentCustomer.AccountData.AccountId
				                                                    });
			}
		}
		
		
		private bool CanExecuteRefreshCustomers()
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
			if (_messageBoxService.Question("Delete Customer", "Are you sure to delete this Customer?"))
		    {
				_businessService.DeleteAccount(CurrentCustomer);
				_eventAggregator.GetEvent<DataNavRefresh>().Publish(new object[]
				                                                    {
				                                                    	Names.DataNavTypeCustomer
				                                                    	, null
				                                                    });

		    }
		}
		

		private bool CanExecuteDeleteAccount()
		{
			return CurrentCustomer != null ? !CurrentCustomer.IsDirty : false;
		}
		
		#endregion ------------------------------------------------------
		
		
		#region ----- Navigation Event Handling -------------------------------
		
		private void OnDataNavBeforeChanging(object[] args)
		{
			var leavingId = args[0].ToString();
			var targettingId = args[1].ToString();
			if (leavingId == CurrentCustomer.AccountData.AccountId
			    && CurrentCustomer.IsDirty)
			{
				if (_messageBoxService.Question("Changes not saved yet", "There are unsaved changes. Do you want to save them?"))
			    {
					_businessService.SaveAccount(CurrentCustomer);
					_eventAggregator.GetEvent<DataNavRefresh>().Publish(new object[]
			                                                    {
			                                                    	Names.DataNavTypeCustomer
			                                                    	, targettingId
			                                                    });
				}
			}
		}

		
		private void OnDataNavChanged(object id)
		{
			CurrentCustomer = _businessService.LoadDetails(_businessService.GetAccount(id));
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
			                              	  { Names.SelectedIdName, CurrentCustomer != null 
			                              	  							? CurrentCustomer.AccountData != null 
			                              	  								? CurrentCustomer.AccountData.AccountId 
			                              	  								: "" 
			                              	  							: "" }
				                      	  }));
		
			// Assign relay commands to module (composite-)commands
			GlobalCommands.NewCommand.RegisterSingleCommand(AddAccountCommand);
			GlobalCommands.SaveCommand.RegisterSingleCommand(SaveAccountCommand);
			GlobalCommands.RefreshCommand.RegisterSingleCommand(RefreshCustomersCommand);
			GlobalCommands.DeleteCommand.RegisterSingleCommand(DeleteAccountCommand);		
		}
	
		
		public void OnNavigatedFrom(NavigationContext navigationContext)
		{
			if (CurrentCustomer != null && CurrentCustomer.IsDirty)
			{
				if (_messageBoxService.Question("Changes not saved yet", "There are unsaved changes. Do you want to save them?"))
			    {
					_businessService.SaveAccount(CurrentCustomer);
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