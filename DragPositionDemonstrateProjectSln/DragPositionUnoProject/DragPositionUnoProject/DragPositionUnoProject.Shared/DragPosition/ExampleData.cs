using System;
using System.ComponentModel;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DragPosition
{
    public static class ExampleDataAp
    {
        public static readonly DependencyProperty DataProperty = 
            DependencyProperty.RegisterAttached("Data", typeof(ExampleData), typeof(ExampleDataAp),
                     new PropertyMetadata(null, OnDataChanged));

        private static void OnDataChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            Debug.WriteLine($"{nameof(OnDataChanged)} dependencyObject:{dependencyObject}; NewValue:{args.NewValue?.GetType().Name ?? "null"}");
        }

        public static void SetData(DependencyObject element, ExampleData value)
        {
            element.SetValue(DataProperty, value);
        }
        public static ExampleData GetData(DependencyObject element)
        {
            return (ExampleData)element.GetValue(DataProperty);
        }
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
