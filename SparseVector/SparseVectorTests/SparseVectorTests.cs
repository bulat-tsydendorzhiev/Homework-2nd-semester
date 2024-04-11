using TestWork;

namespace SparseVectorTests;

public class Tests
{
    private int[] testVector = new int[] { 0, 0, 0, 0, 10, 123, 0, 0, 12};
    
    [TestCaseSource(typeof(TestDataClass), nameof(TestDataClass.TestCasesForCorrectAddition))]
    public void AdditionMethod_WithCorrectVector_ShouldReturn_RightResultOfAddition(int[] additionVector, int[] expectedVector)
    {
        var sparseVector = new SparseVector(testVector);
        
        Assert.That(IsCorrectAnswer(sparseVector.MakeAddition(additionVector), expectedVector), Is.True);
    }
    
    [TestCaseSource(typeof(TestDataClass), nameof(TestDataClass.TestCasesForCorrectSubstraction))]
    public void SubstractionMethod_WithCorrectVector_ShouldReturn_RightResultOfAddition(int[] substractionVector, int[] expectedVector)
    {
        var sparseVector = new SparseVector(testVector);
        
        Assert.That(IsCorrectAnswer(sparseVector.MakeSubstraction(substractionVector), expectedVector), Is.True);
    }
    
    [TestCaseSource(typeof(TestDataClass), nameof(TestDataClass.TestCasesForCorrectMultiplication))]
    public void MultiplicationMethod_WithCorrectVector_ShouldReturn_RightResultOfAddition(int[] multiplicationVector, int[] expectedVector)
    {
        var sparseVector = new SparseVector(testVector);
        
        Assert.That(IsCorrectAnswer(sparseVector.MakeMultiplication(multiplicationVector), expectedVector), Is.True);
    }
    
    [TestCaseSource(typeof(TestDataClass), nameof(TestDataClass.TestCasesForArgumentExceptionThrowing))]
    public void AdditionMethod_ShouldThrow_ArgumentException_WithIncorrectVector(int[] incorrectVector)
    {
        var sparseVector = new SparseVector(testVector);
        
        Assert.Throws<ArgumentException>(() => sparseVector.MakeAddition(incorrectVector));
    }
    
    [TestCaseSource(typeof(TestDataClass), nameof(TestDataClass.TestCasesForArgumentExceptionThrowing))]
    public void SubstractionMethod_ShouldThrow_ArgumentException_WithIncorrectVector(int[] incorrectVector)
    {
        var sparseVector = new SparseVector(testVector);
        
        Assert.Throws<ArgumentException>(() => sparseVector.MakeSubstraction(incorrectVector));
    }
    
    [TestCaseSource(typeof(TestDataClass), nameof(TestDataClass.TestCasesForArgumentExceptionThrowing))]
    public void MultiplicationMethod_ShouldThrow_ArgumentException_WithIncorrectVector(int[] incorrectVector)
    {
        var sparseVector = new SparseVector(testVector);
        
        Assert.Throws<ArgumentException>(() => sparseVector.MakeMultiplication(incorrectVector));
    }
    
    [Test]
    public void IsNullVector_ShouldReturn_True_WithNullVector()
    {
        var sparseVector = new SparseVector(new int[] {0, 0, 0, 0, 0});
        
        Assert.That(sparseVector.IsNullVector(), Is.True);
    }
    
    [Test]
    public void IsNullVector_ShouldReturn_False_WithNotNullVector()
    {
        var sparseVector = new SparseVector(new int[] {0, 0, 1, 0, 0});
        
        Assert.That(sparseVector.IsNullVector(), Is.False);
    }
    
    public bool IsCorrectAnswer(int[] resultVector, int[] expectedVector)
    {
        for (int i = 0; i < resultVector.Length; ++i)
        {
            if (resultVector[i] != expectedVector[i])
            {
                return false;
            }
        }
        
        return true;
    }
}

public class TestDataClass
{
    public static object[] TestCasesForCorrectAddition =
    {
        new object[] { new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0}, new int[] { 0, 0, 0, 0, 10, 123, 0, 0, 12} },
        new object[] { new int[] { 0, 0, 0, 0, 10, 123, 0, 0, 12}, new int[] { 0, 0, 0, 0, 20, 246, 0, 0, 24} }
    };
    
    public static object[] TestCasesForCorrectSubstraction =
    {
        new object[] { new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0}, new int[] { 0, 0, 0, 0, 10, 123, 0, 0, 12} },
        new object[] { new int[] { 0, 0, 0, 0, 10, 123, 0, 0, 12}, new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0} },
        new object[] { new int[] { 0, 0, 0, 0, -10, -123, 0, 0, -12}, new int[] { 0, 0, 0, 0, 20, 246, 0, 0, 24} }
    };
    
    public static object[] TestCasesForCorrectMultiplication =
    {
        new object[] { new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0}, new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0} },
        new object[] { new int[] { 0, 0, 0, 0, 10, 123, 0, 0, 12}, new int[] { 0, 0, 0, 0, 100, 15129, 0, 0, 144} }
    };
    
    public static object[] TestCasesForArgumentExceptionThrowing =
    {
        new int[0],
        new int[] {0, 0, 0}
    };
}