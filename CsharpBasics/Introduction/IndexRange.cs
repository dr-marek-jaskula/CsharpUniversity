namespace CsharpBasics.Introduction;

public class IndexRange
{
    public static void InvokeIndexesExamples()
    {
        //Indexing arrays:
        //The language will provide an instance indexer member with a single parameter of type Index for types which meet the following criteria:

        //The type is Countable.
        //The type has an accessible instance indexer which takes a single int as the argument.
        //The type does not have an accessible instance indexer which takes an Index as the first parameter. The Index must be the only parameter or the remaining parameters must be optional.

        //A type is Countable if it has a property named Length or Count with an accessible getter and a return type of int.

        string[] newArray = new[] { "Marek", "Jakub", "Andy", "Alice", "Emily", "Samantha", "Oliver" };

        //old way to get the last one
        string lastElementOldWay = newArray[newArray.Length - 1];
        //normal way to get the last one.
        string lastElementNormalWay = newArray[^1];

        //^0 is same as newArray.Length
        //More precisely, ^ operator define a struc object:
        Index length = ^0;
        Index lastIndex = ^1;

        //this struc has two properties:
        //1) store the value that was given after operator '^'
        int lastIndexValue = lastIndex.Value;
        //2) determine does the index should be consider from the end or from the beginning
        bool lastIndexIsFromEnd = lastIndex.IsFromEnd; //for operator '^' its set to "true"

        //Other example:
        List<char> list = new() { '3', 'f', '5', 'p', '[', 'd', '4', 'k' };
        var value = list[^1];
        // Gets translated to
        //var value = list[list.Count - 1];

        //We can create our own index struct
        Index customIndex = new(3, false);
        char customElement = list.ElementAt(customIndex);

        //Range is just a pair of indexes, with some method
        Range range = 2..4;
        Index startIndex = range.Start;
        Index endIndex = range.End;

        //HOWEVER, the element from collection are taken from the StartIndext (including) to the EndIndext(excluding)
        //Therefore, the whole range is 0..^0

        //we can define range setting the End index property IsFromEnd to true (we can do it also manually)
        Range range2 = 2..^1;

        //we can define range setting the Start index property IsFromEnd to true (we can do it also manually)
        Range range3 = ^3..^1;

        //This syntax says that the Start index is just the first one, so Index with value 0 and IsFromEnd set to false, End index points the last element (but will exclude it)
        Range range4 = ..^1;

        //This syntax says that we take elements from the second one, to the last one
        Range range5 = 1..; //its short from 1..^0

        //This is just from the beginning to the end (the max range)
        Range range6 = ..; //its short from 0..^0
        
        var getbyRange = newArray[range];
        var getbyRange2 = newArray[range2];
        var getbyRange3 = newArray[range3];
        var getbyRange4 = newArray[range4];
        var getbyRange5 = newArray[range5];
        var getbyRange6 = newArray[range6];
    }
}

