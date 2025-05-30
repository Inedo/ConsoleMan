namespace ConsoleMan;

/// <summary>
/// Interface used to configure an option.
/// </summary>
public interface ICommandBuilder
{
    /// <summary>
    /// Adds the specified option as supported by the current command.
    /// </summary>
    /// <typeparam name="TOption">The option type.</typeparam>
    /// <param name="overrides">Optional <see cref="OptionOverrides"/> instance used to override option metadata.</param>
    /// <returns>The current <see cref="ICommandBuilder"/> instance.</returns>
    ICommandBuilder WithOption<TOption>(OptionOverrides? overrides) where TOption : IConsoleOption;
    /// <summary>
    /// Adds the specifiec command as a subcommand supported by the current command.
    /// </summary>
    /// <typeparam name="TCommand">The subcommand type.</typeparam>
    /// <returns>The current <see cref="ICommandBuilder"/> instance.</returns>
    ICommandBuilder WithCommand<TCommand>() where TCommand : IConsoleCommand;
}
