## ASP.NET Core WebApi University

This is a tutorial .NET 6+ WebApi with many different scenarios like: using NoThrow, Refit or Polly.
Comments are used in much intense, even if in a commercial application they should be only used if there is no other way to improve code readability.

In this project there are presented also not recommended approaches (see for instance DurationLoggerAttribute), because in some very rare situation they can used to solved a problem.

This project refers "EFCore" library, where the models and configurations are made.

The major part of service registration was moved to the "Registration" directory. 
Each class in the "Registration" directory was added to the Microsoft.Extensions.DependencyInjection namespace.
The reason is to avoid adding additional using and to simulate that it is a default functionality (this approach is very common in many libraries)
Moreover, there is also an "MyCustomExtension" class - a template for extension method with an additional options parameters (in forms of lambdas)

Async programming for WebApi is used not to block the thread. It is not connected with a performance.

## Naviagion

- Controllers general informations can be found in: UniversityController
- Logging and telemetry can be found in: Program, LogDemoController, Logging (folder), appsettings.json
	- Logging with LoggerAdapter was used to improve testability (we can inject ILoggerAdapter instead of just a ILogger)
    - Warning! Do not use string interpolation for logging. Use: _logger.LogInformation("Create User: {User} at {When}", user, when);
- Versioning and Swagger can be found in: Program, AccountController, Swagger (folder), wwwroot swaggerstyles, SwaggerController + [FILTERING](https://github.com/mattfrear/Swashbuckle.AspNetCore.Filters)
- Validation (Fluent) can be found in: Program, Models (folder), Validators, DataTransferObjects
- Exception Handling can be found in: Program, Middlewares (folder), ErrorHandlingMiddleware
- AutoMapper can be found in: Program, MapperProfiles, Services (Account, Order) 
    - ! The use of "ProjectTo" approach for better performance is presented in "AddressService -> GetById"
	- ! For very high performance scenarios consider using "Mapster" NuGet Package instead of "AutoMapper". Also look on AutoGenerationCode for Mapping.
- Authentication and Authorization can be found in: Program, Authentication (folder), OrderService, OrderController
- Static Files can be found in: FileController, wwwroot, PrivateFiles, Program.cs
- HealthChecks can be found in: program.cs (in configure and configure services), appsettings.json, HealthChecks folder
- BackgroundService with moder Timer (PeriodicTimer introduced in .NET 6) is presented in the RepetingBackgroundService
- IHttpCLientFactory can be found in: GitHubService
- Decorators with Scrutor can be found in: Decorators directory (in Services directory), DecoratorsRegistration (in Registration directory) and in DecoratedGitHubService, OrderService
- Refit usage: RefitGitHubService (Service directory) and HttpClientRegistration (Registration directory)

## Option Pattern for Entity Framework Core

The Option folder with "DatabaseOptions.cs" and "DatabaseOptionsSetup.cs" were created to apply Option Pattern.
Also the "DatabaseOptions" section in appsettings.json was created. It has properties with names that match properties names in "DatabaseOptions" class.

In program.cs we need to add line "builder.Services.ConfigureOptions<DatabaseOptions>();" and then user the AddDbContext overload with serviceProvider

Using this approach we have the following benefit:
- To apply any new changes we just need to update the application settings and restart the application (we do not need to do another deployment)

Other way (without Option Pattern) is just:
```csharp
builder.Services.AddDbContext<MyDbContext>(options => options
    .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), options => options.EnableRetryOnFailure(3))
    ));
```

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

- LazyLoading is turned off in this project. To use implicit Lazy Loading use one of the following approach:
* Proxies
1) Install package "Microsoft.EntityFrameworkCore.Proxies"
2) Confirm that are database references (relation) are marked as "virtual"
3) Add "UseLazyLoadingProxies()" on DbContext registration (Be careful, because LazyLoading can cause troubles)

* [ILazyLoader] (https://www.learnentityframeworkcore.com/lazy-loading)
1) Use the "Microsoft.EntityFrameworkCore.Abstractions"
2) Inject ILazyLoader into the certain data model (with using Microsoft.EntityFrameworkCore.Infrastructure;):
3) Then the getter should be like:
```csharp
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

- Splitting queries (using "AsSplitQuery" method) was used to avoid the cartesian explosion problem. It was used in "OrderService" -> "GetById"
It can be used to increase performance, but we should be careful:
```
When using split queries with Skip/Take, pay special attention to making your query ordering fully unique; not doing so could cause incorrect data to be returned. For example, if results are ordered only by date, but there can be multiple results with the same date, then each one of the split queries could each get different results from the database. Ordering by both date and ID (or any other unique property or combination of properties) makes the ordering fully unique and avoids this problem. Note that relational databases do not apply any ordering by default, even on the primary key.
```

For instance for id = 1, the not split (without AsSplitQuery)
```sql
SELECT [p].[Id], [p].[Name], [p].[Price], [t].[ShopId], [t].[ProductId], [t].[Amount], [t].[Id], [t].[AddressId], [t].[Description], [t].[Name], [t0].[TagId], [t0].[ProductId], [t0].[Id], [t0].[ProductTag]
FROM [Product] AS [p]
LEFT JOIN (
    SELECT [p0].[ShopId], [p0].[ProductId], [p0].[Amount], [s].[Id], [s].[AddressId], [s].[Description], [s].[Name]
    FROM [Product_Amount] AS [p0]
    INNER JOIN [Shop] AS [s] ON [p0].[ShopId] = [s].[Id]
) AS [t] ON [p].[Id] = [t].[ProductId]
LEFT JOIN (
    SELECT [p1].[TagId], [p1].[ProductId], [t1].[Id], [t1].[ProductTag]
    FROM [Product_Tag] AS [p1]
    INNER JOIN [Tag] AS [t1] ON [p1].[TagId] = [t1].[Id]
) AS [t0] ON [p].[Id] = [t0].[ProductId]
WHERE [p].[Id] = 1
ORDER BY [p].[Id], [t].[ShopId], [t].[ProductId], [t].[Id], [t0].[TagId], [t0].[ProductId];
```
Above query results in two rows in which we have some duplicated data.

But with splitting queries we have two queries from it:
```sql
SELECT [t].[ShopId], [t].[ProductId], [t].[Amount], [t].[Id], [t].[AddressId], [t].[Description], [t].[Name], [p].[Id]
FROM [Product] AS [p]
INNER JOIN (
    SELECT [p0].[ShopId], [p0].[ProductId], [p0].[Amount], [s].[Id], [s].[AddressId], [s].[Description], [s].[Name]
    FROM [Product_Amount] AS [p0]
    INNER JOIN [Shop] AS [s] ON [p0].[ShopId] = [s].[Id]
) AS [t] ON [p].[Id] = [t].[ProductId]
WHERE [p].[Id] = 1
ORDER BY [p].[Id];
```
and
```sql
SELECT [t0].[TagId], [t0].[ProductId], [t0].[Id], [t0].[ProductTag], [p].[Id]
FROM [Product] AS [p]
INNER JOIN (
    SELECT [p0].[TagId], [p0].[ProductId], [t].[Id], [t].[ProductTag]
    FROM [Product_Tag] AS [p0]
    INNER JOIN [Tag] AS [t] ON [p0].[TagId] = [t].[Id]
) AS [t0] ON [p].[Id] = [t0].[ProductId]
WHERE [p].[Id] = 1
ORDER BY [p].[Id]
```
there is no duplicated data in these rows.

## Polly

Polly package usage was presented in: PollyPollicies, GitHubController, GitHubServices, GitHubServicePollyDecorator.
Decorator pattern for Polly Polities is **very important**. It was used in GitHubServicePollyDecorator and DecoratedGitHubService.

## Filtering and Pagination

Filtering and Pagination with async programming and Polly were used in: Models directory, PageResult (mainly), QueryObjects, OrderQueryValidator, OrderController, OrderService, Polly

Pagination in: PageResult, OrderQueryValidator, QueryObjects, OrderController, OrderService

## Error handling middleware 

In order to centralize the exception handling and provide the preferred way of returning the message about raised exception we use "ErrorHandlingMiddleware".
Then, we use "ProblemDetails" class that returns both human and machine readable problem details.

## Repostory Pattern

The repository pattern was demonstrated in "AddressServiceRepositoryPattern" and folder "Repositories". 
Aim of this approach is to distinguish the db layer for the service layer (business logic). 
One of the advantages of this approach is that, the dbContext can be easily mock - or rather, we just need to mock the certain repository.
Without it, to mock the dbContext, we would need to make an in-memory database or use library like "EntityFrameworkCoreMock.NSubstitute"

## DateTimeProvider

In order to make classes that uses the DateTime testable, the most common approach is to use DateTimeProvider.
Here, the DateTimeProvider (and the IDateTimeProvider interface) is placed in "Helpers" and it is used just for DateTimeValidator, but in reality it should be used everywhere it is needed.

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

## Secrets

Consider secrets to store local data, like connection strings. Do not use secrets for sensitive data.
