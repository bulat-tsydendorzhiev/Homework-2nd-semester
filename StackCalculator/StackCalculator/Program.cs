using StackCalculator;

Console.Write("Input expression in postfix form: ");
var expression = Console.ReadLine();

try
{
    double result = StackCalculator.StackCalculator.CalculateExpression(expression, new ListStack());
    Console.WriteLine($"Result of calculating = {result}");
}
catch (ArgumentException)
{
    Console.WriteLine("Wrong format of expression");
}
catch (DivideByZeroException)
{
    Console.WriteLine("You are not a god to divide by zero");
}

