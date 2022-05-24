using ASP.NETCoreWebAPI.Filters;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using SerilogTimings;

namespace ASP.NETCoreWebAPI.Controllers;

// Setup serilog in a two-step process. First, we configure basic logging to be able to log errors during ASP.NET Core startup.
// Later, we read log settings from appsettings.json. Read more at:
// https://github.com/serilog/serilog-aspnetcore#two-stage-initialization.
// General information about serilog can be found at:
// https://serilog.net/

//We use the Serilog because it provides "sinks" which are "log targets"
//So log server for many db, Microsoft, console, insides, files, Xamarin
//Other possibility is NLog.
//NLog is older framework, while Serilog is brand new one
//Serilog is more popular

//We install NuGet:
//Serilog.AspNetCore
//and also its good to get: (for timing purpose, for duration of request and other)
//SerilogTimings
//and also the log server (free for personal use, good for small teams, small or medium solution for apps) -> this one of the Sinks! A good one
//Serilog.Sinks.Seq
//Therefore we need to go to the page: https://datalust.co/seq
//and download are install latest version of Seq
//seq uri: http://localhost:5341/
//Seq uri is specified in appsettings.json

//Serilog is the most popular and its good because it include "sinks" (many log targets) -> only configuration changes to log other place
//Serilog configurations is in "appsettings.json" -> go there to examine configurations

//Important thing to examine the logs of RequestId -> each request has the unique RequestId. We can filter by RequestId and see everything what happened during this id

[ApiController]
[Route("[controller]")]
public class LogDemoController : ControllerBase
{
    //With Serilog, this logger will be transposed to serilogger
    private readonly ILogger<LogDemoController> _logger;

    //For additional metadata included in Serilog (in seq, but in log file there will be also mentioned)
    //Many times we do not need to have a technical details but for example a details about all certain member "Paweł" requests
    //Its possible to add additional data about user (its important)
    private readonly IDiagnosticContext _diagnosticContext;

    public LogDemoController(ILogger<LogDemoController> logger, IDiagnosticContext diagnosticContext)
    {
        _logger = logger;
        _diagnosticContext = diagnosticContext;
    }

    [HttpGet]
    [ServiceFilter(typeof(LoggingFilter))]
    public string Ping()
    {
        //ASP.NET Core has a basic logger for simple purposes. Therefore, we could just use it like that without serilog (log to console is easy)
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
        //without string interpolation here, because of filtering in seq (mb in newer version we could use just interpolated string)
        _logger.LogInformation("PingId {id}", id);

        //easy to know the timing by serilog
        //this will automatically start the stopwatch, and then end it at the end of the block so block is needed
        using (Operation.Time("Do some DB Query"))
        {
            //In reality we would use async await with Entity Framework Core but here we just simulate database query
            Thread.Sleep(500);
        }

        return "PongId";
    }

    //IDiagnosticContext is what serilog uses
    [HttpGet("[action]")]
    public string PingDiagnosticContext()
    {
        //You can enrich the diagnostic context with custom properties.
        //They will be logged with the HTTP request
        //Add additional diagnostic metadata to serilog (seq)
        _diagnosticContext.Set("UserId", "someone"); // we could use the true user id here

        //Then in seq in " HTTP GET /LogDemo/PingDiagnosticContext (someone) responded 200 in 9.6877ms" will be
        //UserId      someone

        return "Pong";
    }
}