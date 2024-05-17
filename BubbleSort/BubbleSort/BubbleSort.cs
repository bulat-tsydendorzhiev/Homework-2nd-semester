namespace BubbleSort;

/// <summary>
/// Class that has method of sorting list using bubble sort algorithm.
/// </summary>
public class BubbleSorter
{
    /// <summary>
    /// Sorts list.
    /// </summary>
    /// <typeparam name="T">Type of values in list.</typeparam>
    /// <param name="list"></param>
    /// <param name="comparer"></param>
    public static void Sort<T>(IList<T> list, IComparer<T> comparer)
    {
        ArgumentNullException.ThrowIfNull(list);
        ArgumentNullException.ThrowIfNull(comparer);

        for (int i = 0; i < list.Count; i++)
        {
            for (int j = 0; j < list.Count - i - 1; j++)
            {
                if (comparer.Compare(list[j], list[j + 1]) > 0)
                {
                    (list[j + 1], list[j]) = (list[j], list[j + 1]);
                }
            }
        }
    }
}
