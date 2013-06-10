using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using <SolutionName>.Base;
using <SolutionName>.Base.Data.Model.System;
using <SolutionName>.Base.Data.Enums.System;
using <SolutionName>.Base.Interfaces.Services;
using <SolutionName>.Base.Mvvm;
using <SolutionName>.Base.Names;
using <SolutionName>.Base.Navigation;
using ${ProjectName}.ViewModels.Interfaces;
using ${ProjectName}.Views;

namespace ${ProjectName}.ViewModels
{

	public class RibbonTabViewModel : ViewModelBase, IRibbonTabViewModel
	{
		
		#region ----- Fields --------------------------------------------------	

		private IRegionManager _regionManager;
		private IAppResourceService _appResourceService;
		private IEventAggregator _eventAggregator;
		
		private List<CommandItem> Commands { get; set; }
		public List<CommandItem> NavList { get; set; }
		public List<CommandItem> CommandList { get; set; }
		
		// TODO: Delclare further Lists of NavigationItems or CommandItems
		
		#endregion ------------------------------------------------------------
        

		#region ----- .ctor ---------------------------------------------------

		public RibbonTabViewModel(IAppResourceService appResourceService, IRegionManager regionManager, IEventAggregator eventAggregator)
		{
        	_appResourceService = appResourceService;
        	_regionManager = regionManager;
        	_eventAggregator = eventAggregator;

			VmTitle = Names.RibbonTabView;
        	_eventAggregator.GetEvent<RibbonTabChangedEvent>().Subscribe(OnRibbonTabChanged);
        	
        	// TODO: Subscribe to any other Event you need to handle here

        	InitializeCommands();
        }
		
        #endregion ------------------------------------------------------------
        

        #region ----- Initialization ------------------------------------------

        void InitializeCommands()
		{
			// TODO: Create a List of NavigationItems or CommandItems
			
			#region --> Sample Code: Command List
			//Commands = new List<CommandItem>
			//    {
			
			#region --> Sample Code NavigationItem

			//		new CommandItem
			//            {
			//                Caption = Names.NavItem1
			//                ,Type = CommandType.Navigation
			//                ,GroupName = Names.RibbonViewGroupName
			//                ,RadioGroupName = Names.RibbonNavGroupName
			//                ,Command = NavigateCommand
			//                ,CommandParameter = typeof (View1).CreateNavigationPath(null)
			//                ,Icon = _appResourceService.GetPng32("form")
			//                ,IconSize = "Large"
			//                ,SizeDefinition = "Large"
			//                ,Order = 10
			//                ,IsActive = true
			//            }
			//
			//        ,new CommandItem
			//            {
			//                Caption = Names.NavItem2
			//                ,Type = CommandType.Navigation
			//                ,GroupName = Names.RibbonViewGroupName
			//                ,RadioGroupName = Names.RibbonNavGroupName
			//                ,Command = NavigateCommand
			//                ,CommandParameter = typeof (View2).CreateNavigationPath(null)
			//                ,Icon = _appResourceService.GetPng32("table")
			//                ,IconSize = "Large"
			//                ,SizeDefinition = "Large"
			//                ,Order = 20
			//            }

			#endregion

			#region --> Sample Code: CommandItems

			//        ,new CommandItem
			//            {
			//                Caption = "New Contact"
			//                ,Type = CommandType.Command
			//                ,GroupName = Names.RibbonStartGroupName
			//                ,Command = GlobalCommands.NewCommand
			//                ,Icon = _appResourceService.GetPng32("document_new")
			//                ,IconSize = "Large"
			//                ,SizeDefinition = "Large"
			//                ,Order = 10
			//            }
			//
			//        ,new CommandItem
			//            {
			//                Caption = "Save"
			//                ,Type = CommandType.Command
			//                ,GroupName = Names.RibbonStartGroupName
			//                ,Command = GlobalCommands.SaveCommand
			//                ,Icon = _appResourceService.GetPng16("disk")
			//                ,IconSize = "Small"
			//                ,SizeDefinition = "Middle"
			//                ,Order = 15
			//            }
			//
			//        ,new CommandItem
			//            {
			//                Caption = "Delete"
			//                ,Type = CommandType.Command
			//                ,GroupName = Names.RibbonStartGroupName
			//                ,Command = GlobalCommands.DeleteCommand
			//                ,Icon = _appResourceService.GetPng16("cross")
			//                ,IconSize = "Small"
			//                ,SizeDefinition = "Middle"
			//                ,Order = 20
			//            }
			//
			//        ,new CommandItem
			//            {
			//                Caption = "Refresh"
			//                ,Type = CommandType.Command
			//                ,GroupName = Names.RibbonStartGroupName
			//                ,Command = GlobalCommands.RefreshCommand
			//                ,Icon = _appResourceService.GetPng16("arrow_refresh")
			//                ,IconSize = "Small"
			//                ,SizeDefinition = "Middle"
			//                ,Order = 30
			//            }

			#endregion
			
			//    };

			#endregion

            // TODO: Select the Command Lists to bind to in XAML Code
            
            #region --> Sample Code: NavList and CommandList to bind to
            
			//NavList = Commands.Where(i => i.Type == CommandType.Navigation).ToList();
			//CommandList = Commands.Where(i => i.Type == CommandType.Command).ToList();
            
			#endregion
		}
        
        #endregion ------------------------------------------------------------
        
        
   		#region ----- NavigateCommand -----------------------------------------
		
		private RelayCommand<string> _navigateCommand;
		public RelayCommand<string> NavigateCommand
		{
			get { return _navigateCommand ?? (_navigateCommand = new RelayCommand<string>(Navigate)); }
		}


		private void Navigate(string navigationPath)
        {
        	_regionManager.NavigateToMainRegion(navigationPath);
        }
		
		#endregion ------------------------------------------------------------
 

  		#region ----- Common Event Handling -----------------------------------
        
        public void OnRibbonTabChanged(string tabName)
  		{
  			if (tabName == Names.RibbonTabView) 
  			{
  				NavList.NavigateToActiveItem(_regionManager, RegionNames.MainRegion);
  			}
        }
        
   		#endregion ---------------------------------------------------------
        
	}

}
