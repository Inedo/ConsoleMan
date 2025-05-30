namespace ConsoleMan;

/// <summary>
/// Console option that has no value.
/// </summary>
public interface IConsoleFlagOption : IConsoleOption
{
    static bool IConsoleOption.Required => false;
    static bool IConsoleOption.HasValue => false;
}
