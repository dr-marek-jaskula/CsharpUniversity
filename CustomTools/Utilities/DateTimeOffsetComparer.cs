namespace CustomTools.Utilities;

public sealed class DateTimeOffsetComparer : IComparer<DateTimeOffset?>
{
    public static readonly IComparer<DateTimeOffset?> Instance = new DateTimeOffsetComparer();

    int IComparer<DateTimeOffset?>.Compare(DateTimeOffset? first, DateTimeOffset? second)
    {
        const int lessThan = -1;
        const int greaterThan = 1;

        if (first.HasValue is false)
        {
            return greaterThan;
        }

        if (second.HasValue is false)
        {
            return lessThan;
        }

        return first.Value.CompareTo(second.Value);
    }
}