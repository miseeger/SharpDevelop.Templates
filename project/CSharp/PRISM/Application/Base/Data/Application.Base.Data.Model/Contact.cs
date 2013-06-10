using ${SolutionName}.Base.Data.Model.Interfaces;
using ${SolutionName}.Base.Mvvm;
using ${SolutionName}.Base.Services.Persistence.PetaPoco;

namespace ${SolutionName}.Base.Data.Model
{

	[TableName("Contact")]
	[PrimaryKey("ContactId")]
	[ExplicitColumnsIgnoreBase]
	public class Contact : EditableDataObject, IContact
	{
	    private string _contactId;
		[Column] 
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
		[Column] 
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
		
		private string _gender;
		[Column] 
		public string Gender
		{
			get { return _gender; }
			set
			{ 
				if(_gender != value)
				{
					_gender = value;
					RaisePropertyChanged(() => Gender);
				}
			}	
		}

		private string _salutation;
		[Column] 
		public string Salutation
		{
			get { return _salutation; }
			set
			{ 
				if(_salutation != value)
				{
					_salutation = value;
					RaisePropertyChanged(() => Salutation);
				}
			}	
		}
		
		private string _lastname;
		[Column] 
		public string Lastname
		{
			get { return _lastname; }
			set
			{ 
				if(_lastname != value)
				{
					_lastname = value;
					RaisePropertyChanged(() => Lastname);
					RaisePropertyChanged(() => LastFirst);
					RaisePropertyChanged(() => FirstLast);
				}
			}	
		}
		
		private string _firstname;
		[Column] 
		public string Firstname
		{
			get { return _firstname; }
			set
			{ 
				if(_firstname != value)
				{
					_firstname = value;
					RaisePropertyChanged(() => Firstname);
					RaisePropertyChanged(() => LastFirst);
					RaisePropertyChanged(() => FirstLast);
				}
			}	
		}
		
		
		private string _phone;
		[Column]
		public string Phone
		{
			get { return _phone; }
			set
			{
				if(_phone != value)
				{
					_phone = value;
					RaisePropertyChanged(() => Phone);
				}
			}
		}
		
		
		private string _mobilePhone;
		[Column]
		public string MobilePhone
		{
			get { return _mobilePhone; }
			set
			{
				if(_mobilePhone != value)
				{
					_mobilePhone = value;
					RaisePropertyChanged(() => MobilePhone);
				}
			}
		}
		

		[ResultColumn]
		public string LastFirst
		{ 
			get 
			{ 
				return string.Format("{0}, {1}", Lastname, Firstname).Trim();
			}
		}
		

		[ResultColumn]
		public string FirstLast
		{ 
			get 
			{ 
				return string.Format("{0} {1}", Firstname, Lastname).Trim();
			}
		}
		
	}

}
