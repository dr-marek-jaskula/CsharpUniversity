namespace ASP.NETCoreWebAPI.Logging;

//LoggerAdapter approach (Pattern) is important in case of testability.
//ILogger use the internal components and external methods -> this is very hard to unit test (hard to moq)
//To avoid this problem we use ILoggerAdapter (way around)

public class LoggerAdapter<TType> : ILoggerAdapter<TType>
{
    private readonly ILogger<LoggerAdapter<TType>> _logger;

    public LoggerAdapter(ILogger<LoggerAdapter<TType>> logger)
    {
        _logger = logger;
    }

    public void Log(LogLevel logLevel, string template, params object[] args)
    {
        _logger.Log(logLevel, template, args);
    }

    public void LogInformation(string template, params object[] args)
    {
        Log(LogLevel.Information, template, args);
    }

    public IDisposable TimedOperation(string template, params object[] args)
    {
        return new TimedLogOperation<TType>(this, LogLevel.Information, template, args);
    }
}

//Just LoggerAdapter for testability (the one above is for decorating)
//public class LoggerAdapter<TType> : ILoggerAdapter<TType>
//{
//    private readonly ILogger<LoggerAdapter<TType>> _logger;

//    public LoggerAdapter(ILogger<LoggerAdapter<TType>> logger)
//    {
//        _logger = logger;
//    }

//    public void LogInformation(string template, params object[] args)
//    {
//        _logger.LogInformation(template, args);
//    }
//}