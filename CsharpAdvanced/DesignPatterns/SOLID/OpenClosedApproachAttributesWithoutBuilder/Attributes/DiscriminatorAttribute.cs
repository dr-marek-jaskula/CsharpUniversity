using OpenClosed.Enums.CleanOption;

namespace OpenCloseAttributes.Attributes.CleanOption;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public sealed class DiscriminatorAttribute : Attribute
{
    public readonly ProductType ProductType;

    public DiscriminatorAttribute(ProductType productType)
    {
        ProductType = productType;
    }
}