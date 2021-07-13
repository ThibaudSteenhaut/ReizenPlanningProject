using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace ReizenPlanningProject.Converters
{
    class DateFormatter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return null;

            DateTime dt = DateTime.Parse(value.ToString());
            return dt.ToString("dddd d MMMM yyyy 'at' HH:mm");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
