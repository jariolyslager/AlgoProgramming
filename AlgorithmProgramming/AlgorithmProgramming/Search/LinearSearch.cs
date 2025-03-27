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
        public static int Search<T>(IList<T> list, T key) where T : IComparable<T>
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].CompareTo(key) == 0)
                {
                    return i;
                }
            }
            return -1;
        }

        public static List<Stock> SearchDoublyLinkedList<T>(DoublyLinkedList<T> list, T key) where T : IComparable<T>
        {
            DoublyLinkedList<T>.Node? node = list.Head;
            int index = 0;
            List<Stock> foundResults = new List<Stock>();
            while (node != null)
            {
                if (node.Data.CompareTo(key) == 0)
                {
                    if (node.Data is Stock stockData)
                    {
                        foundResults.Add(stockData);
                    }
                }
                node = node.Next;
                index++;
            }
            if (foundResults.Count > 0)
            {
                return foundResults;
            }
            else
            {
                throw new KeyNotFoundException("This key doesn't exist");
            }
        }
    }
}
