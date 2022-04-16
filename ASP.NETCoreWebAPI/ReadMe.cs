//Controllers (general info) -> UniversityController
//Logging and telemetry -> Program, LogDemoController, Logs (folder)
//Versioning and Swagger -> Program, AccountController, Swagger, wwwroot swaggerstyles
//Validation (Fluent) -> Program, Models (folder) -> Validators, DataTransferObjects
//Exception Handling -> Program, Middlewares (folder) -> ErrorHandlingMiddleware
//AutoMapper -> Program, MapperProfiles, Services (Account, Order)
//Authentication and Authorization -> Program, Authentication (folder), OrderController
//Polly -> PollyPollicies

//Additional -> Enums, Exceptions

/////////////

//Consider secrets for Authentication

//CreateById do -> mb in this database for users, Or expand our database

//Default value for user name

//Register User Dto:
//                { Employee.DateOfBirth: DateTime } => user.Employee.DateOfBirth.Value.ToString("yyyy-MM-dd"),
//{ Customer.DateOfBirth: DateTime } => user.Customer.DateOfBirth.Value.ToString("yyyy-MM-dd"),
//_ => "null"

//sprobowac zrobic DateOfBirth jako null i zobaczyc

//TO DO:
//[Produces("application/json")]
//[Consumes("application/json")]

//The same of:
//[Produces(MediaTypeNames.Application.Json)]
//[Consumes(MediaTypeNames.Application.Json)]

//[HttpGet]
////Inform user about return and request body
//[Route("ProducesConsumes")]
//[Produces(MediaTypeNames.Application.Json)]
//[Consumes(MediaTypeNames.Application.Json)]
//[AllowAnonymous]
//public ActionResult GetProducesConumes([FromBody] int body)
//{
//    return Ok();
//}

//[ServiceFilter(typeof(ValidationFilterAttribute))]

//[ResponseCache(CacheProfileName = "120SecondsDuration")]

//[ApiExplorerSettings(GroupName = "v2")]