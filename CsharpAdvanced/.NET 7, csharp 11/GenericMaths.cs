using System.Numerics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CsharpAdvanced.NET_7__csharp_11;

public sealed class GenericMaths
{
    public static void InvokeExample()
    {
        double[] numbers = { 1, 2, 3, 4, 5, 6, 0.69 };

        var sum = AddAll(numbers);
    }

    public static T AddAll<T>(T[] values)
        where T : INumber<T>
    {
        T result = T.Zero;

        foreach (var item in values)
        {
            result += item;
        }

        return result;
    }
}