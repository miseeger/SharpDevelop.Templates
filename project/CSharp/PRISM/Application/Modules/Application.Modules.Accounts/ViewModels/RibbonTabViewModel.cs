using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using ${SolutionName}.Base;
using ${SolutionName}.Base.Data.Model.System;
using ${SolutionName}.Base.Data.Enums.System;
using ${SolutionName}.Base.Interfaces.Services;
using ${SolutionName}.Base.Mvvm;
using ${SolutionName}.Base.Names;
using ${SolutionName}.Base.Navigation;
using ${SolutionName}.Modules.Accounts.ViewModels.Interfaces;
using ${SolutionName}.Modules.Accounts.Views;

namespace ${SolutionName}.Modules.Accounts.ViewModels
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
		
		#endregion ------------------------------------------------------------
        

		#region ----- .ctor ---------------------------------------------------

		public RibbonTabViewModel(IAppResourceService appResourceService, IRegionManager regionManager, IEventAggregator eventAggregator)
		{
        	_appResourceService = appResourceService;
        	_regionManager = regionManager;
        	_eventAggregator = eventAggregator;

			VmTitle = Names.RibbonTabView;
        	_eventAggregator.GetEvent<RibbonTabChangedEvent>().Subscribe(OnRibbonTabChanged);

        	InitializeCommands();
        }
		
        #endregion ------------------------------------------------------------
        

        #region ----- Initialization ------------------------------------------

        void InitializeCommands()
		{
			Commands = new List<CommandItem>
			    {
			        new CommandItem
			            {
			                Caption = Names.NavItem1
			                ,Type = CommandType.Navigation
			                ,GroupName = Names.RibbonNavGroupName
			                ,RadioGroupName = Names.RibbonViewGroupName
			                ,Command = NavigateCommand
			                ,CommandParameter = typeof (View1)
			                    .CreateNavigationPath(new Dictionary<string, string>
			                        {
			                            {Names.DataNavTypeName, Names.DataNavTypeCustomer}
			                        })
			                ,Icon = _appResourceService.GetPng16("group")
			                ,IconSize = "Small"
			                ,SizeDefinition = "Middle"
			                ,Order = 10
			                ,IsActive = false
			            }

			        ,new CommandItem
			            {
			                Caption = Names.NavItem2
			                ,Type = CommandType.Navigation
			                ,GroupName = Names.RibbonNavGroupName
			                ,RadioGroupName = Names.RibbonViewGroupName
			                ,Command = NavigateCommand
			                ,CommandParameter = typeof (View2)
			                    .CreateNavigationPath(new Dictionary<string, string>
			                        {
			                            {Names.DataNavTypeName, Names.DataNavTypeVendor}
			                        })
			                ,Icon = _appResourceService.GetPng16("building")
			                ,IconSize = "Small"
			                ,SizeDefinition = "Middle"
			                ,Order = 20
			                ,IsActive = true
			            }
			        
                   , new CommandItem
                        {
			                Caption = "New Account"
			                ,Type = CommandType.Command
			                ,GroupName = Names.RibbonStartGroupName
			                ,Command = GlobalCommands.NewCommand
			                ,Icon = _appResourceService.GetPng32("document_new")
			                ,IconSize = "Large"
			                ,SizeDefinition = "Large"
			                ,Order = 10
			            }

			        ,new CommandItem
			            {
			                Caption = "Save"
			                ,Type = CommandType.Command
			                ,GroupName = Names.RibbonStartGroupName
			                ,Command = GlobalCommands.SaveCommand
			                ,Icon = _appResourceService.GetPng16("disk")
			                ,IconSize = "Small"
			                ,SizeDefinition = "Middle"
			                ,Order = 15
			            }

			        ,new CommandItem
			            {
			                Caption = "Delete"
			                ,Type = CommandType.Command
			                ,GroupName = Names.RibbonStartGroupName
			                ,Command = GlobalCommands.DeleteCommand
			                ,Icon = _appResourceService.GetPng16("cross")
			                ,IconSize = "Small"
			                ,SizeDefinition = "Middle"
			                ,Order = 20
			            }

			        ,new CommandItem
			            {
			                Caption = "Refresh"
			                ,Type = CommandType.Command
			                ,GroupName = Names.RibbonStartGroupName
			                ,Command = GlobalCommands.RefreshCommand
			                ,Icon = _appResourceService.GetPng16("arrow_refresh")
			                ,IconSize = "Small"
			                ,SizeDefinition = "Middle"
			                ,Order = 30
			            }
			    };

            NavList = Commands.Where(i => i.Type == CommandType.Navigation).ToList();
			CommandList = Commands.Where(i => i.Type == CommandType.Command).ToList();
			
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