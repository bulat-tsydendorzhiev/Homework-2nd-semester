namespace UniqueList;

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