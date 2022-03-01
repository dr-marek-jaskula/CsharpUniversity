using System.Collections;
using System.Xml.Linq;

namespace CsharpAdvanced.Introduction;

public class Interators
{
    /*
    The declaration of an iterator must meet the following requirements:

    The return type must be one of the following types:
        IAsyncEnumerable<T>
        IEnumerable<T>
        IEnumerable
        IEnumerator<T>
        IEnumerator
    The declaration can't have any in, ref, or out parameters.
    */

    //by implementing one of above interfaces, we can create a custom iterator. 
    //in this file, custom iterator will be presented

    //IEnumerable interface has one method:
    //GetEnumerator of IEnumerator return type (or generic one)

    //IEnumerator has two method and one property:
    //method MoveNext of bool return type
    //method Reset of void return type
    //property Current that is readonly (only getter)

    //Enumerator has no 'length' because it just iterates until it fails
    //Therefore iterator can be of 'infinite' length (examples below)

    public static void InvokeIteratorExamples()
    {
        int[] array1 = new int[] { 1, 3, 5 };

        //proof that array is IEnumerable
        var enumerator = array1.GetEnumerator();

        //MoveNext moves to the next element and return true if it was successful movement
        while (enumerator.MoveNext()) 
            Console.WriteLine($"{enumerator.Current}");

        //this will result in the infinite loop, but the break keyword was used to leave the loop
        var infiniteEnumerable = new MyInfiniteEnumerable();
        foreach (var i in infiniteEnumerable)
        {
            Console.WriteLine($"{i}");
            if ((int)i == 100) break; 
        }
        //in each iteration GetEnumeator is executed

        //bother example
        var infiniteEnumerable2 = new MyInfiniteEnumerable();
        var enumerator2 = infiniteEnumerable2.GetEnumerator();
        while (enumerator2.MoveNext())
        {
            Console.WriteLine($"{enumerator2.Current}");
            if ((int)enumerator2.Current == 100) break;
        }

        //finite ones
        var finiteEnumerable3 = new MyFiniteEnumerable2(new int[] { 4, 7, 32, 1 });
        var enumerator3 = finiteEnumerable3.GetEnumerator();

        Console.WriteLine(enumerator3.Current);

        while (enumerator3.MoveNext()) 
            Console.WriteLine($"A is {enumerator3.Current}");
    }
}

//custom infinite enumerable
public class MyInfiniteEnumerable : IEnumerable<int> 
{
    public IEnumerator GetEnumerator()
    {
        return new MyInfiniteEnumerator();
    }

    IEnumerator<int> IEnumerable<int>.GetEnumerator()
    {
        return new MyInfiniteEnumerator();
    }
}

//custom infinite enumerator
public class MyInfiniteEnumerator : IEnumerator<int>
{
    public int Current { get; private set; } = 0;

    object IEnumerator.Current => Current; 

    public void Dispose()
    {
    }

    public bool MoveNext()
    {
        Current++;
        return true;
    }

    public void Reset()
    {
        Current = 0;
    }
}

//custom finite enumerable with data stored
public class MyFiniteEnumerable2 : IEnumerable<int>
{
    private int[] myData1; 

    public MyFiniteEnumerable2(int[] array)
    {
        myData1 = array;
    }

    public IEnumerator GetEnumerator()
    {
        return new MyFiniteEnumerator2(myData1);
    }


    IEnumerator<int> IEnumerable<int>.GetEnumerator()
    {
        return new MyFiniteEnumerator2(myData1);
    }
}

//custom finite enumerator
public class MyFiniteEnumerator2 : IEnumerator<int>
{
    public int Current { get; private set; } = 0;

    object IEnumerator.Current => mValues1[mIndex1];
    public void Dispose()
    {
    }

    public bool MoveNext()
    {
        mIndex1++;
        return mIndex1 < mValues1.Length;
    }

    public void Reset()
    {
        Current = 0;
    }

    private int[] mValues1;
    private int mIndex1 = -1; //the reason why its -1 is that after +1 it will be 0

    public MyFiniteEnumerator2(int[] array)
    {
        mValues1 = array;
    }
}


