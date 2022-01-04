using System.ComponentModel;
using System.Diagnostics;
using Windows.UI.Xaml;

namespace DragPosition
{

    public static class ExampleDataAp
    {


        /// <summary>Возвращает значение присоединённого свойства Data для <paramref name="element"/>.</summary>
        /// <param name="element"><see cref="FrameworkElement"/> значение свойства которого будет возвращено.</param>
        /// <returns><see cref="ExampleData"/> значение свойства.</returns>
        public static ExampleData GetData(FrameworkElement element)
        {
            return (ExampleData)element.GetValue(DataProperty);
        }

        /// <summary>Задаёт значение присоединённого свойства Data для <paramref name="element"/>.</summary>
        /// <param name="element"><see cref="FrameworkElement"/> значение свойства которого будет возвращено.</param>
        /// <param name="value"><see cref="ExampleData"/> значение для свойства.</param>
        public static void SetData(FrameworkElement element, ExampleData value)
        {
            element.SetValue(DataProperty, value);
        }

        /// <summary><see cref="DependencyProperty"/> для методов <see cref="GetData(FrameworkElement)"/> и <see cref="SetData(FrameworkElement, ExampleData)"/>.</summary>
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.RegisterAttached(nameof(GetData).Substring(3), typeof(ExampleData), typeof(ExampleDataAp), new PropertyMetadata(null));


    }

    [Bindable(true)]
    public class ExampleData
    {
        public int Value { get; set; }

        private static int s_count;
        private object data;
        private int count;

        public ExampleData()
        {
            s_count++;
            count = s_count;
            Debug.WriteLine($"{nameof(ExampleData)} Instance #{count}");
        }

        public object Data
        {
            get => data;
            set
            {
                data = value;
                Debug.WriteLine($"{nameof(ExampleData)} Instance #{count} DataType:{value?.GetType().Name ?? "null"}");
            }
        }
    }
}
