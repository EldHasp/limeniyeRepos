using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace TilesBox
{
    public partial class TilesPage : ContentControl
    {

        /// <summary>Визуальный контент для плитки "Следующая страница".</summary>
        public UIElement NextTileContent
        {
            get { return (UIElement)GetValue(NextTileContentProperty); }
            set { SetValue(NextTileContentProperty, value); }
        }

        /// <summary>DependencyProperty для <see cref="PageIndex"/>.</summary>
        public static readonly DependencyProperty NextTileContentProperty =
            DependencyProperty.Register(nameof(NextTileContent), typeof(UIElement), typeof(TilesPage), new PropertyMetadata(null));

        /// <summary>Визуальный контент для плитки "Предыдушая страница".</summary>
        public UIElement PreviousTileContent
        {
            get { return (UIElement)GetValue(PreviousTileContentProperty); }
            set { SetValue(PreviousTileContentProperty, value); }
        }

        /// <summary>DependencyProperty для <see cref="PageIndex"/>.</summary>
        public static readonly DependencyProperty PreviousTileContentProperty =
            DependencyProperty.Register(nameof(PreviousTileContent), typeof(UIElement), typeof(TilesPage), new PropertyMetadata(null));


    }
}
