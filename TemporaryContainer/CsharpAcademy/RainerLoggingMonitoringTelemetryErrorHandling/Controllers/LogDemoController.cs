namespace RainerLoggingMonitoringTelemetryErrorHandling.Controllers;

using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Events;
using SerilogTimings;
using System.ComponentModel.DataAnnotations;

// Setup serilog in a two-step process. First, we configure basic logging
// to be able to log errors during ASP.NET Core startup. Later, we read
// log settings from appsettings.json. Read more at
// https://github.com/serilog/serilog-aspnetcore#two-stage-initialization.
// General information about serilog can be found at
// https://serilog.net/

[ApiController]
[Route("[controller]")]
public class LogDemoController : ControllerBase
{
    //With Serilogger, this logger will be transposed to seriloger
    private readonly ILogger<LogDemoController> _logger;

    public LogDemoController(ILogger<LogDemoController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public string Ping()
    {
        _logger.LogInformation("Inside of PING");

        throw new InvalidOperationException("Something bad happened");

        //return "Pong";
    }

    [HttpGet("customers/{id}")]
    public string CreateCustomer(int id)
    {
        _logger.LogInformation("Create customer {id}", id); //without string interpolation here, because of filtering in seq

        //easy to know the timing
        using (Operation.Time("Do some DB Query")) //this wil automatically start the stopwatch, and then end it at the end of the block so block is needed
        {
            Thread.Sleep(500);
        }

        return "something";
    }

    [HttpPost("customers")]
    public IActionResult CreateCustomer(Customer customer) //if we specify like this, asp will take it from the body
    {
        _logger.LogInformation("Writing customer {Name} to DB", customer.Name);
        return Ok();
    }
}

//just by specifying Data Transfer Object (DTO) with validation attributes, asp make a model validation properly
public record class Customer(
    [Required][MaxLength] string Name,
    [Range(0, 100)] int Age);


//We use the Serilog because it provides "sinks" which are "log targets"
//So log server for many db, Microsoft, console, insides, files, Xamarin
//Other possibility it NLog. 
//NLog is older framework, while Serilog is brand new one
//Serilog is more popular

//We install NuGet:
//Serilog.AspNetCore
//and also its good to get: (for timing purpose)
//SerilogTimings
//and also the log server (free for personal use, good for small teams)
//Serilog.Sinks.Seq
//Therefore we need to go to the page: https://datalust.co/seq
//and download are install lastest version od Seq
//seq uri: http://localhost:5341/
