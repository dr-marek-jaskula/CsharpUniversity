using System.Text.RegularExpressions;
using static System.Text.RegularExpressions.RegexOptions;
using static System.Uri;

namespace CustomTools.Utilities;

public static class UriUtility
{
    private static readonly Regex _slashRegex;

    static UriUtility()
    {
        string schemes = string.Join("|", UriSchemeHttp, UriSchemeHttps, UriSchemeFtp, UriSchemeFtps, UriSchemeFile, UriSchemeSftp);
        string pattern = $"(?<!(?:{schemes})[:])[/]+";
        _slashRegex = new Regex(pattern, Compiled | IgnoreCase);
    }

    public static string Normalize(string uriSpec)
    {
        const string singleSlash = "/";
        return _slashRegex.Replace(uriSpec, singleSlash);
    }
}