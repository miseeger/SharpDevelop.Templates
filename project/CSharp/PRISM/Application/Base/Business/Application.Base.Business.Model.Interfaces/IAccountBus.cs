using System.Collections.ObjectModel;
using ${SolutionName}.Base.Data.Model.Interfaces;

namespace ${SolutionName}.Base.Business.Model.Interfaces
{

	public interface IAccountBus : IDirtyBus
	{
		IAccount AccountData {get; set;}
		IAddress AddressData {get; set;}
		ObservableCollection<IContact> Contacts {get; set;}
	}

}
