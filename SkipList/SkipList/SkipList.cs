using System.Collections;

namespace Lists;

/// <summary>
/// Skip list, a probabilistic data structure that provides on average the logarithmic complexity of basic operations.
/// </summary>
/// <typeparam name="T">Type of stored value.</typeparam>
public class SkipList<T> : IList<T>
    where T: IComparable<T>
{
    private readonly Random _random = new();

    private SkipListElement _head;

    /// <inheritdoc/>
    public T this[int index]
    {
        get
        {
            return default;
        }
        set => throw new NotSupportedException("Skip list doesn't support setting value.");
    }

    /// <inheritdoc/>
    public int Count {get; }

    /// <inheritdoc/>
    public bool IsReadOnly => false;

    /// <inheritdoc/>
    public void Add(T item)
    {
        
    }

    /// <inheritdoc/>
    public void Clear()
    {
        
    }

    /// <inheritdoc/>
    public bool Contains(T item)
    {
        return true;
    }

    /// <inheritdoc/>
    public void CopyTo(T[] array, int arrayIndex)
    {
        
    }

    /// <inheritdoc/>
    public int IndexOf(T item)
    {
        return 1;
    }

    /// <inheritdoc/>
    public void Insert(int index, T item)
        => throw new NotSupportedException("Skip list doesn't support insert operation.");

    /// <inheritdoc/>
    public bool Remove(T item)
    {
        return true;
    }

    /// <inheritdoc/>
    public void RemoveAt(int index)
    {
        
    }

    /// <inheritdoc/>
    public Enumerator GetEnumerator()
    {
        return new Enumerator();
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }

    public readonly struct Enumerator
    {
        /// <inheritdoc/>
        public T Current { get; }

        /// <inheritdoc/>
        public void Dispose()
        {
            
        }
        
        /// <inheritdoc/>
        public bool MoveNext()
        {
            return true;
        }
    }

    private class SkipListElement
    {
        public SkipListElement? Next { get; set; }
        
        public int Level { get; set; }
        
        public T? Value { get; set; }
    }
}
