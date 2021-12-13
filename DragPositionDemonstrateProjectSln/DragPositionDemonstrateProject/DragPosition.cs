using Microsoft.Xaml.Interactivity;
using System;
using System.Diagnostics;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace DragPositionDemonstrateProject
{
    public partial class DragPosition : DependencyObject
    {
        public static DependencyObject AssociatedObject { get; set; }
        public static UIElement AssociatedUIElement { get; private set; }

        private static Point prevPoint;
        private static int pointerId = -1;

        #region Life circle
        private static void Initizlize(DependencyObject associatedObject)
        {
            if (!(associatedObject is UIElement associatedUIElement))
            {
                throw new ArgumentException("Только для UIElement", nameof(associatedObject));
            }

            if (associatedObject != AssociatedObject)
            {
                AssociatedObject = associatedObject;
                AssociatedUIElement = associatedUIElement;

                associatedUIElement.AddHandler(UIElement.PointerPressedEvent, (PointerEventHandler)OnElementPointerPressed, true);
            }
        }
        public void Detach()
        {
            AssociatedUIElement.PointerPressed -= OnElementPointerPressed;
            AssociatedObject = null;
            AssociatedUIElement = null;
        }
        #endregion

        #region Handle pointer input
        private static void OnElementPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            var parent = GetBaseParent((DependencyObject)sender);
            if (parent == null)
                return;

            parent.AddHandler(UIElement.PointerReleasedEvent, (PointerEventHandler)OnElementPointerReleased, true);

            // Возможно здесь ещё нужно прописать событие выхода за пределы панели

            if (AssociatedUIElement == null)
                return;

            parent.PointerMoved += OnMove;

            prevPoint = e.GetCurrentPoint(parent).Position;
            pointerId = (int)e.Pointer.PointerId;
        }

        private static void OnElementPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            var basePanel = (UIElement)sender;

            basePanel.RemoveHandler(UIElement.PointerReleasedEvent, (PointerEventHandler)OnElementPointerReleased);

            basePanel.PointerMoved -= OnMove;

            if (e.Pointer.PointerId != pointerId)
                return;

            if (AssociatedUIElement == null)
                return;

            pointerId = -1;
        }

        private static void OnMove(object sender, PointerRoutedEventArgs e)
        {
            var parent = GetBaseParent((DependencyObject)sender);

            if (parent == null)
                return;

            double zommFactor = GetZoomFactor((UIElement)sender);

            if (AssociatedUIElement is null)
                return;

            var pos = e.GetCurrentPoint(parent).Position;

            SetOffsetX(AssociatedUIElement, GetOffsetX(AssociatedUIElement) + (pos.X - prevPoint.X) / zommFactor);
            SetOffsetY(AssociatedUIElement, GetOffsetY(AssociatedUIElement) + (pos.Y - prevPoint.Y) / zommFactor);

            prevPoint = pos;
        }
        #endregion
    }
}
