using ${SolutionName}.Base.Business.Model.Interfaces;
using ${SolutionName}.Base.Data.Model.Interfaces;

namespace ${SolutionName}.Base.Business.Model
{

	public class ContactBus : IContactBus
	{

		public IContact ContactData {get; set;}
        public IAddress AddressData { get; set; }
        public IAccountBus AccountBusData { get; set; }


	    public bool IsDirty
		{
			get
			{
				return ((ContactData != null && ContactData.IsDirty)
		        		|| (AddressData != null && AddressData.IsDirty));
			}
		}
	}

}
