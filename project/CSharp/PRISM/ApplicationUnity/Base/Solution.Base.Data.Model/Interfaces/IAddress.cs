using System;

namespace ${SolutionName}.Base.Data.Model.Interfaces
{

	public interface IAddress
	{
		string AddressId {get; set;}
		string Street {get; set;}
		string PoBox {get; set;}
		string ZipCode {get; set;}
		string City {get; set;}
		string Country {get; set;}
	}

}
