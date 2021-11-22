using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TilesBox;

namespace Examples
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnValueChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
             parameters = $"{e.Property.Name}; {e.OldValue}; {e.NewValue}";
        }
        string parameters;

        TilesPageItem tile = new TilesPageItem();
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            //TilesPage.SetItemIndex(tile, 20);
            int? ind = TilesPage.GetItemIndex(tile);

            TilesPage.ClearItemIndex(tile);
            ind = TilesPage.GetItemIndex(tile);
        }

        public class UnsetConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return DependencyProperty.UnsetValue;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
    }
}
