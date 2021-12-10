using Microsoft.Xaml.Interactivity;
using System;
using System.Diagnostics;
using System.Linq;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace DragPositionDemonstrateProject
{
    public partial class DragPositionBehavior
    {
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

    }

    public class DragPositionData
    {
        private object zoomFactor;
        private object baseParent;

        public object ZoomFactor
        {
            get => zoomFactor;
            set
            {
                if ((value ?? 0.0) is double ||
                    value is BindingBase ||
                    (double.TryParse(value.ToString(), out double val) && (value = val) != null))
                    zoomFactor = value;
                else
                    throw new ArgumentException(nameof(value));
            }
        }
        public object BaseParent
        {
            get => baseParent;
            set
            {
                if (value is UIElement || value is BindingBase)
                    baseParent = value;
                else
                    throw new ArgumentException(nameof(value));
            }
        }
    }
}
