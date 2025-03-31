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
        /// <summary>
        /// Sorts a HashMap of stocks in ascending order based on stock price using the Bubble Sort algorithm.
        /// </summary>
        /// <param name="inputMap">A HashMap with stock tickers as keys and Stock objects as values.</param>
        /// <returns>A new HashMap with stocks sorted by price in ascending order.</returns>
        public static List<Stock> SortHashMapByPrice(HashMap<string, Stock> inputMap)
        {
            // Put the hashmap values in a list
            List<Stock> stockList = new List<Stock>(inputMap.Values);

            int listSize = stockList.Count;
            bool swapped;

            // Bubble sort on the list
            for (int i = 0; i < listSize - 1; i++)
            {
                swapped = false;
                for (int j = 0; j < listSize - 1 - i; j++)
                {
                    // Compare adjacent stock prices
                    if (stockList[j].Price > stockList[j+1].Price)
                    {
                        // Swap if the previous price is higher then the next
                        var temp = stockList[j];
                        stockList[j] = stockList[j+1];
                        stockList[j+1] = temp;
                        swapped = true;
                    }
                }

                // If no swaps happened, the list is already sorted
                if (!swapped)
                {
                    break;
                }
            }


            return stockList;
        }
    }
}
