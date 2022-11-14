using System.Text;

namespace CsharpBasics.Introduction;

//The deep analysis of different ways of string interpolations efficiencies is covered in Benchmarks project, StringsBenchmarks.cs file

public class Strings
{
    //A String object is a sequential collection of System.Char objects that represent a string
    //string is a reference type
    //string inherits directly from 'object', not like value types that inherits from a ValueType class
    //In C#, the string keyword is an alias for String
    //strings in c# are immutable

    public static void InvokeStringsExample()
    {
        #region Declaring, defining

        string declaredString; // declare a string variable
        string nullString = null; //string can be null

        //There are three ways to create an empty string. The differences are insignificant
        string emptyString1 = String.Empty;
        string emptyString2 = string.Empty; //my preferred way of creating an empty string
        string emptyString3 = "";

        //defining string
        string simpleString = "This is a double quotes string"; //preferred way
        String otherSimpleString = "Other way";
        var otherSimpleString2 = "Type is obtained from the right hand side";

        //string from a char array
        string stringFromCharacters = new string(new char[] { 'T', 'h', 'i', 's' });
        char characterFromString = stringFromCharacters[2];

        #endregion Declaring, defining

        #region Escape characters

        //Escape character are character that can influence our string. To place them in the string we must use backslash \ before them
        //Escape characters list:
        // 1) \'     write single quote
        // 2) \"     write double quote
        // 3) \\     write backslash
        // 4) \n     make new line
        // 5) \t     make horizontal tab
        // 6) \v     make vertical tab
        // 7) \u     write unicode character (UTF-16)
        // 8) /U     write unicode character (UTF-32)

        string stringWithManyEscapeCharacters = "This is single quote: \', this is double one \", this is backslash \\, this is new line \n, this is tab \t, this is vertical tab \v ";
        string stringWithunicodeCharacters = "Example of unicode character is \u0041";

        //We can use escape character for example to store paths:
        string backsleshesInStrings = "C:\\Development\\Projects";

        //Alternate way is to place '@' (called "at sign", or "address sign" before the string. This tells compiler that '\' should be treated as a characters.
        string atSignString = @"C:\Development\Projects"; //Useful for paths

        //Other example
        string regularString = "The best movie is \"Superman\"";
        //using '@' if we need to write a double quote, we must make two of them
        string verbatumString = @"the best movie is ""Superman""";

        #endregion Escape characters

        #region Immutibility and constant strings

        //we can define constant strings
        const string constantString = "This text cannot be change at runtime";

        //String objects are immutable: they cannot be changed after they have been created.
        //All of the String methods and C# operators that appear to modify a string actually return the results in a new string object.
        //An old string is still existing

        //Proof:
        string narrative = "Superman come from the planet, Krypton";
        string narrative2 = narrative; //refers to narrative
        narrative += "\nSuperman is a legal alien";
        Console.WriteLine(narrative);
        Console.WriteLine(narrative2); //writes "Superman come from the planet, Krypton" even the narrative was modified

        //String interpolation for constans (from C# 10)
        const string basePath = "basePath/";
        const string path1 = $"{basePath}path1";

        #endregion Immutibility and constant strings

        #region String concatenation (also with interpolation)

        //String concatenation is a process of appending one string to the end of another string
        //Basic way: concatenate strings by using the + operator (this is not an efficient way, try to avoid if the performance is important)
        string sumOfStrings = nullString + " " + emptyString1 + " " + backsleshesInStrings + " " + atSignString;

        //other example
        sumOfStrings += " Secret Message";

        //Other, also not not efficient way - using Concat method (to examin this issue go to Benchmarks project)
        string concatedString = string.Concat(1993.ToString(), "/", 6.ToString(), "/", 9.ToString()); //better performance
        string concatedString2 = string.Concat(1993, "/", 6, "/", 9); //worse performance

        //The major approach is to use "string interpolation" (especially after c# 10.0 when it was optimized)
        //to use the string interpolation we need to place the '$' before the string.

        //helper strings
        string userName = "Marek";
        string date = DateTime.Today.ToShortDateString();

        //Use string interpolation to concatenate strings.
        string interpolatedString = $"Hello {userName}. Today is {date}.";
        interpolatedString = $"{interpolatedString} How are you today?";

        //the string interpolation uses ToString() method in the background if it is needed. Example:

        //helper variables
        int number = 102;
        bool boolean = true;

        string interpolatedString2 = $"It is {boolean} that Superman can leap in a single bound over the 'Empire State Building' which is {number} stories high";

        //Beginning with C# 10, you can use string interpolation to initialize a constant string when all the expressions used for placeholders are also constant strings.
        const string constantString2 = $"Hello world";
        const string constantString3 = $"Hello{" "}World";
        const string constantString4 = $"{constantString2} Kevin, welcome to the team!";

        //The old way of doing string interpolation is by using String.Format method. To examine efficiency to go project "Benchmarks" and "StringBenchamrs.cs" plus "StringBenchmarksResults.png"
        string oldWayInterpolatedString = string.Format("It is {0} that Superman can leap in a single bound over the 'Empire State Building' which is {1} stories high", boolean, number);
        string oldWayInterpolatedString2 = String.Format("It is {0} that Superman can leap in a single bound over the 'Empire State Building' which is {1} stories high", boolean, number);

        #endregion String concatenation (also with interpolation)

        #region String Methods

        //Substring
        string mainString = "Superman is impervious to gun fire.";
        string subString = mainString.Substring(12, 10);

        //Replace
        string subString2 = "gun";
        string subString3 = "missile";
        string resultString = mainString.Replace(subString2, subString3);
        Console.WriteLine($"The index position of '{subString}' in our main string is {mainString.IndexOf(subString, StringComparison.InvariantCultureIgnoreCase)}");

        //Join
        string joinedString = string.Join(",", "First string", "Second string");
        string joinedString2 = string.Join(" ", new List<string>() { "one", "two", "banana" });

        //Split
        string stringToSplit = "Jon,Tim,Mary,Sue,Bob";
        string[] arrayOfSplitedStrings = stringToSplit.Split(',');

        //Trim
        string stringToTrim = "     Hello World          ";
        string partlyTrimedString = stringToTrim.TrimStart(); //remove spaces from the beggining of the string
        string fullyTrimedString = stringToTrim.TrimEnd(); //remove space from the end of the string
        string fulltTrimedString2 = stringToTrim.Trim(); //remove spaces from the begging and the end of the string.

        //Pad
        string stringToPad = "1.15";
        string leftPaddedString = stringToPad.PadLeft(10, '0');// put zeros before string to make whole string to be 10 characters, need to be in single quotes
        string rigthPaddedString = stringToPad.PadRight(10, '*'); //stars at the end

        //StartsWith, EndsWith, IndexOf, LasIndexOf
        string testString = "this is a test of the search. Let's see how its testing works out";
        bool testStringBooleanHelper;
        int testStringIntegerHelper;

        testStringBooleanHelper = testString.StartsWith("This is");
        testStringBooleanHelper = testString.EndsWith("works out");
        testStringBooleanHelper = testString.Contains("search");
        testStringIntegerHelper = testString.IndexOf("test"); ///when start string "test"
        testStringIntegerHelper = testString.IndexOf("test", 11); ///when start string "test" after character 11
        testStringIntegerHelper = testString.LastIndexOf("test"); ///when start string "test"
        testStringIntegerHelper = testString.LastIndexOf("test", 11); ///when start string "test" after character 11

        //Compare

        //StringComparison will result in not taking into account the difference in upper/lower case in InvariantCultureIgnoreCase is specified;
        string.Compare("Marek", "marek", StringComparison.InvariantCultureIgnoreCase);

        //Insert
        string insertString = "My aim is to test my test project".Insert(4, "(test) ");

        //Remove
        //starting from the position 3 will remove 4 characters
        string removeString = "My aim is to test the c sharp code".Remove(3, 4);

        #endregion String Methods

        #region String Builder

        //String builder is the most efficient way in sens of speed, to deal with string (but for memory allocation, string interpolation is the best)
        //To examine this go to Benchmark project

        StringBuilder stringBuilder = new();
        stringBuilder.AppendLine("Superstring theore is an attempt");
        stringBuilder.AppendLine("to explan all of the particles and");
        stringBuilder.AppendLine("fundamental forces fo nature in");
        stringBuilder.AppendLine("one theory by modelling them");
        stringBuilder.AppendLine("as vibrations of tiny");
        stringBuilder.AppendLine("supersymmetric strings.");

        stringBuilder[8] = 'a';
        string stringBuilderString = stringBuilder.ToString();

        #endregion String Builder

        #region RawStringsLiteral

        //The RawStringLiteral starts with """ and also ends with the same.
        //If we have in the string more doubleuates, then we can make it 4 times
        var xmlPrologue = """<?xml version="1.0" encoding="UTF-8"?>""";
        var xmlPrologue2 = """"<?xml version="1.0" encoding="UTF-8"?>"""";
        //or 5
        var xmlPrologue3 = """""<?xml version="1.0" encoding="UTF-8"?>""""";

        //for json is better
        var json1 = """
        {
            "name" : "Marek"
        }            
        """;

        string marek = "Marek";
        //for string interpolation we need to use that many $$$$$$$ that we want to use for string interpolation:
        var json2 = $$"""
        {
            "name" : {{marek}}
        }            
        """;

        #endregion
    }
}