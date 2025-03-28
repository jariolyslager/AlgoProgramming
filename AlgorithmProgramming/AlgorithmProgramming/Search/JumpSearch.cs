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
        /// Search on a sorted stocks list to find all entries matching a specific date
        /// </summary>
        /// <param name="stocks">A sorted ArrayList of Stocks</param>
        /// <param name="targetDate">DateTime to search for</param>
        /// <returns>An ArrayList containing all stocks from the given date</returns>
        public Datastructures.ArrayList<Stock> JumpSearchByDate(IList<Stock> stocks, DateTime targetDate)
        {
            int listSize = stocks.Count;
            int prev = 0;
            int step = (int)Math.Sqrt(listSize);

            var result = new Datastructures.ArrayList<Stock>();

            // Go through the list in jumps
            while (prev < listSize && stocks[Math.Min(step, listSize) - 1].Date < targetDate)
            {
                prev = step;
                step += (int)Math.Sqrt(listSize);

                // If we go outside of the array size the key is not in the list
                if (prev >= listSize)
                {
                    return result; // Return empty list
                }
            }

            // Lineair search from the last step
            for (int i = prev; i < listSize; i++)
            {
                if (stocks[i].Date.Equals(targetDate))
                {
                    result.Add(stocks[i]);
                }
                else if (stocks[i].Date > targetDate)
                {
                    break;
                }
            }

            return result;
        }
    }
}
