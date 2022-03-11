namespace CsharpAdvanced.Introduction;

public class ExtendedMethods
{
    //Extended method are method that are added to other class, to improve the application on some type. 
    //Mostly we extend those types that we cannot access directly, like int or string
    //The extension method are defined using "this" keyword before the first method parameter

    //Extension methods must be in static class, and they also have to be static
    //We can extend every class/structure that is not static

    //Extension is in the scope of current namespace

    public void InvokeExtendedMethodExamples()
    {
        string learningString = "Let us analyze extension methods";

        //The string was extended by "PrintToConsole" method
        learningString.PrintToConsole(); 
        learningString.PrintToConsole2(); 
        learningString.PrintToConsole2(15); 

        "myString".PrintToConsole();

        SimpleLogger logger = new();
        logger.LogError("This is an error");
    }
}

internal static class ClassUsedToExtend
{
    //This extension method extends the string. 
    //Therefore, "this string message" is crucial:
    //"this" determines that this method extends other class/structure,
    //"string" determines that the extended class will be string
    //"message" is the string on which the method will be used
    public static void PrintToConsole(this string message)
    {
        Console.WriteLine(message);
    }

    //We can also add additional parameters
    public static void PrintToConsole2(this string message, int number = 10)
    {
        Console.WriteLine(message + $" {number}");
    }

    //We can also extend other, custom classes
    public static void LogError(this SimpleLogger logger, string message)
    {
        var defaultColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        logger.Log(message, "Error");
        Console.ForegroundColor = defaultColor;
    }

    //We can also extend the interface [However we can just make a new interface that implement the other one]
    public static void NewMethod(this IStreamable streamable)
    {
        // Do something with streamable.
    }
}

public class SimpleLogger 
{
    public void Log(string message) => Console.WriteLine(message);
    public void Log(string message, string messageType) => Log($"{messageType}: {message}");
}

internal interface IStreamable
{
    void SimpleMethod();
}