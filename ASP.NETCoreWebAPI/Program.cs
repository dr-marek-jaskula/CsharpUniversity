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

//Versioning !!!!!!!!!!!!!!!!!!!
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = ApiVersion.Default;

    //options.DefaultApiVersion = new ApiVersion(1, 1); //to robi, ¿e defaultowa wersja jest 1.1
    //options.ApiVersionReader = new MediaTypeApiVersionReader("version"); //to sprawie, ¿e zamiast jako parametr, bêdzie w headerze "Accept" i trzeba napis¹c "application/json; version=2.0"
    //options.ApiVersionReader = new HeaderApiVersionReader("CustomHeaderVersion"); // to tworzy ze nie bêdzie w headerze Accept, tylko w "CustomHeaderVersion". Wtedy nie trzeba pisaæ "version=2.0" ale po porstu "2.0"

    /*mo¿na te¿ oba naraz zrobiæ 
options.ApiVersionReader = ApiVersionReader.Combine(
    new MediaTypeApiVersionReader("version"),
    new HeaderApiVersionReader("CustomHeaderVersion")
    ); */

    //options.ReportApiVersions = true; //to robi, ¿e daje odpowiedz, gdzie w headerze info o supportowanych versjach
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

//Validators

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