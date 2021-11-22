using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace TilesBox
{
    public partial class TilesPage : ContentControl
    {
        private void RenderTilesBinding()
        {
            int itemIndex = firstItemIndex;
            TilesPageItem tile;
            if (!(proxyCount.Value is int count))
                count = -1;
            for (int row = 0; row < rows; row++)
                for (int column = 0; column < columns; column++)
                {
                    tile = tiles2d[row, column];
                    if (IsItemTile(row, column))
                    {
                        Binding binding = new Binding($"{nameof(ItemsSource)}[{itemIndex}]")
                        { Source = this };
                        tile.SetBinding(DataContextProperty, binding);
                        SetItemIndex(tile, itemIndex);
                        SetTileType(tile, TileTypeContentEnum.Item);
                        SetIsTileEnabled(tile, itemIndex < count);
                        tile.SetBinding(ContentTemplateProperty, bindingItemTileTemplate);
                        itemIndex++;
                    }
                    else
                    {
                        if (column == 0)
                        {
                            tile.DataContext = pageIndex > 0;
                            ClearItemIndex(tile);
                            SetTileType(tile, TileTypeContentEnum.PreviousButton);
                            SetIsTileEnabled(tile, pageIndex > 0);
                            tile.SetBinding(ContentTemplateProperty, bindingPreviousTemplate);
                        }
                        else
                        {
                            tile.DataContext = pageIndex < pagesCount - 1;
                            ClearItemIndex(tile);
                            SetTileType(tile, TileTypeContentEnum.NextButton);
                            SetIsTileEnabled(tile, pageIndex < pagesCount - 1);
                            tile.SetBinding(ContentTemplateProperty, bindingNextTemplate);
                        }
                    }
                }
        }

        private bool IsItemTile(int row, int column)
        {
            if (row < rows - 1)
                return true;
            if (column != 0 && column < columns - 1)
                return true;
            if (pageIndex == 0 && column == 0 && disabledButtonVisibility != Visibility.Collapsed)
                return false;
            if (pageIndex >= pagesCount && column >= columns - 1 && disabledButtonVisibility != Visibility.Collapsed)
                return false;
            return true;
        }

    }
}
