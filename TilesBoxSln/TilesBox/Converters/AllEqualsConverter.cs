using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace TilesBox
{
    /// <summary>Возвращает <see langword="true"/>, если все значения эквивалентны.</summary>
    /// <remarks>Проверка эквивалентности производится методом <see cref="object.Equals(object, object)"/> первого значения со всеми остальными.
    /// Если типы разные то они приводятся к типу первого значения чере <see cref="TypeConverter"/>.</remarks>
    [ValueConversion(typeof(object), typeof(bool))]
    public class AllEqualsConverter : IMultiValueConverter
    {
        /// <summary>Если <see cref=""/>, то ответ инвертируется.</summary>
        public bool Invert { get; }

        /// <summary>Создаёт экземпляр конвертера с заданым значением свойства <see cref="Invert"/>.</summary>
        /// <param name="invert">Значение для свойства <see cref="Invert"/>.</param>
        public AllEqualsConverter(bool invert)
        {
            Invert = invert;
        }


         /// <summary>Создаёт экземпляр конвертера со значением свойства <see cref="Invert"/> = <see langword="false"/>.</summary>
       public AllEqualsConverter()
            : this(false)
        { }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length == 0)
                return DependencyProperty.UnsetValue;

            object value = values[0];

            if (value == null)
                return values.Skip(1).All(v => v == null) ^ Invert;

            Type valueType = value.GetType();

            TypeConverter valueConverter = TypeDescriptor.GetConverter(valueType);

            return values.Skip(1).All(v => equals(v)) ^ Invert;

            bool equals(object obj)
            {
                if (Equals(value, obj))
                    return true;

                if (!valueConverter.IsValid(obj))
                    return false;

                return Equals(value, valueConverter.ConvertFrom(obj));
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>Экземпляр конвертера <see cref="AllEqualsConverter"/>.</summary>
        public static AllEqualsConverter Instance { get; } = new AllEqualsConverter();

        /// <summary>Инвертирующий экземпляр конвертера <see cref="AllEqualsConverter"/>.</summary>
        public static AllEqualsConverter InvertInstance { get; } = new AllEqualsConverter(true);

    }
}
