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
//Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer
//Microsoft.Extensions.PlatformAbstractions
//Newtonsoft.Json
//AspNetCore.HealthChecks.SqlServer
//AspNetCore.HealthChecks.UI
//AspNetCore.HealthChecks.UI.Client
//AspNetCore.HealthChecks.UI.InMemory.Storage
//Swashbuckle.AspNetCore.FiltersSwashbuckle.AspNetCore.Filters

using ASP.NETCoreWebAPI.Authentication;
using ASP.NETCoreWebAPI.HealthChecks;
using ASP.NETCoreWebAPI.Middlewares;
using ASP.NETCoreWebAPI.Models.Validators;
using ASP.NETCoreWebAPI.PollyPolicies;
using ASP.NETCoreWebAPI.Services;
using ASP.NETCoreWebAPI.StringApproxAlgorithms;
using ASP.NETCoreWebAPI.Swagger.SwaggerVersioning;
using EFCore;
using EFCore.Data_models;
using FluentValidation.AspNetCore;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

//This attribute make the Program class (that is from this Top-Level-Statement file and which is internal by default) visible to project "xUnitTestsForWebApi"
[assembly: InternalsVisibleTo("xUnitTestsForWebApi")]

//Logger (Serilog) as a singleton (this logger logs to console - but we can change is)
Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console() //to console, because at the very beginning we have only console
            .CreateBootstrapLogger();

try
{
    //Example log (for tutorial purpose)
    Log.Information("Staring the web host");

    //Create a application builder using WebApplication factory
    var builder = WebApplication.CreateBuilder(args);

    //Configure the Serilog using settings from appsettings.json and enables serilog. We need to also use Serilog in request pipeline
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

    //Add custom authorization based on the custom claims and requirements
    //Nevertheless, this authorization is not dynamic, the dynamic one is defined below for example for "ResourceOperationRequirementHandler"
    builder.Services.AddAuthorization(options =>
    {
        //Add policy based on a custom claim "Nationality" specified in AccountService in GenerateJwt method
        options.AddPolicy(name: MyAuthorizationPolicy.HasNationality, builder =>
            builder.RequireClaim(
                claimType: ClaimPolicy.Nationality,
                ClaimHasNationality.Germany,
                ClaimHasNationality.Poland,
                ClaimHasNationality.Valheim));

        //Add policy based on a custom requirement "MinimumAgeRequirement" specified in Authentication -> MinimumAgeRequirement, MinimumAgeRequirementHandler
        options.AddPolicy(name: MyAuthorizationPolicy.Mature,
            builder => builder.AddRequirements(new MinimumAgeRequirement(MaturePolicy.Eighteen)));

        //Add policy based on a custom requirement "OrderCountRequirement" specified in Authentication -> OrderCountRequirement, OrderCountRequirementHandler
        options.AddPolicy(name: MyAuthorizationPolicy.CreateAtLeastTwoOrders,
            builder => builder.AddRequirements(new MinimumOrderCountRequirement(CreateAtLeast.Two)));
    });

    //Next we need to register handlers for authorization policy
    builder.Services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();
    builder.Services.AddScoped<IAuthorizationHandler, MinimumOrderCountRequirementHandler>();

    //Then we register handler connected with dynamic requirements (One that need to be defined in run-time)
    //We will execute this handler when the specific shop resource is reached
    //1. Inject into OrderService the IAuthorizationService
    //2. Inject into OrderService the IUserContextService (custom made in Services). Made for flexibility of using user claims (its Dependency Inversion): to do this first
    //3. Register Service "IUserContextService"
    builder.Services.AddScoped<IUserContextService, UserContextService>();
    //4. Then we need to Register Service "AddHttpContextAccessor" to be able to Access the IUserService (by this we can inject "IHttpContextAccessor" into IUserContextService)
    builder.Services.AddHttpContextAccessor();
    //5. Add to for instance OrderService "ClaimsPrincipal? user = _userContextService.User;"
    //6. In certain method like "Update" use
    //_authorizationService.AuthorizeAsync(user, order, new ResourceOperationRequirement(ResourceOperation.Update));
    //To verify the requirement
    //4. "User" in the OrderService is the object with claims, that is required for the authorization
    //5. I add claim "PersonId" (in GenerateJwt, in AccountService) to identify if the user has created this order
    builder.Services.AddScoped<IAuthorizationHandler, ResourceOperationRequirementHandler>();

    //Controllers with Fluent Validation (Models -> Validators)
    builder.Services.AddControllers().AddFluentValidation(options =>
    {
        //Validate child properties and root collection elements
        //options.ImplicitlyValidateChildProperties = true; //enables validation of child properties. Its an option to enable whether or not child properties should be implicitly validated if a matching validator can be found. You have to enable this option, if you want it, as it by default is set to false.
        //options.ImplicitlyValidateRootCollectionElements = true; //enables validation of root elements should be implicitly validated. This will only happen when the root model is a collection and a matching validator can be found for the element type.

        //Automatic registration of validators in assembly (therefore there is no need to register validators below)
        options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()); //Makes sure that we automatically register validators from the assembly. We get the execution assembly by using System.Reflection.
    });

    //Versioning (examine AccountController)
    //In versioning the different controllers usually are separate in other subfolders named "V1" and "V2".
    builder.Services.AddApiVersioning(options =>
    {
        //options.DefaultApiVersion = new ApiVersion(1, 0); //set default version to 1.0. This is the same as below (ApiVersion.Default == new ApiVersion(1,0))
        options.DefaultApiVersion = ApiVersion.Default;

        //Router fallback to the default version (specified by the DefaultApiVersion setting) in cases where the router is unable to determine the requested API version.
        options.AssumeDefaultVersionWhenUnspecified = true;

        //Response informs about supported versions of api in "api-supported-versions" header and about obsolete version in header "api-deprecated-versions"
        options.ReportApiVersions = true;

        //Api version needs to be specified in requests.

        //API Versioning Strategies - How to pass the Version Information:
        //1. Default approach: by URL Segment
        //options.ApiVersionReader = new UrlSegmentApiVersionReader();

        //2. Query approach: via query -> In this approach, the version is attached to the URL as a query parameter
        //options.ApiVersionReader = new QueryStringApiVersionReader(); // "/api/account/register/?api-version=1.1" which is default option
        //options.ApiVersionReader = new QueryStringApiVersionReader("v"); // "/api/account/register/?v=1.1" other option

        //3. Header approach: via Header -> In this approach we pass our version information via the request headers
        //options.ApiVersionReader = new HeaderApiVersionReader("api-version");
        //Change header "CustomHeaderVersion" with value "1.1"
        //options.ApiVersionReader = new HeaderApiVersionReader("CustomHeaderVersion");

        //4. ContentType approach: via ContentType -> by extending the media types we use in our request headers to pass on the version information
        //options.ApiVersionReader = new MediaTypeApiVersionReader(); // Content-Type: "application/json;v=1.1" which is default option ("Accept" header)
        //options.ApiVersionReader = new MediaTypeApiVersionReader("v"); // the same as above
        //options.ApiVersionReader = new MediaTypeApiVersionReader("version"); // Content-Type: "application/json;version=1.1" other option

        //5. Reading from more then one sources:
        //Our version information can be obtained from various sources instead of sticking to just one common constraint on all times
        //options.ApiVersionReader = ApiVersionReader.Combine(
        //  new UrlSegmentApiVersionReader(),
        //  new HeaderApiVersionReader("api-version"),
        //  new QueryStringApiVersionReader("api-version"),
        //  new MediaTypeApiVersionReader("version"));
    });

    //Versioning + Swagger
    builder.Services.AddVersionedApiExplorer(options =>
    {
        //Add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
        //The specified format code will format the version as "'v'major[.minor][-status]"
        options.GroupNameFormat = "'v'VVV";

        //This option is only necessary when versioning by url segment.
        //The SubstitutionFormat can also be used to control the format of the API version in route templates
        options.SubstituteApiVersionInUrl = true;
    });

    //DbContext
    builder.Services.AddDbContext<MyDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    // Both with UseDeveloperExceptionPage provides default exception handling for "Developer" stage of api. More information below near "UseDeveloperExceptionPage"
    //.AddDatabaseDeveloperPageExceptionFilter();

    //HealthChecks (need to also Map to the endpoint in the "Configure HTTP request pipeline" region, using the minimal API approach)
    //Add SqlServer health checks and custom one MyHealthCheck (go to: HealthChecks folder)
    builder.Services.AddHealthChecks()
        .AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        .AddCheck<RandomHealthCheck>("Random health check")
        .AddCheck<EndpointHealthCheck>("Endpoint health check");
    builder.Services.AddHealthChecksUI().AddInMemoryStorage();

    //AutoMapper (mapping entities to DataTransferObjects, short. DTO's)
    builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

    //Add Services (dependencies) to the default ASP.Net Core dependency container
    builder.Services.AddScoped<IAccountService, AccountService>();
    builder.Services.AddScoped<IOrderService, OrderService>();
    builder.Services.AddScoped<IGitHubService, GitHubService>();
    builder.Services.AddScoped<IAddressService, AddressService>();

    //ApproximationAlgorithm (there should be one for dictionary, and maybe more for approximation to certain set of strings)
    SymSpells symSpells = new();
    SymSpell symSpellEnDictionary = SymSpellFactory.CreateSymSpell();
    symSpells.SymSpellsDictionary.Add("en", symSpellEnDictionary);
    builder.Services.AddSingleton(symSpells);

    //Register Polly Policies (method ConfigurePollyPolicies extends IServiceCollection)
    builder.Services.ConfigurePollyPolicies(PollyPolicies.GetSyncPolicies(), PollyPolicies.GetAsyncPolicies(), PollyPolicies.GetAsyncPoliciesGitHubUser(), PollyPolicies.GetAsyncPoliciesHttpResponseMessage());

    //Configure IHttpClientFactory for GitHub. "Named client" (it is for GitHub service)
    builder.Services.AddHttpClient("GitHub", client =>
    {
        //Headers required by GitHub
        //Base address
        client.BaseAddress = new Uri("https://api.github.com/");
        //GitHub API versioning
        client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
        //GitHub requires a user-agent
        client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
    });

    //Middlewares
    builder.Services.AddScoped<ErrorHandlingMiddleware>();
    builder.Services.AddScoped<RequestTimeMiddleware>();

    //Password hasher
    builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

    //Validators - we can register them manually here. Sometimes we would like to register them as Singletons to optimize process, but mostly we register them at once, in FluentValidation options that are specified above.
    //Configure to automatically send response "Status: 400 BadRequest" with validation details when invalid request is received.
    builder.Services.AddMvc(opt =>
    {
        opt.Filters.Add(typeof(ValidatorActionFilter));
    });

    //Swagger
    //Swagger and versioning
    builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

    builder.Services.AddSwaggerGen(options =>
    {
        options.OperationFilter<SwaggerDefaultValues>();

        //Replaces "{version}" in swagger to for example "1.0" or "1.1"
        //It is not necessary because of "options.SubstituteApiVersionInUrl = true;"
        //Can be used for other purposes
        //options.DocumentFilter<ReplaceVersionWithExactValueInPathFilter>();

        //Without versioning we just need:
        //options.SwaggerDoc("v1", new OpenApiInfo { Title = "CsharpUniversity API", Version = "v1" });

        //Add filters from this assembly (look "Swagger" folder), needed for "builder.Services.AddSwaggerExamplesFromAssemblyOf();"
        options.ExampleFilters();

        //// adds any string you like to the request headers - in this case, a correlation id
        //options.OperationFilter<AddHeaderOperationFilter>("correlationId", "Correlation Id for the request", false);
        //// [SwaggerResponseHeader]
        //options.OperationFilter<AddResponseHeadersFilter>();

        //Integrate xml comments (ones with "///" before the method)
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);

        // Adds "(Auth)" to the summary so that you can see which endpoints have Authorization (additionally add info about claims and requirements)
        options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

        // add Security information to each operation for OAuth2 (on the right they are padlock generated -> open or close)
        options.OperationFilter<SecurityRequirementsOperationFilter>();

        //To tell the Swagger that authorization is needed -> give bearer and token to be authorized in the swagger
        options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
            In = ParameterLocation.Header, //where the "bearer {toke}" will be stored
            Name = "Authorization", //name of the header
            Type = SecuritySchemeType.ApiKey //the type of security
        });
    });

    //We add swagger response/request examples that can be found in "Swagger" folder. We use "Swashbuckle.AspNetCore.Filters"
    //Then we need to Add another configuration into "AddWaggerGen"
    builder.Services.AddSwaggerExamplesFromAssemblies(Assembly.GetExecutingAssembly());

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

    //This is default approach to obtain the endpoints (and its descriptions) in the browser -> but swagger its the better approach => use Swagger!
    //builder.Services.AddEndpointsApiExplorer();

    #endregion Configure Services

    //Build the application
    var app = builder.Build();

    #region Configure HTTP request pipeline

    //Serilog (middleware for logging every request)
    app.UseSerilogRequestLogging(options =>
    {
        options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} ({UserId}) responded {StatusCode} in {Elapsed:0.0000}ms";
    }); // We want to log all HTTP requests (every request)

    //ResponseCaching
    app.UseResponseCaching();

    //Static files (the default path for static files is "wwwroot")
    app.UseStaticFiles();

    //Corse (use policy added above)
    app.UseCors("FrontEndClient");

    //Middlewares
    app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseMiddleware<RequestTimeMiddleware>();

    //Authentication
    app.UseAuthentication();

    //Redirection
    app.UseHttpsRedirection();

    //Swagger
    //Swagger versioning (add version provider)
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    if (app.Environment.IsDevelopment())
    {
        //We can also use the DeveloperExpectionPage (just when "ASPNETCORE_ENVIRONMENT": "Development")
        //Nevertheless, it will disable our custom exception handling (middleware one). Moreover, it will not be available when ASPNETCORE_ENVIRONMENT is set to "Production"
        //This provide for us detail information about the exception when the call is done by browser. For example by Firefox:
        //https://localhost:7240/LogDemo/PingException
        //app.UseDeveloperExceptionPage();

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            //Build a swagger endpoint for each discovered API version
            foreach (var description in provider.ApiVersionDescriptions)
                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());

            //Overwrite swagger style to dark style (from static files wwwroot -> swaggerstyles -> SwaggerDark.css)
            options.InjectStylesheet("/swaggerstyles/SwaggerDark.css");
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
        //Additional Map the HealthChecks endpoint and health check UI. Additional configuration need to be added to the appsettings.json
        endpoints.MapHealthChecks("/health", new HealthCheckOptions //the /health endpoint give info about the health checks in not bad format
        {
            //To bring together the /healthcheck endpoint and the healthcheksUI
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
    return 1; //the app does not went properly
}
finally
{
    Log.CloseAndFlush();
}

return 0; //the app went properly