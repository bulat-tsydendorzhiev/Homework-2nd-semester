namespace Routers;

/// <summary>
/// Exception that throws when network is disconnected.
/// </summary>
public class DisconnectedNetworkException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DisconnectedNetworkException"> class.
    /// </summary>
    public DisconnectedNetworkException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DisconnectedNetworkException"> class.
    /// </summary>
    public DisconnectedNetworkException(string message) : base(message)
    {
    }
}