using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace TilesBox
{
    public partial class TilesPage : ContentControl
    {
        /// <summary>Количество строк плиток.
        /// Не может быть меньше 1.</summary>
        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        /// <summary>DependencyProperty для <see cref="Rows"/>.</summary>
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.RegisterAttached(nameof(Rows), typeof(int), typeof(TilesPage), new PropertyMetadata(0, RowsChanged, CoerceRows));

        private static void RowsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TilesPage tilesPage)
                tilesPage.RowsOrColumnsChanged(e);
            else if (d is Grid grid)
            {
                int rowsNew = (int)e.NewValue;
                int rows = grid.RowDefinitions.Count;
                for (; rows < rowsNew; rows++)
                    grid.RowDefinitions.Add(new RowDefinition());
                for (rows--; rows > rowsNew; rows--)
                    grid.RowDefinitions.RemoveAt(rows);

                //for (int i = 0; i < rows; i++)
                //    grid.RowDefinitions[i].Height = OneStar;
                foreach (RowDefinition rowDefinition in grid.RowDefinitions)
                    rowDefinition.Height = OneStar;
            }
            else
                throw new ArgumentException($"Только для {typeof(TilesPage)} и {typeof(Grid)}.", nameof(d));
        }

        private static object CoerceRows(DependencyObject d, object baseValue)
        {
            int value = (int)baseValue;
            if (d is TilesPage)
            {
                if (value < 1)
                    return 1;
            }
            else if (d is Grid)
            {
                if (value < 0)
                    return 0;
            }
            return baseValue;
        }


        /// <summary>Количество колонок плиток.
        /// Не может быть меньше 3.</summary>
        public int Columns
        {
            get { return (int)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        /// <summary>DependencyProperty для <see cref="Columns"/>.</summary>
        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.RegisterAttached(nameof(Columns), typeof(int), typeof(TilesPage), new PropertyMetadata(0, ColumnsChanged, CoerceColumns));

        /// <summary>Однократный пропорциональный размер.</summary>
        protected static readonly GridLength OneStar = new GridLength(1, GridUnitType.Star);
        private static void ColumnsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TilesPage tilesPage)
                tilesPage.RowsOrColumnsChanged(e);
            else if (d is Grid grid)
            {
                int columnsNew = (int)e.NewValue;
                int columns = grid.ColumnDefinitions.Count;
                for (; columns < columnsNew; columns++)
                    grid.ColumnDefinitions.Add(new ColumnDefinition());
                for (columns--; columns >= columnsNew; columns--)
                    grid.ColumnDefinitions.RemoveAt(columns);

                //for (int i = 0; i < columns; i++)
                //    grid.ColumnDefinitions[i].Width = OneStar;
                foreach (ColumnDefinition columnDefinition in grid.ColumnDefinitions)
                    columnDefinition.Width = OneStar;
            }
            else
                throw new ArgumentException($"Только для {typeof(TilesPage)} и {typeof(Grid)}.", nameof(d));
        }

        private static object CoerceColumns(DependencyObject d, object baseValue)
        {
            int value = (int)baseValue;
            if (d is TilesPage)
            {
                if (value < 3)
                    return 3;
            }
            else if (d is Grid)
            {
                if (value < 0)
                    return 0;
            }
            return baseValue;
        }



        /// <summary>
        /// Общее количество плиток на странице
        /// </summary>
        public int TilesCount
        {
            get => tilesCount;
            protected set { SetValue(TilesCountProperty, value); }
        }

        private int tilesCount;

        /// <summary><see cref="DependencyPropertyKey"/> для свойства <see cref="TilesCount"/>.</summary>
        protected static readonly DependencyPropertyKey TilesCountPropertyKey =
             DependencyProperty.RegisterReadOnly(nameof(TilesCount), typeof(int), typeof(TilesPage), new PropertyMetadata(0, (d, e) => ((TilesPage)d).tilesCount = (int) e.NewValue));
        /// <summary><see cref="DependencyProperty"/> для свойства <see cref="TilesCount"/>.</summary>
        public static readonly DependencyProperty TilesCountProperty = TilesCountPropertyKey.DependencyProperty;

        private int rows, columns;
        private void RowsOrColumnsChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == RowsProperty)
                rows = (int)e.NewValue;
            else if (e.Property == ColumnsProperty)
                columns = (int)e.NewValue;


            if (!IsInitialized)
                return;

            int tilesCount = rows * columns;

            tiles2d = new TilesPageItem[rows, columns];
            int index = 0;
            TilesPageItem currentTile;
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    if (index < tiles.Count)
                    {
                        currentTile = tiles[index];
                    }
                    else
                    {
                        currentTile = new TilesPageItem();
                        currentTile.SetBinding(ContentProperty, bindingDefault);
                        currentTile.SetBinding(StyleProperty, bindingTileStyle);
                        currentTile.SetBinding(ContentTemplateProperty, bindingItemTileTemplate);
                        tiles.Add(currentTile);
                    }
                    currentTile.Row = row;
                    currentTile.Column = column;
                    tiles2d[row, column] = currentTile;
                    index++;
                }
            }

            for (int i = tiles.Count - 1; i >= index; i--)
            {
                tiles[i].ClearAllValues();
                tiles.RemoveAt(i);
            }
            TilesCount = tilesCount;
            RenderTilesBinding();
        }



        /// <summary>Возвращает значение присоединённого свойства Rows для <paramref name="grid"/>.</summary>
        /// <param name="grid"><see cref="Grid"/> значение свойства которого будет возвращено.</param>
        /// <returns><see cref="int"/> значение свойства.</returns>
        public static int GetRows(Grid grid)
        {
            return (int)grid.GetValue(RowsProperty);
        }

        /// <summary>Задаёт значение присоединённого свойства Rows для <paramref name="grid"/>.</summary>
        /// <param name="grid"><see cref="Grid"/> значение свойства которого будет возвращено.</param>
        /// <param name="value"><see cref="int"/> значение для свойства.</param>
        public static void SetRows(Grid grid, int value)
        {
            grid.SetValue(RowsProperty, value);
        }



        /// <summary>Возвращает значение присоединённого свойства Columns для <paramref name="grid"/>.</summary>
        /// <param name="grid"><see cref="Grid"/> значение свойства которого будет возвращено.</param>
        /// <returns><see cref="int"/> значение свойства.</returns>
        public static int GetColumns(Grid grid)
        {
            return (int)grid.GetValue(ColumnsProperty);
        }

        /// <summary>Задаёт значение присоединённого свойства Columns для <paramref name="grid"/>.</summary>
        /// <param name="grid"><see cref="Grid"/> значение свойства которого будет возвращено.</param>
        /// <param name="value"><see cref="int"/> значение для свойства.</param>
        public static void SetColumns(Grid grid, int value)
        {
            grid.SetValue(ColumnsProperty, value);
        }
    }
    public static class ExtensionMethods
    {
        public static void ClearAllValues(this DependencyObject depobj)
        {
            if (depobj == null)
                throw new ArgumentNullException(nameof(depobj));
            LocalValueEnumerator allValue = depobj.GetLocalValueEnumerator();

            while (allValue.MoveNext())
            {
                DependencyProperty property = allValue.Current.Property;
                if (!property.ReadOnly)
                    depobj.ClearValue(property);
            }
        }
    }

    //public static class GridAP
    //{


    //    /// <summary>Возвращает значение присоединённого свойства Rows для <paramref name="grid"/>.</summary>
    //    /// <param name="grid"><see cref="Grid"/> значение свойства которого будет возвращено.</param>
    //    /// <returns><see cref="int"/> значение свойства.</returns>
    //    public static int GetRows(Grid grid)
    //    {
    //        return (int)grid.GetValue(RowsProperty);
    //    }

    //    /// <summary>Задаёт значение присоединённого свойства Rows для <paramref name="grid"/>.</summary>
    //    /// <param name="grid"><see cref="Grid"/> значение свойства которого будет возвращено.</param>
    //    /// <param name="value"><see cref="int"/> значение для свойства.</param>
    //    public static void SetRows(Grid grid, int value)
    //    {
    //        grid.SetValue(RowsProperty, value);
    //    }

    //    /// <summary><see cref="DependencyProperty"/> для методов <see cref="GetRows(Grid)"/> и <see cref="SetRows(Grid, int)"/>.</summary>
    //    public static readonly DependencyProperty RowsProperty =
    //        DependencyProperty.RegisterAttached(nameof(GetRows).Substring(3), typeof(int), typeof(GridAP), new PropertyMetadata(1, RowsChanged, CoerceRowsOrColumns));

    //    private static object CoerceRowsOrColumns(DependencyObject d, object baseValue)
    //    {
    //        int value = (int)baseValue;
    //        if (value < 0)
    //            return 0;
    //        return baseValue;
    //    }

    //    private static void RowsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    //    {
    //        if (!(d is Grid grid))
    //            throw new ArgumentException($"Только для {typeof(Grid)}.", nameof(d));

    //        int rowsNew = (int)e.NewValue;
    //        int rows = grid.RowDefinitions.Count;
    //        for (; rows < rowsNew; rows++)
    //            grid.RowDefinitions.Add(new RowDefinition());
    //        for (; rows > rowsNew; rows--)
    //            grid.RowDefinitions.RemoveAt(rows - 1);

    //        for (int i = 0; i < rows; i++)
    //            grid.RowDefinitions[i].Height = new GridLength(1, GridUnitType.Star);

    //    }



    //    /// <summary>Возвращает значение присоединённого свойства Columns для <paramref name="grid"/>.</summary>
    //    /// <param name="grid"><see cref="Grid"/> значение свойства которого будет возвращено.</param>
    //    /// <returns><see cref="int"/> значение свойства.</returns>
    //    public static int GetColumns(Grid grid)
    //    {
    //        return (int)grid.GetValue(ColumnsProperty);
    //    }

    //    /// <summary>Задаёт значение присоединённого свойства Columns для <paramref name="grid"/>.</summary>
    //    /// <param name="grid"><see cref="Grid"/> значение свойства которого будет возвращено.</param>
    //    /// <param name="value"><see cref="int"/> значение для свойства.</param>
    //    public static void SetColumns(Grid grid, int value)
    //    {
    //        grid.SetValue(ColumnsProperty, value);
    //    }

    //    /// <summary><see cref="DependencyProperty"/> для методов <see cref="GetColumns(Grid)"/> и <see cref="SetColumns(Grid, int)"/>.</summary>
    //    public static readonly DependencyProperty ColumnsProperty =
    //        DependencyProperty.RegisterAttached(nameof(GetColumns).Substring(3), typeof(int), typeof(GridAP), new PropertyMetadata(1, ColumnsChanged, CoerceRowsOrColumns));

    //    private static void ColumnsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    //    {
    //        if (!(d is Grid grid))
    //            throw new ArgumentException($"Только для {typeof(Grid)}.", nameof(d));

    //        int columnsNew = (int)e.NewValue;
    //        int columns = grid.ColumnDefinitions.Count;
    //        for (; columns < columnsNew; columns++)
    //            grid.ColumnDefinitions.Add(new ColumnDefinition());
    //        for (; columns > columnsNew; columns--)
    //            grid.ColumnDefinitions.RemoveAt(columns - 1);

    //        for (int i = 0; i < columns; i++)
    //            grid.ColumnDefinitions[i].Width = new GridLength(1, GridUnitType.Star);
    //    }
    //}
}
