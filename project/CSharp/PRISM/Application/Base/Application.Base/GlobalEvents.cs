using System;
using Microsoft.Practices.Prism.Events;

namespace ${SolutionName}.Base
{

	public class RibbonTabChangedEvent : CompositePresentationEvent<string> { }
	public class ShowDataNavigationEvent : CompositePresentationEvent<Boolean> { }
	
}