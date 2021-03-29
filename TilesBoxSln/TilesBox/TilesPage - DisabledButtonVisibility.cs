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


        public Visibility DisabledButtonVisibility
        {
            get { return (Visibility)GetValue(DisabledButtonVisibilityProperty); }
            set { SetValue(DisabledButtonVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DisabledButtonVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisabledButtonVisibilityProperty =
            DependencyProperty.Register(nameof(DisabledButtonVisibility), typeof(Visibility), typeof(TilesPage), new PropertyMetadata(Visibility.Visible));


    }
}
