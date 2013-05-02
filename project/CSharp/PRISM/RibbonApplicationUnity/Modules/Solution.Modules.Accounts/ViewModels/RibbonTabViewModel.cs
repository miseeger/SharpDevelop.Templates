using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using ${SolutionName}.Base;
using ${SolutionName}.Base.Enums;
using ${SolutionName}.Base.Interfaces.Services;
using ${SolutionName}.Base.Mvvm;
using ${SolutionName}.Base.Names;
using ${SolutionName}.Base.Navigation;
using ${SolutionName}.Modules.Accounts.ViewModels.Interfaces;
using ${SolutionName}.Modules.Accounts.Views;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;

namespace ${SolutionName}.Modules.Accounts.ViewModels
{

	public class RibbonTabViewModel : ViewModelBase, IRibbonTabViewModel
	{
		
		private IRegionManager _regionManager;
		private IAppResourceService _appResourceService;
		private IEventAggregator _eventAggregator;
		
		private List<CommandItem> Commands { get; set; }
		public List<CommandItem> NavList { get; set; }
		public List<CommandItem> CommandList { get; set; }
        

		private RelayCommand<string> _navigateCommand;
		public RelayCommand<string> NavigateCommand
		{
			get { return _navigateCommand ?? (_navigateCommand = new RelayCommand<string>(Navigate)); }
		}

        private void Navigate(string NavigationPath)
        {
        	_regionManager.NavigateToMainRegion(NavigationPath);
        }
 

        public RibbonTabViewModel(IAppResourceService AppResourceService, IRegionManager RegionManager, IEventAggregator EventAggregator)
		{
        	_appResourceService = AppResourceService;
        	_regionManager = RegionManager;
        	_eventAggregator = EventAggregator;

			VmTitle = Names.RibbonTabView;
        	_eventAggregator.GetEvent<RibbonTabChangedEvent>().Subscribe(OnRibbonTabChanged);

        	InitializeCommands();
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
        

        void InitializeCommands()
		{
			Commands = new List<CommandItem>();
			
			Commands.Add(new CommandItem() 
		          		{ 
		              		Caption = Names.NavItem1
		              		, Type = CommandType.Navigation
	              			, CommandUiType = CommandUiType.ToggleButton.ToString()
		              		, GroupName = Names.RibbonNavGroupName
		              		, RadioGroupName = Names.RibbonViewGroupName
		              		, Command = NavigateCommand
		              		, CommandParameter = CreateNavigationPath(Names.NavItem1)
		              		, Icon = _appResourceService.GetPng16("users")
		              		, IconSize = "Small"
	              			, SizeDefinition = "Middle"
		              		, Order = 10
		              		, IsActive = true
		              	});
			
			Commands.Add(new CommandItem() 
		              	{ 
		              		Caption = Names.NavItem2
		              		, Type = CommandType.Navigation
	              			, CommandUiType = CommandUiType.ToggleButton.ToString()
	              			, GroupName = Names.RibbonNavGroupName
		              		, RadioGroupName = Names.RibbonViewGroupName
		              		, Command = NavigateCommand
		              		, CommandParameter = CreateNavigationPath(Names.NavItem2)
		              		, Icon = _appResourceService.GetPng16("world")
		              		, IconSize = "Small"
		              		, SizeDefinition = "Middle"
		              		, Order = 20
		              	});
			
			Commands.Add(new CommandItem()
			             {
			             	Caption = "Save"
			             	, Type = CommandType.Command
		             		, CommandUiType = CommandUiType.Button.ToString()
			             	, GroupName = Names.RibbonStartGroupName
			             	, Command = ModuleCommands.SaveCommand
		             		, Icon = _appResourceService.GetPng32("disk-black")
		              		, IconSize = "Large"
			             	, SizeDefinition = "Large"
			             	, Order = 10
			             });
			             
			Commands.Add(new CommandItem()
			             {
			             	Caption = "Delete"
			             	, Type = CommandType.Command
		             		, CommandUiType = CommandUiType.Button.ToString()
			             	, GroupName = Names.RibbonStartGroupName
			             	, Command = ModuleCommands.DeleteCommand
		             		, Icon = _appResourceService.GetPng16("cross")
		              		, IconSize = "Small"
			             	, SizeDefinition = "Middle"
			             	, Order = 20
			             });

			Commands.Add(new CommandItem()
			             {
			             	Caption = "Refresh"
			             	, Type = CommandType.Command
		             		, CommandUiType = CommandUiType.Button.ToString()
			             	, GroupName = Names.RibbonStartGroupName
			             	, Command = ModuleCommands.RefreshCommand
		             		, Icon = _appResourceService.GetPng16("refresh")
		              		, IconSize = "Small"
			             	, SizeDefinition = "Middle"
			             	, Order = 30
			             });

			NavList = Commands.Where(i => i.Type == CommandType.Navigation).ToList();
			CommandList = Commands.Where(i => i.Type == CommandType.Command).ToList();
			
		}
        
        
        public void OnRibbonTabChanged(string TabName)
  		{
  			if (TabName == Names.RibbonTabView) 
  			{
  				NavList.NavigateToActiveItem(_regionManager, RegionNames.MainRegion);
  			}
        }
        
	}

}