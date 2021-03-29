using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace TilesBox
{
    public partial class TilesPage : ContentControl
    {
        /// <summary>Задаёт индекс элемента коллекции к которому привязан <see cref="tile"/>.</summary>
        /// <param name="tile">Экземпляр <see cref="TilesPageItem"/>.</param>
        /// <returns>Индекс привязанной коллекции</returns>
        public static int? GetItemIndex(TilesPageItem tile)
        {
            return (int?)tile.GetValue(ItemIndexProperty);
        }

        /// <summary>Задаёт индекс элемента коллекции к которому привязан <see cref="tile"/>.</summary>
        /// <param name="tile">Экземпляр <see cref="TilesPageItem"/>.</param>
        /// <param name="index">Значение индекса элемента.</param>
        public static void SetItemIndex(TilesPageItem tile, int index)
        {
            tile.SetValue(ItemIndexProperty, index);
        }

        // Using a DependencyProperty as the backing store for ItemIndex.  This enables animation, styling, binding, etc...
        /// <summary>Attached DependencyProperty для <see cref="GetItemIndex(TilesPageItem)"/> и <see cref="SetItemIndex(TilesPageItem, int)"/>.</summary>
        public static readonly DependencyProperty ItemIndexProperty =
            DependencyProperty.RegisterAttached("ItemIndex", typeof(int?), typeof(TilesPage), new PropertyMetadata(null, ItemIndexChanged));

        private static void ItemIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is TilesPageItem tile))
                throw new InvalidCastException($"Только для экземпляров совместимых с типом {typeof(TilesPageItem)}.");

            if (e.NewValue == null)
            {

            }
            else
            {
                int index = (int)e.NewValue;
                if (index < 1)
                    tile.DataContext = null;
                else
                {
                    tile.SetBinding(DataContextProperty, new Binding($"{nameof(ItemsSource)}[{index}]")
                    {
                        RelativeSource = relativeSourceTilesPage,
                        Mode = BindingMode.OneWay
                    });
                }
            }
        }

        private static readonly RelativeSource relativeSourceTilesPage = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(TilesPage), 1);
        public static void ClearItemIndex(TilesPageItem tile)
        {
            tile.ClearValue(ItemIndexProperty);
        }



        public static TileStatusEnum? GetTileStatus(TilesPageItem tile)
        {
            return (TileStatusEnum?)tile.GetValue(TileStatusProperty);
        }

        public static void SetTileStatus(TilesPageItem tile, TileStatusEnum? value)
        {
            tile.SetValue(TileStatusProperty, value);
        }

        // Using a DependencyProperty as the backing store for TileStatus.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TileStatusProperty =
            DependencyProperty.RegisterAttached("TileStatus", typeof(TileStatusEnum), typeof(TilesPage), new PropertyMetadata(null, TileStatusChanged));

        private static void TileStatusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is TilesPageItem tile))
                throw new InvalidCastException($"Только для экземпляров совместимых с типом {typeof(TilesPageItem)}.");
        }
    }

    [Flags]
    public enum TileStatusEnum
    {
        OutTilePage = 0,

        InTilePage = 1,

        Disabled = InTilePage,
        Enabled = 2 | InTilePage,

        Item = Disabled,
        ItemDisabled = Item,
        ItemEnabled = Enabled,

        Button = 4,
        ButtonDisabled = Button | Disabled,
        ButtonEnabled = Button | Enabled,

        ButtonNext = 8,

        Previous = Button,
        PreviousDisabled = Previous | Disabled,
        PreviousEnabled = Previous | Enabled,

        Next = Button | ButtonNext,
        NextDisabled = Next | Disabled,
        NextEnabled = Next | Enabled,

    }


    /// <summary>Получает для <see cref="TilesPageItem"/> индекс элемента коллекции по данным <see cref="TilesPageItem"/> и <see cref="TilesPage"/>.</summary>
    /// <remarks>Данные в values:
    /// 0 - <see cref="TilesPage.ItemsSource"/>.Count;
    /// 1 - <see cref="TilesPage.Rows"/>;
    /// 2 - <see cref="TilesPage.Columns"/>;
    /// 3 - <see cref="TilesPage.DisabledButtonVisibility"/>;
    /// 4 - <see cref="TilesPage.PageIndex"/>;
    /// 5 - <see cref="TilesPageItem.Row"/>;
    /// 6 - <see cref="TilesPageItem.Column"/>;
    /// 7 - <see cref="TilesPageItem"/>;
    /// </remarks>
    public class ItemIndexConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(
                (values[0] is int count) &&
                (values[1] is int rows) &&
                (values[2] is int columns) &&
                (values[3] is Visibility disabledButtonVisibility) &&
                (values[4] is int pageIndex) &&
                (values[5] is int row) &&
                (values[6] is int column) &&
                (values[7] is TilesPageItem tile)
                ))
                return DependencyProperty.UnsetValue;


            // Количество Tile на средних страницах
            int pageTilesCount = rows * column - 2;

            // Индекс первого Tile на странице
            int firstIndexTile = -1;
            int lastIndexTile = -1;
            if (pageIndex >= 0)
            {
                firstIndexTile = pageIndex * pageTilesCount;
                if (disabledButtonVisibility == Visibility.Collapsed
                    && pageIndex > 0)
                {
                    firstIndexTile++;
                }
                lastIndexTile = firstIndexTile + pageTilesCount;
                if (disabledButtonVisibility == Visibility.Collapsed
                    && lastIndexTile + 2 >= count)
                {
                    lastIndexTile++;
                }

            }
            // Определение кнопок
            if (row + 1 == rows)
            {
                // Определение кнопки Previous
                if (column == 0)
                {
                    // Определение отображения кнопки
                    if (pageIndex < 1)
                    {
                        if (disabledButtonVisibility != Visibility.Collapsed)
                        {
                            TilesPage.SetTileStatus(tile, TileStatusEnum.PreviousDisabled);
                            return null;
                        }

                    }
                    TilesPage.SetTileStatus(tile, TileStatusEnum.PreviousEnabled);
                    return null;
                }

                // Определение кнопки Next
                else if (column + 1 == columns)
                {
                    if (lastIndexTile + 1 >= count)
                    {
                        TilesPage.SetTileStatus(tile, TileStatusEnum.NextDisabled);
                        return null;
                    }
                    TilesPage.SetTileStatus(tile, TileStatusEnum.NextEnabled);
                    return null;
                }
            }

            // Дальше работа с Tile для элементов коллекции

            // Возврат для отрицательных страниц
            if (pageIndex < 0)
            {
                TilesPage.SetTileStatus(tile, TileStatusEnum.ItemDisabled);
                return -1;
            }

            // Возврат для элементов за пределами страницы
            if (row < 0 || column < 0 ||
                row >= rows || column >= columns)
            {
                TilesPage.SetTileStatus(tile, TileStatusEnum.ItemDisabled);
                return -1;
            }


            // Индекс элемента для плитки
            int index = firstIndexTile + row * columns + column;
            if (row + 1 == rows && (pageIndex > 0 && disabledButtonVisibility != Visibility.Collapsed))
                index++;

            // Возврат для элементов за пределами коллекции
            if (index >= count)
            {
                TilesPage.SetTileStatus(tile, TileStatusEnum.ItemDisabled);
                return -1;
            }

            TilesPage.SetTileStatus(tile, TileStatusEnum.ItemEnabled);
            return index;

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public static ItemIndexConverter Instance { get; } = new ItemIndexConverter();
    }
}
