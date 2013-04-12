using System;
using System.Collections;
using System.ComponentModel;
using ${SolutionName}.Base.Mvvm.Events;

namespace ${SolutionName}.Base.Mvvm.Interfaces
{

	public interface INotifyDataErrorInfo
	{
		event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
		bool HasErrors { get; }
		IEnumerable GetErrors(string propertyName);
	}
}