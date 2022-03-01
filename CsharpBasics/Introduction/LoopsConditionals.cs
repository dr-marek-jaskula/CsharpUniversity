namespace CsharpBasics.Introduction;

public class LoopsConditionals
{
    public static void InvokeLoopsExamples()
    {
        Euklides(42, 32);
        Factorial(8);
        CountSmallElements(new List<int>() { 1, 2, 6, 33, 0 });
        PrintCollection(new List<int>() { 1, 2, 6, 33, 0 });
        InstructiveExample(new List<int>() { 1, 2, 6, 33, 0 });
        GuessMagicNumber(3);
        GuessMagicNumber2(4);
    }

    //Example of while loop, used to design the Euklides algorithm 
    private static int Euklides(int a, int b)
    {
        int x = Math.Max(a, b);
        int y = Math.Min(a, b);
        int temporaryNWD = y;
        int NWD = y;

        //the loop will be executed until the condition occurs false. The condition is examined when the while loop is reached.
        while (temporaryNWD > 0)
        {
            NWD = temporaryNWD;
            temporaryNWD = x % y;
            x = y;
            y = temporaryNWD;
        }

        return NWD;
    }

    //Example of do-while loop, used to design the factorial calculation
    private static int Factorial(int a)
    {
        int i = 1;
        int x = 1;

        //the loop will be executed until the condition occurs false. The condition is examined when the first iteration of the do-while loop has finished.
        do
        {
            x *= i;
            i++;
        } while (i <= a);
        
        return x;
    }

    //Example of foreach loop, to iterate though the collection
    private static int CountSmallElements(ICollection<int> collection)
    {
        int counter = 0;

        foreach (int element in collection)
            if (element < 10)
                counter++;

        return counter;
    }

    //Example of for loop
    private static void PrintCollection(ICollection<int> collection)
    {
        //lowered for loop is just a while loop. The first statement is the line that is executed once, next is the condition that is verified before each iteration. Last statement is the line that is executed after the iteration
        for (int i = 0; i < collection.Count; i++)
            Console.WriteLine(collection.ElementAt(i));
    }

    //Example of using break, continue and goto
    private static void InstructiveExample(ICollection<int> collection)
    {
        foreach (int number in collection)
        {
            if (number is 0)
                break; //end the foreach loop

            if (number is 3 or 5)
                Console.WriteLine(number);
            else if (number is 4 or 6)
                continue;
            else if (number is 5 or 7)
                goto SpecialAnnouncement;
            else if (number is 13)
                return;

            Console.WriteLine("Iteration Announcement");
        }
        SpecialAnnouncement:
            Console.WriteLine($"This was goto keyword");
    }

    //Example of switch statement  
    private static void GuessMagicNumber(int number)
    {
        //lowered switch statement is just a combination of if statements. If non of the cases fit the input, the default case is executed
        switch (number) 
        {
            case 1:
                Console.WriteLine("One is too small number");
                break;
            case 2:
                Console.WriteLine("Almost");
                break;
            case 3:
                Console.WriteLine("Indeed, it is the magic number that every student loves");
                goto case 1; //this reault in executing case 1
            case 4:
            case 5:
            case 6:
                Console.WriteLine("Not even close"); //this line of code will be executed if one of above cases is reached
                break;
            default:
                Console.WriteLine("The default case");
                break;
        }
    }

    //Example of if and ? 
    private static void GuessMagicNumber2(int number)
    {
        //standard way
        if (number is 1)
            Console.WriteLine("One is too small number");
        else if (number is 2)
            Console.WriteLine("Almost");
        else if (number is 3)
            Console.WriteLine("Indeed, it is the magic number that every student loves");
        else
            Console.WriteLine("The default case");

        Console.WriteLine("\n-----------\n");

        //using question mark
        Console.WriteLine(number is 1 ? "One is too small number" : 
                          number is 2 ? "Almost" : 
                          number is 3 ? "Indeed, it is the magic number that every student loves" : 
                          "The default case");
    }
}
