namespace StackCalculatorTask;

/// <summary>
/// Implementation of a stack on an list.
/// </summary>
public class ListStack<T> : IStack<T>
{
    private readonly List<T> _stackElements = [];

    /// <inheritdoc />
    public bool IsEmpty() => _stackElements.Count == 0;

    /// <inheritdoc />
    public void Push(T value)
    {
        _stackElements.Add(value);
    }

    /// <inheritdoc />
    public T Pop()
    {
        if (_stackElements.Count == 0)
        {
            throw new InvalidOperationException("Stack is empty. You can't delete anything from it.");
        }

        var value = _stackElements.Last();
        _stackElements.RemoveAt(_stackElements.Count - 1);
        return value;
    }

    /// <inheritdoc />
    public void Clear()
    {
        _stackElements.Clear();
    }
}
