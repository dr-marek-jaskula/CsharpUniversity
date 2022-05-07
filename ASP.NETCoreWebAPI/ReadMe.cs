//Controllers (general info) -> UniversityController
//Logging and Telemetry -> Program, LogDemoController, Logs (folder) [with RegulatiosLogStoring.txt], appsettings.json
//Versioning and Swagger -> Program, AccountController, Swagger, wwwroot swaggerstyles
//Validation (Fluent) -> Program, Models (folder) -> Validators, DataTransferObjects
//Exception Handling -> Program, Middlewares (folder) -> ErrorHandlingMiddleware
//AutoMapper -> Program, MapperProfiles, Services (Account, Order)
//Authentication and Authorization -> Program, Authentication (folder), OrderService, OrderController
//Static Files -> FileController, wwwroot, PrivateFiles, Program.cs

//IHttpCLientFactory -> GitHubService
//Polly -> PollyPollicies, GitHubController, GitHubServices and look below
//Filtering and Pagination with async programming and Polly -> Models (PageResult [mainly], QueryObjects), OrderQueryValidator, OrderController, OrderService, Polly
//  Pagination: PageResult -> OrderQueryValidator -> QueryObjects -> OrderController -> OrderService

//Additional -> Enums, Exceptions

//Same Origin Policy (SOP): the requests are banned until CORS is applied.
//CORS policy: Cross-Origin Resource Sharing
//To examine if the request is possible to be executed at first the request with HTTP verb "OPTIONS" is send
//The Headers with this request are (can be):
//"Access-Control-Request-Method" with HTTP verb like "Access-Control-Request-Method: GET"
//"Access-Control-Request-Headers" with Header like "Access-Control-Request-Headers: Authorization"

//The response for this OPTIONS request with Headers like:s
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