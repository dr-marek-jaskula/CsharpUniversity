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

//Versioning - api version is different from swagger version (!!!!!!!!!!!!! should connect them)
builder.Services.AddApiVersioning(options =>
{
    //options.DefaultApiVersion = ApiVersion.Default;
    options.DefaultApiVersion = new ApiVersion(1, 1); //set default version to 1.1

    //Router fallback to the default version (specified by the DefaultApiVersion setting) in cases where the router is unable to determine the requested API version
    options.AssumeDefaultVersionWhenUnspecified = true;

    //This changes the header "Accept" to "CustomHeaderVersion" and there is no need to write "version=2.0" but just "2.0"
    //options.ApiVersionReader = new HeaderApiVersionReader("CustomHeaderVersion"); 

    //the version of api needs to be specified in requests. For example in header "Accept" -> "application/json; version=2.0"
    //options.ApiVersionReader = new MediaTypeApiVersionReader("version");

    //responses informs in "api-supported-versions" header about supported versions of api.
    //options.ReportApiVersions = true;

    //To do both we can:
    //options.ApiVersionReader = ApiVersionReader.Combine(
    //new MediaTypeApiVersionReader("version"),
    //new HeaderApiVersionReader("CustomHeaderVersion")
    //);
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