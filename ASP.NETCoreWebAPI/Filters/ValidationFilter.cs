using Microsoft.AspNetCore.Mvc.Filters;

namespace ASP.NETCoreWebAPI.Filters;

public class ValidatorActionFilter : IActionFilter
{
    //If the response is not immediately validated with BadRequest response (containing all errors). Then uncomment lines 16 and 17

    /// <summary>
    /// Executes when request is send, after validation.
    /// </summary>
    /// <param name="filterContext"></param>
    public void OnActionExecuting(ActionExecutingContext filterContext)
    {
        //Respond as Status: 400 BadRequest when invalid data is send
        //if (!filterContext.ModelState.IsValid)
        //    filterContext.Result = new BadRequestObjectResult(filterContext.ModelState);
    }

    public void OnActionExecuted(ActionExecutedContext filterContext)
    {
    }
}