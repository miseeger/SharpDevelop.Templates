using System;
using System.Globalization;
using System.Windows.Data;

namespace ${SolutionName}.Base.Converters
{
	
    [ValueConversion(typeof (Int64), typeof (Boolean))]
    public class Bool2GridWidthConverter : IValueConverter
    	
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
        	var visibility = (bool) value;
            return visibility ? 250 : 0;
        }


        public object ConvertBack(object value, Type targetType, object parameter,
                                  CultureInfo culture)
        {
            var gridWidth = (Int64) value;
            return (gridWidth == 250);
        }

    }

}

