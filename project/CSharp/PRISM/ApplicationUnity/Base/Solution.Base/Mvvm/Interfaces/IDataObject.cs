using System.Collections.Generic;
using System.ComponentModel;
using ${SolutionName}.Base.Mvvm.Enum;

namespace ${SolutionName}.Base.Mvvm.Interfaces
{

	public interface IDataObject
	{
		DataObjectStatus ObjectStatus { get; set; }
		bool IsDirty { get; }
	}

}
