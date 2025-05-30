namespace ConsoleMan;

/// <summary>
/// Option for a console command.
/// </summary>
public interface IConsoleOption : IConsoleArgument
{
    /// <summary>
    /// Gets a value indicating whether the option is required.
    /// </summary>
    static abstract bool Required { get; }
    /// <summary>
    /// Gets a value indicating whether the option has a value (<see langword="true"/> for flag options).
    /// </summary>
    static virtual bool HasValue => true;
    /// <summary>
    /// Gets the valid values for the option.
    /// </summary>
    static virtual string[]? ValidValues => null;
    /// <summary>
    /// Gets the default value for the option.
    /// </summary>
    static virtual string? DefaultValue => null;
    /// <summary>
    /// Gets a value indicating whether a value not present in <see cref="ValidValues"/> will be a warning instead of an error.
    /// </summary>
    static virtual bool WarnWhenInvalidValue => false;
}

/// <summary>
/// Option for a console command that uses an <see langword="enum"/> for its valid values.
/// </summary>
/// <typeparam name="TEnum">Type of the <see langword="enum"/> value.</typeparam>
public interface IConsoleEnumOption<TEnum> : IConsoleOption where TEnum : struct, Enum
{
    static string[]? IConsoleOption.ValidValues
    {
        get
        {
            var names = Enum.GetNames<TEnum>();
            for (int i = 0; i < names.Length; i++)
                names[i] = names[i].ToLowerInvariant();

            return names;
        }
    }
}
