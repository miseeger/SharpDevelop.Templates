using ${SolutionName}.Base.Data.Model.Interfaces;

namespace ${SolutionName}.Base.Business.Model.Interfaces
{

	public interface IContactBus : IDirtyBus
	{
		IContact ContactData {get; set;}
		IAddress AddressData {get; set;}
		IAccountBus AccountBusData {get; set;}
	}

}
