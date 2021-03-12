using System;
using System.Windows.Data;

namespace Common.Converters
{
    public class LongToShortConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string result = "";
            // decimal.TryParse(value.ToString(), out result);

            if (Decimal.TryParse(value.ToString(), out decimal resultDecimal))
            {
                result = string.Format("{0:0.00}", resultDecimal);
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}