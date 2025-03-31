using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlgorithmProgramming.Datastructures;
using AlgorithmProgramming.Models;

namespace AlgorithmProgramming.Sorting
{
    internal class BubbleSort
    {
        public static HashMap<string, Stock> SortHashMapByPrice(HashMap<string, Stock> inputMap)
        {
            List<Stock> stockList = new List<Stock>();
            var sortedMap = new HashMap<string, Stock>();

            // Put the hashmap values in a list
            foreach (var pair in inputMap)
            {
                stockList.Add(pair.Value);
            }

            
            // (Bubble sort based on price)


            // Put sorted values in a hashmap
            foreach (var stock in stockList)
            {
                sortedMap.Add(stock.Ticker, stock);
            }

            return sortedMap;
        }
    }
}
