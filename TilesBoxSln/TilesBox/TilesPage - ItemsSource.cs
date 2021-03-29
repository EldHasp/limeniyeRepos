using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace TilesBox
{
    public partial class TilesPage : ContentControl
    {

        /// <summary>Коллекция элементов для отображения.</summary>
        public IList ItemsSource
        {
            get { return (IList)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        /// <summary>DependencyProperty для <see cref="ItemsSource"/>.</summary>
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IList), typeof(TilesPage), new PropertyMetadata(null, ItemsSourceChanged));

        private static void ItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TilesPage tilesPage = (TilesPage)d;
            if (e.OldValue is INotifyPropertyChanged oldList)
                oldList.PropertyChanged -= tilesPage.OnItemsSourcePropertyChanged;
            if (e.NewValue is INotifyPropertyChanged newList)
            {
                newList.PropertyChanged += tilesPage.OnItemsSourcePropertyChanged;
                tilesPage.CoerceValue(PageIndexProperty);
            }
        }

        private void OnItemsSourcePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ItemsSource.Count))
                CoerceValue(PageIndexProperty);
        }
    }
}
