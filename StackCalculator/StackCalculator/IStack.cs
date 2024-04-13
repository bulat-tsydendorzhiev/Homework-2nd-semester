namespace StackCalculatorTask;

/// <summary>
/// Interface of a stack, a last-in-first-out container for double values.
/// </summary>
public interface IStack
{
    /// <summary>
    /// Checks whether stack is empty.
    /// </summary>
    /// <returns>true if stack is empty</returns>
    bool IsEmpty();

    /// <summary>
    /// Pushes value at the top of the stack.
    /// </summary>
    /// <param name="value">Pushing value.</param>
    void Push(double value);

    /// <summary>
    /// Gets a top value of the stack and remove it.
    /// </summary>
    /// <returns>Top value of stack.</returns>
    double Pop();

    /// <summary>
    /// Clears stack.
    /// </summary>
    void Clear();
}