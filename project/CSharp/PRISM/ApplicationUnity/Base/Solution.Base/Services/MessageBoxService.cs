using System;
using System.Windows;
using ${SolutionName}.Base.Interfaces.Services;

namespace ${SolutionName}.Base.Services
{

	public class MessageBoxService : IMessageBoxService
	{
		
		public MessageBoxService()
		{
		}

		public void Message(string title, string message)
		{
			MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
		}
		
		public void Message(string message)
		{
			Message(string.Empty, message);
		}
		
		public bool Warning(string title, string message)
		{
			return (MessageBox.Show(message, title, MessageBoxButton.OKCancel, MessageBoxImage.Warning) == 
			        MessageBoxResult.OK);
		}
		
		public bool Warning(string message)
		{
			return Warning(string.Empty, message);
		}
		
		public System.Windows.MessageBoxResult WarningYnc(string title, string message)
		{
			return MessageBox.Show(message, title, MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
		}
		
		public System.Windows.MessageBoxResult WarningYnc(string message)
		{
			return WarningYnc(string.Empty, message);
		}
		
		public void Error(string title, string message)
		{
			MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
		}
		
		public void Error(string message)
		{
			Error(string.Empty, message);
		}
		
		public bool Question(string title, string message)
		{
			return (MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Question) == 
			        MessageBoxResult.Yes);
		}
		
		public bool Question(string message)
		{
			return Question(string.Empty, message);
		}
		
		public System.Windows.MessageBoxResult QuestionYnc(string title, string message)
		{
			return MessageBox.Show(message, title, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
		}
		
		public System.Windows.MessageBoxResult QuestionYnc(string message)
		{
			return QuestionYnc(string.Empty, message);
		}

	}

}
