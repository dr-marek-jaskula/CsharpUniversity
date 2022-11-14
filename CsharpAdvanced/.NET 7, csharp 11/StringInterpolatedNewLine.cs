namespace CsharpAdvanced.NET_7__csharp_11;

public sealed class StringInterpolatedNewLine
{
    public static void InvokeExample()
    {
        var world = "World";

        var nextInterpolatedStringWithNewLine = $"Hello, {world
            .ToLower()!}"; //this is still one line
    }
}