using System.Collections;

namespace SkipListLibrary;

/// <summary>
/// Skip list, a probabilistic data structure that provides on average the logarithmic complexity of basic operations.
/// </summary>
/// <typeparam name="T">Type of stored value.</typeparam>
public class SkipList<T> : IList<T>
    where T: IComparable<T>
{
    private const int MaxLevel = 5;

    private int _currentMaxLevel = 1; 

    private readonly Random _random = new();

    private SkipListNode _head;

    /// <summary>
    /// Initializes a new instance of <see cref="SkipList"/>.
    /// </summary>
    public SkipList()
    {
        _head = new(default, _currentMaxLevel);
    }

    /// <inheritdoc/>
    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            var currentNode = _head;

            for (int i = 0; i < index; ++i)
            {
                currentNode = currentNode.Next[0];
            }

            return currentNode.Value;
        }
        set => throw new NotSupportedException("Skip list doesn't support set value operation.");
    }

    /// <inheritdoc/>
    public int Count { get; private set; }

    /// <inheritdoc/>
    bool ICollection<T>.IsReadOnly => false;

    /// <inheritdoc/>
    public void Add(T value)
    {
        var newNodeLevelsAmount = GetLevelsAmount();
        var newNode = new SkipListNode(value, newNodeLevelsAmount);

        var currentNode = _head;

        for (int i = _currentMaxLevel - 1; i >= 0; --i)
        {
            while (currentNode.Next[i] is not null)
            {
                if (value.CompareTo(currentNode.Value) < 0)
                {
                    break;
                }

                currentNode = currentNode.Next[i];
            }

            if (i < newNodeLevelsAmount)
            {
                newNode.Next[i] = currentNode.Next[i];
                currentNode.Next[i] = newNode;
            }
        }
        
        ++Count;
    }

    /// <inheritdoc/>
    public void Clear()
    {
        Count = 0;
        _currentMaxLevel = 1;
        _head = new(default, _currentMaxLevel);
    }

    /// <inheritdoc/>
    public bool Contains(T value)
    {
        return IndexOf(value) != -1;
    }

    /// <inheritdoc/>
    public void CopyTo(T[] array, int arrayIndex)
    {
        ArgumentNullException.ThrowIfNull(array);

        if (arrayIndex < 0 || arrayIndex >= Count)
        {
            throw new ArgumentOutOfRangeException(nameof(arrayIndex));
        }

        if (arrayIndex >= Count)
        {
            throw new ArgumentException("Index must be less than amount of elements in skip list.");
        }

        var currentNode = _head;

        for (int i = 0; i < Count; ++i)
        {
            currentNode = currentNode.Next[0];

            if (i >= arrayIndex)
            {
                array[i] = currentNode.Value;
            }
        }
    }

    /// <inheritdoc/>
    public int IndexOf(T value)
    {
        var currentNode = _head;
        int index = 0;
        
        for (int i = _currentMaxLevel - 1; i >= 0; --i)
        {
            while (currentNode.Next[i] is not null)
            {
                if (value.CompareTo(currentNode.Value) < 0)
                {
                    break;
                }
                else if (value.CompareTo(currentNode.Value) == 0)
                {
                    return index;
                }

                currentNode = currentNode.Next[i];
                ++index;
            }
        }
        
        return -1;
    }

    /// <summary>
    /// Not supported operation.
    /// </summary>
    public void Insert(int index, T value)
        => throw new NotSupportedException("Skip list doesn't support insert operation.");

    /// <inheritdoc/>
    public bool Remove(T value)
    {
        var successfulRemoving = false;
        var currentNode = _head;
        
        for (int i = _currentMaxLevel - 1; i >= 0; --i)
        {
            while (currentNode.Next[i] is not null)
            {
                if (value.CompareTo(currentNode.Value) < 0)
                {
                    break;
                }
                else if (value.CompareTo(currentNode.Next[i].Value) == 0)
                {
                    currentNode.Next[i] = currentNode.Next[i].Next[i];
                    successfulRemoving = true;
                }

                currentNode = currentNode.Next[i];
            }
        }
        
        if (successfulRemoving)
        {
            --Count;
        }
        
        return successfulRemoving;
    }

    /// <inheritdoc/>
    public void RemoveAt(int index)
    {
        Remove(this[index]);
    }

    private int GetLevelsAmount()
    {
        int levelsAmount = 1;
        
        while (levelsAmount < MaxLevel && CanContinue())
        {
            ++levelsAmount;
        }
        
        return levelsAmount;
    }

    private bool CanContinue()
    {
        return _random.Next(0, 2) == 1;
    }

    /// <inheritdoc/>
    public Enumerator GetEnumerator()
    {
        return new(this);
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        return new Enumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return new Enumerator(this);
    }

    /// <inheritdoc/>
    public struct Enumerator : IEnumerator<T>
    {
        private SkipList<T> _skipList;

        private T? _currentValue;
        
        private SkipListNode? _currentNode;

        /// <inheritdoc/>
        public T Current { get; }

        object IEnumerator.Current => Current;

        public Enumerator(SkipList<T> skipList)
        {
            _skipList = skipList;
            _currentValue = default;
            _currentNode = skipList._head;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
        }
        
        /// <inheritdoc/>
        public bool MoveNext()
        {
            return true;
        }

        /// <inheritdoc/>
        public void Reset()
        {
            throw new NotImplementedException();
        }
    }

    private class SkipListNode(T value, int levelsAmount)
    {
        public T Value { get; } = value;

        public SkipListNode[] Next { get; set; } = new SkipListNode[levelsAmount];
    }
}
