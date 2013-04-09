using System;
using <SolutionName>.Base;
using <SolutionName>.Base.Mvvm;
using ${StandardNamespace}.ViewModels.Interfaces;

namespace ${StandardNamespace}.ViewModels
{

	// TODO: Register ViewModel and View in ModuleInit
	//       -----------------------------------------
	
	// TODO: ViewModel-Registration
	//       _container.RegisterType<I${FileNameWithoutExtension}ViewModel, ${FileNameWithoutExtension}ViewModel>();
	// TODO: Registration for ViewNavigation
	//       _container.RegisterTypeForNavigation<${FileNameWithoutExtension}View>();
	
	// TODO: Add Names
	//       public const string ${FileNameWithoutExtension}NavItem = "${FileNameWithoutExtension}";
	//       public const string ${FileNameWithoutExtension}View = "${FileNameWithoutExtension}";
	
	// TODO: Add Navigation Item to NavigationViewModel
	//		 NavItems.Add(new NavigationItem() 
	//						{ 
	//		              		Caption = Names.${FileNameWithoutExtension}NavItem
	//		              		, NavigationPath = CreateNavigationPath(Names.${FileNameWithoutExtension}NavItem)
	//		              		, ItemImage = _appResourceService.GetPng16("jar")
	//		              	});
	
	// TODO: Add code to create a navigation path
	//       case Names.${FileNameWithoutExtension}NavItem:
    //       	return typeof(${FileNameWithoutExtension}View).FullName + query;


	public class ${FileNameWithoutExtension}ViewModel : ViewModelBase, I${FileNameWithoutExtension}ViewModel
	{
		
		public ${FileNameWithoutExtension}ViewModel()
		{
			VmTitle = Names.${FileNameWithoutExtension}View;
		}
	
	}

}