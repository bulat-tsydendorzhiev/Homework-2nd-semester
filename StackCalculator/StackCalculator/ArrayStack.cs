namespace StackCalculatorTask;

/// <summary>
/// Implementation of a stack on an array.
/// </summary>
public class ArrayStack<T> : IStack<T>
{
    private const int InitialCapacity = 10;
    private T?[] _stackElements = new T?[InitialCapacity];
    private int _count;

    /// <inheritdoc />
    public bool IsEmpty() => _count == 0;

    /// <inheritdoc />
    public void Push(T value)
    {
        if (_count == _stackElements.Length)
        {
            Array.Resize(ref _stackElements, _stackElements.Length * 2);
        }

        _stackElements[_count++] = value;
    }

    /// <inheritdoc />
    public T Pop()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("Stack is empty. You can't delete anything from it.");
        }

        --_count;
        var value = _stackElements[_count];
        _stackElements[_count] = default;

        return value;
    }

    /// <inheritdoc />
    public void Clear()
    {
        _stackElements = new T[InitialCapacity];
        _count = 0;
    }
}

