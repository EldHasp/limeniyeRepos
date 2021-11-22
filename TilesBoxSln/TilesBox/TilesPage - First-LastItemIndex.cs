using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace TilesBox
{
    public partial class TilesPage : ContentControl
    {
        private int firstItemIndex = -1, lastItemIndex = -1;

        /// <summary>
        /// Индекс первого элемента на странице
        /// </summary>
        public int FirstItemIndex
        {
            get { return (int)GetValue(FirstItemIndexProperty); }
            protected set { SetValue(FirstItemIndexPropertyKey, value); }
        }

        /// <summary><see cref="DependencyPropertyKey"/> для свойства <see cref="FirstItemIndex"/>.</summary>
        protected static readonly DependencyPropertyKey FirstItemIndexPropertyKey =
               DependencyProperty.RegisterReadOnly(nameof(FirstItemIndex), typeof(int), typeof(TilesPage), new PropertyMetadata(-1, FirstOrLastItemChanged));


        /// <summary><see cref="DependencyProperty"/> для свойства <see cref="FirstItemIndex"/>.</summary>
        public static readonly DependencyProperty FirstItemIndexProperty = FirstItemIndexPropertyKey.DependencyProperty;
        protected static void FirstOrLastItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TilesPage tilePage = (TilesPage)d;
            if (e.Property == FirstItemIndexProperty)
                tilePage.firstItemIndex = (int)e.NewValue;
            else if (e.Property == LastItemIndexProperty)
                tilePage.lastItemIndex = (int)e.NewValue;
        }




        /// <summary>
        /// Индекс последнего элемента на странице
        /// </summary>
        public int LastItemIndex
        {
            get { return (int)GetValue(LastItemIndexProperty); }
            protected set { SetValue(LastItemIndexPropertyKey, value); }
        }

        /// <summary><see cref="DependencyPropertyKey"/> для свойства <see cref="LastItemIndex"/>.</summary>
        protected static readonly DependencyPropertyKey LastItemIndexPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(LastItemIndex), typeof(int), typeof(TilesPage), new PropertyMetadata(-1));
        /// <summary><see cref="DependencyProperty"/> для свойства <see cref="LastItemIndex"/>.</summary>
        public static readonly DependencyProperty LastItemIndexProperty = LastItemIndexPropertyKey.DependencyProperty;



    }
}
