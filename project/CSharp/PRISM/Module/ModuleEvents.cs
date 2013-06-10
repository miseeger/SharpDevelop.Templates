using Microsoft.Practices.Prism.Events;

namespace ${ProjectName}
{

	public class DataNavBeforeChanging : CompositePresentationEvent<object[]> { }
	public class DataNavChanged : CompositePresentationEvent<object> { }
	public class DataNavRefresh : CompositePresentationEvent<object> { }
	public class ContactChanged : CompositePresentationEvent<object> { }

	// TODO: Declare more Module specific Events

}
