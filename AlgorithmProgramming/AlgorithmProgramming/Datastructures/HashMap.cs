using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProgramming.Datastructures
{
    public class HashMap<TKey, TValue> : ICollection, IDictionary<TKey, TValue>, IEnumerable, IDeserializationCallback, ISerializable where TKey : notnull
    {
        /// <summary>
        /// Struct for the definition of an Entry in the HashMap.
        /// </summary>
        private struct Entry
        {
            public int hashCode;
            public int next;
            public TKey key;
            public TValue? value;
            public bool isDeleted;
        }

        private Entry[]? entries;
        private int capacity;
        private int threshold;
        private const double LoadFactor = 1.0;

        /// <summary>
        /// Initializes a new instance of the HashMap class.
        /// </summary>
        /// <param name="initialCapacity">Initial capacity of the HashMap</param>
        public HashMap(int initialCapacity = 16)
        {
            capacity = initialCapacity;
            entries = new Entry[capacity];
            threshold = (int)(capacity * LoadFactor);
            Keys = new List<TKey>();
            Values = new List<TValue>();
        }

        /// <summary>
        /// Helper method to get the bucket index for a key.
        /// </summary>
        /// <param name="key">Key of the Entry</param>
        /// <returns>Index of the bucket based on the hashcode.</returns>
        private int GetBucketIndex(TKey key) => Math.Abs(key.GetHashCode()) % capacity;

        /// <summary>
        /// Gets or sets the element with the specified key.
        /// </summary>
        /// <param name="key">Key of the Entry that needs to be get or set.</param>
        /// <returns>The value that corresponds to the key.</returns>
        /// <exception cref="KeyNotFoundException">Key Not Found</exception>
        public TValue this[TKey key]
        {
            get
            {
                if (TryGetValue(key, out TValue value))
                {
                    return value;
                }
                else
                {
                    throw new KeyNotFoundException();
                }
            }
            set => Add(key, value);
        }

        public ICollection<TKey> Keys { get; private set; }

        public ICollection<TValue> Values { get; private set; }

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        public bool IsSynchronized => false;

        public object SyncRoot => false;

        /// <summary>
        /// Adds an element with the provided key and value to the HashMap.
        /// </summary>
        /// <param name="key">Key for the Entry in the HashMap.</param>
        /// <param name="value">Value that belongs to the key.</param>
        /// <exception cref="ArgumentException">Exception in case of duplicate key.</exception>
        public void Add(TKey key, TValue value)
        {
            if (Count >= threshold)
            {
                Resize();
            }

            int index = GetBucketIndex(key);
            while (entries[index].hashCode != 0 && !entries[index].isDeleted)
            {
                if (entries[index].key.Equals(key))
                {
                    throw new ArgumentException("An item with the same key has already been added.");
                }
                index = (index + 1) % capacity;
            }

            entries[index] = new Entry
            {
                hashCode = key.GetHashCode(),
                key = key,
                value = value,
                next = -1,
                isDeleted = false
            };

            Keys.Add(key);
            Values.Add(value);
            Count++;
        }

        /// <summary>
        /// Adds an item to the HashMap.
        /// </summary>
        /// <param name="item">KeyValuePair of a key and a value.</param>
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        /// <summary>
        /// Empties the HashMap.
        /// </summary>
        public void Clear()
        {
            entries = new Entry[capacity];
            Keys.Clear();
            Values.Clear();
            Count = 0;
        }

        /// <summary>
        /// Checks if the HashMap contains a specific KeyValuePair.
        /// </summary>
        /// <param name="item">KeyValuePair of a key and a value.</param>
        /// <returns>True if the HashMap contains the specified KeyValuePair.</returns>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return TryGetValue(item.Key, out TValue value) && EqualityComparer<TValue>.Default.Equals(value, item.Value);
        }

        /// <summary>
        /// Checks if the HashMap contains a specific key.
        /// </summary>
        /// <param name="key">Key of an Entry in the HashMap.</param>
        /// <returns>True if the HashMap contains an Entry with the specified key.</returns>
        public bool ContainsKey(TKey key)
        {
            return Keys.Contains(key);
        }

        /// <summary>
        /// Copies the elements of the HashMap to an Array, starting at a particular Array index.
        /// </summary>
        /// <param name="array">Destination array of KeyValuePairs.</param>
        /// <param name="arrayIndex">Index where the copying should start.</param>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            for (int i = 0; i < Count; i++)
            {
                array[arrayIndex + i] = new KeyValuePair<TKey, TValue>(entries[i].key, entries[i].value);
            }
        }

        /// <summary>
        /// Copies the elements of the HashMap to an Array, starting at a particular Array index.
        /// </summary>
        /// <param name="array">Destination array.</param>
        /// <param name="index">Index where the copying should start.</param>
        public void CopyTo(Array array, int index)
        {
            Array.Copy(entries, 0, array, index, Count);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the HashMap.
        /// </summary>
        /// <returns>Each KeyValuePair in the HashMap.</returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (var entry in entries)
            {
                if (entry.hashCode != 0 && !entry.isDeleted)
                {
                    yield return new KeyValuePair<TKey, TValue>(entry.key, entry.value);
                }
            }
        }

        /// <summary>
        /// Serializes info from the HashMap.
        /// </summary>
        /// <param name="info">Object that holds the serialized data.</param>
        /// <param name="context"></param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("count", Count);
            info.AddValue("capacity", capacity);
            info.AddValue("entries", entries.Where(entry => entry.hashCode != 0 && !entry.isDeleted).ToArray());
        }

        /// <summary>
        /// Method for deserializing SerializationInfo into a HashMap.
        /// </summary>
        /// <param name="sender"></param>
        /// <exception cref="SerializationException">SerializationInfo is missing.</exception>
        public void OnDeserialization(object? sender)
        {
            if (entries != null)
            {
                return;
            }

            SerializationInfo? info = (SerializationInfo?)sender;
            if (info == null)
            {
                throw new SerializationException("SerializationInfo is missing.");
            }

            Count = info.GetInt32("count");
            capacity = info.GetInt32("capacity");
            entries = (Entry[])info.GetValue("entries", typeof(Entry[]));

            Keys = new List<TKey>();
            Values = new List<TValue>();

            foreach (var entry in entries)
            {
                if (entry.hashCode != 0 && !entry.isDeleted)
                {
                    Keys.Add(entry.key);
                    Values.Add(entry.value);
                }
            }
        }

        /// <summary>
        /// Removes the element with the specified key from the HashMap.
        /// </summary>
        /// <param name="key">Key of the Entry that needs to be removed.</param>
        /// <returns>True if an Entry is found with the specified key and the Entry is successfully removed.</returns>
        public bool Remove(TKey key)
        {
            int index = GetBucketIndex(key);

            while (entries[index].hashCode != 0)
            {
                if (!entries[index].isDeleted && entries[index].key.Equals(key))
                {
                    entries[index].isDeleted = true;
                    Keys.Remove(key);
                    Values.Remove(entries[index].value);
                    Count--;
                    return true;
                }
                index = (index + 1) % capacity;
            }
            return false;
        }

        /// <summary>
        /// Removes the element with the specified key from the HashMap based on a given KeyValuePair.
        /// </summary>
        /// <param name="item">KeyValuePair that needs to be removed from the HashMap.</param>
        /// <returns>True if an Entry is found with the specified key and the Entry is successfully removed.</returns>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

        /// <summary>
        /// Method for resizing the HashMap, removing all deleted entries (tombstones).
        /// </summary>
        private void Resize()
        {
            int newCapacity = capacity * 2;
            var newEntries = new Entry[newCapacity];

            foreach (var entry in entries)
            {
                if (entry.hashCode != 0 && !entry.isDeleted)
                {
                    int newIndex = Math.Abs(entry.hashCode) % newCapacity;
                    while (newEntries[newIndex].hashCode != 0)
                        newIndex = (newIndex + 1) % newCapacity;

                    newEntries[newIndex] = entry;
                }
            }

            capacity = newCapacity;
            entries = newEntries;
            threshold = (int)(capacity * LoadFactor);
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <param name="key">Key of an Entry in the HashMap.</param>
        /// <param name="value">Value of the Entry with the specified Key.</param>
        /// <returns>True if the specified key has been found and the corresponding value has been successfully retrieved.</returns>
        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            int index = GetBucketIndex(key);

            while (entries[index].hashCode != 0)
            {
                if (!entries[index].isDeleted && entries[index].key.Equals(key))
                {
                    value = entries[index].value;
                    return true;
                }
                index = (index + 1) % capacity;
            }

            value = default!;
            return false;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the HashMap.
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
