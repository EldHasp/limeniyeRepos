using Common.Enums;

namespace Common.EventsArgs
{

    /// <summary>Аргументы события изменения словаря.</summary>
    /// <typeparam name="TKey">Тип ключа словаря.</typeparam>
    /// <typeparam name="TValue">Тип значения словаря.</typeparam>
    public class NotifyDictionaryChangedEventArgs<TKey, TValue> : NotifyActionDictionaryChangedEventArgs
    {

        /// <summary>Ключ в отношении которого произведено действие.</summary>
        public TKey Key { get; }

        /// <summary>Удаляемое или измененое значение.</summary>
        public TValue OldValue { get; }

        /// <summary>Добавленное или новое значение.</summary>
        public TValue NewValue { get; }

        public NotifyDictionaryChangedEventArgs(ActionListEnum action, TKey key, TValue oldValue, TValue newValue)
            : base(action)
        {
            Key = key;
            OldValue = oldValue;
            NewValue = newValue;
        }

    }
}
