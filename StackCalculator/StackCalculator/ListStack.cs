namespace StackCalculatorTask;

/// <summary>
/// Implementation of a stack on an list.
/// </summary>
public class ListStack : IStack
{
    private readonly List<double> StackElements;

    /// <summary>
    /// Initializes a new instance of <see cref="ArrayStack">.
    /// </summary>
    public ListStack()
    {
        StackElements = [];
    }

    /// <inheritdoc />
    public bool IsEmpty() => StackElements.Count == 0;

    /// <inheritdoc />
    public void Push(double value)
    {
        StackElements.Add(value);
    }

    /// <inheritdoc />
    public double Pop()
    {
        if (StackElements.Count == 0)
        {
            throw new InvalidOperationException("Stack is empty. You can't delete anything from it.");
        }

        var value = StackElements.Last();
        StackElements.RemoveAt(StackElements.Count - 1);
        return value;
    }

    /// <inheritdoc />
    public void Clear()
    {
        StackElements.Clear();
    }

}
