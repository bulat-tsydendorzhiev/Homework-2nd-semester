namespace Routers;

/// <summary>
/// Disjoint set, a data structure that allows you to administer a set of elements divided into disjoint subsets.
/// </summary>
public class DisjointSetUnion
{
    private SetElement[] _setElements;

    /// <summary>
    /// Initializes a new instance of the <see cref="DisjointSetUnion"/> class.
    /// </summary>
    /// <param name="capacity">Max number of elements in </param>
    public DisjointSetUnion(int capacity)
    {
        _setElements = new SetElement[capacity];
        for (int i = 0; i < capacity; ++i)
        {
            _setElements[i] = new() { Parent = i, Rank = 0 };
        }
    }

    /// <summary>
    /// Find parents of the element.
    /// </summary>
    /// <param name="element">Specified element.</param>
    /// <returns>Parent of the element.</returns>
    public int Find(int element)
    {
        if (_setElements[element].Parent != element)
        {
            _setElements[element].Parent = Find(_setElements[element].Parent);
        }
        
        return _setElements[element].Parent;
    }

    /// <summary>
    /// Unites two sets containing the first and second elements.
    /// </summary>
    /// <param name="firstElement">Element from first set.</param>
    /// <param name="secondElement">Element from second set.</param>
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