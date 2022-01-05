using System.Windows;

namespace DragPositionUnoProject.WPF.Host
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            root.Content = new global::Uno.UI.Skia.Platform.WpfHost(Dispatcher, () => new DragPositionUnoProject.App());
        }
    }
}
