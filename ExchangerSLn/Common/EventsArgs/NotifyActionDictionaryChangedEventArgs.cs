using Common.Enums;
using System;
namespace Common.EventsArgs
{
    /// <summary>Аргументы события изменения словаря. 
    /// Содержит только действие и метод создания экземпляров производных классов.</summary>
    public class NotifyActionDictionaryChangedEventArgs : EventArgs
    {
        /// <summary>Действие изменившее словарь.</summary>
        public ActionListEnum Action { get; }

        /// <summary>Создаёт экземпляр.</summary>
        /// <param name="action">Действие изменившее словарь.</param>
        public NotifyActionDictionaryChangedEventArgs(ActionListEnum action)
        {
            Action = action;
        }

        public static NotifyDictionaryChangedEventArgs<TKey, TValue> AddKey<TKey, TValue>(TKey key, TValue value)
           => new NotifyDictionaryChangedEventArgs<TKey, TValue>(ActionListEnum.Added, key, default, value);
        public static NotifyDictionaryChangedEventArgs<TKey, TValue> RemoveKey<TKey, TValue>(TKey key, TValue value)
            => new NotifyDictionaryChangedEventArgs<TKey, TValue>(ActionListEnum.Removed, key, value, default);
        public static NotifyDictionaryChangedEventArgs<TKey, TValue> ChangedValue<TKey, TValue>(TKey key, TValue oldValue, TValue newValue)
            => new NotifyDictionaryChangedEventArgs<TKey, TValue>(ActionListEnum.Changed, key, oldValue, newValue);
    }

}
