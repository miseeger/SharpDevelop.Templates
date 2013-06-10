using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.ServiceLocation;
using ${SolutionName}.Base.Business.Model;
using ${SolutionName}.Base.Business.Model.Interfaces;
using ${SolutionName}.Base.Data.Model;
using ${SolutionName}.Base.Interfaces.Services;
using ${SolutionName}.Base.Mvvm.Enums;
using ${SolutionName}.Base.Services.Persistence.Interfaces;

namespace ${SolutionName}.Base.Business
{

	public class BusinessService : IBusinessService
	{

		IDataService _dataService;
		ILoggerFacade _logger;
		IRepository _repo;

		
		#region ----- .ctor ---------------------------------------------------

		public BusinessService()
		{
			_dataService = ServiceLocator.Current.GetInstance<IDataService>();
			_logger = ServiceLocator.Current.GetInstance<ILoggerFacade>();
			_logger.Log("${SolutionName} BusinessService was successfully retrieved from container.",
				Category.Info, Priority.None);
			_logger.Log("${SolutionName} Logger for BusinessService was successfully retrieved from container.",
				Category.Info, Priority.None);
			_repo = _dataService.Repo;			
		}
		
		#endregion ------------------------------------------------------------

		
		#region ----- Account -------------------------------------------------
		
		public IAccountBus GetAccount(object accountId)
		{
			var result = new AccountBus();
			var account = _repo.SingleOrDefault<Account>(accountId);

			if (account != null)
			{
				result.AccountData = account;
			}
			
			return result;
		}
		
		
		public IAccountBus LoadDetails(IAccountBus account)
		{
			if (account.AccountData != null) 
			{
				var dataService = ServiceLocator.Current.GetInstance<IDataService>();
				
				if (dataService != null) 
				{
					var repo = dataService.Repo;
					var address = repo.Query<Address>(String.Format("EntityId={0}",account.AccountData.AccountId),"",0,"*")
					                  .FirstOrDefault();

					account.AddressData = null;
					
					if (address != null)
					{
						account.AddressData = address;
					}
				
					var contacts = repo.Query<Contact>(String.Format("AccountId={0}",account.AccountData.AccountId),"",0,"*");
					account.Contacts.Clear();
	
					foreach (var contact in contacts)
					{
						account.Contacts.Add(contact);
					}
				}
			}
			
			return account;
		}
		
		
		public ObservableCollection<IAccountBus> GetAccounts()
		{
			var result = new ObservableCollection<IAccountBus>();
			var accounts = _repo.Query<Account>();
			
			foreach (var account in accounts) 
			{
				result.Add(new AccountBus { AccountData = account });
			}
			
			return result;
		}
		
		
		public ObservableCollection<IAccountBus> GetAccountsByType(string type)
		{
			var result = new ObservableCollection<IAccountBus>();
			var accounts = _repo.Query<Account>(String.Format("TYPE='{0}'",type),"",0,"*");

			foreach (var account in accounts)
			{
				result.Add(new AccountBus { AccountData = account });
			}
			
			return result;
		}
		
		
		public void DeleteAccount(IAccountBus account)
		{
			// Address
			if (account.AddressData != null) 
			{
				_repo.Delete<Address>(account.AddressData.AddressId);
			}
			
			// Contacts
			foreach (var contact in account.Contacts) 
			{
				_repo.Delete<Contact>(contact.ContactId);
			}
			
			// Account
			_repo.Delete<Account>(account.AccountData.AccountId);
		}
		
		
		public void SaveAccount (IAccountBus account)
		{
			// Account data
			switch (account.AccountData.ObjectStatus) 
			{
				case DataObjectStatus.Modified:
					_repo.Update(account.AccountData);
					break;
				case DataObjectStatus.Added:
					_repo.Insert(account.AccountData);					
					break;
			}
			
			// Address
			if (account.AddressData != null) 
			{
				switch (account.AddressData.ObjectStatus) 
				{
					case DataObjectStatus.Modified:
						_repo.Update(account.AddressData);
						break;
					case DataObjectStatus.Added:
						account.AddressData.EntityId = account.AccountData.AccountId;
						_repo.Insert(account.AddressData);
						break;
				}
			}
			
			// Contacts
			foreach (var contact in account.Contacts) 
			{
				switch (contact.ObjectStatus) 
				{
					case DataObjectStatus.Modified:
						_repo.Update(contact);
						break;
					case DataObjectStatus.Added:
						_repo.Insert(contact);
						break;
					case DataObjectStatus.Deleted:
						_repo.Delete<Contact>(contact.ContactId);
						break;
				}
			}			
			
		}
		
		#endregion ------------------------------------------------------------


		#region ----- Contact -------------------------------------------------

		public IContactBus GetContact(object contactId)
		{
			var result = new ContactBus();
			var contact = _repo.SingleOrDefault<Contact>(contactId);

			if (contact != null)
			{
				result.ContactData = contact;
			}
			
			return result;
		}

		
		public IContactBus LoadDetails(IContactBus contact)
		{
			if (contact.ContactData != null) 
			{
				var dataService = ServiceLocator.Current.GetInstance<IDataService>();
				
				if (dataService != null) 
				{
					var repo = dataService.Repo;
					var address = repo.Query<Address>(String.Format("EntityId={0}",contact.ContactData.ContactId),"",0,"*")
						.FirstOrDefault();

					if (address != null)
					{
						contact.AddressData = address;
					}
					else
					{
						contact.AddressData = new Address
						                            {
														ObjectStatus = DataObjectStatus.Added
													};
					}
				
					var account = repo.Query<Account>(String.Format("AccountId={0}",contact.ContactData.AccountId),"",0,"*")
					                  .FirstOrDefault();
					
					contact.AccountBusData = new AccountBus();
					
					if (account != null) 
					{
						contact.AccountBusData.AccountData = account;
						contact.AccountBusData = LoadDetails(contact.AccountBusData);
					}
				}
			}
			
			return contact;
		}
		
		
		public ObservableCollection<IContactBus> GetContacts()
		{
			var result = new ObservableCollection<IContactBus>();
			var contacts = _repo.Query<Contact>();
			
			foreach (var contact in contacts) 
			{
				result.Add(new ContactBus { ContactData = contact });
			}
			
			return result;
		}
		
		
		public void DeleteContact(IContactBus contact)
		{
		    // Address
			if (contact.ContactData != null) 
			{
				_repo.Delete<Address>(contact.AddressData.AddressId);
			}
			
			// Contact
		    if (contact.ContactData != null) _repo.Delete<Contact>(contact.ContactData.ContactId);
		}


	    public void SaveContact(IContactBus contact)
		{
			// Contact data
			switch (contact.ContactData.ObjectStatus) 
			{
				case DataObjectStatus.Modified:
					_repo.Update(contact.ContactData);
					break;
				case DataObjectStatus.Added:
					_repo.Insert(contact.ContactData);
					break;
			}
			
			// Address
			if (contact.AddressData != null) 
			{
				switch (contact.AddressData.ObjectStatus) 
				{
					case DataObjectStatus.Modified:
						_repo.Update(contact.AddressData);
						break;
					case DataObjectStatus.Added:
						contact.AddressData.EntityId = contact.ContactData.ContactId;
						_repo.Insert(contact.AddressData);
						break;
				}
			}
			
		}
		
		#endregion ------------------------------------------------------------

	}

}
