using System;
using System.Windows.Data;

namespace Common.Converters
{
    public class RoundingNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string result = "";
            // decimal.TryParse(value.ToString(), out result);

            if (Decimal.TryParse(value.ToString(), out decimal resultDecimal))
            {
                if ((resultDecimal - (int)resultDecimal) != 0)
                    result = ((int)++resultDecimal).ToString();
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
