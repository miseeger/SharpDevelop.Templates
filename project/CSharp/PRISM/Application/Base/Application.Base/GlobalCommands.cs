using Microsoft.Practices.Prism.Commands;

namespace ${SolutionName}.Base
{
   
	public class GlobalCommands
	{
		public static readonly CompositeCommand NewCommand = new CompositeCommand();
		public static readonly CompositeCommand SaveCommand = new CompositeCommand();
		public static readonly CompositeCommand DeleteCommand = new CompositeCommand();
		public static readonly CompositeCommand CancelCommand = new CompositeCommand();
		public static readonly CompositeCommand RefreshCommand = new CompositeCommand();
		public static readonly CompositeCommand PrintCommand = new CompositeCommand();
	}

}
