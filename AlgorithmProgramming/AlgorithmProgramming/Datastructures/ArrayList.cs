using System.Collections;
using System.Collections.Specialized;

namespace AlgorithmProgramming.Datastructures
{
    public class ArrayList : ICloneable, IList, INotifyCollectionChanged
    {
        private object?[] items = new object?[0];

        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        public bool IsFixedSize => false;

        public bool IsReadOnly => false;

        public int Count { get; private set; }

        public bool IsSynchronized => false;

        public object SyncRoot => this;

        /// <summary>
        /// Gets or sets the element at the specified index
        /// </summary>
        /// <param name="index">Index to get or set</param>
        /// <returns>The object at the specified index</returns>
        /// <exception cref="ArgumentOutOfRangeException">Index out of range</exception>
        public object? this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                    throw new ArgumentOutOfRangeException(nameof(index));
                return items[index];
            }
            set
            {
                if (index < 0 || index >= Count)
                    throw new ArgumentOutOfRangeException(nameof(index));
                items[index] = value;
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        /// <summary>
        /// Adds an object to the end of the ArrayList
        /// </summary>
        /// <param name="value">The object to be added</param>
        /// <returns>The position into which the new element was inserted</returns>
        public int Add(object? value)
        {
            EnsureCapacity(Count + 1);
            items[Count] = value;
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, value));
            return Count++;
        }

        /// <summary>
        /// Removes all elements from the ArrayList
        /// </summary>
        public void Clear()
        {
            if (Count > 0)
            {
                Array.Clear(items, 0, Count);
                Count = 0;
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        /// <summary>
        /// Creates a copy of the ArrayList
        /// </summary>
        /// <returns>Copy of the ArrayList</returns>
        public object Clone()
        {
            ArrayList clone = new ArrayList();
            clone.items = (object?[])items.Clone();
            clone.Count = Count;
            return clone;
        }

        /// <summary>
        /// Checks if the ArrayList contains the object
        /// </summary>
        /// <param name="value">Object to find in the ArrayList</param>
        /// <returns>True if found, otherwise false</returns>
        public bool Contains(object? value)
        {
            foreach (var item in items)
            {
                if (item.Equals(value))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Copies the elements of the ArrayList to an array
        /// </summary>
        /// <param name="array">Array to copy to</param>
        /// <param name="index">Index at which copying begins</param>
        public void CopyTo(Array array, int index)
        {
            Array.Copy(items, 0, array, index, Count);
        }

        /// <summary>
        /// Ensures that the ArrayList has the capacity to hold a minimum number of elements
        /// </summary>
        /// <param name="min">Minimum capacity needed</param>
        private void EnsureCapacity(int min)
        {
            if (items.Length < min)
            {
                int newCapacity = items.Length == 0 ? 4 : items.Length * 2;
                if (newCapacity < min) newCapacity = min;
                var newItems = new object?[newCapacity];
                Array.Copy(items, newItems, Count);
                items = newItems;
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the ArrayList
        /// </summary>
        /// <returns>An enumerator</returns>
        public IEnumerator GetEnumerator()
        {
            foreach (var item in items)
            {
                yield return item;
            }
        }

        /// <summary>
        /// Returns the index of the object in the ArrayList
        /// </summary>
        /// <param name="value">The object to locate</param>
        /// <returns>The index of the object</returns>
        public int IndexOf(object? value)
        {
            foreach (var item in items)
            {
                if (item.Equals(value))
                {
                    return Array.IndexOf(items, item);
                }
            }
            return -1;
        }

        /// <summary>
        /// Inserts an object into the ArrayList at the specified index
        /// </summary>
        /// <param name="index">Index at which the object should be inserted</param>
        /// <param name="value">The object to be inserted</param>
        /// <exception cref="ArgumentOutOfRangeException">Index out of range</exception>
        public void Insert(int index, object? value)
        {
            if (index < 0 || index > Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
            EnsureCapacity(Count + 1);
            Array.Copy(items, index, items, index + 1, Count - index);
            items[index] = value;
            Count++;
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, value));
        }

        /// <summary>
        /// Removes the object from the ArrayList
        /// </summary>
        /// <param name="value">The object to remove</param>
        public void Remove(object? value)
        {
            int index = IndexOf(value);
            if (index != -1)
            {
                RemoveAt(index);
            }
        }

        /// <summary>
        /// Removes the object at the specified index
        /// </summary>
        /// <param name="index">Index of the element to remove</param>
        /// <exception cref="ArgumentOutOfRangeException">Index out of range</exception>
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            Count--;
            if (index < Count)
            {
                Array.Copy(items, index + 1, items, index, Count - index);
            }
            items[Count] = null;
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove));
        }
    }
}
