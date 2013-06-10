using ${SolutionName}.Base.Mvvm.Interfaces;
namespace ${SolutionName}.Base.Data.Model.Interfaces
{

	public interface IContact : IDataObject
	{
		string ContactId {get; set;}
		string AccountId {get; set;}
		string Gender  {get; set;}
		string Salutation {get; set;}
		string Lastname {get; set;}
		string Firstname {get; set;}
		string Phone {get; set;}
		string MobilePhone {get; set;}
		string LastFirst {get;}
		string FirstLast {get;}
	}

}
