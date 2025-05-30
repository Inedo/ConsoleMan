namespace ConsoleMan;

/// <summary>
/// Recognized command or subcommand of a console program.
/// </summary>
public interface IConsoleCommand : IConsoleArgument
{
    /// <summary>
    /// Gets a value indicating whether addtional unparsed options are allowed for this command.
    /// </summary>
    static virtual bool AllowAdditionalOptions => false;
    /// <summary>
    /// Gets example text to display for the command.
    /// </summary>
    static virtual string? Examples => null;

    /// <summary>
    /// Executes the command.
    /// </summary>
    /// <param name="context">The current context.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Command exit code.</returns>
    static abstract Task<int> ExecuteAsync(CommandContext context, CancellationToken cancellationToken);
    /// <summary>
    /// Configures the command on initialization.
    /// </summary>
    /// <param name="builder">Command builder.</param>
    static abstract void Configure(ICommandBuilder builder);
}
