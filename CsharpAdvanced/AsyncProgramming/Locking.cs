namespace CsharpAdvanced.AsyncProgramming;

public class Locking
{
    //There can happen, that two different threads will try to access the same part of code, that we would like to keep just for one thread at once.
}

//As an example we will use the singleton pattern (internal is done just for tutorial purpose)
internal class Configuration
{
    private static Configuration? _instance = null;
    private static readonly object _lockObject = new();

    public string StringProperty { get; set; } = string.Empty;
    public int IntProperty { get; set; }

    private Configuration()
    {
    }

    public static Configuration GetInstance()
    {
        //Only a single thread can create a configuration
        lock (_lockObject)
        {
            if (_instance == null)
                _instance = new Configuration();
        }

        return _instance;
    }
}