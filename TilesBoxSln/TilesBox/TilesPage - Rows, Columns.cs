using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace TilesBox
{
    public partial class TilesPage : ContentControl
    {
        /// <summary>Количество строк плиток.
        /// Не может быть меньше 1.</summary>
        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        /// <summary>DependencyProperty для <see cref="Rows"/>.</summary>
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register(nameof(Rows), typeof(int), typeof(TilesPage), new PropertyMetadata(0, (d, e) => ((TilesPage)d).RowsOrColumnsChanged(e), CoerceRows));

        private static object CoerceRows(DependencyObject d, object baseValue)
        {
            if ((int)baseValue > 0)
                return baseValue;
            return 1;
        }


        /// <summary>Количество колонок плиток.
        /// Не может быть меньше 3.</summary>
        public int Columns
        {
            get { return (int)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        /// <summary>DependencyProperty для <see cref="Columns"/>.</summary>
        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.Register(nameof(Columns), typeof(int), typeof(TilesPage), new PropertyMetadata(0, (d, e) => ((TilesPage)d).RowsOrColumnsChanged(e), CoerceColumns));


        private static object CoerceColumns(DependencyObject d, object baseValue)
        {
            if ((int)baseValue > 2)
                return baseValue;
            return 3;
        }


        private void RowsOrColumnsChanged(DependencyPropertyChangedEventArgs e)
        {

            ObservableCollection<TilesPageItem> tiles = (ObservableCollection<TilesPageItem>)Resources["tiles"];

            int tilesCount = Rows * Columns;

            for (int i = tiles.Count; i < tilesCount; i++)
            {

                TilesPageItem item = new TilesPageItem();
                item.SetBinding(ContentProperty, bindingDefault);
                item.SetBinding(StyleProperty, bindingTileStyle);
                item.SetBinding(ContentTemplateProperty, bindingTileTemplate);
                item.SetBinding(ItemIndexProperty, multiBindingItemIndex);
                tiles.Add(item);
            }

            for (int i = tiles.Count - 1; i >= tilesCount; i--)
            {
                ClearAllValues(tiles[i]);
                tiles.RemoveAt(i);
            }

            for (int index = 0, row = 0; row < Rows; row++)
                for (int column = 0; column < Columns; column++, index++)
                {
                    tiles[index].Row = row;
                    tiles[index].Column = column;
                }

            BindingRender();
        }

        public static void ClearAllValues(DependencyObject depobj)
        {
            if (depobj == null)
                throw new ArgumentNullException(nameof(depobj));
            var allValue = depobj.GetLocalValueEnumerator();

            while (allValue.MoveNext())
            {
                DependencyProperty property = allValue.Current.Property;
                depobj.ClearValue(property);
            }
        }
    }
}
