using System.Text.RegularExpressions;

namespace CustomTools.Utilities;

public static class RegexUtility
{
    private const string GuidPartPattern = "(?:[0-9a-fA-F])";
    public const string GuidPattern = GuidPartPattern + "{8}"
                                        + "-" + GuidPartPattern + "{4}"
                                        + "-" + GuidPartPattern + "{4}"
                                        + "-" + GuidPartPattern + "{4}"
                                        + "-" + GuidPartPattern + "{12}";

    public static string GroupValue(this Match match, string name)
    {
        return match.Groups[name].Value;
    }
}