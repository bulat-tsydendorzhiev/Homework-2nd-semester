namespace UniqueList;

/// <summary>
/// List, an abstract data type that represents an ordered set of values in which a value can appear more than once.
/// </summary>
public class List
{
    private class ListNode
    {
        public int Value{ get; set; }
        
        public ListNode? Next{ get; set; }
        
        public ListNode(int value, ListNode? next)
        {
            Value = value;
            Next = next;
        }
    }
    
    /// <summary>
    /// Amount of elements in list. 
    /// </summary>
    public int Count { get; private set; }
    
    private ListNode? Head;
    
    /// <summary>
    /// Adds element at the end of the list.
    /// </summary>
    /// <param name="value">Adding value.</param>
    public virtual void Add(int value)
    {
        var lastNode = GetListNodeByPosition(Count - 1);
        lastNode = new ListNode(value, lastNode);
        ++Count;
    }
    
    /// <summary>
    /// Inserts an element into the list at the specified index.
    /// </summary>
    /// <param name="position">Position of inserting element.</param>
    /// <param name="value">New value.</param>
    /// <exception cref="IndexOutOfRangeException">Throws when there is no such position in the list.</exception>
    public virtual void Insert(int position, int value)
    {
        if (!IsValidPosition(position) && position != Count)
        {
            throw new IndexOutOfRangeException("Position out of range");
        }
        
        ++Count;
        
        if (position == 0)
        {
            Head = new ListNode(value, Head);
            return;
        }
        
        var previous = GetListNodeByPosition(position - 1);
        var current = previous.Next;
        ListNode newNode = new(value, current);
        previous.Next = newNode;
    }
    
    /// <summary>
    /// Removes the element at the specified position of the list.
    /// </summary>
    /// <param name="position">Position of deleting element.</param>
    /// <exception cref="IndexOutOfRangeException">Throws when there is no such position in the list.</exception>
    public void RemoveAt(int position)
    {
        if (!IsValidPosition(position))
        {
            throw new IndexOutOfRangeException("Position out of range");
        }
        
        --Count;
        
        if (position == 0)
        {
            Head = Head.Next;
            return;
        }
        
        var previous = GetListNodeByPosition(position - 1);
        var current = previous.Next;
        previous.Next = current.Next;
    }
    
    /// <summary>
    /// Changes the value by specified position.
    /// </summary>
    /// <param name="position">Changing position.</param>
    /// <param name="value">New value.</param>
    /// <exception cref="IndexOutOfRangeException">Throws when there is no such position in the list.</exception>
    public virtual void ChangeValueByPosition(int position, int value)
    {
        if (!IsValidPosition(position))
        {
            throw new IndexOutOfRangeException("Position out of range");
        }
        
        var node = GetListNodeByPosition(position);
        node.Value = value;
    }
    
    /// <summary>
    /// Gets value by specified position.
    /// </summary>
    /// <param name="position">Specified position.</param>
    /// <returns>The value on the specified position.</returns>
    /// <exception cref="IndexOutOfRangeException">Throws when there is no such position in the list.</exception>
    public int GetValueByPosition(int position)
    {
        if (!IsValidPosition(position))
        {
            throw new IndexOutOfRangeException("Position out of range");
        }
        
        var node = GetListNodeByPosition(position);
        
        return node.Value;
    }
    
    /// <summary>
    /// Gets position by specified value.
    /// </summary>
    /// <param name="value">Specified value.</param>
    /// <returns>Position of the specified value if it exists; otherwise -1.</returns>
    public int GetPositionByValue(int value)
    {
        var current = Head;
        int position = 0;
        
        while (current is not null)
        {
            if (current.Value == value)
            {
                return position;
            }
            current = current.Next;
            ++position;
        }
        
        return -1;
    }
    
    /// <summary>
    /// Determines whether an element is in the list.
    /// </summary>
    /// <param name="value">The value to locate in the list.</param>
    /// <returns>true if the value is found in the list; otherwise false.</returns>
    public bool Contains(int value)
    {
        var position = GetPositionByValue(value);
        return position != -1;
    }
    
    private bool IsValidPosition(int position) => position >= 0 && position < Count;
    
    private ListNode? GetListNodeByPosition(int position)
    {
        var current = Head;
        
        for (int i = 0; i < position; ++i)
        {
            current = current.Next;
        }
        
        return current;
    }
}
