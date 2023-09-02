using OpenCloseAttributes.Attributes.WithBuilder;
using System.Collections.ObjectModel;
using System.Reflection;

namespace OpenClosedAttributesDelegetesBuilder.Builders;

public sealed class CacheBuilder<KeyType, DelegateType>
    where KeyType : notnull
    where DelegateType : notnull, Delegate
{
    private readonly static Type[] _allowedTypes = new[] { typeof(string), typeof(int), typeof(decimal), typeof(double), typeof(float) };

    private CacheBuilder()
    {
    }

    public static ReadOnlyDictionary<KeyType, DelegateType> Build(Func<IEnumerable<DelegateType>> dictionaryValuesProvider)
    {
        if (IsInvalidType() is true)
        {
            throw new InvalidOperationException($"Type: {typeof(KeyType)} is not supported.");
        }

        Dictionary<KeyType, DelegateType> cache = new();
        var dictionaryValues = dictionaryValuesProvider();

        foreach (var method in dictionaryValues)
        {
            var discriminator = method
                .GetMethodInfo()
                .GetCustomAttribute<DiscriminatorAttribute<KeyType>>();

            if (discriminator is null)
            {
                throw new InvalidOperationException($"DictionaryValuesProvider must provide only delegates that has custom attribute 'Discriminator' of type {typeof(KeyType)}.");
            }

            var key = discriminator.Key;

            var addResult = cache.TryAdd(key, method);

            if (addResult is false)
            {
                throw new InvalidOperationException($"Duplicated 'Discriminator' for key '{key}'.");
            }
        }

        if (IsEnum() && dictionaryValues.Count() != Enum.GetNames(typeof(KeyType)).Length)
        {
            throw new NotImplementedException($"For each enum '{nameof(KeyType)}', only one method must be provided.");
        }

        return cache.AsReadOnly();
    }

    private static bool IsInvalidType()
    {
        return IsEnum() is false && _allowedTypes.Contains(typeof(KeyType)) is false;
    }

    private static bool IsEnum()
    {
        return typeof(KeyType).IsEnum;
    }
}