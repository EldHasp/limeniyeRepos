using System.Windows;
using System.Windows.Controls;

namespace CustomControls.Code
{
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
