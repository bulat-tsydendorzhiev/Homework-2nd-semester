namespace ParseTree;

/// <summary>
/// Initializes a new instance of Operator.
/// </summary>
/// <param name="operation">Operation.</param>
public class Operator(string operation) : INode
{
    /// <summary>
    /// Left operator or operand.
    /// </summary>
    public INode? LeftSon;
    
    /// <summary>
    /// Right operator or operand.
    /// </summary>
    public INode? RightSon;
    
    private readonly string Operation = operation;
    
    private const float Epsilon = 1e-7f;
    
    /// <inheritdoc/>
    public void Print()
    {
        Console.Write("(");
        LeftSon?.Print();
        Console.Write(Operation);
        RightSon?.Print();
        Console.Write(")");
    }
    
    /// <inheritdoc/>
    /// <exception cref="DivideByZeroException">Throws when division by zero occurs.</exception>
    /// <exception cref="ArgumentException">Throws when invalid operation was given.</exception>
    public double Calculate()
    {
        if (LeftSon is null || RightSon is null)
        {
            return 0;
        }
        
        var leftSonValue = LeftSon.Calculate();
        var rightSonValue = RightSon.Calculate();
        switch (Operation)
        {
            case "+": return leftSonValue + rightSonValue;
            case "-": return leftSonValue - rightSonValue;
            case "*": return leftSonValue * rightSonValue;
            case "/":
            {
                if (rightSonValue < Epsilon)
                {
                    throw new DivideByZeroException("Division by zero.");
                }
                return leftSonValue / rightSonValue;
            }
            default: throw new ArgumentException("Invalid operaion.");
            
        }
    }
}