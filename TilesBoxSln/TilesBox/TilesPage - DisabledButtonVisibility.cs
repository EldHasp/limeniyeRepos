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

        /// <summary>Видимость отключенных кнопок смены страницы.</summary>
        public Visibility DisabledButtonVisibility
        {
            get { return (Visibility)GetValue(DisabledButtonVisibilityProperty); }
            set { SetValue(DisabledButtonVisibilityProperty, value); }
        }

        private Visibility disabledButtonVisibility;


        /// <summary><see cref="DependencyProperty"/> для свойства <see cref="DisabledButtonVisibility"/>.</summary>
        public static readonly DependencyProperty DisabledButtonVisibilityProperty =
            DependencyProperty.Register(nameof(DisabledButtonVisibility), typeof(Visibility), typeof(TilesPage), new PropertyMetadata(Visibility.Visible, DisabledButtonVisibilityChanged, CoerceDisabledButtonVisibility));

        private static object CoerceDisabledButtonVisibility(DependencyObject d, object baseValue)
        {
            if (Enum.IsDefined(typeof(Visibility), baseValue))
                return baseValue;
            return Visibility.Visible;
        }

        private static void DisabledButtonVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TilesPage tilesPage = (TilesPage)d;
            tilesPage.disabledButtonVisibility = (Visibility)e.NewValue;
        }
    }
}
