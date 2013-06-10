using ${SolutionName}.Base.Mvvm.Interfaces;
namespace ${SolutionName}.Base.Data.Model.Interfaces
{

	public interface IAddress : IDataObject
	{
		string AddressId {get; set;}
		string Street {get; set;}
		string PoBox {get; set;}
		string ZipCode {get; set;}
		string City {get; set;}
		string Country {get; set;}
		string EntityId {get; set;}
	}

}
