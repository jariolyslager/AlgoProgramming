using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlgorithmProgramming.Models;

namespace AlgorithmProgramming.Search
{
    public class JumpSearch
    {
        /// <summary>
        /// Generic Jump Search on a sorted list using a comparer.
        /// </summary>
        /// <typeparam name="T">Type of the objects in the list</typeparam>
        /// <param name="list">A sorted list of type T</param>
        /// <param name="comparer">Comparer for comparing elements</param>
        /// <param name="key">The item to search for</param>
        /// <returns>A list containing all matching items</returns>
        public static List<T> Search<T>(IList<T> list, IComparer<T> comparer, T key)
        {
            int listSize = list.Count;
            int prev = 0;
            int step = (int)Math.Sqrt(listSize);

            List<T> result = new List<T>();

            // Go through the list in jumps
            while (prev < listSize && comparer.Compare(list[Math.Min(step, listSize) - 1], key) < 0)
            {
                prev = step;
                step += (int)Math.Sqrt(listSize);

                if (prev >= listSize)
                {
                    return result; // Return empty list
                }
            }

            // Lineair search from the last step
            for (int i = prev; i < Math.Min(step, listSize); i++)
            {
                int comparison = comparer.Compare(list[i], key);
                if (comparison == 0)
                {
                    result.Add(list[i]);
                }
                else if (comparison > 0)
                {
                    break;
                }
            }

            return result;
        }
    }
}
