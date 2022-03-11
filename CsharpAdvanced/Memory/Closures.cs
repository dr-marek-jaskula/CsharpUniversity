namespace CsharpAdvanced.Memory;

public class Closures
{
    //closures allow you to encapsulate some behavior, pass it around like any other object, and still have access to the context in which they were first declared
    //It's easiest to look at a lot of the benefits (and implementations) of closures with an example.

    //In essence, a closure is a block of code which can be executed at a later time, but which maintains the environment in which it was first created - i.e. it can still use the local variables etc of the method which created it, even after that method has finished executing.
    //The general feature of closures is implemented in C# by anonymous methods and lambda expressions.

    //Due to the fact that the block of code will be use later, somewhere in the memory some variables can be stored (in the heap)
    //Example:
    internal static Action CreateAction()
    {
        int counter = 0; //still it uses the heap memory
        return delegate
        {
            counter++;
            Console.WriteLine($"counter = {counter}");
        };
    }

    //some random numbers for the educational use
    private static readonly List<int> Numbers = Enumerable.Range(0, 1000).ToList();

    public static void InvokeClousersExamples()
    {
        //Example one
        Action action = CreateAction();
        action();
        action();

        //Example two
        var bigNumbers = SelectBigNumbers(400);
        Console.WriteLine($"Number of bit numbers is {bigNumbers}");
    }

    public static int SelectBigNumbers(int number)
    {
        //this will result in creating the display (lowered code) object and allocate some memory in the heap
        //This is caused by the fact that the variable has to be stored somewhere for the lambda expression
        return Numbers.Count(x => x > number);
    }

    //there are some way to avoid this scenario
    public static int SelectBigNumbers2()
    {
        //defining const will not result in creating the display object -> it will save some memory
        const int number = 400; 
        return Numbers.Count(x => x > number);
    }

    public static int SelectBigNumbers3()
    {
        //defining the number inside the lambda. Will save memory
        return Numbers.Count(x => { int number = 400; return x > number; });

        //or just
        //return Numbers.Count(x => x > 400);
    }
}