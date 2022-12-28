using System.Collections.ObjectModel;

namespace CustomTools.Utilities;

public static class DictionartyUtility
{
    public static IDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        where TKey : notnull
    {
        return new ReadOnlyDictionary<TKey, TValue>(dictionary);
    }

    public static TValue? ValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue? defaultValue)
    {
        return dictionary.TryGetValue(key, out var value) 
            ? value 
            : defaultValue;
    }

    public static TValue? ValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
    {
        return dictionary.ValueOrDefault(key, default);
    }

    public static IDictionary<TKey, TValue> Empty<TKey, TValue>()
        where TKey : notnull
    {
        return new Dictionary<TKey, TValue>();
    }
}