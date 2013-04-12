using System;
using System.ComponentModel;

namespace ${SolutionName}.Base.Mvvm.Events
{

	public sealed class DataErrorsChangedEventArgs : EventArgs
	{

		public string PropertyName { get; private set; }

		public DataErrorsChangedEventArgs (string propertyName)
		{
			this.PropertyName = propertyName;
		}

	}

}
