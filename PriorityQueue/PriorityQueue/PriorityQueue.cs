namespace PriorityQueueTask;

/// <summary>
/// Implementation of priority queue via binary heap.
/// </summary>
public class PriorityQueue
{
    private class PriorityQueueElement(int priority, int value)
    {
        public int Priority { get; set; } = priority;

        public int Value { get; set; } = value;
    }

    private readonly List<PriorityQueueElement> _elements = [];

    /// <summary>
    /// Determines whether priority queue is empty.
    /// </summary>
    public bool Empty => _elements.Count == 0;

    /// <summary>
    /// Adds value to queue, considering priority.
    /// </summary>
    /// <param name="priority">Priority of adding value.</param>
    /// <param name="value">Adding value.</param>
    public void Enqueue(int priority, int value)
    {
        var newElement = new PriorityQueueElement(priority, value);
        _elements.Add(newElement);
        
        SiftUp();
    }

    /// <summary>
    /// Gets value with highest priority and remove it from queue.
    /// </summary>
    /// <returns>Value with the highest priority.</returns>
    /// <exception cref="InvalidOperationException">Throws when queue is empty.</exception>
    public int Dequeue()
    {
        if (Empty)
        {
            throw new InvalidOperationException("Priority queue is empty.");
        }

        var value = _elements[0].Value;
        _elements[0] = _elements[^1];
        _elements.RemoveAt(_elements.Count - 1);

        SiftDown();

        return value;
    }

    private void SiftUp()
    {
        int i = _elements.Count - 1;
        
         while (_elements[i].Priority > _elements[(i - 1) / 2].Priority)
         {
            (_elements[i], _elements[(i - 1) / 2]) = (_elements[(i - 1) / 2], _elements[i]);

            i = (i - 1) / 2;
         }
    }
    
    private void SiftDown()
    {
        int i = 0;
        
        while (2 * i + 1 < _elements.Count)
        {
            var left = 2 * i + 1;
            var right = 2 * i + 2;
            
            var j = left;
            
            if (right < _elements.Count && _elements[right].Priority > _elements[left].Priority)
            {
                j = right;
            }
            
            if (_elements[i].Priority >= _elements[j].Priority)
            {
                break;
            }
            
            (_elements[i], _elements[j]) = (_elements[j], _elements[i]);
            
            i = j;
        }
    }
}