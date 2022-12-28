using System.Globalization;

namespace CustomTools.Utilities;

public static class DateTimeOffsetUtility
{
    public static string ToIsoString(this DateTimeOffset dateTimeOffset)
    {
        return dateTimeOffset.ToString("yyyy--MM-ddTHH:mm:sszzz"); //2022-06-13T09:18:27+00:00
    }

    public static DateTimeOffset? ParseOrDefault(string input, string format)
    {
        return DateTimeOffset.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTimeOffset dateTime)
            ? dateTime
            : default(DateTimeOffset?);
    }

    public static DateTimeOffset OneHundredthSecondPrecistion(this DateTimeOffset dateTimeOffset)
    {
        const string format = "yyyy-MM-dd HH:mm:ss.ff";
        string formatted = dateTimeOffset.ToString(format);
        return DateTimeOffset.ParseExact(formatted, format, CultureInfo.InvariantCulture);
    }

    public static bool AreEqual(DateTimeOffset value1, DateTimeOffset value2, TimeSpan allowedDifference)
    {
        var difference = value1 - value2;
        return difference.Duration() <= allowedDifference.Duration();
    }
}