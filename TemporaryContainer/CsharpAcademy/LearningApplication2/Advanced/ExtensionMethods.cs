using System;

namespace LearningApplication2
{
    public class LearningHehe
    {
        public void ILearn()
        {
            string learningString = "Time to learn";
            learningString.PrintToConsole(); // look, string has new method!
            //The parameter of PrintToConsole is just this string. Therefore the extension method always has 1 parameters less, because this parameter is just the object

            "hehhe".PrintToConsole();

            SimpleLogger logger = new SimpleLogger();
            logger.LogError("This is an error");
        }
    }

    //Extension methods must be in static class, and they also have to be static
    //we can extend every class/structure that is not static
    public static class ExtensionMethods
    {
        //Extension method extends the given object. To obtain this goal we need to write "this" before the type we want to extend. Then
        public static void PrintToConsole(this string message)
        {
            Console.WriteLine(message);
        }

        //the aim is to extend some libraries.

        //We need to be in the same namespace, otherwise we need to use this namespace (using <namespace name>)

        //We can write namespace System, so then new things will merge with microsoft namespaces (usually we dont marge namespaces)

        //Can extend inferfaces

        //this a better logger.

        public static void LogError(this SimpleLogger logger, string message)
        {
            var defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            logger.Log(message, "Error");
            Console.ForegroundColor = defaultColor;
        }
    }

    public class SimpleLogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }

        public void Log(string message, string messageType)
        {
            Log($"{messageType}: {message}");
        }
    }
}