using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlgorithmProgramming.Models;

namespace AlgorithmProgramming.Search
{
    public class JumpSearch
    {
        public static int SearchJump<T>(T[] arr, T key) where T : IComparable<T> 
        { 
            int ArraySize = arr.Length;
            int prev = 0;
            int step = (int) Math.Sqrt(ArraySize);

            while (prev < ArraySize && arr[Math.Min(step, ArraySize) - 1].CompareTo(key) < 0)
            {
                prev = step;
                step += (int)Math.Sqrt(ArraySize);
                if (prev >= ArraySize) 
                {
                    return -1; //Key is not in array
                }
            }

            while (prev < Math.Min(step, ArraySize) && arr[prev].CompareTo(key) < 1) 
            {
                if (arr[prev].Equals(key))
                {
                    return prev;
                }
                prev++;
            }

            return 1; // Key not found
        }
    }
}
