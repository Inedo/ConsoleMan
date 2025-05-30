namespace ConsoleMan;

/// <summary>
/// Represents a console argument (typically either an option or a command).
/// </summary>
public abstract class ConsoleToken : IEquatable<ConsoleToken>
{
    private protected ConsoleToken()
    {
    }

    /// <summary>
    /// Gets the argument name.
    /// </summary>
    public abstract string Name { get; }
    /// <summary>
    /// Gets the argument description.
    /// </summary>
    public abstract string Description { get; }
    /// <summary>
    /// Gets the argument type.
    /// </summary>
    public abstract Type Type { get; }
    /// <summary>
    /// Gets a value indicating whether this is an undocumented argument.
    /// </summary>
    public abstract bool Undisclosed { get; }

    /// <inheritdoc/>
    public bool Equals(ConsoleToken? other)
    {
        if (ReferenceEquals(this, other))
            return true;
        if (other is null)
            return false;

        return this.Type == other.Type;
    }
    /// <inheritdoc/>
    public override bool Equals(object? obj) => this.Equals(obj as ConsoleToken);
    /// <inheritdoc/>
    public override int GetHashCode() => this.Type.GetHashCode();
    /// <inheritdoc/>
    public override string ToString() => this.Name;
}
