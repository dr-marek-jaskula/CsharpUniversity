using Serilog.Events;
using Serilog;
using Microsoft.AspNetCore.Mvc;

//first we add here the logger
Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateBootstrapLogger();

//detail of serilog should be in appsettings

try
{
    Log.Information("Staring the web host");

    var builder = WebApplication.CreateBuilder(args);

    // Full setup of serilog. We read log settings from appsettings.json
    builder.Host.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()); //after the builder. Stuff important

    // Add services to the container.
    builder.Services.AddControllers();
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    app.UseSerilogRequestLogging(configure =>
    {
        configure.MessageTemplate = "HTTP {RequestMethod} {RequestPath} ({UserId}) responded {StatusCode} in {Elapsed:0.0000}ms";
    }); // We want to log all HTTP requests (EVERY REQUEST)

    app.UseHttpsRedirection();
    app.UseAuthorization();

    //mb use thi in the Error Handling Middleware
    if (!app.Environment.IsDevelopment())
    {
        // Do not add exception handler for dev environment. In dev,
        // we get the developer exception page with detailed error info.
        app.UseExceptionHandler(errorApp =>
        {
            // Logs unhandled exceptions. For more information about all the
            // different possibilities for how to handle errors see
            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/error-handling?view=aspnetcore-5.0
            errorApp.Run(async context =>
            {
                // Return machine-readable problem details. See RFC 7807 for details.
                // https://datatracker.ietf.org/doc/html/rfc7807#page-6
                var pd = new ProblemDetails //this class represents the standard
                {
                    Type = "https://demo.api.com/errors/internal-server-error", //this is just a identifier (can customize)
                    Title = "An unrecoverable error occurred",
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = "This is a demo error used to demonstrate problem details",
                };
                pd.Extensions.Add("RequestId", context.TraceIdentifier);
                await context.Response.WriteAsJsonAsync(pd, pd.GetType(), null, contentType: "application/problem+json");
            });
        });
    }

    app.MapControllers();

    app.MapGet("/ping", () => "pong");

    //additional diagnostic info
    app.MapGet("/request-context", (IDiagnosticContext diagnosticContext) => //this IDiagnosticContext is the part of Serilog
    {
        // You can enrich the diagnostic context with custom properties.
        // They will be logged with the HTTP request.
        diagnosticContext.Set("UserId", "someone"); //this someone is the username for instance taken from the token
    });


    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly"); //this will occur if app doesnt start
    return 1; //the app does not went properly
}
finally
{
    Log.CloseAndFlush();
}

return 0; //the app went properly