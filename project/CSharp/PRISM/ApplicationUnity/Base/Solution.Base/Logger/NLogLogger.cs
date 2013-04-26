using System;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Logging;
using NLog;

namespace ${SolutionName}.Base.Logger
{

	public class NLogLogger : ILoggerFacade
	{

		private static NLog.Logger _logger = LogManager.GetLogger("${SolutionName}");

		public void Log(string message, Category category, Priority priority)
		{
			LogLevel nLevel = null;

			switch (category)
			{
				case Category.Debug:
					nLevel = LogLevel.Debug;
					break;

				case Category.Exception:
					nLevel = LogLevel.Error;
					break;

				case Category.Info:
					nLevel = LogLevel.Info;
					break;

				case Category.Warn:
					nLevel = LogLevel.Warn;
					break;
			}

			_logger.Log(nLevel, message);

		}

	}

}