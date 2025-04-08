using AlgorithmProgramming.Datastructures;
using AlgorithmProgramming.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmProgramming.Search
{
    public class LinearSearch
    {
        /// <summary>
        /// Linear search through a DoublyLinkedList of generic type T.
        /// </summary>
        /// <param name="list">The Doubly Linked List that is being searched.</param>
        /// <param name="key">The key that's filled in into the search bar in the GUI.</param>
        /// <returns></returns>
        public static List<T> SearchDoublyLinkedList<T>(DoublyLinkedList<T> list, IComparer<T> comparer, T key)
        {
            DoublyLinkedList<T>.Node? node = list.Head;
            int index = 0;
            List<T> foundResults = new List<T>();
            while (node != null)
            {
                if(node.Data is T stock && comparer.Compare(stock, key) == 0)
                {
                    foundResults.Add(stock);
                }
                
                node = node.Next;
                index++;
            }

            return foundResults;
        }
    }
}
