using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    ///// на данный проект и заново выполнить построение во избежание ошибок компиляции:
    /////
    /////     Щелкните правой кнопкой мыши нужный проект в обозревателе решений и выберите
    /////     "Добавить ссылку"->"Проекты"->[Выберите этот проект]
    /////
    /////
    ///// Шаг 2)
    ///// Продолжайте дальше и используйте элемент управления в файле XAML.
    /////
    /////     <MyNamespace:CustomControl1/>
    /////
    ///// </summary>
    /// <summary>Страница плиток.</summary>
    public partial class TilesPage : ContentControl
    {
        static TilesPage()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TilesPage), new FrameworkPropertyMetadata(typeof(TilesPage)));
        }
        private readonly ObservableCollection<TilesPageItem> tiles = new ObservableCollection<TilesPageItem>();
        private TilesPageItem[,] tiles2d;
        public TilesPage()
        {
            Resources["tiles"] = tiles;
            bindingTileStyle = new Binding() { Path = new PropertyPath(TileStyleProperty), Source = this, Mode = BindingMode.OneWay };
            bindingItemTileTemplate = new Binding() { Path = new PropertyPath(ItemTileTemplateProperty), Source = this, Mode = BindingMode.OneWay };
            bindingPreviousTemplate = new Binding() { Path = new PropertyPath(PreviousTileTemplateProperty), Source = this, Mode = BindingMode.OneWay };
            bindingNextTemplate = new Binding() { Path = new PropertyPath(NextTileTemplateProperty), Source = this, Mode = BindingMode.OneWay };

            bindingItemsSourceCount = new Binding($"{nameof(ItemsSource)}.{nameof(ItemsSource.Count)}") { Source = this, Mode = BindingMode.OneWay };
            proxyCount.SetValueBinding(bindingItemsSourceCount);
            proxyCount.ValueChanged += OnProxyCountChanged;
            ///// <remarks>Данные в values:
            ///// 0 - <see cref="TilesPage.ItemsSource"/>.Count;
            ///// 1 - <see cref="TilesPage.Rows"/>;
            ///// 2 - <see cref="TilesPage.Columns"/>;
            ///// 3 - <see cref="TilesPage.DisabledButtonVisibility"/>;
            ///// 4 - <see cref="TilesPage.PageIndex"/>;
            ///// 5 - <see cref="TilesPageItem.Row"/>;
            ///// 6 - <see cref="TilesPageItem.Column"/>;
            ///// 7 - <see cref="TilesPageItem"/>;
            ///// </remarks>
            //multiBindingItemIndex = new MultiBinding() { Converter = ItemIndexConverter.Instance };
            //multiBindingItemIndex.Bindings.Add(new Binding() { Path = new PropertyPath(ItemsSourceProperty), Source = this, Mode = BindingMode.OneWay });
            //multiBindingItemIndex.Bindings.Add(new Binding() { Path = new PropertyPath(RowsProperty), Source = this, Mode = BindingMode.OneWay });
            //multiBindingItemIndex.Bindings.Add(new Binding() { Path = new PropertyPath(ColumnsProperty), Source = this, Mode = BindingMode.OneWay });
            //multiBindingItemIndex.Bindings.Add(new Binding() { Path = new PropertyPath(DisabledButtonVisibilityProperty), Source = this, Mode = BindingMode.OneWay });
            //multiBindingItemIndex.Bindings.Add(new Binding() { Path = new PropertyPath(PageIndexProperty), Source = this, Mode = BindingMode.OneWay });
            //multiBindingItemIndex.Bindings.Add(new Binding() { Path = new PropertyPath(TilesPageItem.RowProperty), RelativeSource = RelativeSource.Self, Mode = BindingMode.OneWay });
            //multiBindingItemIndex.Bindings.Add(new Binding() { Path = new PropertyPath(TilesPageItem.ColumnProperty), RelativeSource = RelativeSource.Self, Mode = BindingMode.OneWay });
            //multiBindingItemIndex.Bindings.Add(new Binding() { RelativeSource = RelativeSource.Self, Mode = BindingMode.OneWay });

            AddHandler(ListBoxItem.SelectedEvent, (RoutedEventHandler)OnSelectedTile);

            Rows = 1;
            Columns = 3;
            Initialized += OnInitialized;
        }
        private void OnInitialized(object sender, EventArgs e)
        {
            RowsOrColumnsChanged(default);
        }

        private void OnProxyCountChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            RenderPagesCount();
        }

        private readonly ProxyDO proxyCount = new ProxyDO();

        private static readonly Binding bindingDefault = new Binding();
        private readonly Binding bindingTileStyle;
        private readonly Binding bindingItemTileTemplate;
        private readonly Binding bindingNextTemplate;
        private readonly Binding bindingPreviousTemplate;
        private readonly Binding bindingItemsSourceCount;

        private readonly MultiBinding multiBindingItemIndex;
    }
}
