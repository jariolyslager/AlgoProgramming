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
        private struct Entry
        {
            public int next;
            public TKey key;
            public TValue? value;
        }

        private Entry[]? entries;

        public TValue this[TKey key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ICollection<TKey> Keys { get; private set; }

        public ICollection<TValue> Values { get; private set; }

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        public bool IsSynchronized => false;

        public object SyncRoot => false;

        public void Add(TKey key, TValue value)
        {
            Entry entry = new Entry
            {
                key = key,
                value = value,
                next = -1
            };
            entries[Count] = entry;
            Count++;
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Entry entry = new Entry
            {
                key = item.Key,
                value = item.Value,
                next = -1
            };
            entries[Count] = entry;
            Count++;
        }

        public void Clear()
        {
            entries = [];
            Keys.Clear();
            Values.Clear();
            Count = 0;
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            foreach (var entry in entries)
            {
                if(item.Key.Equals(entry.key) && item.Value.Equals(entry.value))
                {
                    return true;
                }
            }
            return false;
        }

        public bool ContainsKey(TKey key)
        {
            foreach (var item in this.Keys)
            {
                if (item.Equals(key))
                {
                    return true;
                }
            }
            return false;
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Array array, int index)
        {
            Array.Copy(entries, 0, array, index, Count);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (var entry in entries)
            {
                yield return new KeyValuePair<TKey, TValue>(entry.key, entry.value);
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        public void OnDeserialization(object? sender)
        {
            throw new NotImplementedException();
        }

        public bool Remove(TKey key)
        {
            foreach(var entry in entries)
            {
                if (entry.key.Equals(key))
                {
                    
                    Count--;
                    return true;
                }
            }
            return false;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
