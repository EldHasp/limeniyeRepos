using Common.Interfaces.ViewModel;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace CustomControls.Code
{
    public class ChocolateBarCustomControl : ContentControl
    {
        #region Public Properties
        /// <summary>Количество строк плиток.
        /// Минимальное значение - одна.</summary>
        public int Rows { get { return (int)GetValue(RowsProperty); } set { SetValue(RowsProperty, value); } }

        /// <summary>Количество колонок плиток.
        /// Минимальное значение - три.</summary>
        public int Columns { get { return (int)GetValue(ColumnsProperty); } set { SetValue(ColumnsProperty, value); } }

        /// <summary>Количество элемнтов одной страницы.</summary>
        public int Length { get { return (int)GetValue(LengthProperty); } private set { SetValue(LengthPropertyKey, value); } }

        /// <summary>Номер (индекс) страницы.</summary>
        public int PageIndex { get { return (int)GetValue(PageIndexProperty); } set { SetValue(PageIndexProperty, value); } }

        /// <summary> Все элементы полученные из вне. </summary>
        public List<object> AllInputElements { private get => (List<object>)GetValue(AllElementsProperty); set { SetValue(AllElementsPropertyKey, value); } }

        /// <summary> Элементы текущей страницы с индексом <see cref="PageIndex"/>. </summary>
        public IReadOnlyList<object> CurrentPage { get => (List<object>)GetValue(CurrentPageProperty); private set { SetValue(CurrentPagePropertyKey, value); } }
        #endregion

        #region DependencyProperty
        public static readonly DependencyProperty LengthProperty = LengthPropertyKey.DependencyProperty;

        public static readonly DependencyProperty PageIndexProperty = DependencyProperty.Register(nameof(PageIndex), typeof(int), typeof(ChocolateBarCustomControl), new PropertyMetadata(0, PageIndexChanged, CoercePageIndex));

        public static readonly DependencyProperty RowsProperty = DependencyProperty.Register(nameof(Rows), typeof(int), typeof(ChocolateBarCustomControl), new PropertyMetadata(0, RowsChanged, CoerceRows));

        public static readonly DependencyProperty ColumnsProperty = DependencyProperty.Register(nameof(Columns), typeof(int), typeof(ChocolateBarCustomControl), new PropertyMetadata(0, ColumnsChanged, CoerceColumns));


        /// <summary> Входной список элементов. </summary>
        public static readonly DependencyProperty AllElementsProperty = AllElementsPropertyKey.DependencyProperty;

        /// <summary> Страница с индексом <see cref="PageIndex"/>. </summary>
        public static readonly DependencyProperty CurrentPageProperty = CurrentPagePropertyKey.DependencyProperty;
        #endregion

        #region DependencyPropertyKey
        private static readonly DependencyPropertyKey LengthPropertyKey = DependencyProperty.RegisterReadOnly(nameof(Length),
            typeof(int), typeof(ChocolateBarCustomControl), new PropertyMetadata(0, LengthChanged));

        /// <summary> Список текущей страницы с индексом <see cref="PageIndex"/> </summary>
        private static readonly DependencyPropertyKey CurrentPagePropertyKey = DependencyProperty.RegisterReadOnly(nameof(AllInputElements), 
            typeof(List<ICellViewModel>), typeof(ChocolateBarCustomControl), new FrameworkPropertyMetadata(new List<ICellViewModel>()));

        /// <summary> Список текущей страницы с индексом <see cref="PageIndex"/> </summary>
        private static readonly DependencyPropertyKey AllElementsPropertyKey = DependencyProperty.RegisterReadOnly(nameof(AllInputElements),
            typeof(List<object>), typeof(ChocolateBarCustomControl), new FrameworkPropertyMetadata(new List<object>()));
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
            ChocolateBarCustomControl customControl = (ChocolateBarCustomControl)d;
            customControl.RenderPage();
        }
        private static void LengthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Переинициализация свойства-коллекции с данными плиток
            ChocolateBarCustomControl customControl = (ChocolateBarCustomControl)d;
            customControl.Length = (int)e.NewValue;
        }
        private static void PageIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ChocolateBarCustomControl customControl = (ChocolateBarCustomControl)d;
            customControl.RenderPage();
        }
        private static void ColumnsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ChocolateBarCustomControl customControl = (ChocolateBarCustomControl)d;

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
}
