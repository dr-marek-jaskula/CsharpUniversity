using ASP.NETCoreWebAPI.Authentication;
using ASP.NETCoreWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ASP.NETCoreWebAPI.Registration;

public static class AuthenticationRegistration
{
    public static AuthenticationSettings ConfigureAuthentication(this ConfigurationManager configuration)
    {
        AuthenticationSettings authenticationSettings = new();

        //Gets Authentication object from appsettings.json and bind it to our authenticationSettings instance
        configuration.GetSection("Authentication").Bind(authenticationSettings);

        return authenticationSettings;
    }

    public static void RegisterAuthentication(this IServiceCollection services, AuthenticationSettings authenticationSettings)
    {
        //There should be only one authenticationSettings for the whole WebApi. Therefore, it should be Singleton (life cycle equal to application life cycle)
        services.AddSingleton(authenticationSettings);

        //Add authentication with "Bearer" scheme
        services.AddAuthentication(option =>
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
        services.AddAuthorization(options =>
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
        services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();
        services.AddScoped<IAuthorizationHandler, MinimumOrderCountRequirementHandler>();

        //Then we register handler connected with dynamic requirements (One that need to be defined in run-time)
        //We will execute this handler when the specific shop resource is reached
        //1. Inject into OrderService the IAuthorizationService
        //2. Inject into OrderService the IUserContextService (custom made in Services). Made for flexibility of using user claims (its Dependency Inversion): to do this first
        //3. Register Service "IUserContextService"
        services.AddScoped<IUserContextService, UserContextService>();
        //4. Then we need to Register Service "AddHttpContextAccessor" to be able to Access the IUserService (by this we can inject "IHttpContextAccessor" into IUserContextService)
        services.AddHttpContextAccessor();
        //5. Add to for instance OrderService "ClaimsPrincipal? user = _userContextService.User;"
        //6. In certain method like "Update" use
        //_authorizationService.AuthorizeAsync(user, order, new ResourceOperationRequirement(ResourceOperation.Update));
        //To verify the requirement
        //4. "User" in the OrderService is the object with claims, that is required for the authorization
        //5. I add claim "PersonId" (in GenerateJwt, in AccountService) to identify if the user has created this order
        services.AddScoped<IAuthorizationHandler, ResourceOperationRequirementHandler>();
    }
}