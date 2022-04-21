namespace ASP.NETCoreWebAPI.StringApproxAlgorithms;

//SymSpell is the fastest spell checker algorithm, and generally with same use of memory
//1. Get SymSpell from NuGet package (free to use, commercially also)
//2. Use given en frequency_dictionary or create dictionary base on the give test (named here "corpus")

/// <summary>Create/Update an entry in the dictionary.</summary>
/// <remarks>For every word there are deletes with an edit distance of 1..maxEditDistance created and added to the
/// dictionary. Every delete entry has a suggestions list, which points to the original term(s) it was created from.
/// The dictionary may be dynamically updated (word frequency and new words) at any time by calling CreateDictionaryEntry</remarks>
/// <param name="key">The word to add to dictionary.</param>
/// <param name="count">The frequency count for word.</param>
/// <param name="staging">Optional staging object to speed up adding many entries by staging them to a temporary structure.</param>
/// <returns>True if the word was added as a new correctly spelled word,
/// or false if the word is added as a below threshold word, or updates an
/// existing correctly spelled word.</returns>

/// <summary>Load multiple dictionary words from a file containing plain text.</summary>
/// <remarks>Merges with any dictionary data already loaded.</remarks>
/// <param name="corpus">The path+filename of the file.</param>
/// <returns>True if file loaded, or false if file not found.</returns>
//public bool CreateDictionary(string corpus)

public class SymSpellFactory
{
    //At first the proper SymSpell class should be created and shared as a Singleton

    // Dictionary that contains a mapping of lists of suggested correction words to the hashCodes
    // of the original words and the deletes derived from them. Collisions of hashCodes is tolerated,
    // because suggestions are ultimately verified via an edit distance function.
    // A list of suggestions might have a single suggestion, or multiple suggestions.
    // private Dictionary<int, string[]> deletes;

    // Dictionary of unique correct spelling words, and the frequency count for each word.
    // private readonly Dictionary<string, Int64> words;

    // Dictionary of unique words that are below the count threshold for being considered correct spellings.
    // private Dictionary<string, Int64> belowThresholdWords = new Dictionary<string, long>();

    /// <summary>
    /// Create a SymSpell instance for frequency English directory
    /// </summary>
    /// <returns>SymSpell instance for En dictionary</returns>
    public static SymSpell CreateSymSpell()
    {
        // <param name="initialCapacity">The expected number of words in dictionary.</param>
        // Specifying an accurate initialCapacity is not essential.
        // However, it can help speed up processing by alleviating the need for data restructuring as the size grows.
        // 82834 word for en dictionary

        //<param name="maxDictionaryEditDistance">Maximum edit distance for doing lookups.</param>
        SymSpell symSpell = new(82834, 2);

        //Path to en (English) dictionary
        string dictionaryPath = Environment.CurrentDirectory + "\\Helpers\\SymSpell\\frequency_dictionary_en_82_765.txt";
        //column of the term in the dictionary text file
        int termIndex = 0;
        //column of the term frequency in the dictionary text file
        int countIndex = 1;

        if (!symSpell.LoadDictionary(dictionaryPath, termIndex, countIndex))
            throw new DirectoryNotFoundException("English frequency dictionary for SymSpell not found!");

        return symSpell;
    }

    /// <summary>
    /// Create a SymSpell instance. Dictionary is made by corpus aimed by path input
    /// </summary>
    /// <param name="path">Path to corpus</param>
    /// <param name="initialCapacity">Approximated dictionary capacity</param>
    /// <param name="maxDirectoryEditDistance">Maximal word distance</param>
    /// <returns>SymSpell instance</returns>
    public static SymSpell CreateSymSpell(string path, int initialCapacity, int maxDirectoryEditDistance = 2)
    {
        SymSpell symSpell = new(initialCapacity, maxDirectoryEditDistance);

        if (!symSpell.CreateDictionary(path))
            throw new DirectoryNotFoundException("Corpus dictionary for SymSpell not found!");

        return symSpell;
    }

    /// <summary>
    /// Create a SymSpell instance. Dictionary is made by adding an input string list
    /// </summary>
    /// <param name="approximateTo">A list of strings that words will approximate to</param>
    /// <param name="maxDirectoryEditDistance">Maximal word distance</param>
    /// <returns>SymSpell instance</returns>
    public static SymSpell CreateSymSpell(List<string> approximateTo, int maxDirectoryEditDistance = 2)
    {
        int initialCapacity = approximateTo.Count;

        SymSpell symSpell = new(initialCapacity, maxDirectoryEditDistance);

        foreach (string word in approximateTo)
            symSpell.CreateDictionaryEntry(word, 1);

        return symSpell;
    }
}