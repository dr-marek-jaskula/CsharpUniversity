using System.Collections.ObjectModel;
using System.Net.Http.Headers;

namespace CustomTools.Utilities;

public static class IEnumerableUtility
{
    public static IEnumerable<TValue> SafeAppend<TValue>(this IEnumerable<TValue> items, params TValue[] toAppend)
    {
        if (toAppend.IsNullOrEmpty())
        {
            return items;
        }

        var notNulls = toAppend
            .Where(value => value is not null);

        return items.Concat(notNulls);
    }

    public static bool IsNotNullOrEmpty<TValue>(this IEnumerable<TValue> list)
    {
        return list is not null && list.Any();
    }

    public static bool IsNullOrEmpty<TValue>(this IEnumerable<TValue> list)
    {
        return list.IsNotNullOrEmpty() is false;
    }
}