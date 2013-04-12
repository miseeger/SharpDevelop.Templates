using System;
using System.Windows;

namespace ${SolutionName}.Base.Interfaces.Services
{
	
	public interface IMessageBoxService
	{
		
		void Message(string title, string message);
		void Message(string message);
		bool Warning(string title, string message);
		bool Warning(string message);
		MessageBoxResult WarningYnc(string title, string message);
		MessageBoxResult WarningYnc(string message);
		void Error(string title, string message);
		void Error(string message);
		bool Question(string title, string message);
		bool Question(string message);
		MessageBoxResult QuestionYnc(string title, string message);
		MessageBoxResult QuestionYnc(string message);

	}

}
