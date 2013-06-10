using ${SolutionName}.Base.Mvvm.Interfaces;

namespace ${SolutionName}.Base.Data.Model.Interfaces
{

	public interface IAccount : IDataObject
	{
		string AccountId {get; set;}
		string AccountName {get; set;}
		string Type {get; set;}
	}

}
