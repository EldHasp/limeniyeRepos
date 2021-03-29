using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace TilesBox
{
    /// <summary>Возвращает из <see cref="IList"/> значение элемента по индексу.</summary>
    public class ListGetValueConverter : IMultiValueConverter
    {
        /// <summary>Возвращаете значение элемента по индексу из списка <see cref="IList"/>.</summary>
        /// <param name="values">В первой привязке коллекция <see cref="IList"/>.
        /// Во второй привязке индекс элемента.</param>
        /// <param name="targetType">Целевой тип.</param>
        /// <param name="parameter">Параметр конвертера. Не используется.</param>
        /// <param name="culture">Культура для конвертации.</param>
        /// <returns>Значение элемента по индексу.</returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null ||
                values.Length < 2 ||
                !(values[0] is IList list))
                return DependencyProperty.UnsetValue;

            if (!(values[0] is int index))
            {
                if (Int32Converter.IsValid(values[0]))
                    index = (int)Int32Converter.ConvertFrom(null, culture, values[0]);
                else
                    return DependencyProperty.UnsetValue;
            }

            if (index < 0 || index >= list.Count)
                return DependencyProperty.UnsetValue;
            return list[index];
        }

        public static Int32Converter Int32Converter { get; } = new Int32Converter();

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
