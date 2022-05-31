using Throw;

namespace CsharpAdvanced.ThrowNuGet;

public class ThrowNuGet
{
    //Throw is the NuGet Package that help with throwing exception, especially with Guard Clause

    public static void InvokeThrowNuGetExample()
    {
        //MyCustomExample(3, "string", 5);
        MyCustomExample(4, "string", 2);
        //MyCustomExample(11, "string", 2);
        //MyCustomExample(5, "", 2);
        //MyCustomExample(5, "    ", 2);
        //MyCustomExample(5, "string", null);
        //MyCustomExample(5, "string", -4);
        MyCustomExample(5, "string", 4);
    }

    private static void MyCustomExample(int number, string text, int? nullableInt)
    {
        //simple validation
        number.Throw()
            .IfLessThan(4)
            .IfGreaterThan(10);

        text.Throw()
            .IfEmpty()
            .IfWhiteSpace();

        //Even for nullable can be used
        nullableInt.ThrowIfNull()
            .IfNegative()
            .Throw("My custom message exception")
            .IfEquals(4);
    }
}