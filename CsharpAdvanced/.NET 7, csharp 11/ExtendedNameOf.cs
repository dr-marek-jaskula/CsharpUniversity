using Bogus.DataSets;

namespace CsharpAdvanced.NET_7__csharp_11;

public sealed class ExtendedNameOf
{
    public static void InvokeExtendedNameOf()
    {

        //We can use the nameof(T) now
        [Name(nameof(T))]
        void localFunction<T>(T param)
        {
        }

        var lambdaExpression =
            ([Name(nameof(aNumber))] int aNumber) 
                => aNumber.ToString();
    }
}

public sealed class NameAttribute : Attribute
{
    public NameAttribute(string input)
    {
        Name = input;
    }

    public string Name { get; }
}