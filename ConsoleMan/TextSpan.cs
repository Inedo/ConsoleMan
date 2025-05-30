namespace ConsoleMan;

/// <summary>
/// Represents a span of console text.
/// </summary>
/// <param name="Text">The text.</param>
/// <param name="Color">The color of the text.</param>
public readonly record struct TextSpan(string? Text, ConsoleColor? Color = null)
{
    public static implicit operator TextSpan(string? s) => new(s);
}
