using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace DragPositionWPFSolution.DragPosition
{
    public partial class DragPosition
    {

        private class HandlersData : IDisposable
        {
            public void Dispose()
            {
                IsDispose = true;
                element.RemoveHandler(UIElement.MouseDownEvent, (MouseButtonEventHandler)OnElementPointerPressed);
                parent.RemoveHandler(UIElement.MouseUpEvent, (MouseButtonEventHandler)OnElementPointerReleased);
                parent.MouseMove -= OnMove;
            }

            public bool IsDispose { get; private set; }

            private Point prevPoint;
            //private int pointerId = -1;
            int countMove;

            private readonly UIElement parent;
            private readonly UIElement element;

            public HandlersData(UIElement element, UIElement parent)
            {
                this.parent = parent ?? throw new ArgumentNullException(nameof(parent));
                this.element = element ?? throw new ArgumentNullException(nameof(element));

                element.AddHandler(UIElement.MouseDownEvent, (MouseButtonEventHandler)OnElementPointerPressed, true);
            }

            private void OnElementPointerPressed(object sender, MouseButtonEventArgs e)
            {
                parent.AddHandler(UIElement.MouseUpEvent, (MouseButtonEventHandler)OnElementPointerReleased, true);

                countMove = 0;
                parent.MouseMove += OnMove;

                prevPoint = e.GetPosition(null);
                //pointerId = (int)e.Pointer.PointerId;
            }

            private void OnElementPointerReleased(object sender, MouseButtonEventArgs e)
            {
                parent.RemoveHandler(UIElement.MouseUpEvent, (MouseButtonEventHandler)OnElementPointerReleased);

                parent.MouseMove -= OnMove;

                //if (e.Pointer.PointerId != pointerId)
                //    return;

                //pointerId = -1;
            }

            private void OnMove(object sender, MouseEventArgs e)
            {
                Debug.WriteLine($"{countMove++}: {sender}");
                double zommFactor = 1;

                var pos = e.GetPosition(null);

                SetOffsetX(element, GetOffsetX(element) + (pos.X - prevPoint.X) / zommFactor);
                SetOffsetY(element, GetOffsetY(element) + (pos.Y - prevPoint.Y) / zommFactor);

                prevPoint = pos;
            }


        }
    }
}
