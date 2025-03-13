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
        public int SearchHistoricValues(int[] arr, int key) 
        { 
            int ArraySize = arr.Length;
            int prev = 0;
            int step = (int) Math.Sqrt(ArraySize);

            while (step < ArraySize - 1 && arr[step] <= key)
            {
                prev = step;
                step += (int)Math.Sqrt(ArraySize);
            }

            return 1;
        }
    }
}
