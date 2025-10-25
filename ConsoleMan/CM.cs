namespace ConsoleMan;

/// <summary>
/// Contains utility methods for console output.
/// </summary>
public static class CM
{
    /// <summary>
    /// Writes a string to stdout using a specific color.
    /// </summary>
    /// <param name="color">Color of text or null to use default color.</param>
    /// <param name="s">String to write.</param>
    public static void Write(ConsoleColor color, string? s)
    {
        var current = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.Write(s);
        Console.ForegroundColor = current;
    }
    /// <summary>
    /// Writes a string followed by a newline to stdout using a specific color.
    /// </summary>
    /// <param name="color">Color of text or null to use default color.</param>
    /// <param name="s">String to write.</param>
    public static void WriteLine(ConsoleColor color, string? s)
    {
        Write(color, s);
        Console.WriteLine();
    }
    /// <summary>
    /// Writes a string followed by a newline to stderr in red.
    /// </summary>
    /// <param name="s">String to write.</param>
    public static void WriteError(string? s)
    {
        var color = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Error.WriteLine(s);
        Console.ForegroundColor = color;
    }
    /// <summary>
    /// Writes an error message related to the <typeparamref name="TOption"/> option.
    /// </summary>
    /// <typeparam name="TOption">Option which is related to the error.</typeparam>
    /// <param name="s">Error message.</param>
    public static void WriteError<TOption>(string? s) where TOption : IConsoleOption
    {
        WriteError($"{TOption.Name} error: {s}");
    }

    /// <summary>
    /// Writes spans of optionally colored text to the console.
    /// </summary>
    /// <param name="values">Text spans to write.</param>
    /// <exception cref="ArgumentNullException"><paramref name="values"/> is null.</exception>
    public static void Write(params TextSpan[] values)
    {
        ArgumentNullException.ThrowIfNull(values);

        foreach (var (text, color) in values)
        {
            if (color.HasValue)
                Write(color.GetValueOrDefault(), text);
            else
                Console.Write(text);
        }
    }
    /// <summary>
    /// Writes a string to the console.
    /// </summary>
    /// <param name="s">String to write.</param>
    public static void Write(string? s) => Console.Write(s);

    /// <summary>
    /// Writes a newline to the console.
    /// </summary>
    public static void WriteLine() => Console.WriteLine();
    /// <summary>
    /// Writes spans of optionally colored text to the console followed by a newline.
    /// </summary>
    /// <param name="values">Text spans to write.</param>
    /// <exception cref="ArgumentNullException"><paramref name="values"/> is null.</exception>
    public static void WriteLine(params TextSpan[] values)
    {
        ArgumentNullException.ThrowIfNull(values);

        Write(values);
        Console.WriteLine();
    }
    /// <summary>
    /// Writes a string to the console followed by a newline.
    /// </summary>
    /// <param name="s">String to write.</param>
    public static void WriteLine(string? s) => Console.WriteLine(s);

    /// <summary>
    /// Writes a two-column key/value table to the console.
    /// </summary>
    /// <param name="items">Items to write to the console.</param>
    public static void WriteTwoColumnList(params (string Key, string Value)[] items) => WriteTwoColumnList(items, null);
    /// <summary>
    /// Writes a two-column key/value table to the console.
    /// </summary>
    /// <param name="items">Items to write to the console.</param>
    /// <param name="margin">Margin size to indent on the left side.</param>
    public static void WriteTwoColumnList(IEnumerable<(string Key, string Value)> items, int? margin = null)
    {
        margin ??= items.Select(i => i.Key.Length).Max() + 2;
        foreach (var (Key, Value) in items)
        {
            Console.Write(Key);
            for (int i = Key.Length; i < margin; i++)
                Console.Write(' ');

            WordWrapper.WriteOutput(Value, margin.Value);
            Console.WriteLine();
        }
    }
}
