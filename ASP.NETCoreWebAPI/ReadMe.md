## ASP.NET Core WebApi University

This is a tutorial .NET 6+ WebApi with many different scenarios like: using NoThrow, Refit or Polly.
Comments are used in much intense, even if in a commercial application they should be only used if there is no other way to improve code readability.

In this project there are presented also not recommended approaches (see for instance DurationLoggerAttribute), because in some very rare situation they can used to solved a problem.

This project refers "EFCore" library, where the models and configurations are made.

The major part of service registration was moved to the "Registration" directory. 
Each class in the "Registration" directory was added to the Microsoft.Extensions.DependencyInjection namespace.
The reason is to avoid adding additional using and to simulate that it is a default functionality (this approach is very common in many libraries)
Moreover, there is also an "MyCustomExtension" class - a template for extension method with an additional options parameters (in forms of lambdas)

## Naviagion

- Controllers general informations can be found in: UniversityController
- Logging and telemetry can be found in: Program, LogDemoController, Logging (folder), appsettings.json
	- Logging with LoggerAdapter was used to improve testability (we can inject ILoggerAdapter instead of just a ILogger)
- Versioning and Swagger can be found in: Program, AccountController, Swagger (folder), wwwroot swaggerstyles, SwaggerController + [FILTERING](https://github.com/mattfrear/Swashbuckle.AspNetCore.Filters)
- Validation (Fluent) can be found in: Program, Models (folder), Validators, DataTransferObjects
- Exception Handling can be found in: Program, Middlewares (folder), ErrorHandlingMiddleware
- AutoMapper can be found in: Program, MapperProfiles, Services (Account, Order) 
	- ! For high performance scenarios consider using "Mapster" NuGet Package instead of "AutoMapper". Also look on AutoGenerationCode for Mapping.
- Authentication and Authorization can be found in: Program, Authentication (folder), OrderService, OrderController
- Static Files can be found in: FileController, wwwroot, PrivateFiles, Program.cs
- HealthChecks can be found in: program.cs (in configure and configure services), appsettings.json, HealthChecks folder
- BackgroundService with moder Timer (PeriodicTimer introduced in .NET 6) is presented in the RepetingBackgroundService
- IHttpCLientFactory can be found in: GitHubService
- Decorators with Scrutor can be found in: Decorators directory (in Services directory), DecoratorsRegistration (in Registration directory) and in DecoratedGitHubService, OrderService
- Refit usage: RefitGitHubService (Service directory) and HttpClientRegistration (Registration directory)

## Entity Framework Core topics

- BulkUpdates and BulkDelets with "linq2db.EntityFrameworkCore" namespace were used to improve performance.
The example usage was presented in OrderService, BulkUpdate action.
Read more in EFCore -> EF Core advance -> Problems -> BulkUpdateOrDelete.

- Includes with user expressions (like "GetOrder(10252, db, o => o.Customer, o => o.Payment)") were presented for scalability, readability reasons.
The example usage was presented in OrderService, GetOrder method. 

- Safe raw sql with interpolation was presented in EFCore project, but it should be underlined here:
```
.FromSqlInterpolated($@"
    SELECT * FROM [table_name] WHERE Id = {variable};
    ");
```

- LazyLoading is turend off in this project. To use implicit Lazy Loading use one of the following approach:
* Proxies
1) Install package "Microsoft.EntityFrameworkCore.Proxies"
2) Confirm that are database references (relation) are marked as "virtual"
3) Add "UseLazyLoadingProxies()" on DbContext registration (Be careful, because LazyLoading can cause troubles)

* [ILazyLoader] (https://www.learnentityframeworkcore.com/lazy-loading)
1) Use the "Microsoft.EntityFrameworkCore.Abstractions"
2) Inject ILazyLoader into the certain data model (with using Microsoft.EntityFrameworkCore.Infrastructure;):
3) Then the getter should be like:
``csharp
public virtual List<Book> Books
{
    get => _lazyLoader.Load(this, ref _books);
    set => _books = value;
}
```

- ExplicitLoading was be used instead (an explicit Lazy Loading) 
The example usage was presented in OrderService, GetById action. 

- Find method where presented instead of FirstOrDefault for performance reasons (if it can be used)
The example usage was presented in OrderService, GetById action.

## Polly

Polly package usage was presented in: PollyPollicies, GitHubController, GitHubServices, GitHubServicePollyDecorator.
Decorator pattern for Polly Polities is **very important**. It was used in GitHubServicePollyDecorator and DecoratedGitHubService.

## Filtering and Pagination

Filtering and Pagination with async programming and Polly were used in: Models directory, PageResult (mainly), QueryObjects, OrderQueryValidator, OrderController, OrderService, Polly

Pagination in: PageResult, OrderQueryValidator, QueryObjects, OrderController, OrderService

## Error handling middleware 

In order to centralize the exception handling and provide the preferred way of returning the message about raised exception we use "ErrorHandlingMiddleware".
There, we use "ProblemDetails" class that returns both human and machine readable problem details.

## NoThrow by LanguageExt.Core

Exception throwing can be expensive, however the centralized way to deal with them is very convenient. 
If the performance is very important, we can return an exception, rather then throw it. To obtain such behavior in a convenient way, we use
**Language.Ext.Core** NuGet package. 

Use cases are presented in: NoThrowController, NoThrowService;

## Same Origin Policy (SOP)

SOP states that requests are banned until Cross-Origin Resource Sharing (CORS) policy is applied.

To examine if the request is possible to be executed at first the request with HTTP verb "OPTIONS" is send
The Headers with this request are (can be):
"Access-Control-Request-Method" with HTTP verb like "Access-Control-Request-Method: GET"
"Access-Control-Request-Headers" with Header like "Access-Control-Request-Headers: Authorization"

The response for this OPTIONS request with Headers like
"Access-Control-Allow-Origin" with domain name like "Access-Control-Allow-Origin: https://CsharpUniversity.com"
"Access-Control-Allow-Method" with HTTP verb like "Access-Control-Allow-Method: GET, POST, PUT, PATCH"
"Access-Control-Allow-Headers" with Header like "Access-Control-Allow-Headers: Authorization"

The CORS policy is configured in program.cs.

## Secrets

Consider secrets to store local data, like connection strings. Do not use secrets for sensitive data.

## Continuous Integration and Continuous Deployment/Delivery 

Continuous Integration (short. CI), Continuous Deployment (short. CD). 
CI is automatic process of building and testing our app when pushing to the master branch.

Continuous Delivery is almost the same as Continuous Deployment. The only difference is that in delivery the manual acceptation is needed.
In Continuous Deployment all is automated.

Process:
Local -> Pull Request (pipeline) [restore dependencies -> build an application -> run tests] -> merge -> GitHub repository

Tools for CI: 
- Jenkins 
- Azure DevOps 
- TeamCity
- TravisCI
- GitHub Actions

GitHub Actions is a free tool that will be used here.

In order to learn CI, first create a simple test for our application. Then go to folder CI_CD.