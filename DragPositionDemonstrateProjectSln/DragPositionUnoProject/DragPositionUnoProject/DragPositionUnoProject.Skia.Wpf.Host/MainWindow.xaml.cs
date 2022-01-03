using DragPositionUnoProject.Skia.Wpf.Host.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace DragPositionUnoProject.WPF.Host
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var services = new ServiceCollection();
            services.AddSingleton<IDragRectanglesTypeWithUserControl, DragRectanglesTypeWithUserControl>();
            DependencyHandler.ServiceProvider = services.BuildServiceProvider();
            root.Content = new global::Uno.UI.Skia.Platform.WpfHost(Dispatcher, () => new DragPositionUnoProject.App());
        }
    }
}
