using System.Linq.Expressions;
using System;

namespace CsharpBasics.Introduction;

public class TryCatchFinally
{
    //The try-catch-finally blocks are used to handle exceptions that can occur in our application
    //If the exception is thrown in the try block the respective catch block is executed (in catch we can specify what type of exception it handles)
    //The finally block is executed if the try block and catch block (if there was so) were executed
    //The finally block is important from the safety perspective: if the application is connect to the server or other program, it is important to close the connection (call dispose method) if some problems occur or application crashes

    //In catch block we can for example Log exception

    //More interesting syntax were introduced (since c# 8.0) for the try-finally structure: 'using' keyword.
    //To examine this new syntax to go: "CsharpBasics" project, "Keywords" folder and "Using.cs" file


    //We can make try-catch structure, try-finally structure or try-catch-finally structure
    public static void InvakeTryCatchFinallyExamples()
    {
        int globalNumber = 10;

        try
        {
            int number = 10;
            int secondNumber = 0;
            int result = number / secondNumber; // this line of code will result in DivideByZeroException
        } //only one catch block is executed for each exception that is thrown.
        catch (DivideByZeroException divideByZeroException) //  The type specification is called an exception filter. The exception type should be derived from Exception.
        {
            Console.WriteLine($"Exception message: {divideByZeroException.Message}");
            throw new ArgumentException("Argument was invalid"); //this will not lead to the next catch but to the finally block
        }
        catch (ArgumentException argumentException)
        {
            Console.WriteLine($"{argumentException.ParamName}");
            throw; //we can rethrow the same exception if we want to. This will not lead to the next catch but to finally block
        }
        //We can also specify exception filters to add a boolean expression to a catch clause.
        //Exception filters indicate that a specific catch clause matches only when that condition is true
        catch (FileNotFoundException fileNotFoundException) when (globalNumber < 15) //the variable has to out of the try-catch-finally scope
        {
            Console.WriteLine($"{fileNotFoundException.Message}");
        }
        catch (Exception exception) //the default case: if the exception was not of type specified above, execute this block of code
        {
            Console.WriteLine($"Error! {exception.Message}");
        }
        finally
        {
            Console.WriteLine("Close all connections");
        }

        //one of the ideas or re thrown the exception is to catch an non specific exception and throw a specific one (for example custom made)
    }
}
