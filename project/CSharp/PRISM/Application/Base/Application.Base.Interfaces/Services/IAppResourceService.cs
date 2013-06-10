using System.Windows.Media;

namespace ${SolutionName}.Base.Interfaces.Services
{

	public interface IAppResourceService
	{
		ImageSource GetPng(string imageName, int size);
		ImageSource GetPngMisc(string imageName);
		ImageSource GetPng16(string imageName);
		ImageSource GetPng24(string imageName);		
		ImageSource GetPng32(string imageName);
		ImageSource GetPng256(string imageName);
	}

}
