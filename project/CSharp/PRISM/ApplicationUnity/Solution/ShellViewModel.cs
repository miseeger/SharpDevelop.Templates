using System;
using System.Linq;
using Microsoft.Practices.Prism.Regions;
using ${SolutionName}.Base.Interfaces.Services;
using ${SolutionName}.Base.Mvvm;
using ${SolutionName}.Base.Names;
using ${SolutionName}.Base.Navigation;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ${SolutionName}
{

	public class ShellViewModel : ViewModelBase
	{
		
		public ImageSource MainImage { get; set; }
		
		public ShellViewModel(IAppResourceService ResourceService)
		{
			MainImage = ResourceService.GetPng256("Shell");
		}

	}

}
