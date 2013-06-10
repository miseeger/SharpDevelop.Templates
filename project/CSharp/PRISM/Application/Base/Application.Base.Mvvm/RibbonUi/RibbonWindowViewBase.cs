using ${SolutionName}.Base.Mvvm.Interfaces;
using Fluent;

namespace ${SolutionName}.Base.Mvvm.RibbonUi
{

	public class RibbonWindowViewBase: RibbonWindow, IViewDialog
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
			return this.ShowDialog();
		}
		

		public void ShowAsWindow()
		{
			this.Show();
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
