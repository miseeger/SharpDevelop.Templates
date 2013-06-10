using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ${SolutionName}.Base.Interfaces.Services;

namespace ${SolutionName}.Base.Resource
{

	public class AppResourceService : IAppResourceService
	{

		public ImageSource GetPng(string imageName, int size)
		{
			ImageSource result = null;
			
			var imgName = size > 0 
				? String.Format("${SolutionName}.Base.Resource.Images._{0}x{1}.{2}.png",size, size, imageName) 
				: String.Format("${SolutionName}.Base.Resource.Images.Misc.{0}.png", imageName);

			System.IO.Stream fileStream = GetType().Assembly.GetManifestResourceStream(imgName);
			
			if (fileStream != null)
			{
				var bitmapDecoder = new PngBitmapDecoder(fileStream,
					BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
				result = bitmapDecoder.Frames[0];
			}

			return result;
		}
		
		
		public ImageSource GetPngMisc(string imageName)
		{
			return GetPng(imageName, 0);
		}


		public ImageSource GetPng16(string imageName)
		{
			return GetPng(imageName, 16);
		}


		public ImageSource GetPng24(string imageName)
		{
			return GetPng(imageName, 24);
		}

		
		public ImageSource GetPng32(string imageName)
		{
			return GetPng(imageName, 32);
		}
		
		
		public ImageSource GetPng256(string imageName)
		{
			return GetPng(imageName, 256);
		}
		
	}

}
