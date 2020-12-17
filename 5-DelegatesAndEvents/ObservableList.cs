namespace DelegatesAndEvents
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <inheritdoc cref="IObservableList{T}" />
    public class ObservableList<TItem> : IObservableList<TItem>
    {
        private readonly IList<TItem> items = new List<TItem>();
        private readonly bool readOnly;

        /// <inheritdoc cref="IObservableList{T}.ElementInserted" />
        public event ListChangeCallback<TItem> ElementInserted;

        /// <inheritdoc cref="IObservableList{T}.ElementRemoved" />
        public event ListChangeCallback<TItem> ElementRemoved;

        /// <inheritdoc cref="IObservableList{T}.ElementChanged" />
        public event ListElementChangeCallback<TItem> ElementChanged;

        /// <inheritdoc cref="ICollection{T}.Count" />
        public int Count => items.Count;

        /// <inheritdoc cref="ICollection{T}.IsReadOnly" />
        public bool IsReadOnly => this.items.IsReadOnly;

        /// <inheritdoc cref="IList{T}.this" />
        public TItem this[int index]
        {
            get => items[index];
            set
            {
                var old = items[index];
                items[index] = value;
                ElementChanged?.Invoke(this, value, old, index);
            }
        }

        /// <inheritdoc cref="IEnumerable{T}.GetEnumerator" />
        public IEnumerator<TItem> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        /// <inheritdoc cref="IEnumerable.GetEnumerator" />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <inheritdoc cref="ICollection{T}.Add" />
        public void Add(TItem item)
        {
            items.Add(item);
            ElementInserted?.Invoke(this, item, items.IndexOf(item));
        }

        /// <inheritdoc cref="ICollection{T}.Clear" />
        public void Clear()
        {
            for (int i = 0; i < items.Count(); i++)
            {
                ElementRemoved?.Invoke(this, items[i], i);
            }
            items.Clear();
        }

        /// <inheritdoc cref="ICollection{T}.Contains" />
        public bool Contains(TItem item) => items.Contains(item);

        /// <inheritdoc cref="ICollection{T}.CopyTo" />
        public void CopyTo(TItem[] array, int arrayIndex)
        {
            items.ToArray().CopyTo(array, arrayIndex);
        }

        /// <inheritdoc cref="ICollection{T}.Remove" />
        public bool Remove(TItem item)
        {
            ElementRemoved?.Invoke(this, item, items.IndexOf(item));
            return items.Remove(item);
        }

        /// <inheritdoc cref="IList{T}.IndexOf" />
        public int IndexOf(TItem item) => items.IndexOf(item);

        /// <inheritdoc cref="IList{T}.RemoveAt" />
        public void Insert(int index, TItem item)
        {
            List<TItem> copy = new List<TItem>(items);
            items.Insert(index, item);
            ElementInserted?.Invoke(this, item, index);
        }

        /// <inheritdoc cref="IList{T}.RemoveAt" />
        public void RemoveAt(int index)
        {
            ElementRemoved?.Invoke(this, items[index], index);
            items.RemoveAt(index);
        }

        public override bool Equals(object obj)
        {
            return obj is ObservableList<TItem> list &&
                   EqualityComparer<IList<TItem>>.Default.Equals(items, list.items) &&
                   readOnly == list.readOnly;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(items, readOnly);
        }
    }
}
