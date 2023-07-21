using OpenClosed.Enums;
using OpenClosed.Abstractions;
using System.Reflection;

namespace OpenClosed.Factories;

public static class CalculatorFactory
{
    private static readonly Dictionary<CalculatorType, MethodInfo> CalculatorCache = new();

    static CalculatorFactory()
    {
        var calculatorTypes = GetConcreteCalculatorTypes();

        var enums = Enum.GetNames(typeof(CalculatorType));
        var enumCount = enums.Length;

        if (calculatorTypes.Length != enumCount)
        {
            throw new NotImplementedException($"Calculator types: {string.Join(',', calculatorTypes.ToList())}. Current enum types: {string.Join(',', enums)}. For each enum type, respective calculator should be provided");
        }

        foreach (var calculatorType in calculatorTypes)
        {
            var propertyInfo = calculatorType.GetProperty(nameof(ICalculator.Discriminator))!;
            CalculatorType calculatorEnum = (CalculatorType)(propertyInfo!.GetValue(null, null)!);

            MethodInfo createCalculator = calculatorType.GetMethod(nameof(Create))!;

            var addResult = CalculatorCache.TryAdd(calculatorEnum, createCalculator);

            if (addResult is false)
            {
                throw new InvalidOperationException($"Duplicated 'Discriminator' for calculator {calculatorType.FullName}.");
            }
        }
    }

    public static ICalculator Create<TCalculator>()
        where TCalculator : ICalculator
    {
        return TCalculator.Create();
    }

    public static ICalculator Create(CalculatorType calculatorType)
    {
        if (CalculatorCache.TryGetValue(calculatorType, out var createdMethod) is false)
        {
            throw new InvalidOperationException($"There is no corresponding calculator for enum: {calculatorType} or calculator has invalid 'Discriminator' value.");
        }

        return (ICalculator)createdMethod!.Invoke(null, null)!;
    }

    private static Type[] GetConcreteCalculatorTypes()
    {
        return Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(type => type.GetInterfaces().Any(x => x == typeof(ICalculator)))
            .Where(type => type.IsAbstract is false)
            .ToArray();
    }
}
