using PriorityQueueTask;

namespace PriorityQueueTests;

public class Tests
{
    [Test]
    public void DequeueFromEmptyPriorityQueue_ShouldThrowInvalidOperationException()
    {
        var queue = new PriorityQueue();
        
        Assert.Throws<InvalidOperationException>(() => queue.Dequeue());
    }

    [Test]
    public void PropertyEmpty_ShouldReturnTrueWithEmptyPriorityQueueQueue()
    {
        var queue = new PriorityQueue();
        
        Assert.That(queue.Empty, Is.True);
    }

    [Test]
    public void PropertyEmpty_ShouldReturnFalseWithNonEmptyPriorityQueueQueue()
    {
        var queue = new PriorityQueue();

        queue.Enqueue(0, 1);
        
        Assert.That(queue.Empty, Is.False);
    }

    [TestCaseSource(typeof(TestDataClass), nameof(TestDataClass.TestCases))]
    public void DequeueShouldReturnExpectedValues(List<(int Priority, int Value)> addingElements)
    {
        var queue = new PriorityQueue();
        
        for (int i = 0; i < addingElements.Count; i++)
        {
            queue.Enqueue(addingElements[i].Priority, addingElements[i].Value);
        }
        
        var result = new List<int>();
        for (int i = 0; i < addingElements.Count; i++)
        {
            result.Add(queue.Dequeue());
        }
        
        Assert.That(result, Is.EqualTo(addingElements.OrderByDescending(x => x.Priority).Select(x => x.Value).ToList()));
    }
}

public class TestDataClass
{
    public static object[] TestCases =
    {
        new List<(int, int)> {(1, 11), (2, 33), (3, 22), (4, 44), (5, 55)},
        new List<(int, int)> {(123, 123), (-235, 12415), (56, 0), (3, 2)},
        new List<(int, int)> {(1, 2), (1, 3), (2, 3), (2, 4)}
    };
}