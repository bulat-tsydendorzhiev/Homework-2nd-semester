namespace UniqueList;

/// <summary>
/// Exception that throws when existing in unique list value tries to change list.
/// </summary>
public class ExistingValueException : Exception
{
    public ExistingValueException()
    {
    }
    
    public ExistingValueException(string message)
     : base(message)
    {
    }
}