using Moq;
using StackCalculatorTask;

namespace StackCalculatorTests;

public class StackCalculatorTests
{
    private const double Epsilon = 1e-7;

    [TestCaseSource(typeof(TestDataClass), nameof(TestDataClass.ValidTestCases))]
    public void CalculatingValidExpression_WithArrayStack_ShouldReturn_RightAnswer(string expression, double rightAnswer)
    {
        double arrayStackAnswer = StackCalculator.CalculateExpression(expression, new ArrayStack<double>());

        Assert.That(Math.Abs(rightAnswer - arrayStackAnswer), Is.LessThan(Epsilon));
    }

    [TestCaseSource(typeof(TestDataClass), nameof(TestDataClass.ValidTestCases))]
    public void CalculatingValidExpression_WithListStack_ShouldReturn_RightAnswer(string expression, double rightAnswer)
    {
        double listStackAnswer = StackCalculator.CalculateExpression(expression, new ListStack<double>());

        Assert.That(Math.Abs(rightAnswer - listStackAnswer), Is.LessThan(Epsilon));
    }

    [Test]
    public void CalculatingInvalidExpression_ShouldThrow_ArgumentException()
    {
        Assert.Throws<ArgumentNullException>(() => StackCalculator.CalculateExpression(null, new ArrayStack<double>()));
        Assert.Throws<ArgumentNullException>(() => StackCalculator.CalculateExpression(null, new ListStack<double>()));
    }

    [TestCaseSource(typeof(TestDataClass), nameof(TestDataClass.ArgumentExceptionTestCases))]
    public void CalculatingInvalidExpression_ShouldThrow_ArgumentException(string? expression)
    {
        Assert.Throws<ArgumentException>(() => StackCalculator.CalculateExpression(expression, new ArrayStack<double>()));
        Assert.Throws<ArgumentException>(() => StackCalculator.CalculateExpression(expression, new ListStack<double>()));
    }

    [TestCase("1 0 /")]
    [TestCase("1 -1 1 + /")]
    public void CalculatingExpression_WithDivisionByZero_ShouldThrow_DivideByZeroException(string expression)
    {
        Assert.Throws<DivideByZeroException>(() => StackCalculator.CalculateExpression(expression, new ArrayStack<double>()));
        Assert.Throws<DivideByZeroException>(() => StackCalculator.CalculateExpression(expression, new ListStack<double>()));
    }

    [Test]
    public void TestWithMoq()
    {
        var dependencyMoq = new Mock<IStack<double>>();
        dependencyMoq.Setup(dependency => dependency.IsEmpty()).Returns(true);

        var result = StackCalculator.CalculateExpression("1 2 +", dependencyMoq.Object);

        Assert.That(result, Is.EqualTo(3));

        dependencyMoq.Verify(dependency => dependency.IsEmpty(), Times.Exactly(1));
    }

    private class TestDataClass
    {
        public static object[] ValidTestCases =
        [
            new object[] {"1 1 +", 2},
            new object[] {"-1 -1 +", -2},
            new object[] {"1 -1 +", 0},
            new object[] {"1 -1 -", 2},
            new object[] {"5 -2 *", -10},
            new object[] {"5 2 /", 2.5},
            new object[] {"-5 2 /", -2.5},
            new object[] {"123 124 * 5 /", 3050.4}
        ];

        public static object[] ArgumentExceptionTestCases =
        [
            "",
            "1 1 1",
            "/ / /",
            "1 1 1",
            "1 1 / /",
            "1 1 %",
            "Just some words"
        ];
    }
}