namespace UniqueList;

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