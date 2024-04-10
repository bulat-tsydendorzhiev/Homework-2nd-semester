namespace Routers;

/// <summary>
/// Exception that throws when file with incorrect format was given.
/// </summary>
public class IncorrectFormatOfFileException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IncorrectFormatOfFileException"> class.
    /// </summary>
    public IncorrectFormatOfFileException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IncorrectFormatOfFileException"> class.
    /// </summary>
    public IncorrectFormatOfFileException(string message) : base(message)
    {
    }
}