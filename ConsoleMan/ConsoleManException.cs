namespace ConsoleMan;

/// <summary>
/// Raised when a nonrecoverable error has occurred which is intended to be displayed as console output.
/// </summary>
public class ConsoleManException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConsoleManException"/> class.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="exitCode">The process exit code.</param>
    public ConsoleManException(string? message = null, int exitCode = -1) : base(message)
    {
        this.ExitCode = exitCode;
        this.HasMessage = message is not null;
    }

    /// <summary>
    /// Gets the desired process exit code from the error.
    /// </summary>
    public int ExitCode { get; }
    /// <summary>
    /// Gets a value indicating whether a user-friendly message has been specified.
    /// </summary>
    public bool HasMessage { get; }
}
