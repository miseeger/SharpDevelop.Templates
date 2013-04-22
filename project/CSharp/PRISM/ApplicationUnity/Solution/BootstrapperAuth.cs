using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Markup;
using ${SolutionName}.Base;

namespace ${SolutionName}
{

	// The following Bootstrapper lists all Initialization steps 
	// except the registrain the order
	// they are actually processed by the PRISM bootstrapping process.
	public class Bootstrapper : UnityBootstrapper
	{

		// Provides an NLog-Loggers to config using NLog.config file.
		protected override ILoggerFacade CreateLogger()
		{
			ILoggerFacade logger = new NLogLogger();
			logger.Log("${SolutionName} Logger was created.",
				Category.Info, Priority.None);
			return logger;
		}


		// Initialization of the Module Catalog from the "Module" directory
		protected override IModuleCatalog CreateModuleCatalog()
		{
			if (!Directory.Exists(@".\Modules"))
			{
				Directory.CreateDirectory(@".\Modules");
			}
			var moduleCatalog = new DirectoryModuleCatalog();
			moduleCatalog.ModulePath = @".\Modules";
			Logger.Log("${SolutionName} Module Catalog was created",
				Category.Info, Priority.None);
			return moduleCatalog;
		}


		// Configuration of the Unity Container using unity.config file.
		protected override void ConfigureContainer()
		{
			base.ConfigureContainer();
			var configMap = new ExeConfigurationFileMap()
				{
					ExeConfigFilename = @".\unity.config"
				};
			var config = ConfigurationManager.OpenMappedExeConfiguration(configMap,
				ConfigurationUserLevel.None);
			var section = (UnityConfigurationSection)config.GetSection("unity");
			this.Container.LoadConfiguration(section);
			Logger.Log("${SolutionName} Unity-Container was created.",
				Category.Info, Priority.None);
		}


		// Initialization of the RegionAdapterMappings.
		protected override RegionAdapterMappings ConfigureRegionAdapterMappings()
		{
			var mappings = base.ConfigureRegionAdapterMappings();
			if (mappings == null) return null;

			//e.g.: add custom mappings ...
			//var ribbonRegionAdapter = ServiceLocator.Current.GetInstance<RibbonRegionAdapter>();
			//mappings.RegisterMapping(typeof(Ribbon), ribbonRegionAdapter);

			Logger.Log("${SolutionName} RegionAdapterMappings were created.",
				Category.Info, Priority.None);
			return mappings;
		}
		

		// Configuration of Default Region Behaviors
		protected override IRegionBehaviorFactory ConfigureDefaultRegionBehaviors()
		{
    		var factory = base.ConfigureDefaultRegionBehaviors();
			if (factory == null) return null;

    		// example1: registering a behavior
			// factory.AddIfMissing("MyBehavior", typeof(MyCustomBehavior));
			
			// example2: adding a Region Behavior for a Single Region
			//IRegion region = regionManager.Region["Region1"];
			//region.Behaviors.Add("MyBehavior", new MyRegion());
			
			Logger.Log("${SolutionName} DefaultRegionBehaviors were registered.",
				Category.Info, Priority.None);
			return factory;
		}


		// Registration of Exceptions that are available througout the application
		protected override void RegisterFrameworkExceptionTypes()
		{
			// ... registration code here ...

			//Logger.Log("${SolutionName} Framework Exception Types were registered.",
			//	Category.Info, Priority.None);
		}

		
		// Creation of the Shell.
		protected override System.Windows.DependencyObject CreateShell()
		{
			Logger.Log("${SolutionName} Shell was provided.",
				Category.Info, Priority.None);
			return Container.Resolve<Shell>();
		}


		/// Shell Initialization.
		protected override void InitializeShell()
		{
			// Internationalization-Fix for correct StringFormat localization 
			// (e.g. DateTime formatting):
			FrameworkElement.LanguageProperty.OverrideMetadata(
				typeof(FrameworkElement), 
				new FrameworkPropertyMetadata(
					XmlLanguage.GetLanguage(
				CultureInfo.CurrentCulture.IetfLanguageTag)));

			// Calling the Authentication Service
			var authenticationService = Container.Resolve<IAuthenticationService>();
			var messageBoxService = Container.Resolve<IMessageBoxService>();

			if (authenticationService != null)
			{
				authenticationService.Authenticate();

				if (!authenticationService.IsAuthenticated)
				{
					messageBoxService.Error("PrismApplication", "The authentication failed! PrismApplication will be shut down.");
					App.Current.Shutdown(0);
				}
				else
				{
					base.InitializeShell();
					App.Current.MainWindow = (Window)Shell;
					App.Current.MainWindow.Show();
					Logger.Log("PrismApplication Shell was successfully initialized - authorization was passed.",
						Category.Info, Priority.None);
				}

			}
			else
			{
				messageBoxService.Error("Error", "The Authentication Service is not available.");
				App.Current.Shutdown(0);
			}

		}


		/// Module Initialization.
		protected override void InitializeModules()
		{
			try 
			{
				base.InitializeModules();
			} 
			catch (Exception e) 
			{
				MessageBox.Show(e.InnerException.ToString());
			}
			
			Logger.Log("All ${SolutionName} Modules were successfully initialized.",
				Category.Info, Priority.None);
		}

	}

}