using System.Windows;
using ${SolutionName}.Base.Mvvm.Interfaces;

namespace ${SolutionName}.Base.Mvvm
{

	public class WindowViewBase: Window, IViewDialog
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

		
		public bool? ShowAsDialog()
		{
			return ShowDialog();
		}
		

		public void ShowAsWindow()
		{
			Show();
		}
		
		
		private bool? _result;
		public bool? Result
		{
			get
			{
				return _result;
			}

			set
			{
				_result = value;
				DialogResult = value;
			}
		}
	}

}
