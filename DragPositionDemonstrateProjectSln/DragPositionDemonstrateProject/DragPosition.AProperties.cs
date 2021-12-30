using Microsoft.Xaml.Interactivity;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace DragPositionDemonstrateProject
{
    [Bindable]
    public partial class DragPosition
    {
        #region DragPosition
        public static DragPositionData GetData(DependencyObject obj)
        {
            return (DragPositionData)obj.GetValue(DataProperty);
        }

        public static void SetData(DependencyObject obj, DragPositionData value)
        {
            obj.SetValue(DataProperty, value);
        }

        // Using a DependencyProperty as the backing store for DragPosition.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.RegisterAttached("Data", typeof(DragPositionData), typeof(DragPosition), new PropertyMetadata(null, OnDataChanged));

        private static void OnDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DragPositionData data = (DragPositionData)e.NewValue;

            DragPosition local = new DragPosition();

            if (data.ZoomFactor is double zoom)
                SetZoomFactor(d, zoom);
            else
                BindingOperations.SetBinding(local, ZoomFactorProperty, (BindingBase)data.ZoomFactor);
            if (data.BaseParent is UIElement parent)
                SetBaseParent(d, parent);
            else
                BindingOperations.SetBinding(local, BaseParentProperty, (BindingBase)data.BaseParent);

            if (data.OffsetX is double x)
                SetOffsetX(d, x);
            else
                BindingOperations.SetBinding(d, OffsetXProperty, (BindingBase)data.OffsetX);

            if (data.OffsetY is double y)
                SetOffsetY(d, y);
            else
                BindingOperations.SetBinding(d, OffsetYProperty, (BindingBase)data.OffsetY);

            data.BindingAction?.Invoke(d);

            Initizlize(d);
        }
        #endregion

        #region Offset properties
        public static double GetOffsetX(DependencyObject obj)
        {
            return (double)obj.GetValue(OffsetXProperty);
        }

        public static void SetOffsetX(DependencyObject obj, double value)
        {
            obj.SetValue(OffsetXProperty, value);
        }

        // Using a DependencyProperty as the backing store for OffsetX.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OffsetXProperty =
            DependencyProperty.RegisterAttached("OffsetX", typeof(double), typeof(DragPosition), new PropertyMetadata(0.0));



        public static double GetOffsetY(DependencyObject obj)
        {
            return (double)obj.GetValue(OffsetYProperty);
        }

        public static void SetOffsetY(DependencyObject obj, double value)
        {
            obj.SetValue(OffsetYProperty, value);
        }

        // Using a DependencyProperty as the backing store for OffsetY.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OffsetYProperty =
            DependencyProperty.RegisterAttached("OffsetY", typeof(double), typeof(DragPositionData), new PropertyMetadata(0.0));

        #endregion

        #region Parent
        public static UIElement GetBaseParent(DependencyObject obj)
        {
            return (UIElement)obj.GetValue(BaseParentProperty);
        }

        public static void SetBaseParent(DependencyObject obj, UIElement value)
        {
            obj.SetValue(BaseParentProperty, value);
        }

        // Using a DependencyProperty as the backing store for DragPosition.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BaseParentProperty =
            DependencyProperty.RegisterAttached("BaseParent", typeof(UIElement), typeof(DragPosition), new PropertyMetadata(null));
        #endregion

        #region ZoomFactor
        public static double GetZoomFactor(DependencyObject obj)
        {
            return (double)obj.GetValue(ZoomFactorProperty);
        }

        public static void SetZoomFactor(DependencyObject obj, double value)
        {
            obj.SetValue(ZoomFactorProperty, value);
        }

        // Using a DependencyProperty as the backing store for DragPosition.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ZoomFactorProperty =
            DependencyProperty.RegisterAttached("ZoomFactor", typeof(double), typeof(DragPosition), new PropertyMetadata(null));
        #endregion
    }
}
