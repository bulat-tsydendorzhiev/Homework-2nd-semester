using StackCalculator;

namespace StackCalculatorTests;

public class StackCalculatorTests
{
    private double Epsilon = 1e-7;

    [TestCase("1 1 +", 2)]
    [TestCase("-1 -1 +", -2)]
    [TestCase("1 -1 +", 0)]
    [TestCase("1 -1 -", 2)]
    [TestCase("5 -2 *", -10)]
    [TestCase("5 2 /", 2.5)]
    [TestCase("-5 2 /", -2.5)]
    [TestCase("123 124 * 5 /", 3050.4)]
    public void CalculatingValidExpressionShouldReturnRightAnswer(string expression, double rightAnswer)
    {
        double arrayStackAnswer = StackCalculator.StackCalculator.CalculateExpression(expression, new ArrayStack());
        double listStackAnswer = StackCalculator.StackCalculator.CalculateExpression(expression, new ListStack());

        Assert.That(Math.Abs(rightAnswer - arrayStackAnswer), Is.LessThan(Epsilon));
        Assert.That(Math.Abs(rightAnswer - listStackAnswer), Is.LessThan(Epsilon));
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase("1 1 1")]
    [TestCase("/ / /")]
    [TestCase("1 1 / /")]
    [TestCase("1 1 %")]
    [TestCase("Just some words")]
    public void CalculatingInvalidExpressionShouldThrowArgumentException(string? expression)
    {
        Assert.Throws<ArgumentException>(() => StackCalculator.StackCalculator.CalculateExpression(expression, new ArrayStack()));
        Assert.Throws<ArgumentException>(() => StackCalculator.StackCalculator.CalculateExpression(expression, new ListStack()));
    }

    [TestCase("1 0 /")]
    [TestCase("1 -1 1 + /")]
    public void CalculatingExpressionWithDivisionByZeroShouldThrowDivideByZeroException(string expression)
    {
        Assert.Throws<DivideByZeroException>(() => StackCalculator.StackCalculator.CalculateExpression(expression, new ListStack()));
        Assert.Throws<DivideByZeroException>(() => StackCalculator.StackCalculator.CalculateExpression(expression, new ArrayStack()));
    }
}