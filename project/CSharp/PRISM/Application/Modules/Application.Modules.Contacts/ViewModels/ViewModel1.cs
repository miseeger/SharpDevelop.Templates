using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using ${SolutionName}.Base;
using ${SolutionName}.Base.Business.Model;
using ${SolutionName}.Base.Business.Model.Interfaces;
using ${SolutionName}.Base.Data.Model;
using ${SolutionName}.Base.Data.Model.Interfaces;
using ${SolutionName}.Base.Extensions;
using ${SolutionName}.Base.Interfaces.Services;
using ${SolutionName}.Base.Mvvm;
using ${SolutionName}.Base.Mvvm.Enums;
using ${SolutionName}.Base.Navigation;
using ${SolutionName}.Modules.Contacts.ViewModels.Interfaces;
using ${SolutionName}.Modules.Contacts.Views;

namespace ${SolutionName}.Modules.Contacts.ViewModels
{

	public class ViewModel1 : ViewModelBase, IViewModel1, IConfirmNavigationRequest, IRegionMemberLifetime
	{

		#region ----- Fields --------------------------------------------------
		
		private IEventAggregator _eventAggregator;
		private IRegionManager _regionManager;
		private IBusinessService _businessService;
		private IMessageBoxService _messageBoxService;
		
		private IContactBus _currentContact;
		public IContactBus CurrentContact
		{
			get { return _currentContact; }
			set
			{ 
				if(_currentContact != value)
				{
					_currentContact = value;
					RaisePropertyChanged(() => CurrentContact);
				}
			}	
		}
		
		
		private ObservableCollection<IAccount> _accounts;
		public ObservableCollection<IAccount> Accounts
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
		
		
		#region ----- Methods -------------------------------------------------
		
		private void LoadAccounts()
		{
			Accounts = _businessService
				.GetAccounts()
				.Select(a => a.AccountData)
				.OrderBy(o => o.AccountName)
				.ToObservableCollection();
		}
		
		#endregion ------------------------------------------------------------

		
		#region ----- AddContactCommand ---------------------------------------

		private RelayCommand _addContactCommand;
		public RelayCommand AddContactCommand
		{
			get { return _addContactCommand ?? (_addContactCommand = new RelayCommand(AddContact, CanExecuteAddContact)); }
		}

		
		private void AddContact()
		{
			// First "initialize" the new ContactBus-Object and then
			// assign it to CurrentContact like so:
			CurrentContact = new ContactBus {
												ContactData = new Contact {
																			ObjectStatus=DataObjectStatus.Added
																		   }
												, AddressData = new Address {
																			  ObjectStatus=DataObjectStatus.Added
																			}
											  };
		}

		
		private bool CanExecuteAddContact()
		{
			return true;
		}
		
		#endregion

		
		#region ----- SaveContactCommand --------------------------------------
		
		private RelayCommand _saveContactCommand;
		public RelayCommand SaveContactCommand
		{
		    get { return _saveContactCommand ?? (_saveContactCommand = new RelayCommand(SaveContact, CanExecuteSaveContact)); }
		}
		
		private void SaveContact()
		{
			_businessService.SaveContact(CurrentContact);
			_eventAggregator.GetEvent<DataNavRefresh>().Publish(CurrentContact.ContactData.ContactId);
		}
		
		
		private bool CanExecuteSaveContact()
		{
			return CurrentContact != null 
					? (CurrentContact.IsDirty && CurrentContact.ContactData.AccountId != null && CurrentContact.ContactData.Gender != null) 
					: false;
		}

		#endregion ------------------------------------------------------------
		
		
		#region ----- RefreshContactsCommand ---------------------------------

		private RelayCommand _refreshContactsCommand;
		public RelayCommand RefreshContactsCommand
		{
		    get { return _refreshContactsCommand ?? (_refreshContactsCommand = new RelayCommand(RefreshContacts, CanExecuteRefreshContacts)); }
		}

		
		private void RefreshContacts()
		{
			
			if (CurrentContact.ContactData != null) 
			{
				LoadAccounts();
				CurrentContact.ContactData.ResetStatus();
				_eventAggregator.GetEvent<DataNavRefresh>().Publish(CurrentContact.ContactData.ContactId);
			}
		}
		
		
		private bool CanExecuteRefreshContacts()
		{
		    return true;
		}

		#endregion ------------------------------------------------------------		
		
		
		#region ----- DeleteContactCommand ------------------------------------
		
		private RelayCommand _deleteContactCommand;
		public RelayCommand DeleteContactCommand
		{
		    get { return _deleteContactCommand ?? (_deleteContactCommand = new RelayCommand(DeleteContact, CanExecuteDeleteContact)); }
		}
		
		
		private void DeleteContact()
		{
			if (_messageBoxService.Question("Delete Contact", "Are you sure to delete this Contact?"))
		    {
				_businessService.DeleteContact(CurrentContact);
				_eventAggregator.GetEvent<DataNavRefresh>().Publish(null);

		    }
		}
		

		private bool CanExecuteDeleteContact()
		{
			return CurrentContact != null ? !CurrentContact.IsDirty : false;
		}
		
		#endregion ------------------------------------------------------
		
		
		#region ----- Navigation Event Handling -------------------------------
		
		private void OnDataNavBeforeChanging(object[] args)
		{
			var leavingId = args[0].ToString();
			var targettingId = args[1].ToString();
			if (leavingId == CurrentContact.ContactData.ContactId
			    && CurrentContact.IsDirty)
			{
				if (_messageBoxService.Question("Changes not saved yet", "There are unsaved changes. Do you want to save them?"))
			    {
					_businessService.SaveContact(CurrentContact);
					_eventAggregator.GetEvent<DataNavRefresh>().Publish(targettingId);
				}
			}
		}

		
		private void OnDataNavChanged(object id)
		{
			CurrentContact = _businessService.LoadDetails(_businessService.GetContact(id));
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
			                              	  { Names.SelectedIdName, CurrentContact != null 
			                              	  							? CurrentContact.ContactData != null 
			                              	  								? CurrentContact.ContactData.ContactId 
			                              	  								: "" 
			                              	  							: "" }
				                      	  }));

			// Loading Accounts if necessary
			if (Accounts == null || !Accounts.Any()) 
			{
				LoadAccounts();
			}
		
			// Assign relay commands to module (composite-)commands
			GlobalCommands.NewCommand.RegisterSingleCommand(AddContactCommand);
			GlobalCommands.SaveCommand.RegisterSingleCommand(SaveContactCommand);
			GlobalCommands.RefreshCommand.RegisterSingleCommand(RefreshContactsCommand);
			GlobalCommands.DeleteCommand.RegisterSingleCommand(DeleteContactCommand);		
		}
	
		
		public void OnNavigatedFrom(NavigationContext navigationContext)
		{
			if (CurrentContact != null && CurrentContact.IsDirty)
			{
				if (_messageBoxService.Question("Changes not saved yet", "There are unsaved changes. Do you want to save them?"))
			    {
					_businessService.SaveContact(CurrentContact);
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