using System.Collections.Generic;
 
namespace ${SolutionName}.Base.Mvvm 
{ 
	public static class Names 
	{ 
 
		public static string IsDirtyName = "IsDirty";
		public static string ViewName = "View";
		public static string VmTitleName = "VmTitle";
		public static string VmImageName = "VmImage";
		public static string ObjectStatusName = "ObjectStatus";
		public static string IsBusyName = "IsBusy";
		public static string HasErrorsName = "HasErrors";
		public static string IsValidName = "IsValid";
		public static string InDesignName = "InDesign";
		
		public static IEnumerable<string>
			PropBackupIgnoringProperties = new List<string> 
											   { 
												  ViewName
												  ,VmTitleName
												  ,VmImageName
											   }; 
 
		public static IEnumerable<string> 
			ObjectStatusIgnoringProperties = new List<string>() 
												 { 
													 ObjectStatusName
													 ,IsBusyName
													 ,IsDirtyName
													 ,HasErrorsName 
													 ,IsValidName
													 ,InDesignName
													 ,ViewName
													 ,VmTitleName
													 ,VmImageName													 	
												 }; 
		
		public static IEnumerable<string> 
			IsDrityIgnoringProperties = ObjectStatusIgnoringProperties;

	} 

} 
