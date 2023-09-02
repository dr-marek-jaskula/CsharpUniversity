using OpenCloseAttributes.Entities.WithBuilder;
using static OpenClosed.Enums.WithBuilder.ProductType;

namespace CsharpAdvanced.DesignPatterns.SOLID.OpenClosedApproachAttributesWithBuilder;

//How to use this approach
//1. Add new enum type
//2. Add new line in GetCalculatorMethods() with respective attribute (and enum)

public sealed class ExecuteExampleWithAttributesWithBuilder
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

        var totalPriceCommon2 = product.DetermineTotalPrice("Common");
        Console.WriteLine(totalPriceCommon);
        var totalPriceUncommon2 = product.DetermineTotalPrice("Uncommon");
        Console.WriteLine(totalPriceUncommon);
        var totalPriceRare2 = product.DetermineTotalPrice("Rare");
        Console.WriteLine(totalPriceRare);

        //var totalPriceRare3 = product.DetermineTotalPrice2("Rare2");
    }
}