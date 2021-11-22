using Microsoft.Xaml.Interactivity;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace DragPositionDemonstrateProject
{
    public partial class DragPositionBehavior : DependencyObject, IBehavior
    {
        public DependencyObject AssociatedObject { get; set; }

        private Point prevPoint;
        private int pointerId = -1;

        public static readonly DependencyProperty ZoomFactorProperty = DependencyProperty.Register(nameof(ZoomFactor), typeof(double), typeof(DragPositionBehavior), new PropertyMetadata(0));
        public double ZoomFactor { get => (double)GetValue(ZoomFactorProperty); set => SetValue(ZoomFactorProperty, value); }


        public static readonly DependencyProperty BaseParentProperty = DependencyProperty.Register(nameof(BaseParent), typeof(UIElement), typeof(DragPositionBehavior), new PropertyMetadata(null));
        public UIElement BaseParent { get => (UIElement)GetValue(BaseParentProperty); set => SetValue(BaseParentProperty, value); }


        #region Life circle
        public void Attach(DependencyObject associatedObject)
        {
            if ((associatedObject != AssociatedObject) && !Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                AssociatedObject = associatedObject;

                var element = AssociatedObject as FrameworkElement;
                if (element != null)
                {
                    BaseParent.PointerPressed += OnElementPointerPressed;
                    BaseParent.PointerReleased += OnElementPointerReleased;
                }
            }
        }

        public void Detach()
        {
            if (BaseParent != null)
            {
                BaseParent.PointerPressed -= OnElementPointerPressed;
                BaseParent.PointerReleased -= OnElementPointerReleased;
                BaseParent.PointerMoved -= OnMove;
            }

            BaseParent = null;
            AssociatedObject = null;
        }
        #endregion

        #region Handle pointer input
        private void OnElementPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            var element = AssociatedObject as FrameworkElement;

            if (element == null)
                return;

            if (!(element.RenderTransform is TranslateTransform))
                element.RenderTransform = new TranslateTransform();

            BaseParent.PointerMoved += OnMove;

            prevPoint = e.GetCurrentPoint(BaseParent).Position;
            pointerId = (int)e.Pointer.PointerId;
        }

        private void OnElementPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.PointerId != pointerId)
                return;

            var element = AssociatedObject as FrameworkElement;
            if (element == null)
                return;

            BaseParent.PointerMoved -= OnMove;
            pointerId = -1;
        }

        private void OnMove(object o, PointerRoutedEventArgs e)
        {
            var zommFactor = ZoomFactor;
            var element = AssociatedObject as FrameworkElement;

            if (e.Pointer.PointerId != pointerId || element is null)
                return;

            var pos = e.GetCurrentPoint(BaseParent).Position;
            ((TranslateTransform)element.RenderTransform).X += (pos.X - prevPoint.X) / zommFactor;
            ((TranslateTransform)element.RenderTransform).Y += (pos.Y - prevPoint.Y) / zommFactor;
            prevPoint = pos;
        }
        #endregion
    }
}
