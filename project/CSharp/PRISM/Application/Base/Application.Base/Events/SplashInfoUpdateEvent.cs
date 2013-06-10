using Microsoft.Practices.Prism.Events;
  
namespace ${SolutionName}.Base.Events
{

	public class SplashInfoUpdateEvent : CompositePresentationEvent<SplashInfoUpdateEvent>
	{
		public string Info { get; set; }
	}

}
