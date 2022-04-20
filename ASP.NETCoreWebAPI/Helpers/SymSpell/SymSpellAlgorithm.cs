using static SymSpell;

namespace CustomTools.StringApproxAlgorithms.SymSpellAlgorithm;

//SymSpell is the fastest spell checker algorithm, and generally with same use of memory
//1. Get SymSpell from NuGet package (free to use, commercially also)
//2. Use given en frequency_dictionary or create dictionary base on the give test (named here "corpus")


public class SymSpellAlgorithm
{
    private readonly SymSpell _symSpell;
    private Verbosity _verbosity = Verbosity.Closest;

    //Inject the SymSpell [Dependency Inversion on approx algorithm can be implemented]
    public SymSpellAlgorithm(SymSpell symSpell)
    {
        _symSpell = symSpell;
    }

    /// <summary>
    /// Returns the closest term to the input, based on the predefined directory
    /// </summary>
    /// <param name="name">Input string to approximate</param>
    /// <returns>Output approximated string</returns>
    public string FindBestSuggestion(string name)
    {
        int _maxEditDistanceLookup = 1;
        var suggestions = _symSpell.Lookup(name, _verbosity, _maxEditDistanceLookup);
        return suggestions.First().term;
    }
}