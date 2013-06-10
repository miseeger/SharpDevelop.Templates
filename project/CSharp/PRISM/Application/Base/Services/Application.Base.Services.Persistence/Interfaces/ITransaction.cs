using System;

namespace ${SolutionName}.Base.Services.Persistence.Interfaces
{

	public interface ITransaction : IDisposable
	{
		void Complete();
	}

}
