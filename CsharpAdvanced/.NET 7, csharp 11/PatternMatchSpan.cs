namespace CsharpAdvanced.NET_7__csharp_11;

public sealed class PatternMatchSpan
{
    public static void InvokeExample()
    {
        ReadOnlySpan<char> text = "Marek Jaskula";

        if (text is "Marek Jaskula")
        {
            Console.WriteLine("Yes");
        }

        if (text is ['M', ..])
        {
            Console.WriteLine("Name starts with M");
        }
    }
}