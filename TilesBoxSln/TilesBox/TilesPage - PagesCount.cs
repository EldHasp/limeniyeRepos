using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace TilesBox
{
    public partial class TilesPage : ContentControl
    {

        /// <summary>Количество страниц в коллекции <see cref="ItemsSource"/>.</summary>
        public int PagesCount
        {
            get { return (int)GetValue(PagesCountProperty); }
            protected set { SetValue(PagesCountPropertyKey, value); }
        }

        private int pagesCount;

        /// <summary><see cref=DependencyPropertyKey"/> для свойства <see cref="PagesCount"/>.</summary>
        protected static readonly DependencyPropertyKey PagesCountPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(PagesCount), typeof(int), typeof(TilesPage), new PropertyMetadata(0, PagesCountChanged));

        private static void PagesCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TilesPage tilesPage = (TilesPage)d;
            tilesPage.pagesCount = (int)e.NewValue;
            tilesPage.InvalidateProperty(PageIndexProperty);
        }

        /// <summary><see cref=DependencyProperty"/> для свойства <see cref="PagesCount"/>.</summary>
        public static readonly DependencyProperty PagesCountProperty = PagesCountPropertyKey.DependencyProperty;


        /// <summary>Перерасчёт количества страниц.</summary>
        /// <remarks>Метод должен вызываться при изменении: <see cref="ItemsSource"/>.Count,
        /// <see cref="Rows"/> * <see cref="Columns"/>, <see cref="DisabledButtonVisibility"/>.</remarks>
        protected void RenderPagesCount()
        {
            int newPagesCount;
            if (!(proxyCount.Value is int count) || count < 1)
                newPagesCount = 0;
            else
            {
                // Всего плиток на странице
                int tilesCount = Rows * Columns;

                // Плиток для элементов на странице
                int tilesItemCount = tilesCount - 2;

                Visibility disabledButtonVisibility = DisabledButtonVisibility;

                if (disabledButtonVisibility == Visibility.Collapsed)
                {
                    if (tilesCount <= count)
                        newPagesCount = 1;
                    else
                        newPagesCount = (int)Math.Ceiling((count - 2d) / (double)tilesItemCount);
                }
                else
                {
                    newPagesCount = (int)Math.Ceiling((double)count / (double)tilesItemCount);
                }
            }

            if (pagesCount != newPagesCount)
                PagesCount = newPagesCount;
        }

    }
}
