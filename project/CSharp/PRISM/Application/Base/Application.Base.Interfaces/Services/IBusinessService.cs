using System.Collections.ObjectModel;
using ${SolutionName}.Base.Business.Model.Interfaces;

namespace ${SolutionName}.Base.Interfaces.Services
{
	
	public interface IBusinessService
	{

		// Account Logic
		IAccountBus GetAccount(object accountId);
		IAccountBus LoadDetails(IAccountBus account);
		ObservableCollection<IAccountBus> GetAccounts();
		ObservableCollection<IAccountBus> GetAccountsByType(string type);
		void DeleteAccount(IAccountBus account);
		void SaveAccount (IAccountBus account);
		
		// Contact Logic
		IContactBus GetContact(object contactId);
		IContactBus LoadDetails(IContactBus contact);
		ObservableCollection<IContactBus> GetContacts();
		void DeleteContact(IContactBus contact);
		void SaveContact (IContactBus contact);
		
	}
	
}
