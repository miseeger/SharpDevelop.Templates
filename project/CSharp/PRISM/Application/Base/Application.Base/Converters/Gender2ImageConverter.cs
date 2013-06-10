using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

using Microsoft.Practices.ServiceLocation;
using ${SolutionName}.Base.Interfaces.Services;

namespace ${SolutionName}.Base.Converters
{
	
	[ValueConversion(typeof (string), typeof (ImageSource))]
    public class Gender2ImageConverter : IValueConverter
    	
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
        	var resourceService = ServiceLocator.Current.GetInstance<IAppResourceService>();
        	
        	if (resourceService != null)
        	{
        		var gender = (string)value;
        		return gender.ToUpper() == "M" 
        					? resourceService.GetPng32("user")
        					: resourceService.GetPng32("user_female");
        	}
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
                                  CultureInfo culture)
        {
            return null;
        }

    }
}
