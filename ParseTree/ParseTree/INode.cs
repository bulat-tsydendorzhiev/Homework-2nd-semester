namespace ParseTree;

/// <summary>
/// Interface of Parse tree node.
/// </summary>
public interface INode
{
    /// <summary>
    /// Prints subtree including this node.
    /// </summary>
    public void Print();
    
    /// <summary>
    /// Calculates node value.
    /// </summary>
    /// <returns>Result of calculations.</returns>
    public float Calculate();
}