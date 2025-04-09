namespace AlgorithmProgramming.Sorting
{
    public class Quicksort
    {
        /// <summary>
        /// Sorts a list using the default comparer
        /// </summary>
        /// <typeparam name="T">The type of object in the list, must implement IComparable<T> becasue of default comparer</typeparam>
        /// <param name="list">The list to sort</param>
        public static void Sort<T>(IList<T> list) where T : IComparable<T>
        {
            Sort(list, Comparer<T>.Default);
        }

        /// <summary>
        /// If list is not null or has more than 1 object, sort
        /// </summary>
        /// <typeparam name="T">Object in the list</typeparam>
        /// <param name="list">List to sort</param>
        /// <param name="comparer">Comparer to use for sorting</param>
        public static void Sort<T>(IList<T> list, IComparer<T> comparer)
        {
            if (list == null || list.Count <= 1)
            {
                return;
            }

            Sort(list, 0, list.Count - 1, comparer);
        }

        /// <summary>
        /// Get partitionIndex, recursively sort before partition and after
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="comparer">Comparer to use for sorting</param>
        private static void Sort<T>(IList<T> list, int left, int right, IComparer<T> comparer)
        {
            if (left < right)
            {
                int partitionIndex = Partition(list, left, right, comparer);
                Sort(list, left, partitionIndex - 1, comparer);
                Sort(list, partitionIndex + 1, right, comparer);
            }
        }

        /// <summary>
        /// Using right as pivot, loop through list, swap when Compare <= 0
        /// </summary>
        /// <typeparam name="T">IComparable object</typeparam>
        /// <param name="list">List to be used</param>
        /// <param name="left">First object in the list</param>
        /// <param name="right">Last object in the list, used as pivot</param>
        /// <param name="comparer">Comparer to use for sorting</param>
        /// <returns></returns>
        private static int Partition<T>(IList<T> list, int left, int right, IComparer<T> comparer)
        {
            T pivot = list[right];
            int swapIndex = left;

            for (int i = left; i < right; i++)
            {
                if (comparer.Compare(list[i], pivot) <= 0)
                {
                    Swap(list, i, swapIndex);
                    swapIndex++;
                }
            }
            Swap(list, right, swapIndex);
            return swapIndex;
        }

        /// <summary>
        /// Swap two objects in a list
        /// </summary>
        /// <typeparam name="T">IComparable object</typeparam>
        /// <param name="list">List to execute swap in</param>
        /// <param name="i">First object to be swapped</param>
        /// <param name="j">Second object to be swapped</param>
        private static void Swap<T>(IList<T> list, int i, int j)
        {
            (list[j], list[i]) = (list[i], list[j]);
        }
    }
}

