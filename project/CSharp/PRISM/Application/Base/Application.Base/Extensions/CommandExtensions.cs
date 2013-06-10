using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;

namespace ${SolutionName}.Base.Extensions
{

	public static class CommandExtensions
	{
		
		public static void RegisterSingleCommand(this CompositeCommand compositeCommand, ICommand commandToRegister)
		{
			foreach (var command in compositeCommand.RegisteredCommands) 
			{
				compositeCommand.UnregisterCommand(command);
			}

			if (commandToRegister != null)
			{
				compositeCommand.RegisterCommand(commandToRegister);	
			}
		}
		
	}

}
