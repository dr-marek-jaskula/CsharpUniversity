using System.Text.RegularExpressions;
using static System.Text.RegularExpressions.RegexOptions;

namespace CustomTools.Utilities;

public static class NumberUtility
{
    private const string _commaPattern = "[,]";
    private static readonly Regex _commaRegex = new(_commaPattern, Compiled | Singleline);

    public static string Canoicalize(string localized)
    {
        if (string.IsNullOrEmpty(localized))
        {
            return localized;
        }

        const string canonicalFractionSeparator = ".";
        return _commaRegex.Replace(localized, canonicalFractionSeparator);
    }
}