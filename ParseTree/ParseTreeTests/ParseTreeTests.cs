using ParseTree;

namespace ParseTreeTests;

public class Tests
{
    private const float Epsilon = 1e-7f;
    
    [Test]
    public void BuildingNullExpressionShouldThrowArgumentNullException()
    => Assert.Throws<ArgumentNullException>(() => new ParseTree.ParseTree().Build(null));
    
    [Test]
    public void BuildingEmptyExpressionShouldThrowArgumentException()
        => Assert.Throws<ArgumentException>(() => new ParseTree.ParseTree().Build(""));
    
    [TestCaseSource(nameof(InvalidExpressions))]
    public void BuildingEmptyStringShouldThrowArgumentException(string expression)
        => Assert.Throws<ArgumentException>(() => new ParseTree.ParseTree().Build(expression));
    
    [Test]
    public void CalculatingNullExpressionShouldThrowInvalidOperationException()
        => Assert.Throws<InvalidOperationException>(() => new ParseTree.ParseTree().Calculate());
    
    [TestCaseSource(nameof(ExpressionsWithDivizionByZero))]
    public void CalculatingExpressionWithDivizionByZeroShouldThrowArgumentException(string expression)
    {
        ParseTree.ParseTree tree = new();
        tree.Build(expression);
        Assert.Throws<ArgumentException>(() => tree.Calculate());
    }
    
    [TestCaseSource(nameof(ValidExpressionsAndThierCorrectCalculatedResults))]
    public void CalculatingValidExpressionShouldReturnRightAnswer(string expression, double rightAnswer)
    {
        ParseTree.ParseTree tree = new();
        tree.Build(expression);
        Assert.That(Math.Abs(tree.Calculate() - rightAnswer), Is.LessThan(Epsilon));
    }
    
    [Test]
    public void PrintShouldReturnRightAnswer()
    {
        ParseTree.ParseTree tree = new();
        var expression = "(* (+ 1 1) 2)";
        var expected = "((1+1)*2)";
        tree.Build(expression);
        
        using(var output = new StringWriter())
        {
            Console.SetOut(output);
            tree.Print();
            Assert.That(output.ToString(), Is.EqualTo(expected));
        }
    }
    
    private static IEnumerable<TestCaseData> ValidExpressionsAndThierCorrectCalculatedResults
    {
        get
        {
            yield return new TestCaseData("(+ 10 18)", 28);
            yield return new TestCaseData("(+ 1 0)", 1);
            yield return new TestCaseData("(- 1 0)", 1);
            yield return new TestCaseData("(+ 0 0)", 0);
            yield return new TestCaseData("(* 0 0)", 0);
            yield return new TestCaseData("(- 10 18)", -8);
            yield return new TestCaseData("(- -10 10)", -20);
            yield return new TestCaseData("(* (+ 1 1) 2) ", 4);
            yield return new TestCaseData("(/ 16 2)", 8);
            yield return new TestCaseData("(/ 5 2)", 2.5);
            yield return new TestCaseData("(* 5 0)", 0);
            yield return new TestCaseData("(* -5 0)", 0);
        }
    }
    
    private static IEnumerable<TestCaseData> InvalidExpressions
    {
        get
        {
            yield return new TestCaseData("Just a text");
            yield return new TestCaseData("()");
            yield return new TestCaseData("(+ 1 1))");
            yield return new TestCaseData("((+ 1 1)");
            yield return new TestCaseData("(- -10 10 10)");
            yield return new TestCaseData("(* (% 5 1) 2) ");
            yield return new TestCaseData("(- - -)");
            yield return new TestCaseData("5 5 5 5 5 5 5");
            yield return new TestCaseData("(* 12 (+ 7))");
        }
    }
    
    private static IEnumerable<TestCaseData> ExpressionsWithDivizionByZero
    {
        get
        {
            yield return new TestCaseData("(/ 2 0)");
            yield return new TestCaseData("(/ -2 0)");
            yield return new TestCaseData("(/ (+ 1 1) (- 1 1))");
        }
    }
}

