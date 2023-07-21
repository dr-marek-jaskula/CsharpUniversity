using OpenClosed.Enums;

namespace OpenClosed.Abstractions;

public interface ICalculator
{
    int Calculate(int value);
    public abstract static CalculatorType Discriminator { get; }
    public abstract static ICalculator Create();
}
