using System;

namespace ${SolutionName}.Base.Mvvm.Attributes
{

	[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
	public sealed class IgnoreObjectStatusAttribute : Attribute
	{
		public bool IgnoreObjectStatus { get; private set; }

		public IgnoreObjectStatusAttribute(bool ignoreObjectStatus)
		{
			IgnoreObjectStatus = ignoreObjectStatus;
		}

	}

}