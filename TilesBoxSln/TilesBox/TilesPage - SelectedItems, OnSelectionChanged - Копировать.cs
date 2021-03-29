using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Controls.Primitives;

namespace TilesBox
{
    public partial class TilesPage : ContentControl
    {
        /// <summary>Обработка всплывающего события <see cref="Selector.SelectionChanged"/></summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //foreach (TilesBoxItem tilesBoxItem in e.RemovedItems.OfType<TilesBoxItem>())
            //    SelectedTiles.Remove(tilesBoxItem);

            //foreach (TilesBoxItem tilesBoxItem in e.RemovedItems.OfType<TilesBoxItem>())
            //{
            //    if (tilesBoxItem.Row== Rows-1)
            //    {
            //        if (tilesBoxItem.Column==0)

            //    }
            //    SelectedTiles.Remove(tilesBoxItem);
            //}
        }

    }
}
