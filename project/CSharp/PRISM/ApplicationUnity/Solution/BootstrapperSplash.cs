using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Markup;
using System.Xml.Linq;

using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using ${SolutionName}.Base;
using ${SolutionName}.Base.Enum;
using ${SolutionName}.Base.Interfaces;
using ${SolutionName}.Base.Interfaces.Services;

namespace ${SolutionName}
{

	public class Bootstrapper : UnityBootstrapper
	{

		/// <summary>
		/// Provides an NLog-Loggers to config using NLog.config file.
		/// </summary>
		/// <returns>The NLogLogger.</returns>
		protected override ILoggerFacade CreateLogger()
		{
			ILoggerFacade logger = new NLogLogger();
			logger.Log("${SolutionName} Logger was created.",
				Category.Info, Priority.None);
			return logger;
		}


		/// <summary>
		/// Initialization of the Module Catalog from the "Module" directory
		/// </summary>
		/// <returns>The Module Catalog.</returns>
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


		/// <summary>
		/// Configuration of the Unity Container using unity.config file.
		/// </summary>
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

			var defaultModuleInitializer = Container.Resolve<IModuleInitializer>();
			Container.RegisterInstance<IModuleInitializer>("defaultModuleInitializer",
			                                               defaultModuleInitializer);
			Container.RegisterType<IModuleInitializer, SortedModuleInitializer>(
				new ContainerControlledLifetimeManager());
			
			Container.RegisterType<IShell, Shell>(new ContainerControlledLifetimeManager());

			Logger.Log("${SolutionName} Unity-Container was created.",
				Category.Info, Priority.None);
		}


		/// <summary>
		/// Initialization of the RegionAdapterMappings.
		/// </summary>
		/// <returns>The Region Adapter Mappings.</returns>
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
		

		/// <summary>
		/// Configuration of Default Region Behaviors
		/// </summary>
		/// <returns>A RegionBehaviorFactory</returns>
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


		/// <summary>
		/// Creation of the Shell.
		/// </summary>
		/// <returns>The Shell (Main Window).</returns>
		protected override System.Windows.DependencyObject CreateShell()
		{
			Logger.Log("${SolutionName} Shell was provided.",
				Category.Info, Priority.None);
			return Container.Resolve<IShell>() as DependencyObject;
		}


		/// <summary>
		/// Shell Initialization.
		/// </summary>
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
			IAuthenticationService authenticationService = Container.Resolve<IAuthenticationService>();
			var messageBoxService = Container.Resolve<IMessageBoxService>();

			if (authenticationService != null)
			{
				authenticationService.Type = AuthenticationType.SingleSignOn;
				authenticationService.Authenticate();

				if (!authenticationService.IsAuthenticated)
				{
					messageBoxService.Error("${SolutionName}", "The authentication failed! ${SolutionName} will be shut down.");
					App.Current.Shutdown(0);
				}
				else
				{
					base.InitializeShell();
					//App.Current.MainWindow = (Window)Shell;
					//App.Current.MainWindow.Show();
					Logger.Log("${SolutionName} Shell was successfully initialized - authorization was passed.",
						Category.Info, Priority.None);
				}

			}
			else
			{
				messageBoxService.Error("Error", "The Authentication Service is not available.");
				App.Current.Shutdown(0);
			}
			
		}


		/// <summary>
		/// Module Initialization.
		/// </summary>
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
			
			Logger.Log("${SolutionName} was successfully initialized.",
				Category.Info, Priority.None);
		}

	}

}