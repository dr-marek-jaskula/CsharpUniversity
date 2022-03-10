using System.Collections;

namespace CsharpAdvanced.Introduction;

public class CustomComparers
{
    //In order to compare variables, we use default compares, however sometimes it is wise to create a custom one, that suits the needs

    public static void InvokeCustomComparers()
    {
        ObjectsToCompare element1 = new("Jordan", 10);
        ObjectsToCompare element2 = new("Emanuel", 15);
        ObjectsToCompare element3 = new("Borko", 5);
        ObjectsToCompare element4 = new("Goblin", 25);
        List<ObjectsToCompare> compareList = new() { element1, element2, element3, element4 };

        //Due to the CompareTo implementation (from the IComparable interface) we can sort the above list in the previously determined way
        compareList.Sort(); //sort use the CompareTo method form ObjectsToCompare class

        compareList.Reverse();

        //Custom defined comparison
        var resultofComparison = element1.CompareTo(element3); 
        //default comparison (return 1,0,-1)
        var resultofComparison2 = 3.CompareTo(5);

        #region Other example

        // Initialize a string array.
        string[] words = { "The", "quick", "brown", "fox", "jumps", "over", "the", "lazy", "dog" };
        // Display the array values.

        // Sort the array values using the default comparer.
        Array.Sort(words);

        // Sort the array values using the reverse case-insensitive comparer.
        Array.Sort(words, new ReverserClass());

        #endregion

        MyCustomComparer customComparer = new();
        //Sorting use Comparer defined in the class from customComparer is created
        compareList.Sort(customComparer);
    }
}

#region Custom comparison 
//In order to create a custom comparer we need to create a class that implements the IComparable interface. In this interface we have the declaration of CompareTo method
public class ObjectsToCompare : IComparable 
{
    public string name { get; set; }
    public int powerLvl { get; set; }

    public ObjectsToCompare(string name, int powerLvl)
    {
        this.name = name;
        this.powerLvl = powerLvl;
    }

    //This method compares current instances with other instances from the same collection.
    //The returned value is positive, zero or negative - depend of sign of the return value, the comparison is done
    public int CompareTo(object? obj)
    {
        if (obj is null)
            return 0;

        ObjectsToCompare arg = (ObjectsToCompare)obj;

        if (powerLvl < arg.powerLvl)
            return 1;
        else if (powerLvl == arg.powerLvl)
            return 0;
        else
            return -10; //Therefore, -10 here is the same as -1

        //recommended way is to compare the result with 1, 0 or -1:
        //Less than zero(This instance precedes other in the sort order.)
        //Zero(This instance occurs in the same position in the sort order as other.)
        //Greater than zero(This instance follows other in the sort order.)
    }
}

#endregion

//In order to obtain more freedom with comparing, we can create a class that implements the IComperer class like this:
public class MyCustomComparer : IComparer<ObjectsToCompare> //only for comparing the ObjectsToCompare
{
    public int Compare(ObjectsToCompare? x, ObjectsToCompare? y) 
    {
        if (x is null && y is null)
            return 0;
        else if (x is null)
            return -1;
        else if (y is null)
            return 1;

        if (x.powerLvl < y.powerLvl)
            return 1;
        else if (x.powerLvl == y.powerLvl)
            return 0;
        else
            return -1;
    }
}

public class ReverserClass : IComparer
{
    // Call CaseInsensitiveComparer.Compare with the parameters reversed.
    int IComparer.Compare(object? x, object? y)
    {
        return new CaseInsensitiveComparer().Compare(y, x);
    }
}