using System;
using <SolutionName>.Base.Mvvm.Interfaces;

namespace ${ProjectName}.ViewModels.Interfaces
{

	public interface ITaskViewModel : IViewModel
	{
		bool IsActive {get; set;}
	}

}