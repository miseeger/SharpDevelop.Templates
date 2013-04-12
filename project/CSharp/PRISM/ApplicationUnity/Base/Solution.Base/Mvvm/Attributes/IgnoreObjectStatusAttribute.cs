using System;

namespace ${SolutionName}.Base.Mvvm.Attributes
{

	[AttributeUsage(System.AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
	public sealed class IgnoreObjectStatusAttribute : System.Attribute
	{
		public bool IgnoreObjectStatus { get; private set; }

		public IgnoreObjectStatusAttribute(bool ignoreObjectStatus)
		{
			IgnoreObjectStatus = ignoreObjectStatus;
		}

	}

}