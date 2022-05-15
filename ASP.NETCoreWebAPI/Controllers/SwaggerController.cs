using ASP.NETCoreWebAPI.Models.DataTransferObjects;
using ASP.NETCoreWebAPI.Services;
using Bogus;
using EFCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Humanizer.In;

//Add //Swashbuckle.AspNetCore.FiltersSwashbuckle.AspNetCore.Filters in program.cs

namespace ASP.NETCoreWebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
//This attribute will change in the swagger that produces responses are in "application/json" format
[Produces("application/json")]
[AllowAnonymous]
public class SwaggerController : ControllerBase
{
    private readonly MyDbContext _myDbContext;
    private readonly IAddressService _addressService;

    #region Markdowns basics

    //Markdowns are supported (https://www.markdownguide.org/basic-syntax/):
    //1) bold
    //surrounding a string in "**" makes bold (not in summary) -> example: "**input**"

    //2) italic
    //surrounding a string in "*" makes italic (not in summary) -> example: "*input*"

    //3) bold + italic
    //surrounding a string in "***" makes bold + italic (not in summary) -> example: "***input***"

    //4) headers
    //use hashtag before make header (the size of the header depends on the number of # before the text. One is big, more make it smaller:
    //example 1: "# My big header", example 2: "## Still quite big header", example 3: "## large header", and up to 6 hashtags

    //5) blockquotes (like tabs and quotes)
    //use ">" before the sentence (can do multiple). Example "> this is my quote"

    //6) Unordered list with "-" in front of every line like:
    //- First item
    //- Second item
    //- Third item
    //- Fourth item

    //Can have also:
    //- First item
    //- Second item
    //- Third item
    //    - Indented item
    //    - Indented item
    //- Fourth item

    //7) we can add also images:
    //![Tux, the Linux mascot](/assets/images/tux.png)
    //7.5) images with titles (what will be seen when we put cursor on link):
    //![Tux, the Linux mascot](/assets/images/tux.png "Super duck")

    //8) or links:
    //[Duck Duck Go](https://duckduckgo.com).
    //9) link with title (what will be seen when we put cursor on link):
    //[Duck Duck Go](https://duckduckgo.com "The best search engine for privacy")

    //Urls and email addresses (they should be blue but they tend to not work):
    //<https://www.markdownguide.org>
    //<fake@example.com>

    //Some example:
    /*
> #### The quarterly results look great!
>
> - Revenue was off the chart.
> - Profits were higher than ever.
>
>  *Everything* is going according to **plan**.
    */

    //Escape character is "/"

    #endregion Markdowns basics

    public SwaggerController(MyDbContext myDbContext, IAddressService addressService)
    {
        _myDbContext = myDbContext;
        _addressService = addressService;
    }

    /// <summary>
    /// This is action description
    /// </summary>
    /// <param name="message">## This is **input** message</param>
    /// <response code="200">*Returns* ok (my **custom** description)</response>
    /// <response code="400">>Returns bad request (my other custom description)</response>
    /// <response code="404">>>Returns not found (again my other custom description)</response>
    [HttpGet("BasicDescribedRespose")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public ActionResult<string> GetDescribedResponse([FromRoute] string message)
    {
        var random = new Random().Next(1, 4);
        if (random == 1)
            return Ok($"It is ok. {message}");
        else if (random == 2)
            return NotFound($"Can not find the resource. {message}");

        return BadRequest($"This is a very bad request. {message}");
    }

    /// <summary>
    /// Return an address
    /// </summary>
    /// <param name="id">### Id of an address</param>
    /// <response code="200">Address found</response>
    /// <response code="404">Address **not** found</response>
    [HttpGet("{id}")]
    //For swagger response to work this "ProdusesResponseType" is required
    [ProducesResponseType(typeof(AddressDto), 200)]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    public ActionResult<AddressDto> GetAddress([FromRoute] int id)
    {
        var address = _addressService.GetById(id);
        return Ok(address);
    }

    /// <summary>
    /// Adds address to the database
    /// </summary>
    /// <param name="dto">Address in the json format</param>
    /// <response code="201">Address added to the database</response>
    /// <response code="400">**Invalid** body format</response>
    [HttpPost()]
    [ProducesResponseType(201)]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    public async Task<ActionResult> Create([FromBody] CreateAddressDto dto)
    {
        await _addressService.Create(dto);
        return Created($"/api/address/{2}", null);
    }
}