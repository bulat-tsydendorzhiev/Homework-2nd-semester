namespace StackCalculator;

/// <summary>
/// Class which has method for calculating an expression written in postfix form.
/// </summary>
public class StackCalculator
{
    private const double Epsilon = 1e-7;

    /// <summary>
    /// Calculate the value of an expression written in postfix form.
    /// </summary>
    /// <param name="expression">Expression written in postfix form.</param>
    /// <param name="stack">Using stack.</param>
    /// <returns>Result of calculating.</returns>
    /// <exception cref="DivideByZeroException">Division by zero.</exception>
    /// <exception cref="ArgumentException">Invalid expression.</exception>
    public static double CalculateExpression(string? expression, IStack stack)
    {
        if (expression == null)
        {
            throw new ArgumentException("Invalid expression");
        }

        var expressionElements = expression.Split();

        foreach (string element in expressionElements)
        {
            if (IsOperation(element))
            {
                try
                {
                    var number2 = stack.Pop();
                    var number1 = stack.Pop();

                    var operationResult = Calculate(number1, number2, element);
                    stack.Push(operationResult);
                }
                catch (DivideByZeroException)
                {
                    throw new DivideByZeroException("Division by zero");
                }
                catch
                {
                    throw new ArgumentException("Invalid expression");
                }
            }
            else
            {
                try
                {
                    var value = int.Parse(element);
                    stack.Push(value);
                }
                catch
                {
                    throw new ArgumentException("Invalid expression");
                }
            }
        }

        double result;

        try
        {
            result = stack.Pop();
        }
        catch (InvalidOperationException)
        {
            throw new ArgumentException("Invalid expression");
        }

        if (!stack.IsEmpty())
        {
            throw new ArgumentException("Invalid expression");
        }

        stack.Clear();

        return result;
    }

    private static bool IsOperation(string text) => text == "+" || text == "-" || text == "*" || text == "/";

    private static double Calculate(double number1, double number2, string operation)
    {
        switch (operation)
        {
            case "+":
                return number1 + number2;
            case "-":
                return number1 - number2;
            case "*":
                return number1 * number2;
            case "/":
                if (Math.Abs(number2) < Epsilon)
                {
                    throw new DivideByZeroException("Division by zero");
                }
                return number1 / number2;
            default:
                throw new ArgumentException("Invalid expression");
        }
    }
}
