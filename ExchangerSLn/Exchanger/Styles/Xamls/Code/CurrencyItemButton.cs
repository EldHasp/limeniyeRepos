using System.Windows;
using System.Windows.Controls;

namespace Exchanger.Styles.Xamls.Code
{
    public class CurrencyItemButton : Button
    {
        public static readonly DependencyProperty SymbolProperty = DependencyProperty.Register(nameof(Symbol), typeof(string), typeof(CurrencyItemButton), new PropertyMetadata(string.Empty));
        public string Symbol { get => (string)GetValue(SymbolProperty); set => SetValue(SymbolProperty, value); }

        public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(nameof(Type), typeof(string), typeof(CurrencyItemButton), new PropertyMetadata(string.Empty));
        public string Type { get => (string)GetValue(TypeProperty); set => SetValue(TypeProperty, value); }

        public static readonly DependencyProperty RateWithSymbolProperty = DependencyProperty.Register(nameof(RateWithSymbol), typeof(string), typeof(CurrencyItemButton), new PropertyMetadata(string.Empty));
        public string RateWithSymbol { get => (string)GetValue(RateWithSymbolProperty); set => SetValue(RateWithSymbolProperty, value); }


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
