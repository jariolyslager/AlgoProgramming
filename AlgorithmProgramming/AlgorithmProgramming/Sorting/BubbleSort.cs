using AlgorithmProgramming.Datastructures;
using AlgorithmProgramming.Models;

namespace AlgorithmProgramming.Sorting
{
    internal class BubbleSort
    {
        /// <summary>
        /// Sorts a IEnumerable collection based on a comparer using the bubble sort algorithm.
        /// </summary>
        /// <param name="inputCollection">IEnumerable with values to sort.</param>
        /// <param name="comparer"> A comparer to compare the stocks.</param>
        /// <returns>A new List with the sorted values.</returns>
        public static List<T> Sort<T>(IEnumerable<T> inputCollection, IComparer<T> comparer)
        {
            List<T> itemList = new List<T>(inputCollection);

            int listSize = itemList.Count;
            bool swapped;

            // Bubble sort on the list
            for (int i = 0; i < listSize - 1; i++)
            {
                swapped = false;
                for (int j = 0; j < listSize - 1 - i; j++)
                {
                    // Use comparer to compare stocks
                    if (comparer.Compare(itemList[j], itemList[j + 1]) > 0)
                    {
                        // Swap if the previous price is higher then the next
                        var temp = itemList[j];
                        itemList[j] = itemList[j+1];
                        itemList[j+1] = temp;
                        swapped = true;
                    }
                }

                // If no swaps happened, the list is already sorted
                if (!swapped)
                {
                    break;
                }
            }

            return itemList;
        }

        /// <summary>
        /// Sorts a IEnumerable collection based on a default comparer using the bubble sort algorithm.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputCollection">IEnumerable with values to sort.</param>
        /// <returns>A new List with the sorted values.</returns>
        public static List<T> Sort<T>(IEnumerable<T> inputCollection) where T : IComparable<T>
        {
            return Sort(inputCollection, Comparer<T>.Default);
        }
    }
}
