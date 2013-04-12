using System;

namespace ${SolutionName}.Base.Data.Model.Interfaces
{

	public interface IContact
	{
		string ContactId {get; set;}
		string AccountId {get; set;}
		string Lastname {get; set;}
		string Firstname {get; set;}
		string AddressId {get; set;}
		string Type {get; set;}
	}

}
