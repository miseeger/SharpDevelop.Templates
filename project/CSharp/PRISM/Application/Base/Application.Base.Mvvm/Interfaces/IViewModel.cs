using System.ComponentModel;

namespace ${SolutionName}.Base.Mvvm.Interfaces
{
	
	public interface IViewModel : INotifyPropertyChanged
	{
		IView View { get; set; }
		bool IsActive { get; set; }
		bool IsBusy { get; set; }
		bool IsDirty {get; set; }
		bool InDesign { get; }
		string VmTitle { get; set; }
		void Initialize();
	}
}
