namespace ASP.NETCoreWebAPI.Swagger;

public class SwaggerPostman
{
    //In order to import all api from swagger to Postman we need to:

    //1. Run api
    //2. Go to swagger site
    //3. Click F12
    //4. Go to: ang.Networks
    //5. Search for "swagger.json" and open it 
    //6. Save this file with .json extension
    //7. Click "Import" near the workspace
    //8. Upload files -> choose saved file
    //9. We set the base Url for our application and then click "Variables"
    //10. To "Initial Value" and "Current Value" we paste our url (for example "https://localhost:5001") and save

    //Swagger DarkMode (or other custom style):
    //1. Be sure to have static files (app.UseStaticFiles();)
    //2. Out in wwwroot folder swaggerstyle and in it file "SwaggerDark.css" in css code
    //3 In Configuration write:
    //app.UseSwaggerUI(c => 
    //{ 
    //    //Set swagger endpoint
    //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CsharpUniversity API v1");
    //    //Overwirte swagger style to dark style (from static files wwwroot -> swaggerstyles -> SwaggerDark.css)
    //    c.InjectStylesheet("/swaggerstyles/SwaggerDark.css");
    //});
}
