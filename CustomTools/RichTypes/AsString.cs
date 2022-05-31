using System.Collections;

namespace CustomTools;

//Much better Email system with case sensitivity and implicit conversion!
//Write once, use multiple times

public abstract record class AsString : IEnumerable<char>
{
    public string Value { get; init; } = string.Empty;

    public override string ToString()
    {
        return Value;
    }

    public virtual bool Equals(AsString? other)
    {
        return Value.Equals(other?.Value, StringComparison.InvariantCulture);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public IEnumerator<char> GetEnumerator()
    {
        return Value.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return Value.GetEnumerator();
    }
}