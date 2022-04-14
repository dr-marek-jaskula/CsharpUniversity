//This is the main entrypoint of our application.

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

using EFCore;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Reflection;
using ASP.NETCoreWebAPI.PollyPolicies;
using ASP.NETCoreWebAPI.Authentication;
using ASP.NETCoreWebAPI.Middlewares;
using Microsoft.AspNetCore.Identity;
using ASP.NETCoreWebAPI.Services;
using Microsoft.OpenApi.Models;
using EFCore.Data_models;
using Serilog;
using Serilog.Events;
using Microsoft.AspNetCore.Mvc.Versioning;
using ASP.NETCoreWebAPI.Models.Validators;

//Logger (Serilog) as a singleton
Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateBootstrapLogger();

//Example log (for tutorial purpose)
Log.Information("Staring the web host");

//Create a application builder using WebApplication factory
var builder = WebApplication.CreateBuilder(args);

//Configure the Serilog using settings from appsettings.json
builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext());

#region Configure Services

//Authentication settings
AuthenticationSettings authenticationSettings = new();

//Gets Authentication object from appsettings.json and bind it to our authenticationSettings instance
builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);

//There should be only one authenticationSettings for the whole WebApi. Therefore, it should be Singleton (life cycle equal to application life cycle)
builder.Services.AddSingleton(authenticationSettings);

//Add authentication with "Bearer" scheme
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = authenticationSettings.JwtIssuer,
        ValidAudience = authenticationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
    };
});

//Controllers with Fluent Validation (Models -> Validators)
builder.Services.AddControllers().AddFluentValidation(options =>
{
    //Validate child properties and root collection elements
    //options.ImplicitlyValidateChildProperties = true; //enables validation of child properties. Its an option to enable whether or not child properties should be implicitly validated if a matching validator can be found. You have to enable this option, if you want it, as it by default is set to false.
    //options.ImplicitlyValidateRootCollectionElements = true; //enables validation of root elements should be implicitly validated. This will only happen when the root model is a collection and a matching validator can be found for the element type.
    
    //Automatic registration of validators in assembly (therefore there is no need to register validators below)
    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()); //Makes sure that we automatically register validators from the assembly. We get the execution assembly by using System.Reflection.
});

//Versioning
//In versioning the different controllers usually are separate in other subfolders named "V1" and "V2".
builder.Services.AddApiVersioning(options =>
{
    //options.DefaultApiVersion = new ApiVersion(1, 0); //set default version to 1.0. This is the same as below (ApiVersion.Default == new ApiVersion(1,0))
    options.DefaultApiVersion = ApiVersion.Default;

    //Router fallback to the default version (specified by the DefaultApiVersion setting) in cases where the router is unable to determine the requested API version.
    options.AssumeDefaultVersionWhenUnspecified = true;

    //Response informs about supported versions of api in "api-supported-versions" header.
    options.ReportApiVersions = true;

    //Api version needs to be specified in requests.

    //API Versioning Strategies - How to pass the Version Information:
    //1. Default approach: by URL Segment
    //options.ApiVersionReader = new UrlSegmentApiVersionReader();

    //2. Query approach: via query -> In this approach, the version is attached to the URL as a query parameter
    //options.ApiVersionReader = new QueryStringApiVersionReader(); // "/api/account/register/?api-version=2.0" which is default option
    //options.ApiVersionReader = new QueryStringApiVersionReader("v"); // "/api/account/register/?v=2.0" other option

    //3. Header approach: via Header -> In this approach we pass our version information via the request headers
    options.ApiVersionReader = new HeaderApiVersionReader("api-version");

    //4. ContentType approach: via ContentType -> by extending the media types we use in our request headers to pass on the version information
    //options.ApiVersionReader = new MediaTypeApiVersionReader(); // Content-Type: "application/json;v=2.0" which is default option ("Accept" header)
    //options.ApiVersionReader = new MediaTypeApiVersionReader("version"); // Content-Type: "application/json;version=2.0" other option

    //Change header "CustomHeaderVersion" and also "version=2.0" is replace by "2.0"
    //options.ApiVersionReader = new HeaderApiVersionReader("CustomHeaderVersion");

    //5. Reading from more then one sources:
    //Our version information can be obtained from various sources instead of sticking to just one common constraint on all times
    //options.ApiVersionReader = ApiVersionReader.Combine(
    //  new UrlSegmentApiVersionReader(),
    //  new HeaderApiVersionReader("api-version"),
    //  new QueryStringApiVersionReader("api-version"),
    //  new MediaTypeApiVersionReader("version"));
});

//DbContext 
builder.Services.AddDbContext<MyDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//??????????
//.AddDatabaseDeveloperPageExceptionFilter();

//AutoMapper (mapping entities to DataTransferObjects, short. DTO's)
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//Add Services (dependencies) to the default ASP.Net Core dependency container
builder.Services.AddScoped<IAccountService, AccountService>();

//Register Polly Policies (method ConfigurePollyPolicies extends IServiceCollection)
builder.Services.ConfigurePollyPolicies(PollyPolicies.GetPolicies(), PollyPolicies.GetAsyncPolicies());

//Middlewares
builder.Services.AddScoped<ErrorHandlingMiddleware>();

//Password hasher
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

//Validators - we can register them manually here. Sometimes we would like to register them as Singletons to optimize process, but mostly we register them at once, in FluentValidation options that are specified above.
//Configure to automatically send response "Status: 400 BadRequest" with validation details when invalid request is received.
builder.Services.AddMvc(opt =>
{
    opt.Filters.Add(typeof(ValidatorActionFilter));
});

//Context
builder.Services.AddScoped<IUserContextService, UserContextService>();
builder.Services.AddHttpContextAccessor();

//Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CsharpUniversity API", Version = "v1" });
});

//Corse
builder.Services.AddCors(option =>
{
    option.AddPolicy("FrontEndClient", policyBuilder =>
    {
        policyBuilder.AllowAnyMethod().AllowAnyHeader().WithOrigins(builder.Configuration["AllowedOrigins"]);
    });
});

//????
//builder.Services.AddEndpointsApiExplorer();

#endregion Configure Services

//Build the application
var app = builder.Build();

#region Configure HTTP request pipeline

//ResponseCaching
app.UseResponseCaching();

//Static files
app.UseStaticFiles();

//Corse
app.UseCors("FrontEndClient");

//Middlewares
app.UseMiddleware<ErrorHandlingMiddleware>();

//Authentication
app.UseAuthentication();

//Redirection
app.UseHttpsRedirection();

//Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => 
    { 
        //Set swagger endpoint
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CsharpUniversity API v1");
        //Overwirte swagger style to dark style (from static files wwwroot -> swaggerstyles -> SwaggerDark.css)
        c.InjectStylesheet("/swaggerstyles/SwaggerDark.css");
    });
}

//Routing
app.UseRouting();

//Authorization
app.UseAuthorization();

//Endpoints
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

#endregion Configure HTTP request pipeline

//Run the application
app.Run();