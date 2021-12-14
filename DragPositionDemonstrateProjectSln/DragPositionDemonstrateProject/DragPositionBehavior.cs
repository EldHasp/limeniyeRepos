using Microsoft.Xaml.Interactivity;
using System;
using System.Diagnostics;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace DragPositionDemonstrateProject
{
    public partial class DragPositionBehavior : DependencyObject, IBehavior
    {

        public DependencyObject AssociatedObject { get; set; }
        public UIElement AssociatedUIElement { get; private set; }

        private Point prevPoint;
        private int pointerId = -1;

        #region Life circle
        public void Attach(DependencyObject associatedObject)
        {
            //if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            //{
            //    return;
            //}

            //if (!(associatedObject is UIElement associatedUIElement))
            //{
            //    throw new ArgumentException("Только для UIElement", nameof(associatedObject));
            //}

            //HandlersData handlersData = new HandlersData(associatedUIElement, GetBaseParent(associatedUIElement));


        }
        public void Detach()
        {
            ////BaseParent = null;
            //AssociatedUIElement.PointerPressed -= OnElementPointerPressed;
            //AssociatedObject = null;
            //AssociatedUIElement = null;
        }
        #endregion



        /// <summary>Возвращает значение присоединённого свойства HandlersData для <paramref name="element"/>.</summary>
        /// <param name="element"><see cref="UIElement"/> значение свойства которого будет возвращено.</param>
        /// <returns><see cref="HandlersData"/> значение свойства.</returns>
        private static HandlersData GetHandlersData(UIElement element)
        {
            return (HandlersData)element.GetValue(HandlersDataProperty);
        }

        /// <summary>Задаёт значение присоединённого свойства HandlersData для <paramref name="element"/>.</summary>
        /// <param name="element"><see cref="UIElement"/> значение свойства которого будет возвращено.</param>
        /// <param name="value"><see cref="HandlersData"/> значение для свойства.</param>
        private static void SetHandlersData(UIElement element, HandlersData value)
        {
            element.SetValue(HandlersDataProperty, value);
        }

        /// <summary><see cref="DependencyProperty"/> для методов <see cref="GetHandlersData(UIElement)"/> и <see cref="SetHandlersData(UIElement, HandlersData)"/>.</summary>
        private static readonly DependencyProperty HandlersDataProperty =
            DependencyProperty.RegisterAttached(nameof(GetHandlersData).Substring(3), typeof(HandlersData), typeof(DragPositionBehavior),
                new PropertyMetadata(null, HandlersDataChanged));

        private static void HandlersDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UIElement element = (UIElement)d;
            if(e.OldValue is HandlersData handlersData)
            {
                handlersData.Dispose();
            }
        }
    }
}
