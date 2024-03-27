namespace ParseTree;

/// <summary>
/// Parse tree, class to calculate and print expression written in prefix form.
/// </summary>
public class ParseTree
{
    private INode? Root;
    
    private int CurrentIndex;
    
    /// <summary>
    /// Build Parse tree.
    /// </summary>
    /// <param name="expression"></param>
    /// <exception cref="ArgumentException">Throws when invalid input expression was given.</exception>
    public void Build(string? expression)
    {
        ArgumentNullException.ThrowIfNull(expression);
        
        if (!HasParenthesesBalance(expression))
        {
            throw new ArgumentException("Invalid input expression was given.");
        }
        
        var expressionItems = expression.Split(new char[] {' ', '(', ')'}, StringSplitOptions.RemoveEmptyEntries);
        if (expressionItems.Length == 0)
        {
            throw new ArgumentException("Invalid input expression was given.");
        }
        
        Root = BuildTree(expressionItems);
        if (CurrentIndex != expressionItems.Length)
        {
            throw new ArgumentException("Invalid input expression was given.");
        }
    }
    
    /// <summary>
    /// Calculates Parse tree.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">Throws when tree wasn't built.</exception>
    /// <exception cref="ArgumentException">Throws when expression contains division by zero.</exception>
    public float Calculate()
    {
        if (Root is null)
        {
            throw new InvalidOperationException("Tree wasn't built.");
        }
        
        float result;
        
        try
        {
            result = Root.Calculate();
        }
        catch(DivideByZeroException)
        {
            throw new ArgumentException("Expression contains division by zero.");
        }
        
        return result;
    }
    
    /// <summary>
    /// Prints expression.
    /// </summary>
    public void Print()
    {
        Root?.Print();
    }
    
    private INode? BuildTree(string[] expressionItems)
    {
        if (CurrentIndex == expressionItems.Length)
        {
            return null;
        }
        
        string item = expressionItems[CurrentIndex];
        ++CurrentIndex;
        
        if (IsOperation(item))
        {
            var newOperator = new Operator(item)
            {
                LeftSon = BuildTree(expressionItems),
                RightSon = BuildTree(expressionItems)
            };
            
            if (newOperator.LeftSon is null || newOperator.RightSon is null)
            {
                throw new ArgumentException("Invalid input expression was given.");
            }

            return newOperator;
        }
        else
        {
            if (!int.TryParse(item, out int number))
            {
                throw new ArgumentException("Invalid input expression was given.");
            }
            
            var newOperand = new Operand(number);
            
            return newOperand;
        }
    }
    
    private static bool IsOperation(string item) => "+-*/".Contains(item);
    
    private static bool HasParenthesesBalance(string expression)
    {
        int counterOfOpeningParentheses = 0;
        
        foreach (var item in expression)
        {
            if (item == '(')
            {
                ++counterOfOpeningParentheses;
            }
            else if (item == ')')
            {
                if (counterOfOpeningParentheses == 0)
                {
                    return false;
                }
                --counterOfOpeningParentheses;
            }
        }
        
        return counterOfOpeningParentheses == 0;
    }
}