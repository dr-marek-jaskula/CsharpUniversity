namespace CsharpAdvanced.AsyncProgramming;

//Locking:
//There can happen, that two different threads will try to access the same part of code, that we would like to keep just for one thread at once.

//We can use:
//1. Lock objects
//2. Semaphores (and slim versions)

//As an example we will use the singleton pattern (internal is done just for tutorial purpose)
internal class Configuration
{
    private static Configuration? _instance = null;
    //Lock object to make sure just one thread will enter the lock block (but can not do async-await inside the lock)
    private static readonly object _lockObject = new();

    //This object will give us locking (for 1 thread inside, if we need more threads, we would need to give 2 or more in the constructor)
    private SemaphoreSlim _semaphore = new(1);
    //Or full version:
    private Semaphore _semaphore2 = new(1, 1);

    public string StringProperty { get; set; } = string.Empty;
    public int IntProperty { get; set; }

    private Configuration()
    {
    }

    public static Configuration GetInstance()
    {
        //Only a single thread can create a configuration
        //Nevertheless, we can not use the async await in the lock object
        lock (_lockObject)
        {
            if (_instance == null)
                _instance = new Configuration();
        }

        return _instance;
    }

    public async Task DoWorkAsync(int valueToAdd) 
    { 
        //Start the block of code that only one thread can reach and the async job is allowed. 
        await _semaphore.WaitAsync(100); //Its good to make timeout for semaphore
        //Use try-finally to be sure that _semaphore.Release() is called (mb we could make our class and make IDisposable)
        try
        {
            await ComputeAsync(valueToAdd);
        }
        finally
        {
            //We need to release the semaphore 
            _semaphore.Release();
        }
    }

    private static async Task ComputeAsync(int valueToAdd)
    {
        await Task.Delay(1000 * valueToAdd);
    }
}