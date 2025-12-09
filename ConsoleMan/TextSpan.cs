namespace ConsoleMan;

/// <summary>
/// Represents a span of console text.
/// </summary>
public readonly struct TextSpan
{
    private readonly int color;

    public TextSpan(string? text, ConsoleColor color)
    {
        this.Text = text;
        this.color = (int)color;
    }
    public TextSpan(string? text)
    {
        this.Text = text;
        this.color = -1;
    }
    
    public static implicit operator TextSpan(string? s) => new(s);

    public string? Text { get; }
    public ConsoleColor? Color => this.color >= 0 ? (ConsoleColor)this.color : null;

    public bool TryGetColor(out ConsoleColor color)
    {
        color = (ConsoleColor)this.color;
        return color >= 0;
    }
}
