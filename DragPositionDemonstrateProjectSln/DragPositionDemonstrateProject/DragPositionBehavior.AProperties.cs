using Microsoft.Xaml.Interactivity;
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
            var behaviors = Interaction.GetBehaviors(d);

            var dragPositionBehavior = behaviors.OfType<DragPositionBehavior>().FirstOrDefault();
            if (dragPositionBehavior != null)
                behaviors.Remove(dragPositionBehavior);

            DragPositionData data = (DragPositionData)e.NewValue;
            DragPositionBehavior behavior = new DragPositionBehavior();
            if (data.BaseParent is UIElement parent)
                SetBaseParent((UIElement)d, parent);
            else
                BindingOperations.SetBinding(d, BaseParentProperty, (BindingBase)data.BaseParent);

            if (data.OffsetX is double x)
                SetOffsetX(d, x);
            else
                BindingOperations.SetBinding(d, OffsetXProperty, (BindingBase)data.OffsetX);

            if (data.OffsetY is double y)
                SetOffsetY(d, y);
            else
                BindingOperations.SetBinding(d, OffsetYProperty, (BindingBase)data.OffsetY);

            behaviors.Add(behavior);

            data.BindingAction?.Invoke(d);

            UIElement element = (UIElement)d;
            HandlersData handlersData = new HandlersData(element, GetBaseParent(element));
            SetHandlersData(element, handlersData);
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

        #region BaseParent


        /// <summary>Возвращает значение присоединённого свойства BaseParent для <paramref name="element"/>.</summary>
        /// <param name="element"><see cref="UIElement"/> значение свойства которого будет возвращено.</param>
        /// <returns><see cref="UIElement"/> значение свойства.</returns>
        public static UIElement GetBaseParent(UIElement element)
        {
            return (UIElement)element.GetValue(BaseParentProperty);
        }

        /// <summary>Задаёт значение присоединённого свойства BaseParent для <paramref name="element"/>.</summary>
        /// <param name="element"><see cref="UIElement"/> значение свойства которого будет возвращено.</param>
        /// <param name="value"><see cref="UIElement"/> значение для свойства.</param>
        public static void SetBaseParent(UIElement element, UIElement value)
        {
            element.SetValue(BaseParentProperty, value);
        }

        /// <summary><see cref="DependencyProperty"/> для методов <see cref="GetBaseParent(UIElement)"/> и <see cref="SetBaseParent(UIElement, UIElement)"/>.</summary>
        public static readonly DependencyProperty BaseParentProperty =
            DependencyProperty.RegisterAttached(nameof(GetBaseParent).Substring(3), typeof(UIElement), typeof(DragPositionBehavior), new PropertyMetadata(null));


        #endregion
    }
}
