namespace ${SolutionName}.Base.Mvvm.Interfaces
{
	
	public interface IViewDialog : IView
	{
		bool? ShowAsDialog();
		void ShowAsWindow();
		bool? Result { get; set; }
	}
	
}

