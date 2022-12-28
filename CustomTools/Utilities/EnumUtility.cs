namespace CustomTools.Utilities;

public static class EnumUtility
{
    public static IList<TEnum> Values<TEnum>()
        where TEnum : struct
    {
        return Enum
            .GetValues(typeof(TEnum))
            .OfType<TEnum>()
            .ToList()
            .AsReadOnly();
    }
}