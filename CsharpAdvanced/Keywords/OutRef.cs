using System.Diagnostics;

namespace CsharpAdvanced.Keywords;

public class OutRef
{
    //At first, let us deal with "ref" keyword.
    //"ref" keyword determines, that a certain parameters is a reference to a variable, so it wont be copy by value
    //We need to place "ref" keyword in two places: before parameters in function definition and before input when invoking method

    public static void InvokeRefExamples()
    {
        //let us define the int variable (that is a value type)
        int y = 10;

        //to pass 'y' by reference we need to:
        OutKeyword1(ref y);

        //The value of 'y' was modified
        Debug.WriteLine(y);
    }

    //Now let us deal with "out" keyword.
    //"out" pass the reference of the variable, but no its value, so it can be uninitialized, and when it is, its value still needs to be determined

    public static void InvokeOutExamples()
    {
        //Let us declare an int variable
        int x;

        //And define an 'z' variable
        int z = 1000;
        
        OutKeyword2(out x);
        Console.WriteLine(x);
        
        OutKeyword2(out z);
        Console.WriteLine(x);

        //Other examples
        int number;
        int number2;
        bool result4 = int.TryParse("14", out number);
        bool result5 = int.TryParse("abc", out number2);
        Debug.WriteLine(result4);
        Debug.WriteLine(number);
        Debug.WriteLine(result5);
        Debug.WriteLine(number2);
    }

    //To pass by reference, we need to write "ref" before the type
    public static void OutKeyword1(ref int variable)
    {
        variable += 10;
    }

    public static void OutKeyword2(out int variable)
    {
        variable = 0;
        variable += 10;
    }
}


