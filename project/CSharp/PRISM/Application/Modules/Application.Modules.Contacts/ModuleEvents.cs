using Microsoft.Practices.Prism.Events;

namespace ${SolutionName}.Modules.Contacts
{

	public class DataNavBeforeChanging : CompositePresentationEvent<object[]> { }
	public class DataNavChanged : CompositePresentationEvent<object> { }
	public class DataNavRefresh : CompositePresentationEvent<object> { }
	public class ContactChanged : CompositePresentationEvent<object> { }
	
}
