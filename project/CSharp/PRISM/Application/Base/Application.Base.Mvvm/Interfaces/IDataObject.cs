using System.ComponentModel;
using ${SolutionName}.Base.Mvvm.Enums;

namespace ${SolutionName}.Base.Mvvm.Interfaces
{

	public interface IDataObject : INotifyPropertyChanged
	{
		DataObjectStatus ObjectStatus { get; set; }
		bool IgnoreObjectStatus { get; set; }
		bool IsDirty { get; set; }
		void ResetStatus(); 
	}

}
