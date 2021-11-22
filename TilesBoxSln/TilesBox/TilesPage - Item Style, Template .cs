using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace TilesBox
{
    public partial class TilesPage : ContentControl
    {

        /// <summary>Стиль для плиток.</summary>
        public Style TileStyle
        {
            get { return (Style)GetValue(TileStyleProperty); }
            set { SetValue(TileStyleProperty, value); }
        }

        /// <summary>DependencyProperty для <see cref="TileStyle"/>.</summary>
        public static readonly DependencyProperty TileStyleProperty =
            DependencyProperty.Register(nameof(TileStyle), typeof(Style), typeof(TilesPage), new PropertyMetadata(null));


        /// <summary>Шаблон данных для плитки c элементом коллекции.</summary>
        public DataTemplate ItemTileTemplate
        {
            get { return (DataTemplate)GetValue(ItemTileTemplateProperty); }
            set { SetValue(ItemTileTemplateProperty, value); }
        }

        /// <summary>DependencyProperty для <see cref="ItemTileTemplate"/>.</summary>
        public static readonly DependencyProperty ItemTileTemplateProperty =
            DependencyProperty.Register(nameof(ItemTileTemplate), typeof(DataTemplate), typeof(TilesPage), new PropertyMetadata(null));



    }
}
