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
using FluentValidation;
using ASP.NETCoreWebAPI.Models.DataTransferObjects;
using ASP.NETCoreWebAPI.Models.Validators;
using Microsoft.OpenApi.Models;
using EFCore.Data_models;
using Serilog;
using Serilog.Events;

//Logger (Serilog)
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
builder.Services.AddControllers().AddFluentValidation();

//Versioning
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = ApiVersion.Default;
});

//DbContext 
builder.Services.AddDbContext<MyDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionStrings:DefaultConnection")));
//??????????
//.AddDatabaseDeveloperPageExceptionFilter();

//AutoMapper (mapping entities to DataTransferObjects, short. DTO's)
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//Add Services (dependencies) to the default ASP.Net Core dependency container
//Services...

//Register Polly Policies (method ConfigurePollyPolicies extends IServiceCollection)
builder.Services.ConfigurePollyPolicies(PollyPolicies.GetPolicies(), PollyPolicies.GetAsyncPolicies());

//Middlewares
builder.Services.AddScoped<ErrorHandlingMiddleware>();

//Password hasher
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

//Validators
builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();

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
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "CsharpUniversity API v1"); });
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