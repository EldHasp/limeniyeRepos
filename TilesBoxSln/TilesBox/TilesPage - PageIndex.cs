using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace TilesBox
{
    public partial class TilesPage : ContentControl
    {
        /// <summary>Индекс страницы.</summary>
        public int PageIndex
        {
            get { return (int)GetValue(PageIndexProperty); }
            set { SetValue(PageIndexProperty, value); }
        }

        /// <summary>DependencyProperty для <see cref="PageIndex"/>.</summary>
        public static readonly DependencyProperty PageIndexProperty =
            DependencyProperty.Register(nameof(PageIndex), typeof(int), typeof(TilesPage), new PropertyMetadata(-1, (d, e) => ((TilesPage)d).BindingRender(), CoercePageIndex));

        private static object CoercePageIndex(DependencyObject d, object baseValue)
        {
            TilesPage tilesPage = (TilesPage)d;
            int index =  (int)baseValue;
            if (index < 0)
                return -1;
            double count = tilesPage.ItemsSource?.Count ?? 0.0;
            if (count == 0.0)
                return -1;

            int lastIndex = (int) Math.Ceiling(count/tilesPage.TilesCount);
            if (index > lastIndex)
                return lastIndex;
            return index;
        }
    }
}
