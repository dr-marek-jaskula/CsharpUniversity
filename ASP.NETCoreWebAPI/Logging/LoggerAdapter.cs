namespace ASP.NETCoreWebAPI.Logging;

//LoggerAdapter approach is important in case of testability.
//ILogger use the internal components and external methods -> this is very hard to unit test
//To avoid this problem we use ILoggerAdapter (way around)

public class LoggerAdapter<TType> : ILoggerAdapter<TType>
{
    private readonly ILogger<LoggerAdapter<TType>> _logger;

    public LoggerAdapter(ILogger<LoggerAdapter<TType>> logger)
    {
        _logger = logger;
    }

    public void LogInformation(string template, params object[] args)
    {
        _logger.LogInformation(template, args);
    }
}
