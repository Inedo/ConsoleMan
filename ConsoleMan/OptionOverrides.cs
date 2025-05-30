namespace ConsoleMan;

/// <summary>
/// Contains override values for a <see cref="IConsoleOption"/>.
/// </summary>
/// <param name="Required">Indicates whether the option is required.</param>
/// <param name="Description">Description of the option.</param>
/// <param name="ValidValues">Valid values for the option.</param>
/// <param name="DefaultValue">Default value of the option.</param>
/// <param name="Undisclosed">Indicates whether the option is hidden.</param>
public sealed record class OptionOverrides(bool? Required = null, string? Description = null, string[]? ValidValues = null, string? DefaultValue = null, bool? Undisclosed = null);
