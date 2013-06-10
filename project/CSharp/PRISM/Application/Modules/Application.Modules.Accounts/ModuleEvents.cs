using Microsoft.Practices.Prism.Events;

namespace ${SolutionName}.Modules.Accounts
{

	public class DataNavBeforeChanging : CompositePresentationEvent<object[]> { }
	public class DataNavChanged : CompositePresentationEvent<object> { }
	public class DataNavRefresh : CompositePresentationEvent<object[]> { }
	
}
