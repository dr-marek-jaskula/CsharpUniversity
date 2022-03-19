using System.Diagnostics;

namespace CsharpAdvanced.DesignPatterns.SOLID;

class DepedencyInversion
{
    //High-level modules should not depend on low-level modules. Both should depend on abstractions (e.g., interfaces).
    //Abstractions should not depend on details.Details(concrete implementations) should depend on abstractions.

    //For example, even changing the data base provider (for example for MySQL to SQLServer) without Dependency Injection would require massive changes, but using DI we just need to change interface and would be good. (so the logger has only the list of methods but don't know how the function works). 
    //So interface provides the full list of possibilities without full knowledge. 
}

#region Bad Example, just Dependency Injection - without Dependency Inversion
class DatabaseManager
{
    private readonly LoggerManager _loggerManager;
    private readonly FileManager _fileManager;

    //We inject instances by constructor
    public DatabaseManager(LoggerManager loggerManager, FileManager fileManager)
    {
        _loggerManager = loggerManager;
        _fileManager = fileManager;
    }

    public void SendDataFromFileToDatabase()
    {
        //We use injected objects
        SaveDataIntoDatabase(_fileManager.GetDataFromFile());
        _loggerManager.LogOperation();
    }

    public void SaveDataIntoDatabase(string dataToBeSaved)
    {
        Debug.WriteLine("Saving data to database...");
    }
}

class LoggerManager
{
    public void LogOperation()
    {
        Console.WriteLine("I'm Logging the operation");
    }
}

class FileManager
{
    public string GetDataFromFile()
    {
        return "Data from file";
    }
}
#endregion

#region Good example (with Dependency Inversion)

//At first we need to create abstraction (interfaces are preferred)

interface ICustomLogger
{
    void LogOperation();
}

interface ICustomFileManager
{
    string GetDataFromFile();
}

class OtherLoggerManager
{
    public void LogOperation()
    {
        Console.WriteLine("I'm Logging the operation (other way)");
    }
}

class OtherFileManager
{
    public string GetDataFromFile()
    {
        return "Data from file (Other manager)";
    }
}

//Now we do not need the full knowledge of the instance that are being injected
//The dependency is inversed. 
//We can use multiple different loggers and file managers if we want to
class OtherDatabaseManager
{
    private readonly ICustomLogger _loggerManager;
    private readonly ICustomFileManager _fileManager;

    //We inject instances by constructor
    public OtherDatabaseManager(ICustomLogger logger, ICustomFileManager fileManager)
    {
        _loggerManager = logger;
        _fileManager = fileManager;
    }

    public void SendDataFromFileToDatabase()
    {
        //We use injected objects
        SaveDataIntoDatabase(_fileManager.GetDataFromFile());
        _loggerManager.LogOperation();
    }

    public void SaveDataIntoDatabase(string dataToBeSaved)
    {
        Debug.WriteLine("Saving data to database...");
    }
}

#endregion