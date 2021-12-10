using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;

namespace DragPositionDemonstrateProject
{

    public class SomeType : OnNotifyPropertyChanged
    {
        private double _positionX, _positionY;

        public double PositionX { get => _positionX; set => SetProperty(ref _positionX, value); }
        public double PositionY { get => _positionY; set => SetProperty(ref _positionY, value); }
    }

    public class SomeViewModel : OnNotifyPropertyChanged
    {
        public ObservableCollection<SomeType> Types { get; } = new ObservableCollection<SomeType>()
        {
            new SomeType() { PositionX = 10, PositionY = 60},
            new SomeType() { PositionX = 280, PositionY = 60}
        };
    }


    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            //DataContext = new SomeViewModel();
        }
    }
}
