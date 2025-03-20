namespace AlgorithmProgramming.Sorting
{
    public class Quicksort
    {
        /// <summary>
        /// If list is not null or has more than 1 object, sort
        /// </summary>
        /// <typeparam name="T">IComparable object</typeparam>
        /// <param name="list">List to sort</param>
        public static void Sort<T>(IList<T> list) where T : IComparable<T>
        {
            if (list == null || list.Count <= 1)
            {
                return;
            }

            Sort(list, 0, list.Count - 1);
        }

        /// <summary>
        /// Get partitionIndex, recursively sort before partition and after
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        private static void Sort<T>(IList<T> list, int left, int right) where T : IComparable<T>
        {
            if (left < right)
            {
                int partitionIndex = Partition(list, left, right);
                Sort(list, left, partitionIndex - 1);
                Sort(list, partitionIndex + 1, right);
            }
        }

        /// <summary>
        /// Using right as pivot, loop through list, swap when CompareTo <= 0
        /// </summary>
        /// <typeparam name="T">IComparable object</typeparam>
        /// <param name="list">List to be used</param>
        /// <param name="left">First object in the list</param>
        /// <param name="right">Last object in the list, used as pivot</param>
        /// <returns></returns>
        private static int Partition<T>(IList<T> list, int left, int right) where T : IComparable<T>
        {
            T pivot = list[right];
            int swapIndex = left;

            for (int i = left; i < right; i++)
            {
                if (list[i].CompareTo(pivot) <= 0)
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

