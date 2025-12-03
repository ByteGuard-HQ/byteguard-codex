namespace ByteGuard.Codex.Api.Responses;

/// <summary>
/// Default error response.
/// </summary>
public record ErrorResponse
{
    /// <summary>
    /// Error message.
    /// </summary>
    public required string Message { get; set; }
}
