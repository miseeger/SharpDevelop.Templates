using ${SolutionName}.Base.Mvvm.Interfaces;

namespace ${SolutionName}.Base.Services.Persistence.PetaPoco
{

	public static class PetaPocoMvvmExtensions
	{
		
		public static T ResetStatusIfDataObject<T> (this T poco)
		{
			if (poco != null && poco is IDataObject) 
			{
				(poco as IDataObject).ResetStatus();
			}
			
			return poco;
		}
		
	}

}
