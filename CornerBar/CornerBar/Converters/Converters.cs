using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CornerBar.Resources;
using Xamarin.Forms;

namespace CornerBar.Converters
{

    public class HeaderTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            switch ((int)value)
            {
                case 1:
                    return AppResources.Head1;
                case 2:
                    return AppResources.Head2;
                case 3:
                    return AppResources.Head3;
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DetailTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((int) value)
            {
                case 1:
                    return AppResources.About1;
                case 2:
                    return AppResources.About2;
                case 3:
                    return AppResources.About3;
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
