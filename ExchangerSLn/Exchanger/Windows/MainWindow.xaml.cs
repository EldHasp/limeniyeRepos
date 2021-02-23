using System.Windows;

namespace Exchanger.Windows
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Grid_PreviewGotKeyboardFocus(object sender, System.Windows.Input.KeyboardFocusChangedEventArgs e)
        {
            e.Handled = true;
        }
    }
}
