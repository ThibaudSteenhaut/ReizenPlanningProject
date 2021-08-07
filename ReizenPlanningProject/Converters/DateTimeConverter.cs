using System;
using Windows.UI.Xaml.Data;

namespace ReizenPlanningProject.Converters
{
    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
                return new DateTimeOffset(((DateTime)value).ToUniversalTime());
            else
                return new DateTimeOffset(DateTime.Now).ToUniversalTime();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
                return ((DateTimeOffset)value).DateTime;
            else
                return DateTime.Now;
        }
    }
}
