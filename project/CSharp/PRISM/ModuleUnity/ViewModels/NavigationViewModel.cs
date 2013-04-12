using System;
using System.Collections.ObjectModel;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Regions;
using <SolutionName>.Base.Names;
using <SolutionName>.Base.Interfaces.Services;
using <SolutionName>.Base.Mvvm;
using <SolutionName>.Base.Navigation;
using <SolutionName>.Base.Resource;
using ${ProjectName}.ViewModels.Interfaces;
using ${ProjectName}.Views;


namespace ${ProjectName}.ViewModels
{

	public class NavigationViewModel : ViewModelBase, INavigationViewModel, INavigationAware, IRegionMemberLifetime
	{
		
 		private IAppResourceService _appResourceService;
 		private IRegionManager _regionManager;
		
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
        
		private RelayCommand<string> _navigateCommand;
		public RelayCommand<string> NavigateCommand
		{
			get { return _navigateCommand ?? (_navigateCommand = new RelayCommand<string>(Navigate)); }
		}

        private void Navigate(string NavigationPath)
        {
        	_regionManager.NavigateToMainRegion(NavigationPath);
        }
 
        public NavigationViewModel(IAppResourceService AppResourceService, IRegionManager RegionManager)
		{
        	_appResourceService = AppResourceService;
        	_regionManager = RegionManager;

        	VmTitle =  Names.NavTitle;

        	InitializeNavigation();
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
        

        void InitializeNavigation()
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
		
		
		public bool IsNavigationTarget(NavigationContext navigationContext)
		{
			return true;
		}


		public void OnNavigatedTo(NavigationContext navigationContext)
		{
			NavItems.NavigateToActiveItem(_regionManager, RegionNames.MainRegion);
		}
		
		
		public void OnNavigatedFrom(NavigationContext navigationContext)
		{
		}

		
		public bool KeepAlive
		{
			get { return true; }
		}
            
	}
	
}