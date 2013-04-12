using System;
using System.Windows.Media;

namespace ${SolutionName}.Base.Interfaces.Services
{

	public interface IAppResourceService
	{
		ImageSource GetPng(string ImageName, int Size);
		ImageSource GetPngMisc(string imageName);
		ImageSource GetPng16(string imageName);
		ImageSource GetPng256(string imageName);
	}

}
