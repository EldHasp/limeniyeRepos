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
        private static int count;
        /// <summary>Для отладки.</summary>
        public int Number { get; } = count++;

        public DependencyObject AssociatedObject { get; set; }
        public UIElement AssociatedUIElement { get; private set; }

        private Point prevPoint;
        private int pointerId = -1;

        #region Life circle
        public void Attach(DependencyObject associatedObject)
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                return;
            }

            if (!(associatedObject is UIElement associatedUIElement))
            {
                throw new ArgumentException("Только для UIElement", nameof(associatedObject));
            }

            HandlersData handlersData = new HandlersData(associatedUIElement, GetBaseParent(associatedUIElement));

            //if (associatedObject != AssociatedObject)
            //{
            //    AssociatedObject = associatedObject;
            //    AssociatedUIElement = associatedUIElement;

            //    //associatedUIElement.PointerPressed += OnElementPointerPressed;
            //    associatedUIElement.AddHandler(UIElement.PointerPressedEvent, (PointerEventHandler)OnElementPointerPressed, true);
            //}


        }
        public void Detach()
        {
            //BaseParent = null;
            AssociatedUIElement.PointerPressed -= OnElementPointerPressed;
            AssociatedObject = null;
            AssociatedUIElement = null;
        }
        #endregion

        #region Handle pointer input
        private void OnElementPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            UIElement element = (UIElement)sender;
            UIElement BaseParent = GetBaseParent(element);
            if (BaseParent == null)
                return;

            //BaseParent.PointerReleased += OnElementPointerReleased;
            BaseParent.AddHandler(UIElement.PointerReleasedEvent, (PointerEventHandler)OnElementPointerReleased, true);

            // Возможно здесь ещё нужно прописать событие выхода за пределы панели

            //var element = AssociatedObject as FrameworkElement;

            if (AssociatedUIElement == null)
                return;

            countMove = 0;
            BaseParent.PointerMoved += OnMove;

            prevPoint = e.GetCurrentPoint(BaseParent).Position;
            pointerId = (int)e.Pointer.PointerId;
        }

        private void OnElementPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            var basePanel = (UIElement)sender;
            //basePanel.PointerReleased -= OnElementPointerReleased;
            basePanel.RemoveHandler(UIElement.PointerReleasedEvent, (PointerEventHandler)OnElementPointerReleased);

            basePanel.PointerMoved -= OnMove;

            if (e.Pointer.PointerId != pointerId)
                return;

            // var element = AssociatedObject as FrameworkElement;
            if (AssociatedUIElement == null)
                return;

            pointerId = -1;
        }

        int countMove;
        private void OnMove(object sender, PointerRoutedEventArgs e)
        {
            Debug.WriteLine($"{countMove++}: {sender}");
            double zommFactor = 1;

            if (/*e.Pointer.PointerId != pointerId ||*/ AssociatedUIElement is null)
                return;

            var pos = e.GetCurrentPoint(null).Position;

            SetOffsetX(AssociatedUIElement, GetOffsetX(AssociatedUIElement) + (pos.X - prevPoint.X) / zommFactor);
            SetOffsetY(AssociatedUIElement, GetOffsetY(AssociatedUIElement) + (pos.Y - prevPoint.Y) / zommFactor);

            prevPoint = pos;
        }

#endregion
    }
}
