using System.Windows;
using System.Windows.Controls;

namespace Exchanger.Styles.Xamls.Code
{

    public class CustomItemsControl : ContentControl
    {

        #region Public Properties
        /// <summary>Количество строк плиток.
        /// Минимальное значение - одна.</summary>
        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        /// <summary>Количество колонок плиток.
        /// Минимальное значение - три.</summary>
        public int Columns
        {
            get { return (int)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        /// <summary>Количество элемнтов одной страницы.</summary>
        public int Length
        {
            get { return (int)GetValue(LengthProperty); }
            private set { SetValue(LengthPropertyKey, value); }
        }

        /// <summary>Номер (индекс) страницы.</summary>
        public int PageIndex
        {
            get { return (int)GetValue(PageIndexProperty); }
            set { SetValue(PageIndexProperty, value); }
        }
        #endregion

        #region Dependenci Property

        /// <summary><see langword="DependencyProperty"/> для <see cref="Length"/>.</summary>
        public static readonly DependencyProperty LengthProperty = LengthPropertyKey.DependencyProperty;

        /// <summary><see langword="DependencyProperty"/> для <see cref="PageIndex"/>.</summary>
        public static readonly DependencyProperty PageIndexProperty = DependencyProperty.Register(nameof(PageIndex), typeof(int), typeof(CustomItemsControl), new PropertyMetadata(0, PageIndexChanged, CoercePageIndex));

        /// <summary><see langword="DependencyProperty"/> для <see cref="Rows"/>.</summary>
        public static readonly DependencyProperty RowsProperty = DependencyProperty.Register(nameof(Rows), typeof(int), typeof(CustomItemsControl), new PropertyMetadata(0, RowsChanged, CoerceRows));

        /// <summary><see langword="DependencyProperty"/> для <see cref="Columns"/>.</summary>
        public static readonly DependencyProperty ColumnsProperty = DependencyProperty.Register("Columns", typeof(int), typeof(CustomItemsControl), new PropertyMetadata(0, ColumnsChanged, CoerceColumns));


        private static readonly DependencyPropertyKey LengthPropertyKey = DependencyProperty.RegisterReadOnly(nameof(Length), typeof(int), typeof(CustomItemsControl), new PropertyMetadata(0, LengthChanged));

        #endregion

        #region Coerces
        private static object CoerceRows(DependencyObject d, object baseValue)
        {
            if ((baseValue is int rows) && rows > 0)
                return rows;
            return 1;
        }
        private static object CoerceColumns(DependencyObject d, object baseValue)
        {
            if ((baseValue is int columns) && columns > 2)
                return columns;
            return 3;
        }
        private static object CoercePageIndex(DependencyObject d, object baseValue)
        {
            if (!(baseValue is int index) || index < 0)
                return -1;

            // Здесь нужна проверка максимального индекса.
            return 33333333;
        }
        #endregion

        #region Values Changed
        private static void RowsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CustomItemsControl customControl = (CustomItemsControl)d;
            customControl.RenderPage();
        }
        private static void LengthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Переинициализация свойства-коллекции с данными плиток
        }
        private static void PageIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CustomItemsControl customControl = (CustomItemsControl)d;
            customControl.RenderPage();
        }
        private static void ColumnsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CustomItemsControl customControl = (CustomItemsControl)d;

            customControl.RenderPage();
        }
        #endregion

        #region Private Methods
        private void RenderPage()
        {
            // Получаем из Контекста Данных VM по её итерфейсу
            // и заполняем коллекцию данных плиток актуальной страницы
        }
        #endregion
    }

    public class CurrencyItemButton : Button
    {
        public static readonly DependencyProperty SymbolProperty = DependencyProperty.Register(nameof(Symbol), typeof(string), typeof(CurrencyItemButton), new PropertyMetadata(string.Empty));
        public string Symbol { get => (string)GetValue(SymbolProperty); set => SetValue(SymbolProperty, value); }

        public static readonly DependencyProperty SignProperty = DependencyProperty.Register(nameof(Sign), typeof(string), typeof(CurrencyItemButton), new PropertyMetadata(string.Empty));
        public string Sign { get => (string)GetValue(SignProperty); set => SetValue(SignProperty, value); }

        public static readonly DependencyProperty RateProperty = DependencyProperty.Register(nameof(Rate), typeof(string), typeof(CurrencyItemButton), new PropertyMetadata(string.Empty));
        public string Rate { get => (string)GetValue(RateProperty); set => SetValue(RateProperty, value); }

        public static readonly DependencyProperty AvailableProperty = DependencyProperty.Register(nameof(Available), typeof(string), typeof(CurrencyItemButton), new PropertyMetadata(string.Empty));
        public string Available { get => (string)GetValue(AvailableProperty); set => SetValue(AvailableProperty, value); }

        public static readonly DependencyProperty LackProperty = DependencyProperty.Register(nameof(Lack), typeof(string), typeof(CurrencyItemButton), new PropertyMetadata(string.Empty));
        public string Lack { get => (string)GetValue(LackProperty); set => SetValue(LackProperty, value); }

        public static readonly DependencyProperty BaseSignProperty = DependencyProperty.Register(nameof(BaseSign), typeof(string), typeof(CurrencyItemButton), new PropertyMetadata(string.Empty));
        public string BaseSign { get => (string)GetValue(BaseSignProperty); set => SetValue(BaseSignProperty, value); }

        static CurrencyItemButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CurrencyItemButton),
             new FrameworkPropertyMetadata(typeof(CurrencyItemButton)));
        }
    }

    public class RefundAmountItemButton : Button
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(RefundAmountItemButton), new PropertyMetadata(string.Empty));
        public string Text { get => (string)GetValue(TextProperty); set => SetValue(TextProperty, value); }



        static RefundAmountItemButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RefundAmountItemButton),
             new FrameworkPropertyMetadata(typeof(RefundAmountItemButton)));
        }
    }
}
