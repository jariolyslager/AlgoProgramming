using System.Collections;

namespace AlgorithmProgramming.Datastructures
{
    public class DoublyLinkedList<T> : ICollection<T>
    {
        public Node Head { get; set; }
        public Node Tail { get; set; }

        public class Node
        {
            public Node? Next { get; set; }
            public Node? Previous { get; set; }
            public T Data { get; set; }

            public Node(T data)
            {
                Data = data;
            }
        }

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        /// <summary>
        /// Adds an item to the end of the doubly linked list
        /// </summary>
        /// <param name="item">The item to add to the list</param>
        public void Add(T item)
        {
            Node newNode = new Node(item);
            if (Head == null)
            {
                Head = newNode;
                Tail = newNode;
            }
            else
            {
                Tail.Next = newNode;
                newNode.Previous = Tail;
                Tail = newNode;
            }
            Count++;
        }

        /// <summary>
        /// Removes all items from the doubly linked list
        /// </summary>
        public void Clear()
        {
            Head = null;
            Tail = null;
            Count = 0;
        }

        /// <summary>
        /// Checks if the doubly linked list contains the item
        /// </summary>
        /// <param name="item">Item to be checked</param>
        /// <returns>True if item is found, otherwise false</returns>
        public bool Contains(T item)
        {
            Node node = Head;
            while (node != null)
            {
                if (node.Data.Equals(item))
                {
                    return true;
                }
                node = node.Next;
            }
            return false;
        }

        /// <summary>
        /// Copy the doubly linked list to an array
        /// </summary>
        /// <param name="array">Array to copy elements to</param>
        /// <param name="arrayIndex">Index at which the copying begins</param>
        /// <exception cref="ArgumentNullException">Array is null</exception>
        /// <exception cref="ArgumentOutOfRangeException">ArrayIndex param is less than 0</exception>
        /// <exception cref="ArgumentException">Given array is too small to contain all objects</exception>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            }

            if (array.Length - arrayIndex < Count)
            {
                throw new ArgumentException("The array is too small.");
            }

            Node current = Head;
            while (current != null)
            {
                array[arrayIndex++] = current.Data;
                current = current.Next;
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the doubly linked list
        /// </summary>
        /// <returns>An enumerator</returns>
        public IEnumerator<T> GetEnumerator()
        {
            Node current = Head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        /// <summary>
        /// Removes object from the doubly linked list
        /// </summary>
        /// <param name="item">Object to be removed</param>
        /// <returns>True if item is removed, false if not</returns>
        public bool Remove(T item)
        {
            Node current = Head;
            while (current != null)
            {
                if (current.Data.Equals(item))
                {
                    if (current.Previous != null)
                    {
                        current.Previous.Next = current.Next;
                    }
                    else
                    {
                        Head = current.Next;
                    }

                    if (current.Next != null)
                    {
                        current.Next.Previous = current.Previous;
                    }
                    else
                    {
                        Tail = current.Previous;
                    }

                    Count--;
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the doubly linked list
        /// </summary>
        /// <returns>An enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

