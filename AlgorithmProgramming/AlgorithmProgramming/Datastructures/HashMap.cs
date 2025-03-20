﻿using System;
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

        public HashMap(int initialCapacity = 16)
        {
            capacity = initialCapacity;
            entries = new Entry[capacity];
            threshold = (int)(capacity * LoadFactor);
            Keys = new List<TKey>();
            Values = new List<TValue>();
        }

        private int GetBucketIndex(TKey key) => Math.Abs(key.GetHashCode()) % capacity;

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

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            entries = new Entry[capacity];
            Keys.Clear();
            Values.Clear();
            Count = 0;
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return TryGetValue(item.Key, out TValue value) && EqualityComparer<TValue>.Default.Equals(value, item.Value);
        }

        public bool ContainsKey(TKey key)
        {
            return Keys.Contains(key);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            for (int i = 0; i < Count; i++)
            {
                array[arrayIndex + i] = new KeyValuePair<TKey, TValue>(entries[i].key, entries[i].value);
            }
        }

        public void CopyTo(Array array, int index)
        {
            Array.Copy(entries, 0, array, index, Count);
        }

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

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("count", Count);
            info.AddValue("capacity", capacity);
            info.AddValue("entries", entries.Where(entry => entry.hashCode != 0 && !entry.isDeleted).ToArray());
        }

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

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

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

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
