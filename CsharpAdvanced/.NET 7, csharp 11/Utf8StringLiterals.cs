namespace CsharpAdvanced.NET_7__csharp_11;

public sealed class Utf8StringLiterals
{
    public static void InvokeUtf8StringLiteralsExample()
    {
        string text = "Marek Jaskula";

        //by have the "u8" after the string we convert the string to utf8 at once
        ReadOnlySpan<byte> text2 = "Marek Jaskula"u8;
    }
}