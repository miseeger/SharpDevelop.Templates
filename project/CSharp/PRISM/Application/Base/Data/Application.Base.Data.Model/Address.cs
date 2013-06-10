using ${SolutionName}.Base.Data.Model.Interfaces;
using ${SolutionName}.Base.Mvvm;
using ${SolutionName}.Base.Services.Persistence.PetaPoco;

namespace ${SolutionName}.Base.Data.Model
{

	[TableName("Address")]
	[PrimaryKey("AddressId")]
	[ExplicitColumnsIgnoreBase]
	public class Address : EditableDataObject, IAddress
	{
	    private string _addressId;
		[Column] 
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
		
		private string _street;
		[Column] 
		public string Street
		{
			get { return _street; }
			set
			{ 
				if(_street != value)
				{
					_street = value;
					RaisePropertyChanged(() => Street);
				}
			}	
		}
		
		private string _poBox;
		[Column] 
		public string PoBox
		{
			get { return _poBox; }
			set
			{ 
				if(_poBox != value)
				{
					_poBox = value;
					RaisePropertyChanged(() => PoBox);
				}
			}	
		}
		
		private string _zipCode;
		[Column] 
		public string ZipCode
		{
			get { return _zipCode; }
			set
			{ 
				if(_zipCode != value)
				{
					_zipCode = value;
					RaisePropertyChanged(() => ZipCode);
				}
			}	
		}
		
		private string _city;
		[Column] 
		public string City
		{
			get { return _city; }
			set
			{ 
				if(_city != value)
				{
					_city = value;
					RaisePropertyChanged(() => City);
				}
			}	
		}
		
		private string _country;
		[Column] 
		public string Country
		{
			get { return _country; }
			set
			{ 
				if(_country != value)
				{
					_country = value;
					RaisePropertyChanged(() => Country);
				}
			}	
		}
		
		
		private string _entityId;
		[Column] 
		public string EntityId
		{
			get { return _entityId; }
			set
			{ 
				if(_entityId != value)
				{
					_entityId = value;
					RaisePropertyChanged(() => EntityId);
				}
			}	
		}
		
	}

}
