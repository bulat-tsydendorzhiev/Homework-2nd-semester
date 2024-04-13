namespace ParseTreeTask;

/// <summary>
/// Initializes a new instance of <see cref="Operand">.
/// </summary>
/// <param name="value">Value.</param>
public class Operand(int value) : INode
{
    /// <summary>
    /// Value of operand.
    /// </summary>
    public int Value { get; private set; } = value;

    /// <inheritdoc/>
    public void Print()
    {
        Console.Write(Value < 0 ? $"({Value})" : Value);
    }
    
    /// <inheritdoc/>
    public double Calculate() => Value;
}