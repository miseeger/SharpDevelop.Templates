using System.Windows;
using ${SolutionName}.Base.Mvvm.Interfaces;

namespace ${SolutionName}.Base.Mvvm
{

	public class WindowViewBase: Window, IView
	{

		public IViewModel ViewModel
		{
			get
			{
				return (IViewModel)DataContext;
			}
			set
			{
				DataContext = value;
			}
		}

	}
}
