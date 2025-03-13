using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProgramming.Datastructures
{
    public class HashMap<TKey, TValue> : ICollection, IDictionary, IEnumerable, IDeserializationCallback, ISerializable where TKey : notnull
    {
        private struct Entry
        {
            public int next;
            public TKey key;
            public TValue? value;
        }

        private Entry[]? entries;

        public object? this[object key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool IsFixedSize => false;

        public bool IsReadOnly => false;

        public ICollection Keys { get; private set; }

        public ICollection Values { get; private set; }

        public int Count { get; private set; }

        public bool IsSynchronized => false;

        public object SyncRoot => false;

        public void Add(object key, object? value)
        {
            if (key is not TKey castedKey)
            {
                throw new ArgumentException($"Key must be of type {typeof(TKey)}", nameof(key));
            }

            Entry entry = new Entry
            {
                key = castedKey,
                value = (TValue?)value,
                next = -1
            };
            entries[Count] = entry;
            Count++;
        }

        public void Clear()
        {
            entries = [];
            Count = 0;
        }

        public bool Contains(object key)
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

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public IDictionaryEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        public void OnDeserialization(object? sender)
        {
            throw new NotImplementedException();
        }

        public void Remove(object key)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
