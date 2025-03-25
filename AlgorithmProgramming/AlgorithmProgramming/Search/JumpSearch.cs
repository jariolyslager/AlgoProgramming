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
        public List<Stock> JumpSearchByDate(List<Stock> stocks, DateTime targetDate)
        {
            int listSize = stocks.Count;
            int prev = 0;
            int step = (int)Math.Sqrt(listSize);

            List<Stock> result = new List<Stock>();

            // Go through the list in jumps
            while (stocks[Math.Min(step, listSize) - 1].Date < targetDate)
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
