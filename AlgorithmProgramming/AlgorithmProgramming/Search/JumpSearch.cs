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
        public static int SearchJump<T>(ArrayList list, T key) where T : IComparable<T>
        {
            int listSize = list.Count;
            int prev = 0;
            int step = (int)Math.Sqrt(listSize);

            // Go through the list in jumps
            while (prev < listSize)
            {
                // Check if the first element is not null
                object? currentElement = list[Math.Min(step, listSize) - 1];
                if (currentElement != null && ((T)currentElement).CompareTo(key) < 0)
                {
                    prev = step;
                    step += (int)Math.Sqrt(listSize);

                    // If we go outside of the array size the key is not in the list
                    if (prev >= listSize)
                    {
                        return -1; //Key is not in array
                    }
                }
                else
                {
                    break; // Stop if the element was found
                }
            }
            
            // Lineair search from the last step
            while (prev < Math.Min(step, listSize))
            {
                object? currentElement = list[prev];
                if (currentElement != null && ((T)currentElement).CompareTo(key) == 0)
                {
                    return prev;
                }
                prev++;
            }

            return -1; // Key not found
        }
    }
}
