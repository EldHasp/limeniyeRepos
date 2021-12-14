using System;
using System.Diagnostics;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace DragPositionDemonstrateProject
{
    public partial class DragPositionBehavior
    {
        private class HandlersData
        {
            private Point prevPoint;
            private int pointerId = -1;
            int countMove;

            private readonly UIElement parent;
            private readonly UIElement element;

            public HandlersData(UIElement element, UIElement parent)
            {
                this.parent = parent ?? throw new ArgumentNullException(nameof(parent));
                this.element = element ?? throw new ArgumentNullException(nameof(element));

                element.AddHandler(UIElement.PointerPressedEvent, (PointerEventHandler)OnElementPointerPressed, true);
            }

            private void OnElementPointerPressed(object sender, PointerRoutedEventArgs e)
            {
                parent.AddHandler(UIElement.PointerReleasedEvent, (PointerEventHandler)OnElementPointerReleased, true);

                countMove = 0;
                parent.PointerMoved += OnMove;

                prevPoint = e.GetCurrentPoint(parent).Position;
                pointerId = (int)e.Pointer.PointerId;
            }

            private void OnElementPointerReleased(object sender, PointerRoutedEventArgs e)
            {
                parent.RemoveHandler(UIElement.PointerReleasedEvent, (PointerEventHandler)OnElementPointerReleased);

                parent.PointerMoved -= OnMove;

                if (e.Pointer.PointerId != pointerId)
                    return;

                pointerId = -1;
            }

            private void OnMove(object sender, PointerRoutedEventArgs e)
            {
                Debug.WriteLine($"{countMove++}: {sender}");
                double zommFactor = 1;

                var pos = e.GetCurrentPoint(null).Position;

                SetOffsetX(element, GetOffsetX(element) + (pos.X - prevPoint.X) / zommFactor);
                SetOffsetY(element, GetOffsetY(element) + (pos.Y - prevPoint.Y) / zommFactor);

                prevPoint = pos;
            }


        }
    }
}
