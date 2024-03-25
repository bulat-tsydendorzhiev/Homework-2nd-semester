namespace StackCalculator;

/// <summary>
/// Implementation of a stack on an array.
/// </summary>
public class ArrayStack : IStack
{
    private const int InitialCapacity = 10;
    private double[] StackElements;
    private int Count;

    /// <summary>
    /// Initializes a new object ArrayStack.
    /// </summary>
    public ArrayStack()
    {
        StackElements = new double[InitialCapacity];
    }

    /// <inheritdoc />
    public bool IsEmpty() => Count == 0;

    /// <inheritdoc />
    public void Push(double value)
    {
        if (Count == StackElements.Length)
        {
            Array.Resize(ref StackElements, StackElements.Length * 2);
        }

        StackElements[Count++] = value;
    }

    /// <inheritdoc />
    public double Pop()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("Stack is empty. You can't delete anything from it.");
        }

        --Count;
        var value = StackElements[Count];
        StackElements[Count] = 0;

        return value;
    }

    /// <inheritdoc />
    public void Clear()
    {
        StackElements = new double[InitialCapacity];
        Count = 0;
    }

}

