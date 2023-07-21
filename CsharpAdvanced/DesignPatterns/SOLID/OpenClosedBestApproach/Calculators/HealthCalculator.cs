using OpenClosed.Enums;
using OpenClosed.Abstractions;

namespace OpenClosed.Calculators;

public sealed class HealthCalculator : ICalculator
{
    public static CalculatorType Discriminator => CalculatorType.Health;

    public int Calculate(int value)
    {
        return 2 * value;
    }

    public static ICalculator Create() 
    {
        return new HealthCalculator();
    }
}

