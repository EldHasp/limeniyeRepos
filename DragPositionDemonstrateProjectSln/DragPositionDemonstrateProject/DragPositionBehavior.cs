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

        public static readonly DependencyProperty ZoomFactorProperty = DependencyProperty.Register(nameof(ZoomFactor), typeof(double), typeof(DragPositionBehavior), new PropertyMetadata(0));
        public double ZoomFactor { get => (double)GetValue(ZoomFactorProperty); set => SetValue(ZoomFactorProperty, value); }


        public static readonly DependencyProperty BaseParentProperty = DependencyProperty.Register(nameof(BaseParent), typeof(UIElement), typeof(DragPositionBehavior), new PropertyMetadata(null));
        public UIElement BaseParent { get => (UIElement)GetValue(BaseParentProperty); set => SetValue(BaseParentProperty, value); }


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

            if (associatedObject != AssociatedObject)
            {
                AssociatedObject = associatedObject;
                AssociatedUIElement = associatedUIElement;

                associatedUIElement.PointerPressed += OnElementPointerPressed;
                // BaseParent.PointerReleased += OnElementPointerReleased; Регистрацию отпускания надо делать после нажатия.
            }
        }

        public void Detach()
        {
            //if (BaseParent != null)
            //{
            //    BaseParent.PointerPressed -= OnElementPointerPressed;
            //    BaseParent.PointerReleased -= OnElementPointerReleased;
            //    BaseParent.PointerMoved -= OnMove;
            //}

            //BaseParent = null;
            AssociatedUIElement.PointerPressed -= OnElementPointerPressed;
            AssociatedObject = null;
            AssociatedUIElement = null;
        }
        #endregion

        #region Handle pointer input
        private void OnElementPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (BaseParent == null)
                return;

            BaseParent.PointerReleased += OnElementPointerReleased;
            // Возможно здесь ещё нужно прописать событие выхода за пределы панели

            //var element = AssociatedObject as FrameworkElement;

            if (AssociatedUIElement == null)
                return;

            // Это очень криво. Может в элемента есть своя друга трансформация?
            // Это нужно ОБЯЗАТЕЛЬНО заменить на AP-свойства.
            if (!(AssociatedUIElement.RenderTransform is TranslateTransform))
                AssociatedUIElement.RenderTransform = new TranslateTransform();

            countMove = 0;
            BaseParent.PointerMoved += OnMove;

            prevPoint = e.GetCurrentPoint(BaseParent).Position;
            pointerId = (int)e.Pointer.PointerId;
        }

        private void OnElementPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            var basePanel = (UIElement)sender;
            basePanel.PointerReleased -= OnElementPointerReleased;
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
            Debug.WriteLine($"{countMove++}: {sender}" );
            double zommFactor = ZoomFactor;

            if (/*e.Pointer.PointerId != pointerId ||*/ AssociatedUIElement is null)
                return;

            var pos = e.GetCurrentPoint(BaseParent).Position;
            ((TranslateTransform)AssociatedUIElement.RenderTransform).X += (pos.X - prevPoint.X) / zommFactor;
            ((TranslateTransform)AssociatedUIElement.RenderTransform).Y += (pos.Y - prevPoint.Y) / zommFactor;
            prevPoint = pos;
        }
        #endregion
    }
}
