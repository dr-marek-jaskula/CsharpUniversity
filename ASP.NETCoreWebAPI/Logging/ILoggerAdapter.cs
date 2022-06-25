namespace ASP.NETCoreWebAPI.Logging;

public interface ILoggerAdapter<TType>
{
    void LogInformation(string template, params object[] args);

    //This two methods are for even better logging
    void Log(LogLevel logLevel, string template, params object[] args);

    //This is for very elegant decorating the services
    IDisposable TimedOperation(string template, params object[] args);
}

//Just ILoggerAdapter for testability (the one above is for decorating)
//public interface ILoggerAdapter<TType>
//{
//    void LogInformation(string template, params object[] args);
//}