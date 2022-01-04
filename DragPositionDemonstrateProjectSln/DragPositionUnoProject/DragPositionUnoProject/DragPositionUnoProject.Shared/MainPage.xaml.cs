using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;

namespace DragPositionUnoProject
{
    public class SomeType : OnNotifyPropertyChanged
    {
        private double _positionX, _positionY;

        public double PositionX { get => _positionX; set => SetProperty(ref _positionX, value); }
        public double PositionY { get => _positionY; set => SetProperty(ref _positionY, value); }
    }

    public class SomeViewModel : OnNotifyPropertyChanged
    {
        private object _testContent = "Какая-то строка";
        public object TestContent { get => _testContent; set => SetProperty(ref _testContent, value); }

        public ObservableCollection<SomeType> Types { get; } = new ObservableCollection<SomeType>()
        {
            new SomeType() { PositionX = 10, PositionY = 60},
            new SomeType() { PositionX = 280, PositionY = 60}
        };
    }

    public sealed partial class MainPage : Page
    {
        public SomeViewModel ViewModel { get; } = new SomeViewModel();
        public MainPage()
        {
            this.InitializeComponent();
            //            var dataContext = new SomeViewModel();
#if HAS_UNO_SKIA_WPF
            //dataContext.TestContent = DependencyHandler.ServiceProvider.GetService<IDragRectanglesTypeWithUserControl>()?.Content;
            ViewModel.TestContent = DependencyHandler.ServiceProvider.GetService<IDragRectanglesTypeWithUserControl>()?.Content;
#endif
            //            DataContext = dataContext;
        }
    }
}
