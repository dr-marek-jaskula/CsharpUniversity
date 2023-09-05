using OpenClosed.Enums.WithBuilder;
using System.Collections.ObjectModel;
using OpenCloseAttributes.Attributes.WithBuilder;
using OpenClosedAttributesDelegetesBuilder.Builders;
using static OpenClosed.Enums.WithBuilder.ProductType;

namespace OpenCloseAttributes.Entities.WithBuilder;

public sealed class Product
{
    private static readonly ReadOnlyDictionary<ProductType, Func<int>> ProductCacheEnum;
    private static readonly ReadOnlyDictionary<string, Func<int>> ProductCacheString;

    static Product()
    {
        ProductCacheEnum = CacheFactory<ProductType, Func<int>>
            .CreateFor(GetCalculatorMethodsEnum());

        ProductCacheString = CacheFactory<string, Func<int>>
            .CreateFor(GetCalculatorMethodsString());
    }

    public Product(string name, decimal discount)
    {
        Name = name;
        Discount = discount;
    }

    public string Name { get; set; }
    public decimal Discount { get; set; }

    public decimal DetermineTotalPrice(ProductType productType)
    {
        var priceForProductType = ProductCacheEnum[productType]();
        return Math.Round(priceForProductType * (1 - Discount), 2);
    }

    public decimal DetermineTotalPrice(string productType)
    {
        var priceForProductType = ProductCacheString[productType]();
        return Math.Round(priceForProductType * (1 - Discount), 2);
    }

    private static IEnumerable<Func<int>> GetCalculatorMethodsEnum()
    {
        yield return [Discriminator<ProductType>(Common)] () => 1;
        yield return [Discriminator<ProductType>(Uncommon)] () => 2;
        yield return [Discriminator<ProductType>(Rare)] () => 3;
    }

    private static IEnumerable<Func<int>> GetCalculatorMethodsString()
    {
        yield return [Discriminator<string>("Common")] () => 1;
        yield return [Discriminator<string>("Uncommon")] () => 2;
        yield return [Discriminator<string>("Rare")] () => 3;
    }
}
