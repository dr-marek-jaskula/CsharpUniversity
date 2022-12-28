namespace CustomTools.Utilities;

public static class DecimalUtility
{
    public static decimal? ParseOrDefault(string input)
    {
        return decimal.TryParse(input, out var @decimal)
            ? @decimal
            : default;
    }

    public static string PrintFormat(this decimal value)
    {
        return string.Format("{0:#.#####", value);
    }
}