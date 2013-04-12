using System;
using System.Windows;
using System.Data;
using System.Xml;
using System.Configuration;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.UnityExtensions;

namespace ${SolutionName}
{

	public partial class App : Application
	{

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			Bootstrapper bootstrapper = new Bootstrapper();
			bootstrapper.Run();
		}

	}

}