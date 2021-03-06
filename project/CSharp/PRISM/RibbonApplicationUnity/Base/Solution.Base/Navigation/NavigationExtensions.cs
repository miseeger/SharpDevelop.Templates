﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using ${SolutionName}.Base.Names;


namespace ${SolutionName}.Base.Navigation
{

	public static class NavigationExtensions
	{

		public static void RegisterTypeForNavigation<T>(this IUnityContainer container)
		{
			container.RegisterType(typeof(Object), typeof(T), typeof(T).FullName);
		}
		
		
		public static void RegisterTypeForNavigation(this IUnityContainer container, Type type)
		{
			container.RegisterType(typeof(Object), type, type.FullName);
		}
		
		
		public static void NavigateToActiveItem(this List<CommandItem> NavItemsList, 
		                                        IRegionManager RegionManager, string RegionName)
		{
			CommandItem activeNavItem = NavItemsList.Where(x => x.IsActive == true).FirstOrDefault();
			
			if (activeNavItem == null) 
			{
				activeNavItem = NavItemsList.FirstOrDefault();
			}
			
			if (activeNavItem != null) 
			{
				activeNavItem.IsActive = true;
				RegionManager.RequestNavigate(RegionName, new Uri(activeNavItem.CommandParameter.ToString(), UriKind.Relative));
			}
		}
		
		
		public static void NavitageTo(this CommandItem NavItem, IRegionManager RegionManager, string RegionName)
		{
			RegionManager.RequestNavigate(RegionName, new Uri(NavItem.CommandParameter.ToString(), UriKind.Relative));
		}
		
		
		public static void NavigateToMainRegion(this IRegionManager RegionManager, string NavigationPath)
		{
			RegionManager.RequestNavigate(RegionNames.MainRegion, new Uri(NavigationPath, UriKind.Relative));
		}
		
	}

}
