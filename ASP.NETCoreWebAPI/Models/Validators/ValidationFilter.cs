using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ASP.NETCoreWebAPI.Models.Validators;

public class ValidatorActionFilter : IActionFilter
{
    /// <summary>
    /// Executes when request is send, after validation.
    /// </summary>
    /// <param name="filterContext"></param>
    public void OnActionExecuting(ActionExecutingContext filterContext)
    {
        //Respond as Status: 400 BadRequest when invalid data is send
        if (!filterContext.ModelState.IsValid)
            filterContext.Result = new BadRequestObjectResult(filterContext.ModelState);
    }

    public void OnActionExecuted(ActionExecutedContext filterContext)
    {

    }
}