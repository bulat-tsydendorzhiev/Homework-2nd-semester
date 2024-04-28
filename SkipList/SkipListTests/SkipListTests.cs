using SkipListLibrary;

namespace SkipListTests;

public class Tests
{
    private SkipList<int> _skipList = [];

    [TearDown]
    public void CleanUp()
    {
        _skipList = [];
    }

    [Test]
    public void InsertValueToList_ShouldThrowNotSupportedException()
    {
        Assert.Throws<NotSupportedException>(() => _skipList.Insert(0, 0));
    }

    [Test]
    public void SetValueByIndex_ShouldThrowNotSupportedException()
    {
        _skipList.Add(0);
        Assert.Throws<NotSupportedException>(() => _skipList[0] = 1);
    }

    [TestCase(int.MaxValue)]
    [TestCase(-1)]
    public void GetValue_WithInvalidIndex_ShouldThrowArgumentOutOfRangeException(int index)
    {
        _skipList.Add(0);
        Assert.Throws<ArgumentOutOfRangeException>(() => _ = _skipList[index]);
    }

    
}

public class TestDataClass
{
    public static object[] TestCases = 
    {
        
    };
}