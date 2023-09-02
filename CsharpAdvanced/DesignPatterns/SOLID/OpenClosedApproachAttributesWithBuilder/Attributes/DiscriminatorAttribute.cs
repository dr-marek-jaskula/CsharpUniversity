namespace OpenCloseAttributes.Attributes.WithBuilder;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public sealed class DiscriminatorAttribute<KeyType> : Attribute
    where KeyType : notnull
{
    public readonly KeyType Key;

    public DiscriminatorAttribute(KeyType key)
    {
        Key = key;
    }
}