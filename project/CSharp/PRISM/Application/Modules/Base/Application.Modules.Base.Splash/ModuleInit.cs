using System;
using System.Threading;
using System.Windows.Threading;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using ${SolutionName}.Base.Events;
using ${SolutionName}.Base.Interfaces.Ui;
using ${SolutionName}.Modules.Base.Splash.ViewModels;
using ${SolutionName}.Modules.Base.Splash.ViewModels.Interfaces;
using ${SolutionName}.Modules.Base.Splash.Views;

namespace ${SolutionName}.Modules.Base.Splash
{

	[Module(ModuleName="${SolutionName}.Modules.Splash")]
	public class ModuleInit : IModule
	{
		
		private IUnityContainer _container;
		private IEventAggregator _eventAggregator;
		private IShell _shell;

		private AutoResetEvent WaitForCreation { get; set; }
		

		public ModuleInit(IUnityContainer container, IEventAggregator eventAggregator, IShell shell)
		{
			_container = container;
			_eventAggregator = eventAggregator;
			_shell = shell;
		}
		

		public void Initialize()
		{
			Dispatcher.CurrentDispatcher.BeginInvoke(
        		(Action) (() =>
					{
						_shell.Show();
						_shell.Activate();
						_eventAggregator.GetEvent<CloseSplashEvent>().Publish(new CloseSplashEvent());
                    }));

      		WaitForCreation = new AutoResetEvent(false);

      		ThreadStart showSplash =
        		() =>
          			{
            			Dispatcher.CurrentDispatcher.BeginInvoke(
              				(Action) (() =>
								{
									_container.RegisterType<ISplashViewModel, SplashViewModel>();
									_container.RegisterType<SplashView, SplashView>();

                            		var splash = _container.Resolve<SplashView>();
                            		_eventAggregator.GetEvent<CloseSplashEvent>().Subscribe(
                              			e => splash.Dispatcher.BeginInvoke((Action) splash.Close),
                              				ThreadOption.PublisherThread, true);

                            		splash.Show();
								    WaitForCreation.Set();
								}));

            			Dispatcher.Run();
          			};

      		var thread = new Thread(showSplash) {Name = "Splash Thread", IsBackground = true};
      		thread.SetApartmentState(ApartmentState.STA);
      		thread.Start();

      		WaitForCreation.WaitOne();
		}

	}

}