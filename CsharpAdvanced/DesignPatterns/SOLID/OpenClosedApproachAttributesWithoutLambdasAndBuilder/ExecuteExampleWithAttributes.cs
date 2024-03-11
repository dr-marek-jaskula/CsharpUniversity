using OpenCloseAttributes.Entities;
using static OpenClosed.Enums.CleanOption.ProductType;

namespace CsharpAdvanced.DesignPatterns.SOLID.OpenClosedApproachInstances;

//How to use this approach
//1. Add new enum type
//2. Add new Calculator method with respective attribute (and new type as parameter)

public sealed class ExecuteExampleWithAttributes
{
    public static void Execute()
    {
        var product = new Product("Pencil", 0.1m);

        var totalPriceCommon = product.DetermineTotalPrice(Common);
        Console.WriteLine(totalPriceCommon);
        var totalPriceUncommon = product.DetermineTotalPrice(Uncommon);
        Console.WriteLine(totalPriceUncommon);
        var totalPriceRare = product.DetermineTotalPrice(Rare);
        Console.WriteLine(totalPriceRare);
    }
}