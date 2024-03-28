using UniqueList;

namespace UniqueListTests;

public class ListTests
{
    private int[] AddingValues = [4, 2, 2, 12, -2, 0];
    
    [Test]
    public void AddedValuesShouldBeInListAndInRightOrder()
    {
        var list = InitList(AddingValues);
        
        var containingResults = new bool[AddingValues.Length];
        for (var i = 0; i < AddingValues.Length; ++i)
        {
            containingResults[i] = list.Contains(AddingValues[i]);
        }
        
        Assert.That(containingResults.All((contains) => contains), Is.True);
        Assert.That(AreEqual(list, AddingValues), Is.True);
    }
    
    [Test]
    public void InsertedValuesShouldBeInRightOrder()
    {
        UniqueList.List list = new();
        
        List<(int, int)> insertingPairs = [(0, 1), (0, 2), (1, 4), (2, 15), (1, 26), (4, 5), (0, 1)];
        int[] expectedList = [1, 2, 26, 4, 15, 5, 1];
        
        foreach (var (position, value) in insertingPairs)
        {
            list.Insert(position, value);
        }
        
        var containingResults = new bool[expectedList.Length];
        for (var i = 0; i < expectedList.Length; ++i)
        {
            containingResults[i] = list.Contains(expectedList[i]);
        }

        Assert.Multiple(() =>
        {
            Assert.That(containingResults.All((contains) => contains), Is.True);
            Assert.That(AreEqual(list, expectedList), Is.True);
        });
    }

    [Test]
    public void RemovedPositionsValuesShouldNotBeInList()
    {
        var list = InitList(AddingValues);
        
        int[] removingPositions = [5, 3, 2];
        int[] expectedList = [4, 2, -2];
        
        foreach (var position in removingPositions)
        {
            list.RemoveAt(position);
        }

        Assert.Multiple(() =>
        {
            Assert.That(list.Count, Is.EqualTo(AddingValues.Length - removingPositions.Length));
            Assert.That(AreEqual(list, expectedList), Is.True);
        });
    }

    [Test]
    public void ChangedValuesByPositionsShouldBeInList()
    {
        var list = InitList(AddingValues);
        
        List<(int, int)> changingPositionsAndValues = [(0, 1), (2, 3), (4, 123123), (5, 9876)];
        
        foreach (var (position, newValue) in changingPositionsAndValues)
        {
            list.ChangeValueByPosition(position, newValue);
        }
        
        int[] expectedList = [1, 2, 3, 12, 123123, 9876];
        
        Assert.That(AreEqual(list, expectedList), Is.True);
    }
    
    [TestCase(-1)]
    [TestCase(int.MaxValue)]
    public void ListMethodsShouldThrowArgumentOutOfRangeExceptionWithInvalidPosition(int invalidPosition)
    {
        var list = InitList(AddingValues);
        
        Assert.Throws<ArgumentOutOfRangeException>(() => list.Insert(invalidPosition, 123));
        Assert.Throws<ArgumentOutOfRangeException>(() => list.RemoveAt(invalidPosition));
        Assert.Throws<ArgumentOutOfRangeException>(() => list.ChangeValueByPosition(invalidPosition, 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => list.GetValueByPosition(invalidPosition));
    }
    
    private static UniqueList.List InitList(int[] addingValues)
    {
        UniqueList.List newList = new();
        
        foreach (var item in addingValues)
        {
            newList.Add(item);
        }
        
        return newList;
    }
    
    private static bool AreEqual(UniqueList.List list, int[] expectedList)
    {
        if (list.Count != expectedList.Length)
        {
            return false;
        }
        
        for (var i = 0; i < expectedList.Length; ++i)
        {
            if (list.GetValueByPosition(i) != expectedList[i])
            {
                return false;
            }
        }
        
        return true;
    }
}