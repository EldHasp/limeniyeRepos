using Common.Enums;
using System;
using System.Collections.Generic;

namespace Common.EventsArgs
{
    /// <summary> </summary>
    public class NotifyCollectionChangedEventArgs : EventArgs
    {

        public ActionListEnum Action { get; }

        protected NotifyCollectionChangedEventArgs(ActionListEnum action)
        {
            Action = action;
        }

        public static NotifyCollectionChangedEventArgs<T> Added<T>(IEnumerable<T> newItems)
           => NotifyCollectionChangedEventArgs<T>.Added(newItems);

        public static NotifyCollectionChangedEventArgs<T> Changed<T>(IEnumerable<T> newItems, IEnumerable<T> oldItems)
           => NotifyCollectionChangedEventArgs<T>.Changed(oldItems, newItems);


        public static NotifyCollectionChangedEventArgs<T> Clear<T>() => NotifyCollectionChangedEventArgs<T>.Clear();

    }
}
