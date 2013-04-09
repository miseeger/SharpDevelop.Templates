using System;
using System.Collections.ObjectModel;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Regions;
using <SolutionName>.Base.Interfaces.Services;
using <SolutionName>.Base.Mvvm;
using <SolutionName>.Base.Navigation;
using <SolutionName>.Base.Resource;
using ${ProjectName}.ViewModels.Interfaces;
using ${ProjectName}.Views;


namespace ${ProjectName}.ViewModels
{

	public class NavigationViewModel : ViewModelBase, INavigationViewModel
	{
		
 		private IAppResourceService _appResourceService;
		
		private ObservableCollection<NavigationItem> _navItems;
        public ObservableCollection<NavigationItem> NavItems
        {
            get { return _navItems; }
            set
            {
                _navItems = value;
                RaisePropertyChanged(() => this.NavItems);
            }
        }
        
 
        public NavigationViewModel(IAppResourceService AppResourceService)
		{
        	_appResourceService = AppResourceService;
        	VmTitle =  Names.NavTitle;
        	InitializeMenu();
		}
        

        void InitializeMenu()
		{
			NavItems = new ObservableCollection<NavigationItem>();
			
			NavItems.Add(new NavigationItem() 
			          		{ 
			              		Caption = Names.NavItem1
			              		, NavigationPath = CreateNavigationPath(Names.NavItem1)
			              		, ItemImage = _appResourceService.GetPng16("globe")
			              	});
			NavItems.Add(new NavigationItem() 
			              	{ 
			              		Caption = Names.NavItem2
			              		, NavigationPath = CreateNavigationPath(Names.NavItem2)
			              		, ItemImage = _appResourceService.GetPng16("jar")
			              	});
		}
		
		
        private string CreateNavigationPath(string navItemName)
        {
            UriQuery query = new UriQuery();
            query.Add(Names.NavKey, navItemName);
            
            switch (navItemName) 
            {
            	case Names.NavItem1:
            		return typeof(View1).FullName + query;
            	case Names.NavItem2:
            		return typeof(View2).FullName + query;
            	default:
            		return null;
            }
            
        }
        
	}
	
}