using OpenClosed.Abstractions;
using OpenClosed.Enums;

namespace OpenClosed.Calculators;

public sealed class DamageCalculator : ICalculator
{
    public static CalculatorType Discriminator => CalculatorType.Damange;

    public int Calculate(int value)
    {
        return 4 * value;
    }

    public static ICalculator Create()
    {
        return new DamageCalculator();
    }
}

