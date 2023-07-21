using OpenClosed.Calculators;
using OpenClosed.Enums;
using OpenClosed.Factories;

namespace CsharpAdvanced.DesignPatterns.SOLID.OpenClosedApproachInstances;

//How to use this approach
//1. Add new Calculator concrete implementation
//2. Add new CalculatorType enum
//3. Use factory in one of the ways: providing the type (which is rather rare) or providing the enum type (common approach)

//To sum up, we just only need to extend enum, add implementation and use it as always. 

public sealed class ExecuteExample
{
    public static void Execute()
    {
        var healthCalculator = CalculatorFactory.Create<HealthCalculator>();
        var manaCalculator = CalculatorFactory.Create<ManaCalculator>();

        var healthResult = healthCalculator.Calculate(2);
        var manaResult = manaCalculator.Calculate(2);

        Console.WriteLine(healthResult);
        Console.WriteLine(manaResult);


        var healthCalculator3 = CalculatorFactory.Create(CalculatorType.Health);
        var manaCalculator3 = CalculatorFactory.Create(CalculatorType.Mana);

        var healthResult3 = healthCalculator3.Calculate(2);
        var manaResult3 = manaCalculator3.Calculate(2);

        Console.WriteLine(healthResult3);
        Console.WriteLine(manaResult3);
    }
}