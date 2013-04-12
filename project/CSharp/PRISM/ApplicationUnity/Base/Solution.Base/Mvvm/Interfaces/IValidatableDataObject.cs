using System.Collections.Generic;
using System.ComponentModel;
using ${SolutionName}.Base.Mvvm.Enum;

namespace ${SolutionName}.Base.Mvvm.Interfaces
{

	public interface IValidatableDataObject : IEditableDataObject, IDataErrorInfo, INotifyDataErrorInfo
	{
		string Errorlist { get; }
		List<string> ErrorStringlist { get; }
		bool IsValid { get; }
	}

}
