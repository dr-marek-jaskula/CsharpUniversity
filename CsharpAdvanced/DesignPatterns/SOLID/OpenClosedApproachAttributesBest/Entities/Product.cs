using OpenCloseAttributes.Attributes;
using OpenClosed.Enums;
using System.Reflection;

namespace OpenCloseAttributes.Entities;

public sealed class Product
{
    private static readonly Dictionary<ProductType, MethodInfo> ProductCache = new();

    static Product()
    {
        var calculatorMethods = GetCalculatePriceMethods();

        var enums = Enum.GetNames(typeof(ProductType));
        var enumCount = enums.Length;

        if (calculatorMethods.Length != enumCount)
        {
            throw new NotImplementedException($"Calculator methods: {string.Join(',', calculatorMethods.ToList())}. Current enum types: {string.Join(',', enums)}. For each enum type, respective calculator method should be provided");
        }

        foreach (var calculatorMethod in calculatorMethods)
        {
            ProductType productType = calculatorMethod.GetCustomAttribute<DiscriminatorAttribute>()!.ProductType;

            var addResult = ProductCache.TryAdd(productType, calculatorMethod);

            if (addResult is false)
            {
                throw new InvalidOperationException($"Duplicated 'Discriminator' for calculator method: {calculatorMethod.Name}.");
            }
        }
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
        var priceForProductType = CalculatePriceForType(productType);
        return Math.Round(priceForProductType * (1 - Discount), 2);
    }

    #region CalculateMethods

    private int CalculatePriceForType(ProductType productType)
    {
        return (int)ProductCache[productType].Invoke(this, null)!;
    }

    [Discriminator(ProductType.Common)]
    private static int CalculateForCommon()
    {
        return 1;
    }
    
    [Discriminator(ProductType.Uncommon)]
    private static int CalculateForUncommon()
    {
        return 2;
    }

    [Discriminator(ProductType.Rare)]
    private static int CalculateForRare()
    {
        return 3;
    }

    private static MethodInfo[] GetCalculatePriceMethods()
    {
        return typeof(Product)
            .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
            .Where(method => method.GetCustomAttribute<DiscriminatorAttribute>() is not null)
            .ToArray();
    }

    #endregion CalculateMethods
}
