//using static SymSpell;

//namespace CustomTools.StringApproxAlgorithms.SymSpellAlgorithm;

////SymSpell is the fastest spell checker algorithm, and generally with same use of memory
////1. Get SymSpell from NuGet package (free to use, commercially also)
////2. Use given en frequency_dictionary or create dictionary base on the give test (named here "corpus")


//public class SymSpellAlgorithm
//{
//    // Dictionary that contains a mapping of lists of suggested correction words to the hashCodes
//    // of the original words and the deletes derived from them. Collisions of hashCodes is tolerated,
//    // because suggestions are ultimately verified via an edit distance function.
//    // A list of suggestions might have a single suggestion, or multiple suggestions. 
//    // private Dictionary<int, string[]> deletes;

//    // Dictionary of unique correct spelling words, and the frequency count for each word.
//    // private readonly Dictionary<string, Int64> words;

//    // Dictionary of unique words that are below the count threshold for being considered correct spellings.
//    // private Dictionary<string, Int64> belowThresholdWords = new Dictionary<string, long>();

//    private SymSpell _symSpell;
//    private string _name;
//    private int _maxEditDistanceLookup;
//    private Verbosity _verbosity;

//    // <param name="initialCapacity">The expected number of words in dictionary.</param>
//    // Specifying an accurate initialCapacity is not essential, but it can help speed up processing by alleviating the need for 
//    // data restructuring as the size grows.
//    // 82834 word for en dictionary I use

//    //<param name="maxDictionaryEditDistance">Maximum edit distance for doing lookups.</param>

//    public SymSpellAlgorithm(int initialCapacity, int maxDirectoryEditDistance, string name)
//    {
//        _name = name;
//        _symSpell = new SymSpell(initialCapacity, maxDirectoryEditDistance);
//        _maxEditDistanceLookup = 1; //max edit distance per lookup (maxEditDistanceLookup <= maxEditDistanceDictionary)
//        _verbosity = Verbosity.Closest; //Top, Closest, All

//        //Verbosity.Top: suggestion with the highest term frequency of the suggestions of smallest edit distance found.
//        //Verbosity.Closest: All suggestions of smallest edit distance found, suggestions ordered by term frequency.
//        //All: All suggestions within maxEditDistance, suggestions ordered by edit distance, then by term frequency (slower, no early termination).
//    }

//    /// <summary>Create/Update an entry in the dictionary.</summary>
//    /// <remarks>For every word there are deletes with an edit distance of 1..maxEditDistance created and added to the
//    /// dictionary. Every delete entry has a suggestions list, which points to the original term(s) it was created from.
//    /// The dictionary may be dynamically updated (word frequency and new words) at any time by calling CreateDictionaryEntry</remarks>
//    /// <param name="key">The word to add to dictionary.</param>
//    /// <param name="count">The frequency count for word.</param>
//    /// <param name="staging">Optional staging object to speed up adding many entries by staging them to a temporary structure.</param>
//    /// <returns>True if the word was added as a new correctly spelled word,
//    /// or false if the word is added as a below threshold word, or updates an
//    /// existing correctly spelled word.</returns>

//    /// <summary>Load multiple dictionary words from a file containing plain text.</summary>
//    /// <remarks>Merges with any dictionary data already loaded.</remarks>
//    /// <param name="corpus">The path+filename of the file.</param>
//    /// <returns>True if file loaded, or false if file not found.</returns>
//    //public bool CreateDictionary(string corpus)

//    public void UseDictionary(SymSpellDictionary symSpellDictionary)
//    {
//        if (symSpellDictionary is SymSpellDictionary.En)
//        {
//            string dictionaryPath = AppDomain.CurrentDomain.BaseDirectory + "frequency_dictionary_en_82_765.txt";
//            int termIndex = 0; //column of the term in the dictionary text file
//            int countIndex = 1; //column of the term frequency in the dictionary text file
//            if (!_symSpell.LoadDictionary(dictionaryPath, termIndex, countIndex))
//            {
//                Console.WriteLine("File not found!");
//                return;
//            }
//        }
//        else
//        {
//            //Alternatively Create the dictionary from a text corpus (e.g. http://norvig.com/big.txt ) 
//            //Make sure the corpus does not contain spelling errors, invalid terms and the word frequency is representative to increase the precision of the spelling correction.
//            //You may use SymSpell.CreateDictionaryEntry() to update a (self learning) dictionary incrementally
//            //To extend spelling correction beyond single words to phrases (e.g. correcting "unitedkingom" to "united kingdom") simply add those phrases with CreateDictionaryEntry(). or use  https://github.com/wolfgarbe/SymSpellCompound
//            //string path = "big.txt";
//            //if (!symSpell.CreateDictionary(path)) Console.Error.WriteLine("File not found: " + Path.GetFullPath(path));
//        }
//    }

//    public string FindBestSuggestion()
//    {
//        /// <summary>Find suggested spellings for a given input word, using the maximum
//        /// edit distance specified during construction of the SymSpell dictionary.</summary>
//        /// <param name="input">The word being spell checked.</param>
//        /// <param name="verbosity">The value controlling the quantity/closeness of the retuned suggestions.</param>
//        /// <param name="maxEditDistance">The maximum edit distance between input and suggested words.</param>
//        /// <returns>A List of SuggestItem object representing suggested correct spellings for the input word, 
//        /// sorted by edit distance, and secondarily by count frequency.</returns>
//        var suggestions = _symSpell.Lookup(_name, _verbosity, _maxEditDistanceLookup);
//        return suggestions.First().term;
//    }

//    //Other!!!

//    public string WordSegmentation()
//    {
//        //word segmentation and correction for multi-word input strings with/without spaces
//        string inputTerm = "thequickbrownfoxjumpsoverthelazydog";
//        int maxEditDistance = 0; //?????????
//        var suggestion2 = _symSpell.WordSegmentation(inputTerm);
//        return "";
//    }

//    //Add bigram
//}