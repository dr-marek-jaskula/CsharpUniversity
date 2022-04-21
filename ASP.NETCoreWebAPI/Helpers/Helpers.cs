namespace ASP.NETCoreWebAPI.Helpers;

public static class Helpers
{
    /// <summary>
    /// Capitalizes the given string input. First letter will be capitalized and the following ones will be lowered.
    /// <param name="input">The input to be capitalized</param>
    public static void Capitalize(ref string input)
    {
        input = $"{input[0].ToString().ToUpper()}{input[1..].ToLower()}";
    }
}