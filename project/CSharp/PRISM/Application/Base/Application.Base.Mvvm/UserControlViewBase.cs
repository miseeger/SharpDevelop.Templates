using System.Windows.Controls;
using ${SolutionName}.Base.Mvvm.Interfaces;

namespace ${SolutionName}.Base.Mvvm
{

	public class UserControlViewBase: UserControl, IView
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
