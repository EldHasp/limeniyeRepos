using DragPositionUnoProject.Data;
using System;
using System.ComponentModel;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace DragPosition
{
    public partial class DragPosition
    {

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
            DependencyProperty.RegisterAttached(nameof(GetBaseParent).Substring(3), typeof(UIElement), typeof(DragPosition), new PropertyMetadata(null, BaseParentChanged));

        private static void BaseParentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UIElement element = (UIElement)d;

            HandlersData oldData = GetHandlersData(element);
            if (oldData != null)
            {
                oldData.Dispose();
            }

            if (e.NewValue is UIElement parent)
            {
                HandlersData newData = new HandlersData(element, parent);
                SetHandlersData(element, newData);
            }
            else
            {
                element.ClearValue(BaseParentProperty);
            }
        }
        #endregion
    }
}
