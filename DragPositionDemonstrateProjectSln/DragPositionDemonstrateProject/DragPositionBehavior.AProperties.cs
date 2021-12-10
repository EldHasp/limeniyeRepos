﻿using Microsoft.Xaml.Interactivity;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace DragPositionDemonstrateProject
{
    [Bindable]
    public partial class DragPositionBehavior
    {
        #region DragPosition
        public static DragPositionData GetDragPosition(DependencyObject obj)
        {
            return (DragPositionData)obj.GetValue(DragPositionProperty);
        }

        public static void SetDragPosition(DependencyObject obj, DragPositionData value)
        {
            obj.SetValue(DragPositionProperty, value);
        }

        // Using a DependencyProperty as the backing store for DragPosition.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DragPositionProperty =
            DependencyProperty.RegisterAttached("DragPosition", typeof(DragPositionData), typeof(DragPositionBehavior), new PropertyMetadata(null, DragPositionChanged));

        private static void DragPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            /*
                        <interactivity:Interaction.Behaviors>
                            <local:DragPositionBehavior ZoomFactor="1" BaseParent="{Binding ElementName=baseListView}"/>
                        </interactivity:Interaction.Behaviors>
             */

            var behaviors = Interaction.GetBehaviors(d);

            var dragPositionBehavior = behaviors.OfType<DragPositionBehavior>().FirstOrDefault();
            if (dragPositionBehavior != null)
                behaviors.Remove(dragPositionBehavior);

            DragPositionData data = (DragPositionData)e.NewValue;
            DragPositionBehavior behavior = new DragPositionBehavior();
            if (data.ZoomFactor is double zoom)
                behavior.ZoomFactor = zoom;
            else
                BindingOperations.SetBinding(behavior, ZoomFactorProperty, (BindingBase)data.ZoomFactor);
            if (data.BaseParent is UIElement parent)
                behavior.BaseParent = parent;
            else
                BindingOperations.SetBinding(behavior, BaseParentProperty, (BindingBase)data.BaseParent);

            behaviors.Add(behavior);
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
            DependencyProperty.RegisterAttached("OffsetX", typeof(double), typeof(DragPositionBehavior), new PropertyMetadata(0.0));



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
    }
}
