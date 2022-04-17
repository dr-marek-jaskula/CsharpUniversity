//Controllers (general info) -> UniversityController
//Logging and Telemetry -> Program, LogDemoController, Logs (folder) [with RegulatiosLogStoring.txt]
//Versioning and Swagger -> Program, AccountController, Swagger, wwwroot swaggerstyles
//Validation (Fluent) -> Program, Models (folder) -> Validators, DataTransferObjects
//Exception Handling -> Program, Middlewares (folder) -> ErrorHandlingMiddleware
//AutoMapper -> Program, MapperProfiles, Services (Account, Order)
//Authentication and Authorization -> Program, Authentication (folder), OrderController
//Static Files -> FileController, wwwroot, PrivateFiles, Program.cs
//Polly -> PollyPollicies

//Additional -> Enums, Exceptions

//Same Origin Policy (SOP): the requests are banned until CORS is applied.
//CORS policy: Cross-Origin Resource Sharing
//To examine if the request is possible to be executed at first the request with HTTP verb "OPTIONS" is send
//The Headers with this request are (can be):
//"Access-Control-Request-Method" with HTTP verb like "Access-Control-Request-Method: GET"
//"Access-Control-Request-Headers" with Header like "Access-Control-Request-Headers: Authorization"

//The response for this OPTIONS request with Headers like:
//"Access-Control-Allow-Origin" with domain name like "Access-Control-Allow-Origin: https://CsharpUniversity.com"
//"Access-Control-Allow-Method" with HTTP verb like "Access-Control-Allow-Method: GET, POST, PUT, PATCH"
//"Access-Control-Allow-Headers" with Header like "Access-Control-Allow-Headers: Authorization"

//Configure CORS Policy -> Program (services.AddCore())

//Consider secrets for Authentication