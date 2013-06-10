using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using ${SolutionName}.Base;
using ${SolutionName}.Base.Business.Model.Interfaces;
using ${SolutionName}.Base.Extensions;
using ${SolutionName}.Base.Interfaces.Services;
using ${SolutionName}.Base.Mvvm;
using ${SolutionName}.Modules.Contacts.ViewModels.Interfaces;

namespace ${SolutionName}.Modules.Contacts.ViewModels
{

	public class ViewModel2 : ViewModelBase, IViewModel2, IConfirmNavigationRequest, IRegionMemberLifetime
	{
		
		#region ----- Fields --------------------------------------------------
		
		private IEventAggregator _eventAggregator;
	    private IBusinessService _businessService;
		private IMessageBoxService _messageBoxService;
		
		private ObservableCollection<IContactBus> _contacts;
		public ObservableCollection<IContactBus> Contacts
		{
			get { return _contacts; }
			set
			{ 
				if(_contacts != value)
				{
					_contacts = value;
					RaisePropertyChanged(() => Contacts);
				}
			}	
		}
		
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
		
		#endregion ------------------------------------------------------------
		
		
		#region ----- .ctor ---------------------------------------------------
		
		public ViewModel2(IEventAggregator eventAggregator, IBusinessService businessService,
		                  IMessageBoxService messageBoxService)
		{
			_eventAggregator = eventAggregator;
		    _businessService = businessService;
			_messageBoxService = messageBoxService;
			
			VmTitle = Names.View2;
		}
		
		#endregion ------------------------------------------------------------
		
		
		#region ----- SaveContactCommand --------------------------------------
		
		private RelayCommand _saveContactsCommand;
		public RelayCommand SaveContactsCommand
		{
		    get { return _saveContactsCommand ?? (_saveContactsCommand = new RelayCommand(SaveContacts, CanExecuteSaveContacts)); }
		}
		
		private void SaveContacts()
		{
			foreach (IContactBus contact in Contacts.Where(c => c.IsDirty))
			{
				_businessService.SaveContact(contact);	
			}
			_eventAggregator.GetEvent<DataNavRefresh>().Publish(CurrentContact.ContactData.ContactId);
		}
		
		
		private bool CanExecuteSaveContacts()
		{
			return Contacts.Count() > 0 ? Contacts.Any(c => c.IsDirty) : false;
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
			var allContacts = _businessService.GetContacts();
			var fullContacts = new ObservableCollection<IContactBus>();
			
			foreach (var contact in allContacts) 
			{
				fullContacts.Add(_businessService.LoadDetails(contact));
			}
			
			Contacts = fullContacts
						   .OrderBy(c => c.ContactData.LastFirst)
						   .ToObservableCollection();
			
			if (Contacts != null)
			{
				CurrentContact = Contacts.FirstOrDefault();
			}
		}
		
		
		private bool CanExecuteRefreshContacts()
		{
		    return true;
		}

		#endregion ------------------------------------------------------------		
	
		
		#region ----- Navigation Event Handling -------------------------------
		
		public bool IsNavigationTarget(NavigationContext navigationContext)
		{
			return true;
		}


		public void OnNavigatedTo(NavigationContext navigationContext)
		{
			_eventAggregator.GetEvent<ShowDataNavigationEvent>().Publish(false);

			if (Contacts == null)
			{
				RefreshContacts();
			}
			
			if (CurrentContact == null) 
			{
				CurrentContact = Contacts.FirstOrDefault();
			}
		
			// Assign relay commands to module (composite-)commands
			GlobalCommands.NewCommand.RegisterSingleCommand(null);
			GlobalCommands.SaveCommand.RegisterSingleCommand(SaveContactsCommand);
			GlobalCommands.RefreshCommand.RegisterSingleCommand(RefreshContactsCommand);
			GlobalCommands.DeleteCommand.RegisterSingleCommand(null);		
		}
	
		
		public void OnNavigatedFrom(NavigationContext navigationContext)
		{
			if (Contacts.Count() > 0 && Contacts.Any(c => c.IsDirty))
			{
				if (_messageBoxService.Question("Changes not saved yet", "There are unsaved changes. Do you want to save them?"))
			    {
					SaveContacts();
				}
			}
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