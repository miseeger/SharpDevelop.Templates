using System;
using ${SolutionName}.Base.Mvvm.Interfaces;

namespace ${SolutionName}.Modules.Accounts.ViewModels.Interfaces
{

	public interface ITaskViewModel : IViewModel
	{
		bool IsActive {get; set;}
	}

}