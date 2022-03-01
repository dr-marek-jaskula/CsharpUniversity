using AutoMapper;
using MediatR;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ASPDotNetLearningApplication
{
    //this controller was made for learning MediatorR concept
    //1. Add the mediator package

    [Route("api/[controller]")]
    [ApiController]
    public class MediatorController : ControllerBase
	{
        private readonly IRestaurantService _restaurantService;
        private readonly IMediator _mediator;

        public MediatorController(IRestaurantService restaurantService, IMediator mediator)
        {
            _restaurantService = restaurantService;
            _mediator = mediator;
        }

        [HttpGet("async")]
        public async Task<IActionResult> GetAllAsync()
        {
            var queryMediator = new GetAllRestaurantsQuery();
            var result = await _mediator.Send(queryMediator);
			return Ok(result);
        }

        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll([FromQuery] RestaurantQuery query)
        {
            var restaurantsDtos = _restaurantService.GetAll(query);
            return Ok(restaurantsDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<RestaurantDto> Get([FromRoute] int id)
        {
            var restaurant = _restaurantService.GetById(id);
            return Ok(restaurant);
        }

        [HttpPost]
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
        public ActionResult Update([FromBody] UpdateRestaurantDto dto, [FromRoute] int id)
        {
            _restaurantService.Update(id, dto);
            return Ok();
        }
    }
}
