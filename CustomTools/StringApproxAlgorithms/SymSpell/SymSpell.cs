namespace CustomTools.StringApproxAlgorithms.SymSpellAlgorithm;

//SimSpell is the fastest spell checker algorithm, and generally with same use of memory

//1. Get SymSpell from NuGet package (free to use, commercially also)
//2. Use the code below

/*
SymSpell includes an English frequency dictionary

Dictionaries for Chinese, English, French, German, Hebrew, Italian, Russian and Spanish are located here:
SymSpell.FrequencyDictionary

Frequency dictionaries in many other languages can be found here:
FrequencyWords repository
Frequency dictionaries
Frequency dictionaries

https://github.com/hermitdave/FrequencyWords/tree/master/content/2018
*/

public class StringApproximationBySymSpell
{
    public static void InvokeSymSpellExamples()
    {
        //create object
        int initialCapacity = 82765;
        int maxEditDistanceDictionary = 2; //maximum edit distance per dictionary precalculation
        var symSpell = new SymSpell(initialCapacity, maxEditDistanceDictionary);

        //load dictionary
        string dictionaryPath = @"C:\Users\Marek\.nuget\packages\symspell\6.7.2\contentFiles\any\netcoreapp3.0\frequency_dictionary_en_82_765.txt";
        int termIndex = 0; //column of the term in the dictionary text file
        int countIndex = 1; //column of the term frequency in the dictionary text file
        if (!symSpell.LoadDictionary(dictionaryPath, termIndex, countIndex))
        {
            Console.WriteLine("File not found!");
            Console.ReadKey();
            return;
        }

        //lookup suggestions for single-word input strings
        string inputTerm = "house";
        int maxEditDistanceLookup = 1; //max edit distance per lookup (maxEditDistanceLookup<=maxEditDistanceDictionary)
        var suggestionVerbosity = SymSpell.Verbosity.Closest; //Top, Closest, All
        var suggestions = symSpell.Lookup(inputTerm, suggestionVerbosity, maxEditDistanceLookup);

        //display suggestions, edit distance and term frequency
        foreach (var suggestion in suggestions)
            Console.WriteLine(suggestion.term + " " + suggestion.distance.ToString() + " " + suggestion.count.ToString("N0"));

        //load bigram dictionary
        string dictionaryPath2 = @"C:\Users\Marek\.nuget\packages\symspell\6.7.2\contentFiles\any\netcoreapp3.0\frequency_bigramdictionary_en_243_342.txt";
        int termIndex2 = 0; //column of the term in the dictionary text file
        int countIndex2 = 2; //column of the term frequency in the dictionary text file
        if (!symSpell.LoadBigramDictionary(dictionaryPath2, termIndex2, countIndex2))
        {
            Console.WriteLine("File not found!");
            Console.ReadKey();
            return;
        }

        //lookup suggestions for multi-word input strings (supports compound splitting & merging)
        inputTerm = "whereis th elove hehad dated forImuch of thepast who couqdn'tread in sixtgrade and ins pired him";
        maxEditDistanceLookup = 2; //max edit distance per lookup (per single word, not per whole input string)
        suggestions = symSpell.LookupCompound(inputTerm, maxEditDistanceLookup);

        //display suggestions, edit distance and term frequency
        foreach (var suggestion in suggestions)
            Console.WriteLine(suggestion.term + " " + suggestion.distance.ToString() + " " + suggestion.count.ToString("N0"));

        //word segmentation and correction for multi-word input strings with/without spaces
        inputTerm = "thequickbrownfoxjumpsoverthelazydog";
        int maxEditDistance = 0;
        var suggestion2 = symSpell.WordSegmentation(inputTerm);

        //display term and edit distance
        Console.WriteLine(suggestion2.correctedString + " " + suggestion2.distanceSum.ToString("N0"));

        //press any key to exit program
        Console.ReadKey();
    }
}

