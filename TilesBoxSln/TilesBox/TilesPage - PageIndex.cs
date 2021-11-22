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

        private int pageIndex;

        /// <summary>DependencyProperty для <see cref="PageIndex"/>.</summary>
        public static readonly DependencyProperty PageIndexProperty =
            DependencyProperty.Register(nameof(PageIndex), typeof(int), typeof(TilesPage), new PropertyMetadata(-1, PageIndexChanged, CoercePageIndex));

        private static void PageIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TilesPage tilesPage = (TilesPage)d;
            int pageIndex = (int)e.NewValue;
            tilesPage.pageIndex = pageIndex;
            int tilesCount = tilesPage.tilesCount;
            if (pageIndex < 0)
                tilesPage.LastItemIndex = tilesPage.FirstItemIndex = -1;
            else if (pageIndex >= tilesPage.pagesCount)
                tilesPage.LastItemIndex = tilesPage.FirstItemIndex = (tilesPage.proxyCount.Value as int?) ?? 0;
            else
            {
                int firstItemIndex = pageIndex * tilesCount;
            }

            tilesPage.RenderTilesBinding();
        }

        private static object CoercePageIndex(DependencyObject d, object baseValue)
        {
            TilesPage tilesPage = (TilesPage)d;
            int index = (int)baseValue;
            if (index < 0)
                return -1;

            int pagesCount = tilesPage.PagesCount;
            if (index > pagesCount)
                return pagesCount;
            return index;
        }
    }
}
