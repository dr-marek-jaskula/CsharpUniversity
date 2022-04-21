using static SymSpell;

namespace ASP.NETCoreWebAPI.StringApproxAlgorithms;

//SymSpell is the fastest spell checker algorithm, and generally with same use of memory
//1. Get SymSpell from NuGet package (free to use, commercially also)
//2. Use given en frequency_dictionary or create dictionary base on the give test (named here "corpus")

public class SymSpellAlgorithm
{
    //Verbosity.Top: suggestion with the highest term frequency of the suggestions of smallest edit distance found.
    //Verbosity.Closest: All suggestions of smallest edit distance found, suggestions ordered by term frequency.
    //All: All suggestions within maxEditDistance, suggestions ordered by edit distance, then by term frequency (slower, no early termination).

    /// <summary>
    /// Returns the closest term to the input, based on the predefined directory (biggest frequency, lowest distance)
    /// </summary>
    /// <param name="stringToApproximate">Input string to approximate</param>
    /// <param name="symSpell">SymSpell object containing the dictionary to which name is approximated</param>
    /// <param name="maxEditDistance">Distance from the stringToApproximate</param>
    /// <returns>Output approximated string</returns>
    public static string FindBestSuggestion(string stringToApproximate, SymSpell symSpell, int maxEditDistance = 2)
    {
        var suggestions = symSpell.Lookup(stringToApproximate, Verbosity.Top, maxEditDistance);
        return suggestions.First().term;
    }

    /// <summary>
    /// Returns the closest terms to the input, based on the predefined directory (biggest frequency, lowest distance)
    /// </summary>
    /// <param name="name">Input string to approximate</param>
    /// <param name="symSpell">SymSpell object containing the dictionary to which name is approximated</param>
    /// <param name="count">The number of results but at least two</param>
    /// <param name="maxEditDistance">Distance from the stringToApproximate</param>
    /// <returns>Three approximated strings</returns>
    public static List<string> FindMultipleBestSuggestions(string name, SymSpell symSpell, int count, int maxEditDistance = 2)
    {
        if (count <= 1)
            throw new ArgumentException("Count should be at least two");

        var suggestions = symSpell.Lookup(name, Verbosity.Closest, maxEditDistance);
        return suggestions.Take(count).Select(s => s.term).ToList();
    }

    /// <summary>
    /// Provides the string segmentation - decomposes the characters to meaningful words
    /// </summary>
    /// <param name="inputTerm">Term to be segmented, preferred input should contain non segmented text without misspellings</param>
    /// <param name="symSpell">SymSpell object containing the dictionary to which name is approximated</param>
    /// <param name="maxEditDistance">>Distance from the strings in inputTerm</param>
    /// <returns>The segmented string</returns>
    public static string WordSegmentation(string inputTerm, SymSpell symSpell, int maxEditDistance = 0)
    {
        //word segmentation and correction for multi-word input strings with/without spaces
        var suggestion = symSpell.WordSegmentation(inputTerm, maxEditDistance);
        return suggestion.correctedString;
    }
}