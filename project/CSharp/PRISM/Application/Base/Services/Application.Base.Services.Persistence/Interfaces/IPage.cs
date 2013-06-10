using System.Collections.Generic;

namespace ${SolutionName}.Base.Services.Persistence.Interfaces
{
	public interface IPage<T>
	{
		long CurrentPage { get; set; }
		long TotalPages { get; set; } 
		long TotalItems { get; set; }
		long ItemsPerPage { get; set; }
		List<T> Items { get; set; }
		object Context { get; set; }
	}
}
