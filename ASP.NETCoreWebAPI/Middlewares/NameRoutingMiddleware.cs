namespace ASP.NETCoreWebAPI.Middlewares;

//This is a tutorial middleware to show that we do not need to inherit anything.
//Moreover we demonstrate that the HTTP context is read-write context

public class NameRoutingMiddleware
{
    private readonly RequestDelegate _next;

    public NameRoutingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    //We could also replace the memory stream (and write to response body. It is read-write). Something like that: (be careful)
    //using (var ms = new MemoryStream())
    //{
    //  var rs = ctx.Response.Body
    //  ctx.Response.Body = ms;
    //  ...
    //  rs.WriteAsync(bytes, 0, bytes.Length); 
    //}

    public async Task InvokeAsync(HttpContext context)
    {
        //HTTP context is read-write
        var path = context.Request.Path.Value;

        if (path is "/api/University/actionWithNameRouting/2")
            context.Request.Path = "/api/University/actionWithNameRouting/1";

        await _next(context);

        context.Request.Path = path;
    }
}
