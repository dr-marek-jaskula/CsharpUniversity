using OpenCloseAttributes.Entities;
using static OpenClosed.Enums.ProductType;

namespace CsharpAdvanced.DesignPatterns.SOLID.OpenClosedApproachInstances;

//How to use this approach
//1. Add new Calculator concrete implementation
//2. Add new CalculatorType enum
//3. Use factory in one of the ways: providing the type (which is rather rare) or providing the enum type (common approach)

//To sum up, we just only need to extend enum, add implementation and use it as always. 

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