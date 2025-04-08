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
        public static List<Stock> SearchDoublyLinkedList(DoublyLinkedList<Stock> list, string key)
        {
            DoublyLinkedList<Stock>.Node? node = list.Head;
            int index = 0;
            List<Stock> foundResults = new List<Stock>();
            while (node != null)
            {
                if (node.Data.Ticker.Equals(key))
                {
                    foundResults.Add(node.Data);
                }
                node = node.Next;
                index++;
            }

            return foundResults;
        }

        /// <summary>
        /// Linear search through a DoublyLinkedList of generic type T.
        /// </summary>
        /// <param name="list">The Doubly Linked List that is being searched.</param>
        /// <param name="key">The key that's filled in into the search bar in the GUI.</param>
        /// <returns></returns>
        public static List<Stock> GenericSearchDoublyLinkedList<T>(DoublyLinkedList<T> list, string key)
        {
            DoublyLinkedList<T>.Node? node = list.Head;
            int index = 0;
            List<Stock> foundResults = new List<Stock>();
            while (node != null)
            {
                if(node.Data is Stock stock && stock.Ticker.Equals(key))
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
