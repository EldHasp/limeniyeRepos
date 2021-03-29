using System.Collections.Generic;
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

namespace TilesBox
{
    ///// <summary>
    ///// Выполните шаги 1a или 1b, а затем 2, чтобы использовать этот пользовательский элемент управления в файле XAML.
    /////
    ///// Шаг 1a. Использование пользовательского элемента управления в файле XAML, существующем в текущем проекте.
    ///// Добавьте атрибут XmlNamespace в корневой элемент файла разметки, где он 
    ///// будет использоваться:
    /////
    /////     xmlns:MyNamespace="clr-namespace:TilesBox"
    /////
    /////
    ///// Шаг 1б. Использование пользовательского элемента управления в файле XAML, существующем в другом проекте.
    ///// Добавьте атрибут XmlNamespace в корневой элемент файла разметки, где он 
    ///// будет использоваться:
    /////
    /////     xmlns:MyNamespace="clr-namespace:TilesBox;assembly=TilesBox"
    /////
    ///// Потребуется также добавить ссылку из проекта, в котором находится файл XAML,
    ///// на данный проект и пересобрать во избежание ошибок компиляции:
    /////
    /////     Щелкните правой кнопкой мыши нужный проект в обозревателе решений и выберите
    /////     "Добавить ссылку"->"Проекты"->[Поиск и выбор проекта]
    /////
    /////
    ///// Шаг 2)
    ///// Теперь можно использовать элемент управления в файле XAML.
    /////
    /////     <MyNamespace:TilesBoxItem/>
    /////
    ///// </summary>
    /// <summary>Элемент "Плитка" для <see cref="TilesBox"/></summary>
    public class TilesPageItem : ListBoxItem
    {
        static TilesPageItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TilesPageItem), new FrameworkPropertyMetadata(typeof(TilesPageItem)));
        }



        public int Row
        {
            get { return (int)GetValue(RowProperty); }
            set { SetValue(RowProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Row.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowProperty =
            DependencyProperty.Register(nameof(Row), typeof(int), typeof(TilesPageItem), new PropertyMetadata(0));



        public int Column
        {
            get { return (int)GetValue(ColumnProperty); }
            set { SetValue(ColumnProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Column.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColumnProperty =
            DependencyProperty.Register(nameof(Column), typeof(int), typeof(TilesPageItem), new PropertyMetadata(0));


    }

}
