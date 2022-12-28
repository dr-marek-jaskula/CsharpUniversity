using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using static System.Convert;
using static System.Text.RegularExpressions.RegexOptions;

namespace CustomTools.Utilities;

public static class RandomUtility
{
    private const string Base64NonAlpaNumbericCharactersPattern = "(?:[/+=]+)";
    private static readonly Regex Base64NonAlphaNumbericCharacterRegex = new(Base64NonAlpaNumbericCharactersPattern, Compiled | CultureInvariant | Singleline);

    public static string GenerateString(int length)
    {
        using var generator = RandomNumberGenerator.Create();
        var builder = new StringBuilder();
        Append(builder, generator, length);

        while (builder.Length < length)
        {
            Append(builder, generator, length - builder.Length);
        }

        return builder
            .ToString()
            .Substring(0, length);

        static void Append(StringBuilder builder, RandomNumberGenerator generator, int length)
        {
            var bytes = new byte[length];
            generator.GetBytes(bytes);
            string base64 = ToBase64String(bytes);
            string stripped = Base64NonAlphaNumbericCharacterRegex.Replace(base64, string.Empty);
            builder.Append(stripped);
        }
    }

    public static IEnumerable<string> GenerateStrings(int length, int count)
    {
        foreach (var index in 1..count)
        {
            yield return GenerateString(length);
        }
    }

    public static string GenerateTestString()
    {
        Guid opportinityId = Guid.NewGuid();
        Guid systemUserId = Guid.NewGuid();
        string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        StringBuilder sb = new();
        sb.AppendFormat("opportunityid={0}", opportinityId.ToString());
        sb.AppendFormat("&systemuserid={0}", systemUserId.ToString());
        sb.AppendFormat("&currenttime={0}", currentTime);

        return sb.ToString();
    }

    public static byte[] GetRandomData(int length)
    {
        return RandomNumberGenerator.GetBytes(length);
    }
}