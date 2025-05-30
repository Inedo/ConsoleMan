namespace ConsoleMan;

/// <summary>
/// Represents a console option.
/// </summary>
public abstract class Option : ConsoleToken
{
    private protected Option()
    {
    }

    /// <summary>
    /// Gets a value indicating whether the option is required.
    /// </summary>
    public abstract bool Required { get; }
    /// <summary>
    /// Gets the valid values for the option.
    /// </summary>
    public abstract string[]? ValidValues { get; }
    /// <summary>
    /// Gets the default value for the option.
    /// </summary>
    public abstract string? DefaultValue { get; }
    /// <summary>
    /// Gets a value indicating whether a value was specified.
    /// </summary>
    public abstract bool HasValue { get; }
    /// <summary>
    /// Gets a value indicating whether to generate a warning instead of an error for an invalid value.
    /// </summary>
    public abstract bool WarnWhenInvalidValue { get; }

    internal bool TryParse(string arg, out string? value)
    {
        value = null;
        var name = this.Name;

        if (arg.StartsWith(name))
        {
            if (arg.Length == name.Length)
            {
                return true;
            }
            else if (arg[name.Length] == '=')
            {
                value = arg[(name.Length + 1)..];
                return true;
            }
        }

        return false;
    }
}

internal sealed class Option<TOption>(OptionOverrides? o = null) : Option where TOption : IConsoleOption
{
    private readonly OptionOverrides? o = o;

    public override bool Required => o?.Required ?? TOption.Required;
    public override string Name => TOption.Name;
    public override string Description => o?.Description ?? TOption.Description;
    public override Type Type => typeof(TOption);
    public override bool Undisclosed => o?.Undisclosed ?? TOption.Undisclosed;
    public override string[]? ValidValues => o?.ValidValues ?? TOption.ValidValues;
    public override string? DefaultValue => o?.DefaultValue ?? TOption.DefaultValue;
    public override bool HasValue => TOption.HasValue;
    public override bool WarnWhenInvalidValue => TOption.WarnWhenInvalidValue;
}
