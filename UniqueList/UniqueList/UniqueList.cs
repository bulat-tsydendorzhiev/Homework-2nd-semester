namespace UniqueList;

/// <summary>
/// Unique list, an abstract data type that represents an ordered set of values in which a value can appear once.
/// </summary>
public class UniqueList : List
{
    /// <inheritdoc/>
    /// <exception cref="ExistingValueException">Throws when there is such value in the list.</exception>
    public override void Add(int value)
    {
        if (Contains(value))
        {
            throw new ExistingValueException("Such a value is already in the list.");
        }
        
        base.Add(value);
    }

    /// <inheritdoc/>
    /// <exception cref="ExistingValueException">Throws when there is such value in the list.</exception>
    public override void Insert(int position, int value)
    {
        if (Contains(value))
        {
            throw new ExistingValueException("Such a value is already in the list.");
        }
        
        base.Insert(position, value);
    }
    
    /// <summary>
    /// Removes value from list.
    /// </summary>
    /// <param name="value">Deleting value.</param>
    /// <exception cref="NonExistingValueException">Throws when there is no such value in the list.</exception>
    public void Remove(int value)
    {
        if (!Contains(value))
        {
            throw new NonExistingValueException("Such a value is not in list.");
        }
        
        RemoveAt(GetPositionByValue(value));
    }
    
    /// <inheritdoc/>
    /// <exception cref="ExistingValueException">Throws when there is such value in the list.</exception>
    public override void ChangeValueByPosition(int position, int value)
    {
        if (Contains(value))
        {
            if (GetValueByPosition(position) == value)
            {
                return;
            }
            throw new ExistingValueException("Such a value is already in the list.");
        }
        
        base.ChangeValueByPosition(position, value);
    }
}