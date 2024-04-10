namespace MapFilterFold;

/// <summary>
/// Class which has functions to work with list of elements of any type
/// </summary>
public class Functions
{
    /// <summary>
    /// Applies some function to each element of the list and returns a new list.
    /// </summary>
    /// <typeparam name="TIn">The type of elements in the list.</typeparam>
    /// <typeparam name="TOut">The type of elements of returning list.</typeparam>
    /// <param name="list">Specified list.</param>
    /// <param name="func">Function using for each element of the list.</param>
    /// <returns>List of elements with type TOut.</returns>
    public static List<TOut> Map<TIn, TOut>(List<TIn> list, Func<TIn, TOut> func)
    {
        var resultList = new List<TOut>();
        
        foreach (var item in list)
        {
            resultList.Add(func(item));
        }
        
        return resultList;
    }

    /// <summary>
    /// Returns a new list with all elements that match the condition specified in the passed function.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <param name="list">Specified list.</param>
    /// <param name="func">Function that specifies conditions for filtering.</param>
    /// <returns>Filtered list.</returns>
    public static List<T> Filter<T>(List<T> list, Func<T, bool> func)
    {
        var resultList = new List<T>();
        
        foreach (var item in list)
        {
            if (func(item))
            {
                resultList.Add(item);
            }
        }
        
        return resultList;
    }

    /// <summary>
    /// Returns the accumulated value obtained after using the function on the elements while traversing the list.
    /// </summary>
    /// <typeparam name="TIn">The type of elements in the list.</typeparam>
    /// <typeparam name="TOut">The type of returning value.</typeparam>
    /// <param name="list">Specified list.</param>
    /// <param name="startValue">Start value.</param>
    /// <param name="func">A function that is used to accumulate the start value</param>
    /// <returns>Accumulated value.</returns>
    public static TOut Fold<TIn, TOut>(List<TIn> list, TOut startValue, Func<TIn, TOut, TOut> func)
    {
        TOut currentValue = startValue;
        
        foreach (var item in list)
        {
            currentValue = func(item, currentValue);
        }
        
        return currentValue;
    }
}