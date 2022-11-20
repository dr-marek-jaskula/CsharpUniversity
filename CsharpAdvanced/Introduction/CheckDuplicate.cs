namespace CsharpAdvanced.Introduction;

public static class ContainsDuplicates
{
    //Results: use Distinct or All/Any approach
    //These approaches are simple and they performance is good
    public static bool BruteForce<T>(IEnumerable<T> enumerable)
    {
        int outerIndex = -1;
        foreach (var outer in enumerable)
        {
            outerIndex++;

            int innerIndex = -1;
            foreach (var inner in enumerable)
            {
                innerIndex++;

                if (outerIndex == innerIndex)
                {
                    continue;
                }

                if (outer.Equals(inner))
                {
                    return true;
                }
            }
        }

        return false;
    }

    public static bool ForEach<T>(IEnumerable<T> enumerable)
    {
        HashSet<T> set = new();

        foreach (var element in enumerable)
        {
            if (!set.Add(element))
            {
                return true;
            }
        }

        return false;
    }

    public static bool Any<T>(IEnumerable<T> enumerable)
    {
        HashSet<T> set = new();

        return enumerable.Any(e => !set.Add(e));
    }

    public static bool All<T>(IEnumerable<T> enumerable)
    {
        HashSet<T> set = new();

        return !enumerable.All(set.Add);
    }

    public static bool GroupBy<T>(IEnumerable<T> enumerable)
    {
        return enumerable.GroupBy(x => x).Any(g => g.Count() > 1);
    }

    public static bool Distinct<T>(IEnumerable<T> enumerable)
    {
        return enumerable.Distinct().Count() != enumerable.Count();
    }

    public static bool ToHashSet<T>(IEnumerable<T> enumerable)
    {
        return enumerable.ToHashSet().Count != enumerable.Count();
    }

    public static bool NewHashSet<T>(IEnumerable<T> enumerable)
    {
        return new HashSet<T>(enumerable).Count != enumerable.Count();
    }
}