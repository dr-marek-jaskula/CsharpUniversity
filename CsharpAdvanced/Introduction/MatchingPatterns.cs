namespace CsharpAdvanced.Introduction;

public class MatchingPatterns
{
    public static void InvokeMatchingPatternsExamples()
    {
        //int input = 6;

        //if (input is not (float or double))
        //{
        //    return;
        //}
        //PAtterns
        //https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/patterns
    }
}




#region Helpers (ILogger)

public interface ILogger
{
    LogLevel logLevel { get; }
    void LogMessage(string message, LogLevel level = LogLevel.Verbose);
}

public enum LogLevel
{
    Verbose,
    Debug,
    Criteria
}

public class ConsoleLogger : ILogger
{
    public LogLevel logLevel { get; set; }

    public void LogMessage(string message, LogLevel level)
    {
        if (level >= logLevel)
            Console.WriteLine($"Console logger says {message}. LogLevel: {level}");
    }
}

#endregion