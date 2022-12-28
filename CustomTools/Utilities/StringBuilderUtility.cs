using System.Text;

namespace CustomTools.Utilities;

public static class StringBuilderUtility
{
    public static bool IsEmpty(this StringBuilder builder)
    {
        return builder.Length is 0;
    }

    public static bool IsNotEmpty(this StringBuilder builder)
    {
        return builder.IsEmpty() is false;
    }

    public static string? ToErrorMessage(this StringBuilder builder)
    {
        return builder.IsNotEmpty()
            ? builder.ToString()
            : default;
    }
}