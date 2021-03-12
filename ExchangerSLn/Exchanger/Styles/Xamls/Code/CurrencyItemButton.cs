using Common.Interfaces.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Exchanger.Styles.Xamls.Code
{
    /// <summary>Creating CommandBinding on Received RoutedCommand and ICommand</summary>
    public partial class RoutedCommandBinding : Freezable
    {
        #region Property Declaration
        /// <summary>Binding for an popup RoutedCommand</summary>
        public RoutedCommand RoutedCommand
        {
            get { return (RoutedCommand)GetValue(RoutedCommandProperty); }
            set { SetValue(RoutedCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RoutedCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RoutedCommandProperty =
            DependencyProperty.Register(nameof(RoutedCommand), typeof(RoutedCommand), typeof(RoutedCommandBinding),
                new PropertyMetadata(null, RoutedCommandChanged));

        /// <summary>Binding for an executable ICommand</summary>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(RoutedCommandBinding),
                new PropertyMetadata(null, CommandChanged));

        /// <summary>Binding for an Handled completion</summary>
        public bool Handled
        {
            get { return (bool)GetValue(HandledProperty); }
            set { SetValue(HandledProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Handled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HandledProperty =
            DependencyProperty.Register(nameof(Handled), typeof(bool), typeof(RoutedCommandBinding), new PropertyMetadata(true));

        /// <summary>Customized Instance CommandBinding</summary>
        public CommandBinding CommandBinding { get; }
        public static RoutedCommand EmptyCommand { get; } = new RoutedCommand("Empty", typeof(RoutedCommandBinding));

        public RoutedCommandBinding()
        {
            CommandBinding = new CommandBinding(EmptyCommand, ExecuteRoutedMethod, CanExecuteRoutedMethod);
        }

        protected override Freezable CreateInstanceCore()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }

    public partial class RoutedCommandBinding
    {
        #region Methods Declaration

        /// <summary>Default CanExecute Method.</summary>
        /// <param name="parameter">Command Parameter.</param>
        /// <returns>Always <see langword="true"/>.</returns>
        public static bool CanExecuteDefault(object parameter) => true;

        /// <summary>Default Execute Method.</summary>
        /// <param name="parameter">Command Parameter.</param>
        /// <remarks>Empty body.</remarks>
        public static void ExecuteDefault(object parameter) { }

        /// <summary>Delegate for CanExecute.</summary>
        protected Func<object, bool> canExecuteDelegate = CanExecuteDefault;

        /// <summary>Delegate for Execute.</summary>
        protected Action<object> executeDelegate = ExecuteDefault;

        /// <summary>Method for CommandBinding.CanExecuteRouted.</summary>
        /// <param name="sender">The command target that is invoking the handler.</param>
        /// <param name="e">The event data.</param>
        protected virtual void CanExecuteRoutedMethod(object sender, CanExecuteRoutedEventArgs e)
        {
            e.Handled = Handled;
            e.CanExecute = canExecuteDelegate(e.Parameter);
        }

        /// <summary>Method for CommandBinding.ExecuteRouted.</summary>
        /// <param name="sender">The command target that is invoking the handler.</param>
        /// <param name="e">The event data.</param>
        protected virtual void ExecuteRoutedMethod(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = Handled;
            executeDelegate(e.Parameter);
        }
        #endregion

        #region Callback methods Declaration

        /// <summary>Static Callback Method When Changing RoutedCommand Value</summary>
        /// <param name="d">The object in which the value has changed</param>
        /// <param name="e">Change parameters</param>
        private static void RoutedCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => ((RoutedCommandBinding)d).CommandBinding.Command = (RoutedCommand)e.NewValue;

        /// <summary>Static Callback Method When Changing Command Value</summary>
        /// <param name="d">The object in which the value has changed</param>
        /// <param name="e">Change parameters</param>
        private static void CommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RoutedCommandBinding dd = (RoutedCommandBinding)d;
            if (e.NewValue is ICommand newCommand)
            {
                dd.canExecuteDelegate = newCommand.CanExecute;
                dd.executeDelegate = newCommand.Execute;
            }
            else
            {
                dd.canExecuteDelegate = CanExecuteDefault;
                dd.executeDelegate = ExecuteDefault;
            }
        }
        #endregion
    }
    /// <summary>Collection for RoutedCommandBinding</summary>
    public class RoutedCommandBindingCollection : FreezableCollection<RoutedCommandBinding>
    {
        /// <summary>Linked CommandBindingCollection</summary>
        public CommandBindingCollection CommandBindingCollection { get; }

        /// <summary>The only constructor</summary>
        /// <param name="commandBindingCollection">Linked CommandBindingCollection</param>
        /// <exception cref="commandBindingCollection">Thrown when null</exception>
        public RoutedCommandBindingCollection(CommandBindingCollection commandBindingCollection)
        {
            CommandBindingCollection = commandBindingCollection ?? throw new ArgumentNullException(nameof(commandBindingCollection));
            INotifyCollectionChanged notifyCollection = this;
            notifyCollection.CollectionChanged += CollectionChanged;
        }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems?.Count > 0)
                foreach (RoutedCommandBinding commandBinding in e.OldItems)
                    CommandBindingCollection.Remove(commandBinding.CommandBinding);

            if (e.NewItems?.Count > 0)
                foreach (RoutedCommandBinding commandBinding in e.NewItems)
                    CommandBindingCollection.Add(commandBinding.CommandBinding);
        }

    }
    /// <summary>Class with Attachable Property for bound RoutedCommands</summary>
    public class RoutedCommandBindings : Freezable
    {
        /// <summary>Getting the RoutedCommand Collection</summary>
        /// <param name="obj">The object to which the property is attached</param>
        /// <returns>RoutedCommandBinding Collection</returns>
        /// <exception cref="obj">Thrown when not a UIElement or ContentElement</exception>
        public static RoutedCommandBindingCollection GetRoutedCommandBindings(DependencyObject obj)
        {
            RoutedCommandBindingCollection routedCollection = (RoutedCommandBindingCollection)obj.GetValue(RoutedCommandBindingCollectionProperty);
            if (routedCollection == null)
            {
                CommandBindingCollection commandCollection;
                if (obj is UIElement element)
                    commandCollection = element.CommandBindings;
                else if (obj is ContentElement content)
                    commandCollection = content.CommandBindings;
                else
                    throw new ArgumentException("There must be an UIElement or ContentElement", nameof(obj));

                obj.SetValue(RoutedCommandBindingCollectionProperty, routedCollection = new RoutedCommandBindingCollection(commandCollection));
            }

            return routedCollection;
        }

        protected override Freezable CreateInstanceCore()
        {
            throw new NotImplementedException();
        }

        // Using a DependencyProperty as the backing store for RoutedCommandBindingCollection.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RoutedCommandBindingCollectionProperty =
            DependencyProperty.RegisterAttached("ShadowRoutedCommandBindings", typeof(RoutedCommandBindingCollection), typeof(RoutedCommandBindings), new PropertyMetadata(null));

    }


    public class CustomItemsControl : ContentControl
    {
        /// <summary> Максимальное количество элементов на одной странице. </summary>
        private readonly string elementsMaxCount;

        #region DependencyProperties 
        public static readonly DependencyProperty RowsProperty = DependencyProperty.Register(nameof(Rows), typeof(int), typeof(CustomItemsControl), new PropertyMetadata(default(int)));
        //public static readonly DependencyProperty ColumnsProperty = DependencyProperty.Register(nameof(Columns), typeof(int), typeof(CustomItemsControl),
            //new FrameworkPropertyMetadata(default(int), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
            //new PropertyChangedCallback(TextProperty_PropertyChanged)));

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private static readonly DependencyPropertyKey AllStandartElementsPropertyKey = DependencyProperty.RegisterReadOnly(nameof(AllStandartElements),
           typeof(List<string>), typeof(CustomItemsControl),
           new FrameworkPropertyMetadata(OnAInputItemsChanged) { BindsTwoWayByDefault = true });

        public static readonly DependencyProperty AllStandartElementsProperty = AllStandartElementsPropertyKey.DependencyProperty;
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private static readonly DependencyPropertyKey CurrentIndexPagePropertyKey = DependencyProperty.RegisterReadOnly(nameof(CurrentIndexPage),
            typeof(int), typeof(CustomItemsControl),
            new FrameworkPropertyMetadata(default(int),FrameworkPropertyMetadataOptions.None));
        /// <summary> DependencyProperty индекса текущей страницы. </summary>
        public static readonly DependencyProperty CurrentIndexPageProperty = CurrentIndexPagePropertyKey.DependencyProperty;
        #endregion

        /// <summary> Количество строк </summary>
        public int Rows { get => (int)GetValue(RowsProperty); set => SetValue(RowsProperty, value); }
        /// <summary> Количество колонок </summary>
        //public int Columns { get => (int)GetValue(ColumnsProperty); set => SetValue(ColumnsProperty, value); }
        /// <summary> Свойство для передачи списка элементов. </summary>
        public IReadOnlyList<ICellViewModel> AllStandartElements
        {
            get { return (List<ICellViewModel>)this.GetValue(AllStandartElementsProperty); }
            set { this.SetValue(AllStandartElementsProperty, value); }
        }
        /// <summary> Свойство для передачи текущей страницы. </summary>
        public int CurrentIndexPage{get => (int)GetValue(CurrentIndexPageProperty); protected set { SetValue(CurrentIndexPagePropertyKey, value); }}

        public static RoutedCommand Next { get; } = new RoutedUICommand("Next", nameof(Next), typeof(CustomItemsControl));


        private void TextProperty_PropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
           // Columns = (int)e.NewValue;
        }

        public static void OnAInputItemsChanged(
           DependencyObject sender,
           DependencyPropertyChangedEventArgs e)
        {
            // Breakpoint here to see if the new value is being set
            var newValue = e.NewValue;
            Debugger.Break();
        }

        static CustomItemsControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CurrencyItemButton),
             new FrameworkPropertyMetadata(typeof(CurrencyItemButton)));
        }
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
