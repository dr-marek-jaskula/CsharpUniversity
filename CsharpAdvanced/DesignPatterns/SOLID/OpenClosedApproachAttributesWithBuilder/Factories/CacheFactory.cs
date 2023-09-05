using System.Reflection;
using System.Collections.ObjectModel;
using OpenCloseAttributes.Attributes.WithBuilder;

namespace OpenClosedAttributesDelegetesBuilder.Builders;

public sealed class CacheFactory<KeyType, DelegateType>
    where KeyType : notnull
    where DelegateType : notnull, Delegate
{
    private readonly static Type[] _allowedTypes = new[] { typeof(string), typeof(int), typeof(decimal), typeof(double), typeof(float) };

    private CacheFactory()
    {
    }

    public static ReadOnlyDictionary<KeyType, DelegateType> CreateFor(IEnumerable<DelegateType> dictionaryValues)
    {
        if (IsKeyTypeInvalid() is true)
        {
            throw new InvalidOperationException($"Type: {typeof(KeyType)} is not supported.");
        }

        Dictionary<KeyType, DelegateType> cache = new();

        foreach (var @delegate in dictionaryValues)
        {
            AddToCache(cache, @delegate);
        }

        if (IsEnum() && dictionaryValues.Count() != Enum.GetNames(typeof(KeyType)).Length)
        {
            throw new NotImplementedException($"For each enum '{nameof(KeyType)}', only one method must be provided.");
        }

        return cache.AsReadOnly();
    }

    private static void AddToCache(Dictionary<KeyType, DelegateType> cache, DelegateType @delegate)
    {
        var discriminator = @delegate
            .GetMethodInfo()
            .GetCustomAttribute<DiscriminatorAttribute<KeyType>>();

        if (discriminator is null)
        {
            throw new InvalidOperationException($"Each delegate must have custom attribute 'Discriminator' of type {typeof(KeyType)}.");
        }

        var key = discriminator.Key;

        var addResult = cache.TryAdd(key, @delegate);

        if (addResult is false)
        {
            throw new InvalidOperationException($"Duplicated 'Discriminator' for key '{key}'.");
        }
    }

    private static bool IsKeyTypeInvalid()
    {
        return IsEnum() is false && _allowedTypes.Contains(typeof(KeyType)) is false;
    }

    private static bool IsEnum()
    {
        return typeof(KeyType).IsEnum;
    }
}