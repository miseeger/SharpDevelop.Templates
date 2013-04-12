using System;
using ${SolutionName}.Base.Mvvm.Interfaces;

namespace ${SolutionName}.Modules.Contacts.ViewModels.Interfaces
{

	public interface ITaskViewModel : IViewModel
	{
		bool IsActive {get; set;}
	}

}