using System;
using ${SolutionName}.Base.Mvvm;
using ${SolutionName}.Base.Data.Model.Interfaces;

namespace ${SolutionName}.Base.Data.Model
{

	public class Account : DataObject, IAccount
	{

		public Account()
		{
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
		
		
		private string _accountName;
		public string AccountName
		{
			get { return _accountName; }
			set
			{ 
				if(_accountName != value)
				{
					_accountName = value;
					RaisePropertyChanged(() => AccountName);
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
