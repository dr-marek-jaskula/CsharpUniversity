using OpenCloseAttributes.Attributes.CleanOption;
using OpenClosed.Enums.CleanOption;
using System.Reflection;

namespace OpenCloseAttributes.Entities.CleanOption;

public sealed class Product
{
    private static readonly Dictionary<ProductType, Func<int>> ProductCache = new();

    static Product()
    {
        var calculatorMethods = GetCalculatorMethods();
        var enumCount = Enum.GetNames(typeof(ProductType)).Length;

        foreach (var calculatorMethod in calculatorMethods)
        {
            ProductType productType = calculatorMethod.GetMethodInfo().GetCustomAttribute<DiscriminatorAttribute>()!.ProductType;

            var addResult = ProductCache.TryAdd(productType, calculatorMethod);

            if (addResult is false)
            {
                throw new InvalidOperationException($"Duplicated 'Discriminator' for calculator method: {calculatorMethod.Method.Name}.");
            }

            enumCount--;
        }

        if (enumCount is not 0)
        {
            throw new NotImplementedException($"For each enum '{nameof(ProductType)}', single calculator method must be provided.");
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
        var priceForProductType = ProductCache[productType]();
        return Math.Round(priceForProductType * (1 - Discount), 2);
    }

    private static IEnumerable<Func<int>> GetCalculatorMethods()
    {
        yield return [Discriminator(ProductType.Common)] () => 1;
        yield return [Discriminator(ProductType.Uncommon)] () => 2;
        yield return [Discriminator(ProductType.Rare)] () => 3;
    }
}
