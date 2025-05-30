namespace ConsoleMan;

/// <summary>
/// Console command that is an empty container for other commands.
/// </summary>
public interface IConsoleCommandContainer : IConsoleCommand
{
    static Task<int> IConsoleCommand.ExecuteAsync(CommandContext context, CancellationToken cancellationToken)
    {
        context.WriteUsage();
        return Task.FromResult(1);
    }
}
