public enum NodeName
{
    Root,
    NodeOne,
    NodeTwo,
    FirstDeepNodeOne,
    SecondDeepNodeOne,
    FirstDeepNodeTwo,
    SecondDeepNodeTwo,
    VeryDeepNodeOne,
    VeryVeryDeepNodeOne,
    Missing
}

public static class TreeExtension
{
    public static NodeName GetRandomOf(params NodeName[] stepNames)
    {
        return stepNames.PickRandom();
    }

    public static T PickRandom<T>(this IEnumerable<T> source)
    {
        return source.PickRandom(1).Single();
    }

    public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count)
    {
        return source.Shuffle().Take(count);
    }

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
    {
        return source.OrderBy(x => Guid.NewGuid());
    }
}