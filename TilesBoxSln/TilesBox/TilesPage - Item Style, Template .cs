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


        /// <summary>Шаблон данных для плитки.</summary>
        public DataTemplate TileTemplate
        {
            get { return (DataTemplate)GetValue(TileTemplateProperty); }
            set { SetValue(TileTemplateProperty, value); }
        }

        /// <summary>DependencyProperty для <see cref="TileTemplate"/>.</summary>
        public static readonly DependencyProperty TileTemplateProperty =
            DependencyProperty.Register(nameof(TileTemplate), typeof(DataTemplate), typeof(TilesPage), new PropertyMetadata(null));



    }
}
