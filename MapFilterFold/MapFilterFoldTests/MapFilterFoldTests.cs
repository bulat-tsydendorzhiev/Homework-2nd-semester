using MapFilterFold;

namespace MapFilterFoldTests;

public class Tests
{
    [TestCaseSource(typeof(TestDataClass), nameof(TestDataClass.MapCasesWithOneTypeOfValues))]
    public void MapFunction_WithOneTypeOfValue_ShouldReturn_ExpectedList_WithElementsOfTheSameType(
        List<int> list, Func<int, int> func, List<int> expectedList)
    {
        Assert.That(Functions.Map<int, int>(list, func), Is.EqualTo(expectedList));
    }
    
    [TestCaseSource(typeof(TestDataClass), nameof(TestDataClass.MapCasesWithTwoTypeOfValues))]
    public void MapFunction_WithTwoTypeOfValue_ShouldReturn_ExpectedList_WithElementsOfTheSecondType(
        List<int> list, Func<int, string> func, List<string> expectedList)
    {
        Assert.That(Functions.Map<int, string>(list, func), Is.EqualTo(expectedList));
    }
    
    [TestCaseSource(typeof(TestDataClass), nameof(TestDataClass.FilterCases))]
    public void FilterFunction_ShouldReturn_ExpectedList(List<int> list, Func<int, bool> func, List<int> expectedList)
    {
        Assert.That(Functions.Filter<int>(list, func), Is.EqualTo(expectedList));
    }
    
    [TestCaseSource(typeof(TestDataClass), nameof(TestDataClass.FoldCasesWithOneTypeOfValues))]
    public void FoldFunctionWithOneTypeOfValue_ShouldReturn_RightAccumulatedValue(
        List<int> list, int startValue, Func<int, int, int> func, int expectedResult)
    {
        Assert.That(Functions.Fold<int, int>(list, startValue, func), Is.EqualTo(expectedResult));
    }
    
    [TestCaseSource(typeof(TestDataClass), nameof(TestDataClass.FoldCasesWithTwoTypeOfValues))]
    public void FoldFunctionWithTwoTypeOfValue_ShouldReturn_RightAccumulatedValue(
        List<int> list, string startValue, Func<int, string, string> func, string expectedResult)
    {
        Assert.That(Functions.Fold<int, string>(list, startValue, func), Is.EqualTo(expectedResult));
    }
}

public class TestDataClass
{
    public static object[] MapCasesWithOneTypeOfValues =
    {
        new object[] { new List<int> {1, 2, 3}, (int x) => x * 2, new List<int> {2, 4, 6} },
        new object[] { new List<int> {1, 2, 3}, (int x) => x, new List<int> {1, 2, 3} },
        new object[] { new List<int>(), (int x) => x * 123, new List<int>() }
    };
    
    public static object[] MapCasesWithTwoTypeOfValues =
    {
        new object[] { new List<int> {1, 2, 3}, (int x) => x.ToString() + " sheep", new List<string>() {"1 sheep", "2 sheep", "3 sheep"} },
        new object[] { new List<int>(), (int x) => x.ToString() + " sheep", new List<string>() }
    };
    
    public static object[] FilterCases =
    {
        new object[] { new List<int> {1, 2, 3}, (int x) => x % 2 == 0, new List<int> {2} },
        new object[] { new List<int>(), (int x) => x > 0, new List<int>() },
        new object[] { new List<int> {1, 2, 3}, (int x) => x < 0, new List<int>() }
    };
    
    public static object[] FoldCasesWithOneTypeOfValues =
    {
        new object[] { new List<int> {1, 2, 3, 4, 5, 6, 7, 8, 9}, 1, (int x, int acc) => x * acc, 362880},
        new object[] { new List<int> {1, 2, 3}, 0, (int x, int acc) => x - acc, 2 },
        new object[] { new List<int>(), 123, (int x, int acc) => acc - x, 123 }
    };
    
    public static object[] FoldCasesWithTwoTypeOfValues =
    {
        new object[] {new List<int> {1, 2, 3}, "", (int x, string acc) => acc + x.ToString(), "123"},
        new object[] {new List<int> {1, 2}, "Mat-mech", (int x, string acc) => x % 2 == 0 ? $"{acc} is the best" : acc, "Mat-mech is the best" }
    };
}
