using ${SolutionName}.Base.Mvvm.Interfaces;
using Fluent;

namespace ${SolutionName}.Base.Mvvm.RibbonUi
{

	public class RibbonBackstageTabItem : BackstageTabItem, IView
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
