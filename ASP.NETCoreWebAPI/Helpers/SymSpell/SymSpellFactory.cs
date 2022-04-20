using static SymSpell;

namespace CustomTools.StringApproxAlgorithms.SymSpellAlgorithm;

//SymSpell is the fastest spell checker algorithm, and generally with same use of memory
//1. Get SymSpell from NuGet package (free to use, commercially also)
//2. Use given en frequency_dictionary or create dictionary base on the give test (named here "corpus")

//Enum created to present the dictionary options: English (En), Name of product from EFCore db, OtherDictionary (make our own dictionary)
public enum SymSpellDictionary
{
    En,
    ProductNames,
    OtherDictionary
}

public class SymSpellFactory
{
    //At first the proper SymSpell class should be created and shared as a Singleton

    /// <summary>
    /// The factory to create a proper SymSpell instance, depending on the user choice
    /// </summary>
    /// <param name="initialCapacity">Approximated dictionary capacity. Default 82834 what is equal to English dictionary capacity</param>
    /// <param name="maxDirectoryEditDistance">Maximal word distance. Default 2</param>
    /// <param name="symSpellDictionary">One of the supported dictionaries. Default En (English)</param>
    /// <returns>SymSpell instance</returns>
    public static SymSpell CreateSymSpell(int initialCapacity = 82834, int maxDirectoryEditDistance = 2, SymSpellDictionary symSpellDictionary = SymSpellDictionary.En)
    {
        SymSpell symSpell = new(initialCapacity, maxDirectoryEditDistance);

        if (symSpellDictionary is SymSpellDictionary.En)
        {
            string dictionaryPath = AppDomain.CurrentDomain.BaseDirectory + "frequency_dictionary_en_82_765.txt";
            //column of the term in the dictionary text file
            int termIndex = 0;
            //column of the term frequency in the dictionary text file
            int countIndex = 1; 

            if (!symSpell.LoadDictionary(dictionaryPath, termIndex, countIndex))
                Console.WriteLine("File not found!");
        }
        else if (symSpellDictionary is SymSpellDictionary.ProductNames)
        {
            //Alternatively Create the dictionary from a text corpus (e.g. http://norvig.com/big.txt ) 
            //Make sure the corpus does not contain spelling errors, invalid terms and the word frequency is representative to increase the precision of the spelling correction.
            //You may use SymSpell.CreateDictionaryEntry() to update a (self learning) dictionary incrementally
            //To extend spelling correction beyond single words to phrases (e.g. correcting "unitedkingom" to "united kingdom") simply add those phrases with CreateDictionaryEntry(). or use  https://github.com/wolfgarbe/SymSpellCompound
            
            //Path to test file, from which the dictionary will be created and used
            string path = "products.txt";

            if (!symSpell.CreateDictionary(path)) 
                Console.WriteLine("File not found: " + Path.GetFullPath(path));
        }

        return symSpell;
    }
}