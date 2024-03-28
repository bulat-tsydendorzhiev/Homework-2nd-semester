namespace UniqueList;

/// <summary>
/// Exception that throws when nonexisting value tries to be deleted from unique list.
/// </summary>
public class NonExistingValueException : Exception
{
    public NonExistingValueException()
    {
    }
    
    public NonExistingValueException(string message)
     : base(message)
    {
    }
}