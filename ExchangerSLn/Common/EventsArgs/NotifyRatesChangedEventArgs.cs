using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.EventsArgs
{

    /// <summary> </summary>
    public class NotifyCollectionChangedEventArgs<T> : NotifyCollectionChangedEventArgs
    {
        public IReadOnlyCollection<T> OldItems { get; }
        public IReadOnlyCollection<T> NewItems { get; }

        private NotifyCollectionChangedEventArgs(ActionListEnum action, IEnumerable<T> oldItems, IEnumerable<T> newItems)
            : base(action)
        {
            if (oldItems == null)
            {
                OldItems = null;
            }
            else
            {
                OldItems = Array.AsReadOnly(oldItems.ToArray());
            }

            if (newItems == null)
            {
                NewItems = null;
            }
            else
            {
                newItems = Array.AsReadOnly(newItems.ToArray());
            }
        }

        public static NotifyCollectionChangedEventArgs<T> Added(IEnumerable<T> newItems)
           => new NotifyCollectionChangedEventArgs<T>(ActionListEnum.Added, null, newItems);

        public static NotifyCollectionChangedEventArgs<T> Removed(IEnumerable<T> oldItems)
           => new NotifyCollectionChangedEventArgs<T>(ActionListEnum.Removed, oldItems, null);

        public static NotifyCollectionChangedEventArgs<T> Changed(IEnumerable<T> newItems, IEnumerable<T> oldItems)
           => new NotifyCollectionChangedEventArgs<T>(ActionListEnum.Changed, oldItems, newItems);


        private static readonly NotifyCollectionChangedEventArgs<T> clearAction
            = new NotifyCollectionChangedEventArgs<T>(ActionListEnum.Clear, null, null);


        public static NotifyCollectionChangedEventArgs<T> Clear() => clearAction;

    }

}
