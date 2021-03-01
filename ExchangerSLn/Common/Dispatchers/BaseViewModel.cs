using Simplified;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;

namespace Common.Dispatchers
{
    /// <summary>Базовый класс для ViewModel.</summary>
    public abstract class BaseViewModel : BaseInpc, IDispatcher
    {
        public Dispatcher Dispatcher { get; }

        /// <summary>ViewModel создана в режииме Разработке.</summary>
        public bool IsDisignedMode { get; }

        /// <summary>Создаёт экземпляр ViewModel.
        /// Значение для <see cref="Dispatcher"/> получается из Application.Current.Dispatcher.
        /// Значение для <see cref="IsDisignedMode"/> определяется методом <see cref="DesignerProperties.GetIsInDesignMode(DependencyObject)"/>.</summary>
        public BaseViewModel()
            : this(Application.Current.Dispatcher)
        { }

        /// <summary>Создаёт экземпляр ViewModel.
        /// Значение для <see cref="IsDisignedMode"/> определяется методом <see cref="DesignerProperties.GetIsInDesignMode(DependencyObject)"/>.</summary>
        /// <param name="dispatcher">Диспетчер для экземпляра. Не может быть <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException">Если <paramref name="dispatcher"/> = <see langword="null"/>.</exception>
        public BaseViewModel(Dispatcher dispatcher)
            : this(dispatcher, DesignerProperties.GetIsInDesignMode(new DependencyObject()))
        { }

        /// <summary>Создаёт экземпляр ViewModel.
        /// Значение для <see cref="Dispatcher"/> получается из Application.Current.Dispatcher.</summary>
        /// <param name="isDisignedMode">Задаёт режим Разработки.</param>
        public BaseViewModel(bool isDisignedMode)
               : this(Application.Current.Dispatcher, isDisignedMode)
        { }

        /// <summary>Создаёт экземпляр ViewModel.</summary>
        /// <param name="dispatcher">Диспетчер для экземпляра. Не может быть <see langword="null"/>.</param>
        /// <param name="isDisignedMode">Задаёт режим Разработки.</param>
        /// <exception cref="ArgumentNullException">Если <paramref name="dispatcher"/> = <see langword="null"/>.</exception>
        public BaseViewModel(Dispatcher dispatcher, bool isDisignedMode)
        {
            Dispatcher = dispatcher ?? throw new ArgumentNullException("Диспетчер не может быть null.", nameof(Dispatcher));
            IsDisignedMode = isDisignedMode;
        }
    }

}
