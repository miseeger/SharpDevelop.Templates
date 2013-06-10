using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using ${SolutionName}.Base.Business.Model.Interfaces;
using ${SolutionName}.Base.Extensions;
using ${SolutionName}.Base.Interfaces.Services;
using ${SolutionName}.Base.Mvvm;
using ${SolutionName}.Modules.Contacts.ViewModels.Interfaces;

namespace ${SolutionName}.Modules.Contacts.ViewModels
{

	public class DataNavigationViewModel : ViewModelBase, IDataNavigationViewModel, IConfirmNavigationRequest, IRegionMemberLifetime
	{
		
		#region ----- Fields --------------------------------------------------	
		
		IBusinessService _businessService;
		IEventAggregator _eventAggregator;

	    private ObservableCollection<IContactBus> _contacts;
		public ObservableCollection<IContactBus> Contacts
		{
			get { return _contacts; }
			set
			{
				if(_contacts != value)
				{
					_contacts = value;
					RaisePropertyChanged(() => Contacts);
				}
			}
		}
		
		private IContactBus _selectedContact;
		public IContactBus SelectedContact
		{
			get { return _selectedContact; }
			set
			{
				if(_selectedContact != value)
				{
					if (_selectedContact != null && value != null) 
					{
						_eventAggregator.GetEvent<DataNavBeforeChanging>().Publish(new object[]
			                                                    {
			                                                    	_selectedContact.ContactData.ContactId
			                                                    	, value.ContactData.ContactId
			                                                    });
					}
					
					_selectedContact = value;
					RaisePropertyChanged(() => SelectedContact);
					_eventAggregator.GetEvent<DataNavChanged>().Publish(value != null ? value.ContactData.ContactId : "");
				}
			}
		}
		
		
		private ImageSource _icon;
		public ImageSource Icon
		{
			get { return _icon; }
			set
			{
				if(_icon != value)
				{
					_icon = value;
					RaisePropertyChanged(() => Icon);
				}
			}
		}
		
		#endregion ------------------------------------------------------------
		
		
		#region ----- .ctor ---------------------------------------------------

		public DataNavigationViewModel(IBusinessService businessService, IEventAggregator eventAggregator)
		{
			_businessService = businessService;
			_eventAggregator = eventAggregator;
		    _eventAggregator.GetEvent<DataNavRefresh>().Subscribe(OnDataNavRefresh);
		}
		
		#endregion ------------------------------------------------------------
		
		
		#region ----- Common Event Handling -----------------------------------

		private void OnDataNavRefresh(object selectedId)
		{
			Contacts = _businessService.GetContacts().OrderBy(c => c.ContactData.LastFirst).ToObservableCollection();
			
			SelectedContact = (selectedId != null) 
				? Contacts != null ? Contacts.FirstOrDefault(a => a.ContactData.ContactId == selectedId.ToString()) : null
				: Contacts != null ? Contacts.FirstOrDefault() : null;
		}
		
		#endregion ------------------------------------------------------------
		
		
		#region ----- Navigation Event Handling -------------------------------

		public bool IsNavigationTarget(NavigationContext navigationContext)
		{
			return true;
		}
		

		public void OnNavigatedTo(NavigationContext navigationContext)
		{
			VmTitle = navigationContext.Parameters[Names.RibbonTabView];
			var selectedId = navigationContext.Parameters[Names.SelectedIdName];
			OnDataNavRefresh(selectedId != "" ? selectedId : null);
		}
		
		
		public void OnNavigatedFrom(NavigationContext navigationContext)
		{
			// TODO: Impelemtn code ...
		}
		
		
		public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
		{
			bool result = true;
			
			// TODO: Implement code to set result
			
			continuationCallback(result);
		}
		
		#endregion ------------------------------------------------------------
		

		#region ----- Member Lifetime------------------------------------------			

		public bool KeepAlive
		{
			get
			{
				return true;
			}
		}
		
		#endregion ------------------------------------------------------------
	
	}

}