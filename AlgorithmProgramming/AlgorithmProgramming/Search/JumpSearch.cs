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
        public static int SearchJump(ArrayList list, double key)
        {
            int listSize = list.Count;
            int prev = 0;
            int step = (int)Math.Sqrt(listSize);

            // Go through the list in jumps
            while (prev < listSize)
            {
                var currentElement = list[Math.Min(step, listSize) - 1] as Stock;

                if (currentElement != null && currentElement.Price < key)
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
                var currentElement = list[prev] as Stock;
                if (currentElement != null && currentElement.Price == key)
                {
                    return prev;
                }
                prev++;
            }

            return -1; // Key not found
        }
    }
}
