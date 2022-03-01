namespace CsharpBasics.Introduction;

public class Exceptions
{
    //The exception is a class the inherits from a "Exception" class
    //Exception help to examine the undesirable flow of the application
    //Handling exception and logging them is important from the developer point of view
    //For example in middlewares (web api pipelines) we can create a exception handling middleware to handle all exceptions
    //Custom exception can improve the understanding what is going on with the application
    //To create a custom exception is sufficient to make a simple class that inherits from a "Exception" class
    //Custom exception names should end with Exception suffix (for clarity)

    //Exceptions can be thrown by using 'throw' keyword

    public class EmployeeListNotFoundException : Exception
    {
        public int SecretNumber { get; set; }

        public EmployeeListNotFoundException()
        {
            SecretNumber = 1;
        }

        public EmployeeListNotFoundException(string message) : base(message)
        {
            SecretNumber = 2;
        }

        public EmployeeListNotFoundException(string message, Exception inner) : base("!!!!" + message + "!!!!", inner)
        {
            SecretNumber= 3;
        }
    }

    public static void InvokeExceptionsExamples()
    {
        try
        {
            throw new EmployeeListNotFoundException("I have not found this employee");
        }
        catch (EmployeeListNotFoundException employeeListNotFoundException)
        {
            Console.WriteLine($"Massage: {employeeListNotFoundException.Message} \nThe secret number is: {employeeListNotFoundException.SecretNumber}");
        }
    }
}

