namespace UniqueListTests;

public class UniqueListTests
{
    private int[] AddingValues = [2, 1, 124, 84, 26, 62, 35, 777];
    
    [Test]
    public void AddingExistingValueShouldThrowExistingValueException()
    {
        UniqueList.UniqueList list = InitList(AddingValues);
        
        Assert.Throws<UniqueList.ExistingValueException>(() => list.Add(2));
    }
    
    [Test]
    public void InsertingExistingValueShouldThrowExistingValueException()
    {
        UniqueList.UniqueList list = InitList(AddingValues);
        
        Assert.Throws<UniqueList.ExistingValueException>(() => list.Insert(0, 124));
    }
    
    [Test]
    public void RemovingNonExistingValueShouldThrowNonExistingValueException()
    {
        UniqueList.UniqueList list = InitList(AddingValues);
        
        Assert.Throws<UniqueList.NonExistingValueException>(() => list.Remove(123));
    }
    
    [Test]
    public void ChangingExistingValueShouldThrowExistingValueException()
    {
        UniqueList.UniqueList list = InitList(AddingValues);
        
        Assert.Throws<UniqueList.ExistingValueException>(() => list.ChangeValueByPosition(0, 124));
    }
    
    [Test]
    public void ChangingValueWithTheSameValueByPositionShouldNotChangeList()
    {
        UniqueList.UniqueList list = InitList(AddingValues);
        
        list.ChangeValueByPosition(0, 2);
        
        Assert.That(AreEqual(list, AddingValues), Is.True);
    }
    
    private static UniqueList.UniqueList InitList(int[] addingValues)
    {
        UniqueList.UniqueList newList = new();
        
        foreach (var item in addingValues)
        {
            newList.Add(item);
        }
        
        return newList;
    }
    
    private static bool AreEqual(UniqueList.UniqueList list, int[] expectedList)
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