namespace ConsoleMan;

/// <summary>
/// Contains helper methods for <see cref="ICommandBuilder"/>.
/// </summary>
public static class CommandBuilderExtensions
{
    /// <summary>
    /// Adds the specified <typeparamref name="TOption"/> option to the <see cref="ICommandBuilder"/>.
    /// </summary>
    /// <typeparam name="TOption">Type of the option.</typeparam>
    /// <param name="builder"><see cref="ICommandBuilder"/> instance.</param>
    /// <returns><see cref="ICommandBuilder"/> instance.</returns>
    public static ICommandBuilder WithOption<TOption>(this ICommandBuilder builder) where TOption : IConsoleOption => builder.WithOption<TOption>(null);
    /// <summary>
    /// Adds the specified <typeparamref name="TOption"/> option to the <see cref="ICommandBuilder"/>.
    /// </summary>
    /// <typeparam name="TOption">Type of the option.</typeparam>
    /// <param name="builder"><see cref="ICommandBuilder"/> instance.</param>
    /// <param name="required">Override value for the <see cref="IConsoleOption.Required"/> property.</param>
    /// <returns><see cref="ICommandBuilder"/> instance.</returns>
    public static ICommandBuilder WithOption<TOption>(this ICommandBuilder builder, bool required) where TOption : IConsoleOption => builder.WithOption<TOption>(new OptionOverrides(required));
}
