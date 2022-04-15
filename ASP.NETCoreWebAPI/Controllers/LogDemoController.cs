using Microsoft.AspNetCore.Mvc;
using SerilogTimings;

namespace ASP.NETCoreWebAPI.Controllers;

// Setup serilog in a two-step process. First, we configure basic logging to be able to log errors during ASP.NET Core startup.
// Later, we read log settings from appsettings.json. Read more at:
// https://github.com/serilog/serilog-aspnetcore#two-stage-initialization.
// General information about serilog can be found at:
// https://serilog.net/

[ApiController]
[Route("[controller]")]
public class LogDemoController : ControllerBase
{
    //With Serilog, this logger will be transposed to serilogger
    private readonly ILogger<LogDemoController> _logger;

    public LogDemoController(ILogger<LogDemoController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public string Ping()
    {
        _logger.LogInformation("Inside of Ping");
        return "Pong";
    }

    [HttpGet("[action]")]
    public string PingException()
    {
        _logger.LogInformation("Inside of PingException");
        throw new InvalidOperationException("Something bad happened");
    }

    [HttpGet("[action]/{id}")]
    public string PingId(int id)
    {
        _logger.LogInformation("PingId {id}", id); //without string interpolation here, because of filtering in seq (mb in newer version we could use just interpolated string)

        //easy to know the timing
        using (Operation.Time("Do some DB Query")) //this will automatically start the stopwatch, and then end it at the end of the block so block is needed
        {
            Thread.Sleep(500);
        }

        return "PongId";
    }
}

//We use the Serilog because it provides "sinks" which are "log targets"
//So log server for many db, Microsoft, console, insides, files, Xamarin
//Other possibility is NLog.
//NLog is older framework, while Serilog is brand new one
//Serilog is more popular

//We install NuGet:
//Serilog.AspNetCore
//and also its good to get: (for timing purpose)
//SerilogTimings
//and also the log server (free for personal use, good for small teams)
//Serilog.Sinks.Seq
//Therefore we need to go to the page: https://datalust.co/seq
//and download are install latest version of Seq
//seq uri: http://localhost:5341/