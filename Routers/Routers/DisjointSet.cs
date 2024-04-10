namespace Routers;

public class DisjointSet
{
    private SetElement[] _setElements;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="capacity">Max number of elements in </param>
    public DisjointSet(int capacity)
    {
        _setElements = new SetElement[capacity];
        for (int i = 0; i < capacity; ++i)
        {
            _setElements[i] = new() { Parent = i, Rank = 0 };
        }
    }

    public int Find(int element)
    {
        if (_setElements[element].Parent != element)
        {
            _setElements[element].Parent = Find(_setElements[element].Parent);
        }
        
        return _setElements[element].Parent;
    }

    public void Unite(int firstElement, int secondElement)
    {
        firstElement = Find(firstElement);
        secondElement = Find(secondElement);
        
        if (firstElement != secondElement)
        {
            if (_setElements[firstElement].Rank < _setElements[secondElement].Rank)
            {
                (firstElement, secondElement) = (secondElement, firstElement);
            }

            _setElements[secondElement].Parent = firstElement;

            if (_setElements[firstElement].Rank == _setElements[secondElement].Rank)
            {
                ++_setElements[firstElement].Rank;
            }
        }
    }

    private class SetElement
    {
        public int Parent {get; set; }
        
        public int Rank {get; set; }
    }
}