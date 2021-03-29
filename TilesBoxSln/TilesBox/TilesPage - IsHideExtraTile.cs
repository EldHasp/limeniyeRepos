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
        public bool IsHideExtraTile
        {
            get { return (bool)GetValue(IsHideExtraTileProperty); }
            set { SetValue(IsHideExtraTileProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsHideExtraTile.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsHideExtraTileProperty =
            DependencyProperty.Register(nameof(IsHideExtraTile), typeof(bool), typeof(TilesPage), new PropertyMetadata(false));

    }
}
