using ${SolutionName}.Base.Data.Model.Interfaces;
using ${SolutionName}.Base.Mvvm;
using ${SolutionName}.Base.Services.Persistence.PetaPoco;

namespace ${SolutionName}.Base.Data.Model
{

	[TableName("Account")]
	[PrimaryKey("AccountId")]
	[ExplicitColumnsIgnoreBase]
	public class Account : EditableDataObject, IAccount
	{
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
		
		
		private string _accountName;
		[Column]
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
		
		
		private string _type;
		[Column]
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
