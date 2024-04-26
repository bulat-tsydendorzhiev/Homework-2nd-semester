using StackCalculatorTask;

Console.Write("Input expression in postfix form: ");
var expression = Console.ReadLine();

try
{
    var result = StackCalculator.CalculateExpression(expression, new ListStack<double>());
    Console.WriteLine($"Result of calculating = {result}");
}
catch (ArgumentException ex1)
{
    Console.WriteLine(ex1.Message);
}
catch (DivideByZeroException ex2)
{
    Console.WriteLine(ex2.Message);
}

