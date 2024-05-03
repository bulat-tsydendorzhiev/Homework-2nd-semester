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
    public void Insert_ShouldThrow_NotSupportedException()
    {
        Assert.Throws<NotSupportedException>(() => _skipList.Insert(0, 1));
    }

    [Test]
    public void SetValueByIndex_ShouldThrow_NotSupportedException()
    {
        _skipList.Add(0);
        Assert.Throws<NotSupportedException>(() => _skipList[0] = 1);
    }

    [TestCase(int.MaxValue)]
    [TestCase(-1)]
    public void GetValue_WithInvalidIndex_ShouldThrow_ArgumentOutOfRangeException(int index)
    {
        _skipList.Add(0);
        Assert.Throws<ArgumentOutOfRangeException>(() => _ = _skipList[index]);
    }

    [Test]
    public void EnumeratorMethods_AfterChangingSkipList_ShouldThrow_InvalidOperationException()
    {
        _skipList.Add(0);
        _skipList.Add(1);
        _skipList.Add(2);

        var enumerator = _skipList.GetEnumerator();

        enumerator.MoveNext();
        _skipList.Add(4);

        Assert.Throws<InvalidOperationException>(() => enumerator.MoveNext());
        Assert.Throws<InvalidOperationException>(() => enumerator.Reset());
    }

    [Test]
    public void RemovedElement_ShouldNotBeInSkipList()
    {
        FillSkipList([0, 1, 2, 3, 4]);

        _skipList.Remove(0);

        Assert.That(_skipList, Does.Not.Contain(0));
    }

    [Test]
    public void CopyTo_ShouldThrow_RelevantExceptions_WithInvalidData()
    {
        FillSkipList([0, 1, 2, 3, 4]);
        
        Assert.Multiple(() => 
        {
            Assert.Throws<ArgumentNullException>(() => _skipList.CopyTo(null, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => _skipList.CopyTo([1, 2, 3, 4], 123));
            Assert.Throws<ArgumentException>(() => _skipList.CopyTo([0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10], 7));
        });
    }

    [TestCaseSource(typeof(TestDataClass), nameof(TestDataClass.AddingValues))]
    public void CopyTo_ShouldCopyValues_InAscendingOrder(List<int> values)
    {
        FillSkipList(values);

        var array = new int[values.Count];
        _skipList.CopyTo(array, 0);

        Assert.That(array, Is.EqualTo(values.Order().ToArray()));
    }

    [TestCaseSource(typeof(TestDataClass), nameof(TestDataClass.AddingValues))]
    public void Clear_ShouldMakeSkipList_Empty(List<int> values)
    {
        FillSkipList(values);

        _skipList.Clear();

        Assert.That(_skipList.Count == 0 && values.All(value => !_skipList.Contains(value)), Is.True);
    }

    [TestCaseSource(typeof(TestDataClass), nameof(TestDataClass.AddingValues))]
    public void AddedValues_ShouldBeContained_InSkipList(List<int> values)
    {
        FillSkipList(values);

        Assert.That(values.All(value => _skipList.Contains(value)), Is.True);
    }

    [TestCaseSource(typeof(TestDataClass), nameof(TestDataClass.AddingValues))]
    public void SkipListValues_ShouldBeInAscendingOrder_AfterAddingValues(List<int> values)
    {
        FillSkipList(values);

        List<int> result = [];

        foreach (var item in _skipList)
        {
            result.Add(item);
        }

        Assert.That(result, Is.EqualTo(values.Order().ToList()));
    }

    private void FillSkipList(List<int> values)
    {
        foreach (var item in values)
        {
            _skipList.Add(item);
        }
    }
}

public class TestDataClass
{
    public static List<int>[] AddingValues = 
    {
        [1, 2, 3, 4, 5, 6, 7, 8, 9, 10],
        [10, 9, 8, 7, 6, 5, 4, 3, 2, 1],
        [0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
        [123, 36, 12, -142, -640]
    };
}