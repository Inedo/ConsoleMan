using System.Diagnostics.CodeAnalysis;

namespace ConsoleMan;

/// <summary>
/// Contains information about the currently executing command and its options.
/// </summary>
public sealed class CommandContext
{
    private readonly Dictionary<Type, ParsedOption> options;
    private readonly List<Command> commands;
    private readonly List<string> additionalOptions;

    internal CommandContext(Dictionary<Type, ParsedOption> options, List<Command> commands, List<string> additionalOptions)
    {
        this.options = options;
        this.commands = commands;
        this.additionalOptions = additionalOptions;
    }

    /// <summary>
    /// Gets the <see cref="ConsoleMan.Command"/> instance.
    /// </summary>
    public Command Command => this.commands[^1];
    /// <summary>
    /// Gets the child commands of the current command.
    /// </summary>
    public IReadOnlyList<Command> Commands => this.commands;
    /// <summary>
    /// Gets additional unparsed options to the command.
    /// </summary>
    public IReadOnlyList<string> AdditionalOptions => this.additionalOptions;

    /// <summary>
    /// Tries to read the value of the <typeparamref name="TOption"/> option.
    /// </summary>
    /// <typeparam name="TOption">The option type.</typeparam>
    /// <param name="value">Value of the option if specified.</param>
    /// <returns>True if the option was specified; otherwise false.</returns>
    public bool TryGetOption<TOption>([MaybeNullWhen(false)] out string value) where TOption : IConsoleOption
    {
        value = null;
        if (!this.options.TryGetValue(typeof(TOption), out var o) || o.Value is null)
            return false;

        value = o.Value;
        return true;
    }
    /// <summary>
    /// Tries to read the value of the <typeparamref name="TOption"/> option.
    /// </summary>
    /// <typeparam name="TOption">The option type.</typeparam>
    /// <typeparam name="TValue">Type to parse the option as.</typeparam>
    /// <param name="value">Value of the option if specified.</param>
    /// <returns><see langword="true"/> if the option was specified; otherwise <see langword="false"/>.</returns>
    /// <remarks>
    /// This will throw a <see cref="ConsoleManException"/> if the value was specified but could not be parsed.
    /// </remarks>
    /// <exception cref="ConsoleManException">The value was specified but could not parsed.</exception>
    public bool TryGetOption<TOption, TValue>([MaybeNullWhen(false)] out TValue value) where TOption : IConsoleOption where TValue : IParsable<TValue>
    {
        value = default;

        if (!this.TryGetOption<TOption>(out var s))
            return false;

        if (!TValue.TryParse(s, null, out value))
            throw new ConsoleManException($"{TOption.Name} error: value is not in a valid format");

        return true;
    }
    /// <summary>
    /// Returns the value of the <typeparamref name="TOption"/> option if it is specified; otherwise returns <see langword="null"/>.
    /// </summary>
    /// <typeparam name="TOption">The option type.</typeparam>
    /// <returns>Value of the option if it was specified; otherwise <see langword="null"/>.</returns>
    public string? GetOptionOrDefault<TOption>() where TOption : IConsoleOption => this.TryGetOption<TOption>(out var value) ? value : null;
    /// <summary>
    /// Returns the value of the <typeparamref name="TOption"/> option.
    /// </summary>
    /// <typeparam name="TOption">The option type.</typeparam>
    /// <returns>Value of the option.</returns>
    /// <exception cref="ConsoleManException">The option was not specified.</exception>
    /// <remarks>This will throw a <see cref="ConsoleManException"/> if the option was not specified.</remarks>
    public string GetOption<TOption>() where TOption : IConsoleOption
    {
        if (!this.TryGetOption<TOption>(out var value))
            throw new ConsoleManException($"missing required argument: {TOption.Name}");

        return value;
    }
    /// <summary>
    /// Returns the value of the <typeparamref name="TOption"/> option as a <typeparamref name="TValue"/>.
    /// </summary>
    /// <typeparam name="TOption">The option type.</typeparam>
    /// <typeparam name="TValue">The type to parse the option as.</typeparam>
    /// <returns>Value of the option.</returns>
    /// <exception cref="ConsoleManException">The option was not specified or could not be parsed.</exception>
    /// <remarks>This will throw a <see cref="ConsoleManException"/> if the option was not specified.</remarks>
    public TValue GetOption<TOption, TValue>() where TOption : IConsoleOption where TValue : IParsable<TValue>
    {
        if (!this.TryGetOption<TOption, TValue>(out var value))
            throw new ConsoleManException($"missing required argument: {TOption.Name}");

        return value;
    }
    /// <summary>
    /// Tries to get the <typeparamref name="TOption"/> option as a <typeparamref name="TEnum"/> value.
    /// </summary>
    /// <typeparam name="TOption">The option type.</typeparam>
    /// <typeparam name="TEnum">The <see langword="enum"/> type to parse as.</typeparam>
    /// <param name="value">Will be set to the parsed <see langword="enum"/> value if it was specified.</param>
    /// <returns><see langword="true"/> if the option was specified; otherwise <see langword="false"/>.</returns>
    /// <exception cref="ConsoleManException">Option value could not be parsed.</exception>
    public bool TryGetEnumValue<TOption, TEnum>(out TEnum value)
        where TOption : IConsoleEnumOption<TEnum>
        where TEnum : struct, Enum
    {
        value = default;
        if (this.TryGetOption<TOption>(out var s))
        {
            if (Enum.TryParse(s, true, out value))
                return true;

            throw new ConsoleManException($"{TOption.Name} error: invalid value. Valid values are: {string.Join(", ", Enum.GetNames<TEnum>().Select(n => n.ToLowerInvariant()))}");
        }

        return false;
    }
    /// <summary>
    /// Returns the <typeparamref name="TOption"/> option as a <typeparamref name="TEnum"/> value.
    /// </summary>
    /// <typeparam name="TOption">The option type.</typeparam>
    /// <typeparam name="TEnum">The <see langword="enum"/> type to parse as.</typeparam>
    /// <returns>Parsed <see langword="enum"/> value.</returns>
    /// <exception cref="ConsoleManException">Option was not specified or could not be parsed.</exception>
    public TEnum GetEnumValue<TOption, TEnum>()
        where TOption : IConsoleEnumOption<TEnum>
        where TEnum : struct, Enum
    {
        if (!this.TryGetEnumValue<TOption, TEnum>(out var value))
            throw new ConsoleManException($"missing required argument: {TOption.Name}");

        return value;
    }
    /// <summary>
    /// Returns a value indicating whether the <typeparamref name="TOption"/> flag is specified.
    /// </summary>
    /// <typeparam name="TOption">The flag type.</typeparam>
    /// <returns><see langword="true"/> if the flag was specified; otherwise <see langword="false"/>.</returns>
    public bool HasFlag<TOption>() where TOption : IConsoleFlagOption => this.options.ContainsKey(typeof(TOption));
    /// <summary>
    /// Attempts to set the value of an option in the current context.
    /// </summary>
    /// <typeparam name="TOption">The option type.</typeparam>
    /// <param name="value">The value to assign as the option value.</param>
    public void TrySetOption<TOption>(string value) where TOption : IConsoleOption
    {
        this.options.TryAdd(typeof(TOption), new ParsedOption(new Option<TOption>(), value));
    }
    /// <summary>
    /// Writes standard usage text for the command.
    /// </summary>
    public void WriteUsage()
    {
        var subCommands = this.Command.Subcommands.Where(c => !c.Undisclosed).ToList();
        var options = this.Command.GetOptionsInScope()
            .Where(o => !o.Option.Undisclosed)
            .Select(o => (o.Scope, o.Depth, Name: formatName(o.Option), Desc: formatDescription(o.Option), o.Option.Required))
            .ToList();

        Console.WriteLine("Description:");
        Console.Write("  ");
        WordWrapper.WriteOutput(this.Command.Description, 2);
        Console.WriteLine();

        Console.WriteLine();
        Console.WriteLine("Usage:");
        Console.WriteLine($"  {string.Join(' ', this.Commands.Select(c => c.Name))} {(subCommands.Count > 0 ? "[command] " : string.Empty)}[options]");
        Console.WriteLine();

        if (subCommands.Count == 0)
        {
            var optionMargin = options.Count == 0 ? 0 : options.Select(i => i.Name.Length).Max() + 2;
            foreach (var optionGroup in options.GroupBy(o => (o.Depth, o.Scope)))
            {
                if (subCommands.Count == 0 && optionGroup.Key.Depth == 0)
                {
                    Console.WriteLine($"Options:");
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine($"Common Options ({optionGroup.Key.Scope}):");
                }

                CM.WriteTwoColumnList(
                    optionGroup.OrderByDescending(o => o.Required).ThenBy(o => o.Name).Select(o => (o.Name, o.Desc)).ToList(),
                    optionMargin
                );
            }
            if (options.Count > 0)
                CM.WriteTwoColumnList(
                    [("  -?, --help", "Show help and usage information")],
                    optionMargin
                );
        }               
        else
        {
            Console.WriteLine("Commands:");
            CM.WriteTwoColumnList([.. subCommands.Select(c => ($"  {c.Name}", c.Description))]);
        }

        if (!string.IsNullOrWhiteSpace(this.Command.Examples))
        {
            Console.WriteLine();
            Console.WriteLine("Examples:");
            Console.WriteLine(this.Command.Examples);
            Console.WriteLine();
        }

        static string formatName(Option o)
        {
            var value = $"  {o.Name}";
            if (o.HasValue)
                value += $"=<{o.Name.AsSpan().TrimStart("-/")}>";
            if (o.Required)
                value += " (REQUIRED)";

            return value;
        }

        static string formatDescription(Option o)
        {
            var desc = o.Description;
            if (o.ValidValues != null && o.ValidValues.Length > 0)
                desc += $"{Environment.NewLine}Valid values: {string.Join(", ", o.ValidValues)}";

            return desc;
        }
    }
}
