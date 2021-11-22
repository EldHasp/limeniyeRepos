using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace TilesBox
{
    public partial class TilesPage : ContentControl
    {

        /// <summary>Шаблон для плитки "Следующая страница".
        /// Контент плятки <see langword="true"/>, если 
        /// это не последняя страница.</summary>
        public DataTemplate NextTileTemplate
        {
            get { return (DataTemplate)GetValue(NextTileTemplateProperty); }
            set { SetValue(NextTileTemplateProperty, value); }
        }

        /// <summary>DependencyProperty для <see cref="PageIndex"/>.</summary>
        public static readonly DependencyProperty NextTileTemplateProperty =
            DependencyProperty.Register(nameof(NextTileTemplate), typeof(DataTemplate), typeof(TilesPage), new PropertyMetadata(null));

        /// <summary>Шаблон для плитки "Предыдушая страница".
        /// Контент плятки <see langword="true"/>, если 
        /// это не первая страница.</summary>
        public DataTemplate PreviousTileTemplate
        {
            get { return (DataTemplate)GetValue(PreviousTileTemplateProperty); }
            set { SetValue(PreviousTileTemplateProperty, value); }
        }

        /// <summary>DependencyProperty для <see cref="PageIndex"/>.</summary>
        public static readonly DependencyProperty PreviousTileTemplateProperty =
            DependencyProperty.Register(nameof(PreviousTileTemplate), typeof(DataTemplate), typeof(TilesPage), new PropertyMetadata(null));


    }
}
