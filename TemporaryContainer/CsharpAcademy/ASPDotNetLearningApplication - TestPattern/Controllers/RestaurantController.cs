using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ASPDotNetLearningApplication
{
    //[Route("api/restaurant")]  //bez Routa ścieżka jest określana bazowo, jesli jest odpowiedni plik setting (nie wiem jak go zrobic)
    [Route("api/[controller]")]  //robi ze siezka jest "api/restaurant". Działa też "api/Restaurant"
    //[Route("api/v{version:apiVersion}/[controller]")] //to sprawi, że ścieżka do controllera jest:
    //api/v1.0/Restaurant
    //api/v1/Restaurant (też tak zrozumie)
    //lub dla innych wersji to jest api/v2.0/Restaurant albo api/v2/Restaurant
    [Authorize]
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)] //można wypisać jakei wersje dopuszcza. Deprecated dają dodatkową informację w headerach opowiedzi, że ta versja będzie usunięta (ze jest przestarzała)
    [ApiVersion("2.0")] //czyli tutaj dwie versie są dopuszczone
    public class RestaurantController : ControllerBase //nazwa pozinna konczyc sie na "Controller"
    {
        private readonly IRestaurantService _restaurantService;

        //wstrzykujemy abstrakcje restaurantServicea (nie trzeba dbContext ani abstrakcji mappera bo jest w service)
        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet]
        //[Authorize(Policy = "HasNationality")]
        //[Authorize(Policy = "Atleast20")]
        [AllowAnonymous]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll([FromQuery] RestaurantQuery query)
        {
            //on tu poprzez parametry rozumie parametry obiektu (ciekawe). Można normalnie, ze 3 parametry
            var restaurantsDtos = _restaurantService.GetAll(query);
            return Ok(restaurantsDtos);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<RestaurantDto> Get([FromRoute] int id)
        {
            var restaurant = _restaurantService.GetById(id);
            return Ok(restaurant);
        }

        //jak jest tylko Action Result to nic nie zwraca poza statusem
        //dodaje przez ciało zapytania
        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
        {
            int id = _restaurantService.Create(dto);
            return Created($"/api/restaurant/{id}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _restaurantService.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "CreatedAtLeast2Restaurants")]
        public ActionResult Update([FromBody] UpdateRestaurantDto dto, [FromRoute] int id)
        {
            _restaurantService.Update(id, dto);
            return Ok();
        }

        [HttpPost("done")]
        //[ActionName("Thumbnail")] // cos to nie dziala
        [AllowAnonymous]
        [Consumes("text/json")] //requested in heading in "Content-Type" as "text/json". Aczkolwiek jeśli nie ma header to tez przepuszcza, trzeba wymusic, zeby był header "Content-Type"
        [ProducesResponseType(StatusCodes.Status200OK)] //informuje o statusach jakie oddaje. Aczkolwiek jako, że sie robi statusy przez exceptions, to średnie to. Można tylko określić co zwróci sama akcja.
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult ExampleMethod()
        {
            //dzieki temu wymusza aby był nagłówek o nazwie "Content-Type"
            if (!HttpContext.Request.Headers.ContainsKey("Content-Type")) return BadRequest();
            return Ok();
        }

        //wywoła tę metodę: https://localhost:5001/api/restaurant/versioning
        [HttpGet("versioning")]
        [AllowAnonymous]
        public ActionResult ExampleMethodForVersioningV1() //baowo ma wersję 1.0 wiecj jak nic sie nie dzieje to ta funkja sie odpali
        {
            return Ok("Wersja 1.0 bazowa");
        }

        //tę metodę wywoła (bez zmian w StartUp): https://localhost:5001/api/restaurant/versioning?api-version=2.0
        //tę metodę wywoła (ze zmianą ApiVersionReader): https://localhost:5001/api/restaurant/versioning (header "Accept" z wartością "version=2.0"
        //tę metodę wywoła (ze zmianą ApiVersionReader): https://localhost:5001/api/restaurant/versioning (header "CustomHeaderVersion" z wartościa po prostu "2.0"
        [HttpGet("versioning")]
        [AllowAnonymous]
        [MapToApiVersion("2.0")] //łączy z wersją 2.0. Trzeba w postmanie. Zatem w parametr api-version musi być 2.0
        public ActionResult ExampleMethodForVersioningV2()
        {
            return Ok("Wersja 2.0 inna");
        }

        [HttpGet("[action]")] // https://localhost:5001/api/Restaurant/ExampleMethod2
        [AllowAnonymous]
        public ActionResult ExampleMethod2()
        {
            return Ok();
        }

        [HttpGet]
        [Route("komar")] // https://localhost:5001/api/Restaurant/komar
        [Route("tomek")] //nadpisze sciezki i zezwoli na https://localhost:5001/api/Restaurant/tomek
        [AllowAnonymous]
        public ActionResult ExampleMethod3()
        {
            return Ok();
        }

        [HttpGet]
        [Route("Default/GetRecordsById/{id:int:min(1)}")]//the minimum value of the id parameter should be 1, else a 404 error will be returned
        [AllowAnonymous]
        public ActionResult ExampleMethod4()
        {
            //zatem dla "https://localhost:5001/api/Restaurant/Default/GetRecordsById/-3" jest notFount
            //a dla "https://localhost:5001/api/Restaurant/Default/GetRecordsById/3" jest "ok
            return Ok();
        }

        [HttpGet("[action]", Name = "Example")] //mozna dawac imie ale nie wiem po co
        [Route("Home", Order = 2)] //order chyba robi hierarchię ważnośći
        [Route("api/[controller]/[action]", Name = "[controller]_[action]")] //nie wiem co robi "Name" prop
        [AllowAnonymous]
        public ActionResult ExampleMethod5()
        {
            return Ok();
        }

        //[Produces("application/json")]
        //[Consumes("application/json")]
        //to samo inaczej
        //[Produces(MediaTypeNames.Application.Json)]
        //[Consumes(MediaTypeNames.Application.Json)]
        //ProducesAttribute is used to specify the content type of the output that will be sent by the API, and the corresponding ConsumesAttribute is used to specify the request content types the API expects to receive

        //[ServiceFilter(typeof(ValidationFilterAttribute))]

        //[ResponseCache(CacheProfileName = "120SecondsDuration")]

        //[ApiExplorerSettings(GroupName = "v2")]

        /* Asynchroniczny action
                [HttpGet("async")]
        public async Task<IActionResult> GetAllAsync()
        {
            var queryMediator = new GetAllRestaurantsQuery();
            var result = await _mediator.Send(queryMediator);
			return Ok(result);
        }
         */
    }
}