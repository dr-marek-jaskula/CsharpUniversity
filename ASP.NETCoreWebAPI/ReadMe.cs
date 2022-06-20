//Most of the code was move to the registries in the "Registry" folder
//In registry there is also an "MyCustomExtension", a template for extension method with an additional options parameter, in form of lambda (action)
//This approach is very common in many libraries

//Controllers (general info) -> UniversityController
//Logging and Telemetry -> Program, LogDemoController, Logs (folder) [with RegulatiosLogStoring.txt], appsettings.json
//Versioning and Swagger -> Program, AccountController, Swagger (folder), wwwroot swaggerstyles, SwaggerController + FILTERING (https://github.com/mattfrear/Swashbuckle.AspNetCore.Filters)
//Validation (Fluent) -> Program, Models (folder) -> Validators, DataTransferObjects
//Exception Handling -> Program, Middlewares (folder) -> ErrorHandlingMiddleware
//AutoMapper -> Program, MapperProfiles, Services (Account, Order)
//Authentication and Authorization -> Program, Authentication (folder), OrderService, OrderController
//Static Files -> FileController, wwwroot, PrivateFiles, Program.cs
//HealthChecks -> program.cs (in configure and configure services), appsettings.json, HealthChecks folder

//BackgroundService with moder Timer (PeriodicTimer introduced in .NET 6) is presented in the "RepoetingBackgroundService"

//bulkUpdates / bulkDelets with "linq2db.EntityFrameworkCore" -> See EFCore -> EF Core advance -> Problems -> BulkUpdateOrDelete

//Includes with expression user like "Order order = await GetOrder(10252, db, o => o.Customer, o => o.Payment);" -> Services -> OrderService -> GoDown

//Safe raw sql with interpolation example:
//.FromSqlInterpolated($@"
//    SELECT * FROM [table_name] WHERE Id = {variable};
//    ");

//LazyLoading -> (in this project the Lazy Loading is turned off
//  A) Proxies approach
//1) Install package "Microsoft.EntityFrameworkCore.Proxies"
//2) Confirm that are database references (relation) are marked as "virtual"
//3) Add "UseLazyLoadingProxies()" on DbContext registration (Be careful, because LazyLoading can cause troubles!)

//  B) ILazyLoader approach (https://www.learnentityframeworkcore.com/lazy-loading)
//Use the "Microsoft.EntityFrameworkCore.Abstractions"
//Inject ILazyLoader into the certain data model (with using Microsoft.EntityFrameworkCore.Infrastructure;):
//Then the getter should be like:

//  public virtual List<Book> Books
//  {
//      get => _lazyLoader.Load(this, ref _books);
//      set => _books = value;
//  }

//IHttpCLientFactory -> GitHubService
//Polly -> PollyPollicies, GitHubController, GitHubServices and look below
//Filtering and Pagination with async programming and Polly -> Models (PageResult [mainly], QueryObjects), OrderQueryValidator, OrderController, OrderService, Polly
//  Pagination: PageResult -> OrderQueryValidator -> QueryObjects -> OrderController -> OrderService

//Avoiding exception approach using "LanguageExt.Core" NuGet Package. For performance in specific cases -> NoThrowController, NoThrowService

//Additional -> Enums, Exceptions

//Same Origin Policy (SOP): the requests are banned until CORS is applied
//CORS policy: Cross-Origin Resource Sharing
//To examine if the request is possible to be executed at first the request with HTTP verb "OPTIONS" is send
//The Headers with this request are (can be):
//"Access-Control-Request-Method" with HTTP verb like "Access-Control-Request-Method: GET"
//"Access-Control-Request-Headers" with Header like "Access-Control-Request-Headers: Authorization"

//The response for this OPTIONS request with Headers like
//"Access-Control-Allow-Origin" with domain name like "Access-Control-Allow-Origin: https://CsharpUniversity.com"
//"Access-Control-Allow-Method" with HTTP verb like "Access-Control-Allow-Method: GET, POST, PUT, PATCH"
//"Access-Control-Allow-Headers" with Header like "Access-Control-Allow-Headers: Authorization"

//Configure CORS Policy -> Program (services.AddCore())

//Consider secrets for Authentication

//Continuous Integration (short. CI), Continuous Deployment (short. CD). Sometime CD is called Continuous Delivery (which is almost the same, because in delivery the manual acceptation i needed and in Deployment all is automatic the same sense)
//In order to build an app and check all test when pushing, we can manage CI - automatic process of building and testing our app when pushing to the master branch
//Local -> Pull Request (pipeline) [restore dependencies -> build an application -> run tests] -> merge -> GitHub repository
//Tools for CI: Jenkins, Azure DevOps, TeamCity, TravisCI, GitHub Actions
//GitHub Actions is a free tool
//In order to learn CI, first create a simple tests for our application
// -> folder CI_CD 