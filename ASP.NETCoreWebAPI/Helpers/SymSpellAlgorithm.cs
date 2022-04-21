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
    /// <param name="name">Input string to approximate</param>
    /// <param name="symSpell">SymSpell object containing the dictionary to which name is approximated</param>
    /// <returns>Output approximated string</returns>
    public static string FindBestSuggestion(string name, SymSpell symSpell)
    {
        var suggestions = symSpell.Lookup(name, Verbosity.Top, 1);
        return suggestions.First().term;
    }

    /// <summary> 
    /// Returns the closest term to the input, based on the predefined directory (biggest frequency, lowest distance)
    /// </summary>
    /// <param name="name">Input string to approximate</param>
    /// <param name="symSpell">SymSpell object containing the dictionary to which name is approximated</param>
    /// <returns>Three approximated strings</returns>
    public static List<string> FindThreeBestSuggestions(string name, SymSpell symSpell)
    {
        var suggestions = symSpell.Lookup(name, Verbosity.Closest, 1);
        return suggestions.Take(3).Select(s => s.term).ToList();
    }

    //public string WordSegmentation(string inputTerm)
    //{
    //    //word segmentation and correction for multi-word input strings with/without spaces
    //    int maxEditDistance = 0; //
    //    _symSpell.MaxDictionaryEditDistance = 0;
    //    var suggestion2 = _symSpell.WordSegmentation(inputTerm);
    //    return "";
    //}
}