using System.Collections.ObjectModel;
using System.Linq;
using ${SolutionName}.Base.Business.Model.Interfaces;
using ${SolutionName}.Base.Data.Model.Interfaces;

namespace ${SolutionName}.Base.Business.Model
{

	public class AccountBus : IAccountBus
	{

		public IAccount AccountData {get; set;}
        public IAddress AddressData { get; set; }
		public ObservableCollection<IContact> Contacts {get; set;}

		
		public AccountBus()
		{
			Contacts = new ObservableCollection<IContact>();
		}
		
		
		public bool IsDirty
		{
			get
			{
				return ((AccountData != null && AccountData.IsDirty)
		        		|| (AddressData != null && AddressData.IsDirty)
		        		|| Contacts.Any(c => c.IsDirty));
			}
		}
	}

}
