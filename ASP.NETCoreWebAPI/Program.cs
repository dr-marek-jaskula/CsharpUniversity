//This is the main entrypoint of our application.
//Most of the code was move to the registries in the "Registration" folder
//Each class in the "Registration" folder was added to the "namespace Microsoft.Extensions.DependencyInjection;"
//The reason is to avoid adding additional using and simulate it is a default functionality (its very elegant)

//Install NuGet Packages:
//AutoMapper.Extensions.Microsoft.DependencyInjection
//FluentValidation.AspNetCore
//Microsoft.AspNetCore.Authentication.JwtBearer
//Microsoft.AspNetCore.Mvc.Versioning
//Microsoft.AspNetCore.StaticFiles
//Polly
//Polly.Caching.Memory
//Swashbuckle.AspNetCore
//Serilog.AspNetCore
//Serilog.Sinks.Seq
//SerilogTimings
//Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer
//Microsoft.Extensions.PlatformAbstractions
//Newtonsoft.Json
//AspNetCore.HealthChecks.SqlServer
//AspNetCore.HealthChecks.UI
//AspNetCore.HealthChecks.UI.Client
//AspNetCore.HealthChecks.UI.InMemory.Storage
//Swashbuckle.AspNetCore.FiltersSwashbuckle.AspNetCore.Filters
//Microsoft.Extensions.Configuration.UserSecrets -> for secrets to override the appsettings.json locally

using ASP.NETCoreWebAPI.Exceptions;
using ASP.NETCoreWebAPI.HealthChecks;
using ASP.NETCoreWebAPI.PollyPolicies;
using EFCore;
using EFCore.Data_models;
using FluentValidation.AspNetCore;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using System.Reflection;

//This attribute make the Program class (that is from this Top-Level-Statement file and which is internal by default) visible to project "xUnitTestsForWebApi"
//Better approach is to do this in the project file
//[assembly: InternalsVisibleTo("xUnitTestsForWebApi")]

//Logger (Serilog) as a singleton (this logger logs to console - but this will be changed)
Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console() //to console, because at the very beginning we have only a console
            .CreateBootstrapLogger();

try
{
    //Example log (for tutorial purpose)
    Log.Information("Staring the web host");

    //Create a application builder using WebApplication factory
    var builder = WebApplication.CreateBuilder(new WebApplicationOptions
    {
        Args = args,
        ContentRootPath = Directory.GetCurrentDirectory()
    });

    //Environment variables
    //A environment variable with prefix "MyApi_" (example)
    //So after it, there will be a "Database" key like:
    //"MyApi_Database__ConnectionString=Server=db;Port=5432;Database=TestDatabase;User ID=postgres;Password=DataBase;
    //Two underscores "__" mean "go one level deeper" same as colon ':' for appsettings.
    //So we go to the next key "ConnectionString"
    //Then, value is found after '=' character.
    //builder.Configuration.AddEnvironmentVariables("MyApi_");
    //This is not a preferred way: it is better in a docker scenario to use secrets and json file with connection strings
    //For more info go to "DockerSqlUniversity"
    //Nevertheless, sometime this approach can be useful for things like:
    //     - ASPNETCORE_Environment=Production
    //     - ASPNETCORE_URLS=https://+:443;http://+:80

    //Configure the Serilog using settings from appsettings.json and enables serilog. We need to also define ("use") Serilog in request pipeline
    builder.Host.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext());

    #region Configure Services

    //We register "ServiceDescriptors": plans how the services should be resolve (we can also remove services, for example to decorate then and register again)

    //Authentication settings (go to: Registration: AuthenticationRegistration)
    builder.Services.RegisterAuthentication(builder.Configuration.ConfigureAuthentication());

    //Controllers with Fluent Validation (Models -> Validators)
    builder.Services.AddControllers().AddFluentValidation(options =>
    {
        //Validate child properties and root collection elements
        //options.ImplicitlyValidateChildProperties = true; //enables validation of child properties. Its an option to enable whether or not child properties should be implicitly validated if a matching validator can be found. You have to enable this option, if you want it, as it by default is set to false.
        //options.ImplicitlyValidateRootCollectionElements = true; //enables implicit validation of root elements. This will only happen when the root model is a collection and a matching validator can be found for the element type.

        //Automatic registration of validators in assembly (therefore there is no need to register validators below)
        options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()); //Makes sure that we automatically register validators from the assembly. We get the execution assembly by using System.Reflection.
    });

    //For default caching mechanism (Polly politics is preferred). It needs to be defined in pipeline section "app.UseResponseCaching();" This is for tutorial purposes
    builder.Services.AddResponseCaching(); //Nevertheless, the default caching mechanism in .NET 7 looks interesting

    //Versioning (examine AccountController) (go to: Registration: VersioningRegistration)
    builder.Services.RegisterVersioning();

    //DbContext
    builder.Services.AddDbContext<MyDbContext>(options => options
        //.UseLazyLoadingProxies() //To configure all queries to LazyLoading (be careful of it, LazyLoading can cause troubles)
        .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    //Both with UseDeveloperExceptionPage provides default exception handling for "Developer" stage of api. More information below near "UseDeveloperExceptionPage"
    //.AddDatabaseDeveloperPageExceptionFilter();

    //HealthChecks(need to also Map to the endpoint in the "Configure HTTP request pipeline" region, using the minimal API approach)
    //Add SqlServer health checks and custom one MyHealthCheck(go to: HealthChecks folder)
    builder.Services.AddHealthChecks()
        .AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        .AddCheck<RandomHealthCheck>("Random health check")
        .AddCheck<EndpointHealthCheck>("Endpoint health check");
    builder.Services.AddHealthChecksUI().AddInMemoryStorage();

    //AutoMapper (mapping entities to DataTransferObjects)
    builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

    //Add custom services (dependencies) to the default ASP.Net Core dependency container (go to: Registration: ServicesRegistration)
    builder.Services.RegisterServices();

    //Add ServiceDecorators
    builder.Services.RegisterServiceDecorators();

    //ApproximationAlgorithm (there should be one for dictionary, and maybe more for approximation to certain set of strings) (go to: Registration: SymSpellRegistration)
    builder.Services.RegisterSymSpell();

    //Register Polly Policies (method ConfigurePollyPolicies extends IServiceCollection)
    builder.Services.ConfigurePollyPolicies(PollyPolicies.GetSyncPolicies(), PollyPolicies.GetAsyncPolicies(), PollyPolicies.GetAsyncPoliciesGitHubUser(), PollyPolicies.GetAsyncPoliciesHttpResponseMessage());

    //Configure IHttpClientFactory for GitHub. "Named client" (it is for GitHub service) (go to: Registration: HttpClientRegistration)
    builder.Services.RegisterHttpClient(builder.Configuration);

    //Middlewares (go to: Registration: MiddlewaresRegistration)
    builder.Services.RegisterMiddlewares();

    //Password hasher
    builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

    //Validators - we can register them manually here. Sometimes we would like to register them as Singletons to optimize process, but mostly we register them at once, in FluentValidation options that are specified above.
    //Configure to automatically send response "Status: 400 BadRequest" with validation details when invalid request is received.
    //Filters are like Middlewares but for the specific endpoint or controller
    builder.Services.RegisterFilters();

    //Swagger
    //Swagger and swagger versioning (go to: Registration: SwaggerRegistration)
    builder.Services.RegisterSwagger();

    //Optionally -> to avoid the cyclic reference in the serialized json file (DTO is the better approach, but sometime this approach can be useful)
    //builder.Services.Configure<JsonOptions>(options =>
    //{
    //    //options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    //    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    //});

    //Corse
    //Configures CORSE (Cross-Origin Resource Sharing) Policy. Required for connection with frontend
    builder.Services.AddCors(option =>
    {
        //Name of the policy needs to be the same as the name of the Cors policy in the configuration region (below)
        option.AddPolicy("FrontEndClient", policyBuilder =>
        {
            policyBuilder
                .AllowAnyMethod()
                .AllowAnyHeader()
                //In "appsettings.json" we determine what hosts are allowed. To allow all origins we specify "*"
                .WithOrigins(builder.Configuration["AllowedOrigins"]);
        });
    });

    //This is default approach to obtain the endpoints (and its descriptions) in the browser -> but swagger its the better approach => use Swagger
    //builder.Services.AddEndpointsApiExplorer();

    #endregion Configure Services

    //Build the application
    var app = builder.Build();

    #region Configure HTTP request pipeline

    //The middleware order is important

    //Serilog (middleware for logging every request)
    app.UseSerilogRequestLogging(options =>
    {
        options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} ({UserId}) responded {StatusCode} in {Elapsed:0.0000}ms";
    }); // We want to log all HTTP requests (every request)

    //ResponseCaching (default caching, used in FileController.
    //Also the Caching profiles can be used -> in AddControllers options -> option.CacheProfiles.Add(<profileName>, new CacheProfile() { //details };
    app.UseResponseCaching();

    //Static files (the default path for static files is "wwwroot")
    app.UseStaticFiles(); //due to the fact that order is important, here we have that static files are before the authentication

    //Corse (use policy added above)
    app.UseCors("FrontEndClient");

    //Middlewares (go to: Registration: MiddlewaresRegistration)
    app.UseMiddlewares();

    //Authentication
    app.UseAuthentication();

    //Redirection
    app.UseHttpsRedirection();

    if (app.Environment.IsDevelopment())
    {
        //We can also use the DeveloperExpectionPage (just when "ASPNETCORE_ENVIRONMENT": "Development")
        //Nevertheless, it will disable our custom exception handling (middleware one). Moreover, it will not be available when ASPNETCORE_ENVIRONMENT is set to "Production"
        //This provide for us detail information about the exception when the call is done by browser. For example by Firefox:
        //https://localhost:7240/LogDemo/PingException
        //app.UseDeveloperExceptionPage();

        //(go to: Registration: SwaggerRegistration)
        app.ConfigureSwagger();
    }

    #region CustomScope: Apply Migrations

    //The way to create a custom scope.
    //The pending migrations will be applied.

    //1. We get the "IServiceScopeFactory" to create scope.
    //It is a preferred way: the only responsibility of this factory is to create scope
    var serviceScopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();

    //2. Use "using" keyword with the old {} syntax to narrow the scope
    using (var applyMigrationsScope = serviceScopeFactory.CreateScope())
    {
        //2. Get the scoped service (dbContext)
        var dbContext = applyMigrationsScope.ServiceProvider.GetService<MyDbContext>();

        if (dbContext is null)
            throw new UnavailableException("Database is not available");

        var pendingMigrations = dbContext.Database.GetPendingMigrations();

        if (pendingMigrations.Any())
            dbContext.Database.Migrate();
    }

    #endregion CustomScope: Apply Migrations

    //Routing
    app.UseRouting();

    //Authorization
    app.UseAuthorization();

    //Endpoints
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();

        //Additional Map the HealthChecks endpoint and health check UI. Additional configuration need to be added to the appsettings.json
        endpoints.MapHealthChecks("/health", new HealthCheckOptions //the /health endpoint give info about the health checks in not bad format
        {
            //To bring together the /healthcheck endpoint and the healthchecksUI
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
            Predicate = _ => true
        });
        endpoints.MapHealthChecksUI(); //the UI url will be "HealthChecks-ui"
    });

    #endregion Configure HTTP request pipeline

    //Run the application
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly"); //occur if app does not start
    return 1; //app does not went properly
}
finally
{
    Log.CloseAndFlush();
}

return 0; //app went properly