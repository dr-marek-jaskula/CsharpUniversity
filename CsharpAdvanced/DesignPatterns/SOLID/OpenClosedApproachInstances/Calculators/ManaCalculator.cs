using OpenClosed.Enums;
using OpenClosed.Abstractions;

namespace OpenClosed.Calculators;

public sealed class ManaCalculator : ICalculator
{
    public static CalculatorType Discriminator => CalculatorType.Mana;

    public int Calculate(int value)
    {
        return 3 * value;
    }

    public static ICalculator Create()
    {
        return new ManaCalculator();
    }
}
