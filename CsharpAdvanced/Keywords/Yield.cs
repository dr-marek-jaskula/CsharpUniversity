namespace CsharpAdvanced.Keywords;

public class Yield
{
    //When you use the yield contextual keyword in a statement, you indicate that the method, operator, or get accessor in which it appears is an iterator

    //there are two ways of 'yield' use
    //'yield return'
    //'yield break'

    //Use a 'yield return' statement to return each element one at a time
    //The sequence returned from an iterator method can be consumed by using a foreach or for statement or LINQ query.

    //Each iteration of the foreach loop calls the iterator method. When a yield return statement is reached in the iterator method (see "Iterators.cs" file in "Introduction" folder), expression is returned
    //The important this is that the current location in code is retained. Execution is restarted from that location the next time that the iterator function is called.

    //The yield type of an iterator that returns IEnumerable or IEnumerator is object.
    //If the iterator returns IEnumerable<T> or IEnumerator<T>, there must be an implicit conversion from the type of the expression in the yield return statement to the generic type parameter.

    //You can use a yield break statement to end the iteration.

    //We can't include a yield return or yield break statement in:
    //1) Lambda expressions and anonymous methods.
    //2) Methods that contain unsafe blocks.

    //Moreover,
    //A yield return statement can't be located in a try-catch block.
    //A yield return statement can be located in the try block of a try-finally statement.

    public static void InvokeYieldExamples()
    {
        //standard way, return whole data at once
        var getData = GetData(); 

        //Data is returned one by one so we can add additional logic when it is returned
        var yieldedData1 = GetYieldedData1();
        var yieldedData2 = GetYieldedData2(); 

        foreach (int element in yieldedData2)
            if (element > 5)
                break;
        //Let us notice that the "After Yielding" string was not printed to the console

        var a = yieldedData2.First(); // this will not result in reaching the "After Yielding" 
        var b = yieldedData2.Last(); // this will result in reaching the "After Yielding" 

        //this statement also would result in reaching the "After Yielding"
        //var y = yieldedData2.ToList();

        var yieldedData3 = GetYieldedData3();

        //here the yield break is presented. It ends whole iteration
        foreach (int element in yieldedData3)
            if (element > 5)
                break;
    }

    #region Helper functions

    internal static IEnumerable<int> GetData()
    {
        var result = new List<int>();

        for (int i = 0; i < 9; i++)
            result.Add(i + 5);

        return result;
    }

    internal static IEnumerable<int> GetYieldedData1()
    {
        yield return 1;
        yield return 4;
        yield return 14;
        yield return -3;
    }

    internal static IEnumerable<int> GetYieldedData2()
    {
        for (int i = 0; i < 9; i++)
            yield return i + 3;

        Console.WriteLine("After Yielding");
    }

    internal static IEnumerable<int> GetYieldedData3()
    {
        for (int i = 0; i < 9; i++)
        {
            yield return i + 3;

            if (i % 3 == 2)
                yield break;
        }
    }

    #endregion Helper functions
}
