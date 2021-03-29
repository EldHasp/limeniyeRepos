using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace TilesBox
{
    public partial class TilesPage : ContentControl
    {
        //private void BindingRender()
        //{
        //    ObservableCollection<TilesPageItem> tiles = (ObservableCollection<TilesPageItem>)Resources["tiles"];
        //    int tileIndex = 0;
        //    int itemIndex = PageIndex * TilesCount;
        //    TilesPageItem tile;
        //    for (int row = 0; row < Rows - 1; row++)
        //        for (int column = 0; column < Columns; column++, tileIndex++, itemIndex++)
        //        {
        //            Binding binding = new Binding($"{nameof(ItemsSource)}[{itemIndex}]")
        //            { Source = this };
        //            tile = tiles[tileIndex];
        //            tile.SetBinding(DataContextProperty, binding);
        //            SetItemIndex(tile, tileIndex);
        //        }
        //    tileIndex++;
        //    for (int column = 1; column < Columns - 1; column++, tileIndex++, itemIndex++)
        //    {
        //        Binding binding = new Binding($"{nameof(ItemsSource)}[{itemIndex}]")
        //        { Source = this };
        //        tiles[tileIndex].SetBinding(DataContextProperty, binding);
        //    }


        //}

    }
}
