namespace ByteGuard.Codex.Core.Exceptions;

/// <summary>
/// Specific exception type used when a given resource was expected but could not be found.
/// </summary>
public class NotFoundException : Exception
{
    /// <inheritdoc/>
    public NotFoundException()
    {
    }

    /// <inheritdoc/>
    public NotFoundException(string? message) : base(message)
    {
    }

    /// <inheritdoc/>
    public NotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
