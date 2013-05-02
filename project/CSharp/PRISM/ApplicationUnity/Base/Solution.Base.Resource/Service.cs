using System;
using System.Reflection;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ${SolutionName}.Base.Interfaces.Services;

namespace ${SolutionName}.Base.Resource
{

	public class Service : IAppResourceService
	{

		public ImageSource GetPng(string ImageName, int Size)
		{
			ImageSource result = null;
			
			var imgName = Size > 0 
				? String.Format("${SolutionName}.Base.Resource.Images._{0}x{1}.{2}.png",Size, Size, ImageName) 
				: String.Format("${SolutionName}.Base.Resource.Images.Misc.{0}.png", ImageName);

			System.IO.Stream fileStream = this.GetType().Assembly.GetManifestResourceStream(imgName);
			
			if (fileStream != null)
			{
				PngBitmapDecoder bitmapDecoder = new PngBitmapDecoder(fileStream,
					BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
				result = bitmapDecoder.Frames[0];
			}

			return result;
		}
		
		
		public ImageSource GetPngMisc(string ImageName)
		{
			return GetPng(ImageName, 0);
		}


		public ImageSource GetPng16(string ImageName)
		{
			return GetPng(ImageName, 16);
		}


		public ImageSource GetPng24(string ImageName)
		{
			return GetPng(ImageName, 24);
		}

		
		public ImageSource GetPng32(string ImageName)
		{
			return GetPng(ImageName, 32);
		}
		
		
		public ImageSource GetPng256(string ImageName)
		{
			return GetPng(ImageName, 256);
		}
		
	}

}
