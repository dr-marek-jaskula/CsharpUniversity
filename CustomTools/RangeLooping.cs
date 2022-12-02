namespace CustomTools;

public static class RangeExtensions
{
    public static RangeEnumerator GetEnumerator(this Range range)
    {
        return new RangeEnumerator(range);
    }
}

//Use class instead of ref struct if looping in async methods is required
public record RangeEnumerator
{
    private int _current;
    private readonly int _end;

    public RangeEnumerator(Range range)
    {
        if (range.End.IsFromEnd)
        {
            throw new NotSupportedException("Infinite enumeration is not supported");
        }

        _current = range.Start.Value - 1;
        _end = range.End.Value;
    }

    public int Current => _current;

    public bool MoveNext()
    {
        _current++;
        return _current <= _end;
    }
}