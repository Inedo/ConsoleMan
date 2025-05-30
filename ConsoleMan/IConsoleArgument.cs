namespace ConsoleMan;

/// <summary>
/// Contains information common to all argument types.
/// </summary>
/// <remarks>
/// This should not normally be implemented directly.
/// </remarks>
public interface IConsoleArgument
{
    /// <summary>
    /// Gets the argument name.
    /// </summary>
    static abstract string Name { get; }
    /// <summary>
    /// Gets the argument description.
    /// </summary>
    static abstract string Description { get; }
    /// <summary>
    /// Gets a value indicating whether the argument is undisclosed (hidden).
    /// </summary>
    static virtual bool Undisclosed => false;
}
