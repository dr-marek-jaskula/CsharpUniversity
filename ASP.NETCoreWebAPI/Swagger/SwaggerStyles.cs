namespace ASP.NETCoreWebAPI.Swagger;

public class SwaggerStyles
{
    //Swagger DarkMode (or other custom style):
    //1. Be sure to have static files (app.UseStaticFiles();)
    //2. In wwwroot create folder swaggerstyle and in it file "SwaggerDark.css" with css code
    //3 In Configuration write:
    //app.UseSwaggerUI(c => 
    //{ 
    //    //Set swagger endpoint
    //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CsharpUniversity API v1");
    //    //Overwirte swagger style to dark style (from static files wwwroot -> swaggerstyles -> SwaggerDark.css)
    //    c.InjectStylesheet("/swaggerstyles/SwaggerDark.css");
    //});
}
