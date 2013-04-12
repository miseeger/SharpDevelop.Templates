﻿using System;
using PrismApp.Base.Mvvm;
using PrismApp.Base.Data.Model.Interfaces;

namespace PrismApp.Base.Data.Model
{

	public class Contact : DataObject, IContact
	{

		public Contact()
		{
		}
		
		private string _contactId;
		public string ContactId
		{
			get { return _contactId; }
			set
			{ 
				if(_contactId != value)
				{
					_contactId = value;
					RaisePropertyChanged(() => ContactId);
				}
			}	
		}
		
		private string _accountId;
		public string AccountId
		{
			get { return _accountId; }
			set
			{ 
				if(_accountId != value)
				{
					_accountId = value;
					RaisePropertyChanged(() => AccountId);
				}
			}	
		}
		
		private string _lastname;
		public string Lastname
		{
			get { return _lastname; }
			set
			{ 
				if(_lastname != value)
				{
					_lastname = value;
					RaisePropertyChanged(() => Lastname);
				}
			}	
		}
		
		private string _firstname;
		public string Firstname
		{
			get { return _firstname; }
			set
			{ 
				if(_firstname != value)
				{
					_firstname = value;
					RaisePropertyChanged(() => Firstname);
				}
			}	
		}
		
		private string _addressId;
		public string AddressId
		{
			get { return _addressId; }
			set
			{ 
				if(_addressId != value)
				{
					_addressId = value;
					RaisePropertyChanged(() => AddressId);
				}
			}	
		}
		
		private string _type;
		public string Type
		{
			get { return _type; }
			set
			{ 
				if(_type != value)
				{
					_type = value;
					RaisePropertyChanged(() => Type);
				}
			}	
		}
		
	}

}
