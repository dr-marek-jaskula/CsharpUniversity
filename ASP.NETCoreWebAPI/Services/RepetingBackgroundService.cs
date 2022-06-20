namespace ASP.NETCoreWebAPI.Services;

//BackgroundService is the service that will run in the background while the application is running

public class RepetingBackgroundService : BackgroundService
{
    //PeriodicTimer was introduced in .NET 6 and it provides timing in a modern way

    //Timespan can be provided from elsewhere 
    private readonly PeriodicTimer _timer = new(TimeSpan.FromMilliseconds(1000));

    //We can use dependecy injection as it is demonstrated here
    private readonly ILogger<RepetingBackgroundService> _logger;

    public RepetingBackgroundService(ILogger<RepetingBackgroundService> logger)
    {
        _logger = logger;
    }

    //This is a method that needs to be overriden when inheriting from BackgroundService. It was make "async"
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (await _timer.WaitForNextTickAsync(stoppingToken) && !stoppingToken.IsCancellationRequested)
        {
            //even thought the work takes time, each second the time is displayed
            await DoWorkAsync();
        }
    }

    private async Task DoWorkAsync()
    {
        Console.WriteLine(DateTime.Now.ToString("O"));
        //Time used for working
        await Task.Delay(500);
    }
}