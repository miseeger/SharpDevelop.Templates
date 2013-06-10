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
using ${SolutionName}.Modules.Contacts.ViewModels.Interfaces;
using ${SolutionName}.Modules.Contacts.Views;

namespace ${SolutionName}.Modules.Contacts.ViewModels
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
			                ,GroupName = Names.RibbonViewGroupName
			                ,RadioGroupName = Names.RibbonNavGroupName
			                ,Command = NavigateCommand
			                ,CommandParameter = typeof (View1).CreateNavigationPath(null)
			                ,Icon = _appResourceService.GetPng32("form")
			                ,IconSize = "Large"
			                ,SizeDefinition = "Large"
			                ,Order = 10
			                ,IsActive = true
			            }

			        ,new CommandItem
			            {
			                Caption = Names.NavItem2
			                ,Type = CommandType.Navigation
			                ,GroupName = Names.RibbonViewGroupName
			                ,RadioGroupName = Names.RibbonNavGroupName
			                ,Command = NavigateCommand
			                ,CommandParameter = typeof (View2).CreateNavigationPath(null)
			                ,Icon = _appResourceService.GetPng32("table")
			                ,IconSize = "Large"
			                ,SizeDefinition = "Large"
			                ,Order = 20
			            }

			        ,new CommandItem
			            {
			                Caption = "New Contact"
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
