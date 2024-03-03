namespace StackCalculator;

/// <summary>
/// Interface of a stack, a last-in-first-out container for double values.
/// </summary>
public interface IStack
{
    /// <summary>
    /// Check whether stack is empty.
    /// </summary>
    /// <returns>true if stack is empty</returns>
    bool IsEmpty();

    /// <summary>
    /// Push value at the top of the stack.
    /// </summary>
    /// <param name="value">Pushing value.</param>
    void Push(double value);

    /// <summary>
    /// Get a top value of the stack and remove it.
    /// </summary>
    /// <returns>Top value of stack.</returns>
    double Pop();

    /// <summary>
    /// Clear stack.
    /// </summary>
    void Clear();
}