using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ${SolutionName}.Base.Converters
{
	
    [ValueConversion(typeof (string), typeof (Boolean))]
    public class Bool2VisibilityConverter : IValueConverter
    	
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
        	var visibility = (bool) value;
            return visibility ? Visibility.Visible : Visibility.Collapsed;
        }


        public object ConvertBack(object value, Type targetType, object parameter,
                                  CultureInfo culture)
        {
            var convVisibilityValue = (Visibility) value;
            return (convVisibilityValue == Visibility.Visible);
        }

    }

}

