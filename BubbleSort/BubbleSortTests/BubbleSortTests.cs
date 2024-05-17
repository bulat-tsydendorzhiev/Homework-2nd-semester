using BubbleSort;

namespace BubbleSortTests;

public class Tests
{
    [TestCaseSource(typeof(TestData), nameof(TestData.TestCasesForInteger))]
    public void BubbleSorterForInteger_ShouldSort_WithCorrectCases(IList<int> list)
    {
        BubbleSorter.Sort<int>(list, new AscendingComparer());

        Assert.That(list.ToList(), Is.EqualTo(list.Order().ToList()));
    }

    [TestCaseSource(typeof(TestData), nameof(TestData.TestCasesForStrings))]
    public void BubbleSorterForStrings_ShouldSort_WithCorrectCases(IList<string> list)
    {
        BubbleSorter.Sort<string>(list, new StringAscendingComparer());

        Assert.That(list.ToList(), Is.EqualTo(list.Order().ToList()));
    }

    [Test]
    public void BubbleSorterForStrings_ShouldThrow_ArgumentNullException_WithInCorrectData()
    {
        Assert.Throws<ArgumentNullException>(() => BubbleSorter.Sort<int>([5, 4, 3, 2, 1], null));
        Assert.Throws<ArgumentNullException>(() => BubbleSorter.Sort<int>(null, new AscendingComparer()));
    }

    private class AscendingComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            return x > y ? 1 : x < y ? -1 : 0;
        }
    }
    
    private class StringAscendingComparer : IComparer<string>
    {
        public int Compare(string str1, string str2)
        {
            int length = Math.Min(str1.Length, str2.Length);
            
            for (int i = 0; i < length; i++)
            {
                if (str1[i] > str2[i])
                {
                    return 1;
                }
                else if (str1[i] < str2[i])
                {
                    return -1;
                }
            }

            return str1.Length == str2.Length ? 0: str1.Length == length ? 1 : -1;
        }
    }
}

public class TestData
{
    public static List<int>[] TestCasesForInteger =
    {
        [5, 4, 3, 2, 1],
        [1, 1, 1, 1, 1],
        [1],
        [1, 2, 3, 4, 5]
    };

    public static List<string>[] TestCasesForStrings =
    {
        ["cow", "math, mathematician", "ololo", "branch"],
        ["math-mech is the best!"],
        ["ololo", "ololo", "ololo", "ololo", "ololo"],
        ["a", "b", "c", "d", "e"]
    };
}