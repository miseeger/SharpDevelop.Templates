using System;
using PrismApp.Base.Mvvm;
using PrismApp.Base.Data.Model.Interfaces;

namespace PrismApp.Base.Data.Model
{

	public class Address : DataObject, IAddress
	{

		public Address()
		{
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
		
		private string _street;
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
		
	}

}
