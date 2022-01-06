using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace DragPositionDemonstrateProject
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class PointPage : Page
    {
        public Point2D ViewModel { get; } = new Point2D() { X = 100, Y = 200 };
        public PointPage()
        {
            this.InitializeComponent();
        }

        private void OnClick(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)sender;

            if (element.Width < 50)
            {
                element.Width = element.Height = 80;
            }
            else
            {
                element.Width = element.Height = 40;
            }
        }
    }

    public class Point2D : OnNotifyPropertyChanged
    {
        private double _x;
        private double _y;

        public double X { get => _x; set => SetProperty(ref _x, value); }
        public double Y { get => _y; set => SetProperty(ref _y, value); }
    }
}
