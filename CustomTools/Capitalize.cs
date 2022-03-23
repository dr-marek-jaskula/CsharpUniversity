namespace CustomTools;

public class CapitalizeTool
{
    /// <summary>
    /// Capitalizes the given string input. First letter will be capitalized and the following ones will be lowered.
    /// </summary>
    /// <param name="input">The input to be capitalized</param>
    public static void Capitalize(ref string input)
    {
        input = $"{input[0].ToString().ToUpper()}{input[1..].ToLower()}";
    }
}

